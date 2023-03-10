using UsuariosApi.Data.Dto;

namespace UsuariosApi.Profiles
{
	public class UsuarioProfile : Profile
	{
		public UsuarioProfile()
		{
			CreateMap<CreateUsuarioDto, Usuario>();
		}
	}
}
