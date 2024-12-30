using AutoMapper;
using Business.Dtos;
using Business.Entities;

namespace Api.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<TransacaoDto, Transacao>().ReverseMap();
            CreateMap<CategoriaDto, Categoria>();
        }
    }
}
