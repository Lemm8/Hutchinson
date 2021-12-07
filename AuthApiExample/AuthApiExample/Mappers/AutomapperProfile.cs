using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AuthApiExample.Models;
using AuthApiExample.DTOs;

namespace AuthApiExample.Mappers
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            // ENTIDAD ORIGEN -> ENTIDAD DESTINO
            CreateMap<Auditorias6s, Auditorias6sDto>();
            CreateMap<Auditorias6sDto, Auditorias6s>();
        }
    }
}
