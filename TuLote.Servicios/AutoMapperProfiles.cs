using AutoMapper;
using TuLote.Entidades;
using TuLote.Entidades.DTOs;

namespace TuLote.Servicios
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Provincia, ProvinciaDTO>().ReverseMap();//mapea desde Provincia hacia ProvinciaDTO y viceversa
            CreateMap<ProvinciaCreacionDTO, Provincia>();//mapea desde ProvinciaCreacionDTO hacia Provincia
            CreateMap<Municipio, MunicipioDTO>().ReverseMap();
            CreateMap<MunicipioCreacionDTO, Municipio>();
            CreateMap<Localidad, LocalidadDTO>().ReverseMap();
            CreateMap<LocalidadCreacionDTO, Localidad>();
            CreateMap<Barrio, BarrioDTO>().ReverseMap();
            CreateMap<BarrioCreacionDTO, Barrio>();
            CreateMap<Lote, LoteDTO>().ReverseMap();
            CreateMap<LoteCreacionDTO, Lote>();
        }
    }
}
