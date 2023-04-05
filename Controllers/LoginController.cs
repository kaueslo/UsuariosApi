﻿using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Request;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private LoginService _loginService;

		public LoginController(LoginService loginService)
		{
			_loginService = loginService;
		}

		[HttpPost]
		public IActionResult LogaUsuario(LoginRequest request)
		{
			var resultado = _loginService.LogaUsuario(request);

			if (resultado.IsFailed) return Unauthorized();

			return Ok();
		}
	}
}
