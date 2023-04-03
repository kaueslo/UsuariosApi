using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dto;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class CadastroController : ControllerBase
	{
		private CadastroService _cadastroService;

		public CadastroController(CadastroService cadastroService)
		{
			_cadastroService = cadastroService;
		}

		[HttpPost("CadastraUsuario")]
		public IActionResult CadastraUsuario(CreateUsuarioDto createDto)
		{
			var resultado = _cadastroService.CadastraUsuario(createDto);

			if (resultado.IsFailed) return StatusCode(500);
			return Ok();
		}
	}
}
