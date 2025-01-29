using Api.Dtos;
using AutoMapper;
using Business.Entities;

namespace Api.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<TransacaoDto, Transacao>().ReverseMap();
            CreateMap<CategoriaDto, Categoria>().ReverseMap();
            CreateMap<LimiteOrcamentoDto, LimiteOrcamento>().ReverseMap();
        }
    }
}
