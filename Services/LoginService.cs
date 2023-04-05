using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Request;

namespace UsuariosApi.Services
{
	public class LoginService
	{
		private SignInManager<IdentityUser<int>> _signInManager;

		public LoginService(SignInManager<IdentityUser<int>> signInManager)
		{
			_signInManager = signInManager;
		}

		public Result LogaUsuario(LoginRequest request)
		{
			//IsPersistent = persistencia na tentativa de Login
			//lockoutOnFailure = caso queira travar o login caso tenha o login seja feita de maneira errada
			var resultadoIdentity = _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);

			if (resultadoIdentity.Result.Succeeded) return Result.Ok();

			return Result.Fail("Login falhou");
		}
	}
}
