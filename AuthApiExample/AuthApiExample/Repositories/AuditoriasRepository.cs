using Microsoft.EntityFrameworkCore;
using AuthApiExample.DTOs;
using AuthApiExample.Models;
using AuthApiExample.Interfaces;
using AuthApiExample.Sistema6sData;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace AuthApiExample.Repositories
{
    public class AuditoriasRepository : IAuditoriasRepository
    {
        // INYECCIÓN DE DEPENDENCIA DEL CONTEXTO DE BD
        private readonly Sistema6SContext _dbcontext;
        public AuditoriasRepository(Sistema6SContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        public async Task<IEnumerable<Auditorias6s>> GetAuditorias()
        {
            // CONEXIÓN CON BD Y RETORNAR LISTA ASÍNCRONA
            var auditorias = await _dbcontext.Auditorias6s.ToListAsync();
            return auditorias;
        }

        public async Task<Auditorias6s> GetAuditoria(int id)
        {
            var auditoria = await _dbcontext.Auditorias6s.FirstOrDefaultAsync(x => x.AuditoriaId == id);
            return auditoria;
        }

        public async Task PostAuditoria(Auditorias6s auditoria)
        {
            _dbcontext.Auditorias6s.Add(auditoria);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<bool> UpdateAuditorias(Auditorias6s auditoria)
        {
            var currentAuditoria = await GetAuditoria(auditoria.AuditoriaId);

            currentAuditoria.FechaTarget = auditoria.FechaTarget;
            currentAuditoria.FechaCompleto = auditoria.FechaCompleto;
            currentAuditoria.Estado = auditoria.Estado;
            currentAuditoria.CalificacionId = auditoria.CalificacionId;

            int rows = await _dbcontext.SaveChangesAsync();
            return rows > 0;
        }


        public async Task<bool> DeleteAuditoria(int id)
        {
            var currentAuditoria = await GetAuditoria(id);
            _dbcontext.Auditorias6s.Remove(currentAuditoria);

            int rows = await _dbcontext.SaveChangesAsync();
            return rows > 0;
        }


        public async Task<IEnumerable<Auditores6s>> GetAuditores()
        {
            var auditores = await _dbcontext.Auditores6s.ToListAsync();
            return auditores;
        }

        public async Task<Auditores6s> GetAuditor(int id)
        {
            var auditor = await _dbcontext.Auditores6s.FirstOrDefaultAsync(x => x.UserId == id);
            return auditor;
        }

        public async Task<IEnumerable<Areas6s>> GetAreas()
        {
            var areas = await _dbcontext.Areas6s.ToListAsync();
            return areas;
        }

        public async Task<Areas6s> GetArea(int id)
        {
            var area = await _dbcontext.Areas6s.FirstOrDefaultAsync(x => x.AreaId == id);
            return area;
        }
    }
}
