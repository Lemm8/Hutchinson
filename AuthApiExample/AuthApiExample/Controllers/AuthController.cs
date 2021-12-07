using AuthApiExample.Models;
using AuthApiExample.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApiExample.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] dynamic data)
        {
            string username = data.username;
            string password = data.password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return BadRequest(new { message = "Usuario y contraseña requeridos!" });

            try
            {
                User user = _authService.Authenticate(username, password);

                if (user == null)
                    return BadRequest(new { message = "Invalid credentials" });

                string session = _authService.GenerateSessionToken(user.NoEmpleado);

                if (session == null)
                    return BadRequest(new { Message = "Error interno: No se pudo iniciar la sessión" });

                string token = _authService.GenerateAccessToken(user.NoEmpleado);

                HttpContext.Response.Cookies.Append("session_id", session, new CookieOptions() { HttpOnly = true, Expires = DateTime.Now.AddDays(30) });

                return Ok(new { User = user, _token = token });

            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }

        }


        [AllowAnonymous]
        [HttpGet("me")]
        public IActionResult GetAuthenticatedUser()
        {

            string sessionid = HttpContext.Request.Cookies["session_id"];

            if (sessionid == null || sessionid == string.Empty)
                return BadRequest(new { Message = "Session not found" });

            try
            {
                string noemp = _authService.GetUsernameFromRefreshToken(sessionid);

                if (noemp == null)
                    return BadRequest(new { Message = "Session not found" });

                var jwtToken = _authService.GenerateAccessToken(noemp);

                User user = _authService.GetAuthenticatedUser(noemp);

                return Ok(new { User = user, _token = jwtToken });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.ToString() });
            }

        }


        [AllowAnonymous]
        [HttpPost("session/refresh")]
        public IActionResult TokenRefresh()
        {

            string sessionid = HttpContext.Request.Cookies["session_id"];

            if (sessionid == null || sessionid == string.Empty)
                return BadRequest(new { Message = "Session not found" });

            try
            {
                RefreshToken refreshToken = _authService.GetRefreshTokenBySessionId(sessionid);

                if (refreshToken == null)
                    return BadRequest(new { Message = "Session not found" });

                DateTime rightNow = DateTime.Now;

                if (rightNow.Date == refreshToken.ExpirityAt.Date)
                {
                    _authService.DeleteSessionTokenFromDatabase(refreshToken.RefreshTokenSessionId);

                    sessionid = _authService.GenerateSessionToken(refreshToken.NoEmp);

                    HttpContext.Response.Cookies.Delete("session_id");

                    HttpContext.Response.Cookies.Append("session_id", sessionid, new CookieOptions() { HttpOnly = true, Expires = DateTime.Now.AddDays(30) });
                }

                User user = _authService.GetAuthenticatedUser(refreshToken.NoEmp);

                string token = _authService.GenerateAccessToken(refreshToken.NoEmp);

                return Ok(new { User = user, _token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Internal error: {ex.ToString()}" });
            }
        }

        [AllowAnonymous]
        [HttpPost("session/end")]
        public IActionResult RemoveToken()
        {
            string sessionid = HttpContext.Request.Cookies["session_id"];

            if (sessionid == null || sessionid == string.Empty)
                return Ok(true);

            HttpContext.Response.Cookies.Delete("session_id");

            if (!_authService.DeleteSessionTokenFromDatabase(sessionid))
                return BadRequest(new { Message = "Error al terminar sessión" });

            return Ok(true);

        }


    }
}
