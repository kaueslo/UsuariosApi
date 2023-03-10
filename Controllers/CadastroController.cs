using Microsoft.AspNetCore.Mvc;

namespace UsuariosApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class CadastroController : ControllerBase
	{
		[HttpPost("CadastraUsuario")]
		public IActionResult CadastraUsuario(CreateUsuarioDto createDto)
		{
			//TODO chamar o service
			return Ok();
		}
	}
}
