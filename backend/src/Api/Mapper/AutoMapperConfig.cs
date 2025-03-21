using Api.Dtos;
using AutoMapper;
using Business.Entities;
using System.Globalization;

namespace Api.Mapper
{
    public class AutoMapperConfig : Profile
    {
        const string FORMATO_DATA = "dd/MM/yyyy";
        const string FORMATO_VALOR = "R$ {0:###,###,##0.00}";
        const string CULTURA_BRASIL = "pt-BR";

        public AutoMapperConfig()
        {
            CreateMap<TransacaoDto, Transacao>().ReverseMap();
            CreateMap<CategoriaDto, Categoria>().ReverseMap();
            CreateMap<LimiteOrcamentoDto, LimiteOrcamento>();
            CreateMap<LimiteOrcamento, LimiteOrcamentoDto>().ForMember(dest => dest.CategoriaNome, src => src.MapFrom(x => x.Categoria.Nome));
            CreateMap<LimiteOrcamento, LimiteOrcamentoUtilizadoDto>().ForMember(dest => dest.CategoriaNome, src => src.MapFrom(x => x.Categoria.Nome));
            CreateMap<Transacao, RelatorioTransacaoDto>()
                .ForMember(dest => dest.Data, src => src.MapFrom(x => x.Data.ToString(FORMATO_DATA)))
                .ForMember(dest => dest.Tipo, src => src.MapFrom(x => x.Tipo.ToString()))
                .ForMember(dest => dest.Categoria, src => src.MapFrom(x => x.Categoria.Nome))
                .ForMember(dest => dest.Valor, src => src.MapFrom(x => string.Format(CultureInfo.GetCultureInfo(CULTURA_BRASIL), FORMATO_VALOR, x.Valor)));
        }

    }
}
