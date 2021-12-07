using AuthApiExample.Models;
using AuthApiExample.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthApiExample.Services
{
    public interface IAuthService
    {
        User Authenticate(string username, string password);
        string GenerateSessionToken(string noEmpleado);
        string GenerateAccessToken(string noEmpleado);
        string GetUsernameFromRefreshToken(string sessionid);
        User GetAuthenticatedUser(string noemp);
        RefreshToken GetRefreshTokenBySessionId(string sessionid);
        bool DeleteSessionTokenFromDatabase(string refreshTokenSessionId);
    }

    public class AuthService: IAuthService
    {
        private JwtConfiguration _settings;
        IHttpContextAccessor _accessor;


        public AuthService(IOptions<JwtConfiguration> settings, IHttpContextAccessor accessor)
        {
            _settings = settings.Value;
            _accessor = accessor;
            
        }

        public User Authenticate(string username, string password)
        {
            bool auth = true;
            //procedimiento de authenticacion que regrese el objeto User

            User usr = new User();
            if (usr.Leer(username, password) != 0)
                auth = false;

            if (auth)
                return usr;
            
            return null;

        }

        public string GenerateSessionToken(string user)
        {

            string refreshToken = Guid.NewGuid().ToString();

            var sql = SqlManager.Instance;

            string ip = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            string query = "INSERT INTO UsuariosJwtRefreshTokens (NoEmp, RefreshTokenSessionId, Revoked, Ip , Source) " +
                            "VALUES(@noemp, @refresh, 0, @ip, 'hutchinson microservices')";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@noemp", user);
            parameters.Add("@refresh", refreshToken);
            parameters.Add("@ip", ip);

            int affected = sql.Insert(query, parameters);

            if (affected > 0)
                return refreshToken;

            return null;

        }

        public string GenerateAccessToken(string user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user),
                new Claim(ClaimTypes.NameIdentifier, user)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));

            var jwtToken = new JwtSecurityToken(
                issuer: _settings.Issuer,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_settings.accessTokenDurationInMinutes),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)

            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);

        }

        public User GetAuthenticatedUser(string noemp)
        {
            //rutina para obtener la informacion del usuario de session
            return new User();
        }

        public string GetUsernameFromRefreshToken(string sessionid)
        {
            var sql = SqlManager.Instance;

            string query = "SELECT NoEmp FROM UsuariosJwtRefreshTokens WHERE RefreshTokenSessionId = @sessionid";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@sessionid", sessionid);

            DataSet ds = sql.Select(query, parameters);
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count == 0)
                return null;

            return dt.Rows[0]["NoEmp"].ToString();

        }

        public RefreshToken GetRefreshTokenBySessionId(string sessionid)
        {
            var sql = SqlManager.Instance;

            string query = "SELECT ID,NoEmp,RefreshToken,Revoked,Ip,CreatedAt,UpdatedAt,Source,ExpirityAt FROM UsuariosJwtRefreshTokens WHERE RefreshToken = @sessionid";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@sessionid", sessionid);

            DataSet ds = sql.Select(query, parameters);
            DataTable dt = ds.Tables[0];
                 
            RefreshToken token = dt.AsEnumerable().Select(c => new RefreshToken()
            {
                ID = (int)c["ID"],
                NoEmp = c["NoEmp"].ToString(),
                RefreshTokenSessionId = c["RefreshToken"].ToString(),
                Revoked = (int)c["Revoked"],
                Source = c["Source"].ToString(),
                CreatedAt = (DateTime)c["CreatedAt"],
                ExpirityAt = (DateTime)c["ExpirityAt"]
            })
            .FirstOrDefault();

            return token;
        }

        public bool DeleteSessionTokenFromDatabase(string refreshTokenSessionId)
        {
            var sql = SqlManager.Instance;

            string query = "DELETE FROM UsuariosJwtRefreshTokens " +
                            "WHERE RefreshToken = @refresh AND Source = 'hutchinson hes'";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@refresh", refreshTokenSessionId);

            int affected = sql.Delete(query, parameters);

            if (affected > 0)
                return true;

            return false;
        }

    }
}
