using Microsoft.AspNetCore.Mvc;
using AuthApiExample.DTOs;
using AuthApiExample.Interfaces;
using AuthApiExample.Models;
using AuthApiExample.Sistema6sData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace AuthApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditoriasController : ControllerBase
    {
        private readonly IAuditoriasRepository _repository;
        private readonly IMapper _mapper;
        private readonly Sistema6SContext _dbcontext;

        public AuditoriasController(IAuditoriasRepository repository, IMapper mapper, Sistema6SContext dbcontext)
        {
            _repository = repository;
            _mapper = mapper;
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public dynamic GetAuditorias()
        {

            var query = from audis in _dbcontext.Auditorias6s
                        join audes in _dbcontext.Auditores6s
                            on audis.AuditorId equals audes.UserId
                        join ar in _dbcontext.Areas6s
                            on audis.AreaId equals ar.AreaId
                        select new
                        {
                            auditoriaId = audis.AuditoriaId,
                            auditoriaNombre = audis.Nombre,
                            auditoriaFechaInicio = audis.FechaInicio,
                            auditoriaFechaTarget = audis.FechaTarget,
                            auditoriaFechaCompleto = audis.FechaCompleto,
                            auditoriaEstado = audis.Estado,
                            auditorNombre = audes.Nombre,
                            area = ar.Nombre,
                            auditoriaMes = audis.mes
                        };

            return Ok(query);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuditoria(int id)
        {
            var auditoria = await _repository.GetAuditoria(id);
            var auditoriaDto = _mapper.Map<Auditorias6sDto>(auditoria);
            return Ok(auditoriaDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostAuditoria(Auditorias6sDto auditoriaDto)
        {
            var auditoria = _mapper.Map<Auditorias6s>(auditoriaDto);

            await _repository.PostAuditoria(auditoria);

            return Ok(auditoria);
        }

        [HttpPut]
        public async Task<IActionResult> PutAuditoria(Auditorias6s auditoria)
        {
            var result = await _repository.UpdateAuditorias(auditoria);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuditoria(int id)
        {
            var result = await _repository.DeleteAuditoria(id);

            return Ok(result);
        }

    }
}
