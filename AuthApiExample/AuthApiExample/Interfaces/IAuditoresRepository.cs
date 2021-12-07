using System.Collections.Generic;
using System.Threading.Tasks;
using AuthApiExample.Models;
using AuthApiExample.DTOS;

namespace AuthApiExample.Interfaces
{
    public interface IAuditoresRepository
    {
        Task<IEnumerable<Auditorias6s>> GetAuditorias();
        Task<Auditorias6s> GetAuditoria(int id);
        Task PostAuditoria(Auditorias6s auditoria);
        Task<bool> UpdateAuditorias(Auditorias6s auditoria);
        Task<bool> DeleteAuditoria(int id);


        Task<IEnumerable<Auditores6s>> GetAuditores();
        Task<Auditores6s> GetAuditor(int id);
        

        Task<IEnumerable<Areas6s>> GetAreas();
        Task<Areas6s> GetArea(int id);
    }
}
