using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AuthApiExample.DTOs;
using AuthApiExample.Entities;
using AuthApiExample.Interfaces;
using AuthApiExample.Sistema6SData;

namespace AuthApiExample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditoresController : ControllerBase
    {
         private readonly IAuditoriasRepository _repository;
        private readonly IMapper _mapper;
        private readonly AuthApiExampleContext _dbcontext;

        public AuditoresController(IAuditoriasRepository repository, IMapper mapper, AuthApiExampleContext dbcontext)
        {
            _repository = repository;
            _mapper = mapper;
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public dynamic GetAuditores()
        {

            var query = from audes in _dbcontext.Auditores6s
                        join audis in _dbcontext.Auditorias6s
                            on audes.UserId equals audis.AuditorId
                        select new
                        {
                            userId = audes.UserId,
                            auditorNombre = audes.Nombre,
                            auditoriaFechaCompleto = audis.FechaCompleto,
                            auditoriaEstado = audis.Estado
                        };

            return Ok(query);
        }
    }
}
