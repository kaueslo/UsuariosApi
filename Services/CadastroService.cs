﻿using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Web;
using UsuariosApi.Data.Dto;
using UsuariosApi.Data.Request;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
	public class CadastroService
	{

		private IMapper _mapper;
		private UserManager<IdentityUser<int>> _userManager;
		private EmailService _emailService;

		public CadastroService(IMapper mapper, UserManager<IdentityUser<int>> userManager, EmailService emailService)
		{
			_mapper = mapper;
			_userManager = userManager;
			_emailService = emailService;
		}

		public Result CadastraUsuario(CreateUsuarioDto createDto)
		{
			var usuario = _mapper.Map<Usuario>(createDto);

			var usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);
			var resultadoIdentity = _userManager.CreateAsync(usuarioIdentity, createDto.Password);

			if (resultadoIdentity.Result.Succeeded) 
			{
				//Variavel de codigo de ativacao
				var code = _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity);

				var encodedCode = HttpUtility.UrlEncode(code.Result);

				_emailService.EnviarEmail(new[] {usuarioIdentity.Email }, "Link de ativação", usuarioIdentity.Id, encodedCode);

				return Result.Ok().WithSuccess(code.Result); 
			}

			return Result.Fail("Falha ao cadastrar usuário");
		}

		public Result AtivaContaUusario(AtivaContaRequest request)
		{
			var identityUser = _userManager.Users.FirstOrDefault(x => x.Id == request.UsuarioId);

			//Ativando a conta do usuario
			var identityResult = _userManager.ConfirmEmailAsync(identityUser, request.CodigoDeAtivacao).Result;

			if (identityResult.Succeeded) return Result.Ok();

			return Result.Fail("Falha ao ativar conta de usuário");
		}
	}
}
