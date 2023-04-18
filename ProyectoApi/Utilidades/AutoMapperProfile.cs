using AutoMapper;
using ProyectoApi.DTOs;
using ProyectoApi.Models;
using System.Runtime.InteropServices;

namespace ProyectoApi.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<ConductorPostDto, Conductor>();
            CreateMap<DatosUsuarioPostDto,DatosUsuario>();
            CreateMap<DepartamentoPostDto,Departamento>();
            CreateMap<DestinoPostDto,Destino>();
            CreateMap<MunicipioPostDto,Municipio>();
            CreateMap<RetornoPostDto,Retorno>();
            CreateMap<SalidaPostDto,Salida>();
            CreateMap<TipoUsuarioPostDto,TipoUsuario>();
            CreateMap<TipoViajePostDto,TipoViaje>();
            CreateMap<UnidadPostDto,Unidad>();
            CreateMap<UsuarioPostDto, Usuario>();
        }
    }
}
