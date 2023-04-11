using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dto;
using UsuariosApi.Data.Request;
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
			return Ok(resultado.Successes.FirstOrDefault());
		}

		[HttpGet("AtivaContaUsuario")]
		public IActionResult AtivaContaUsuario([FromQuery] AtivaContaRequest request)
		{
			var resultado = _cadastroService.AtivaContaUusario(request);
			if(resultado.IsFailed) return StatusCode(500);
			return Ok(resultado.Successes.FirstOrDefault());
		}
	}
}
