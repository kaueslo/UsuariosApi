using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Request;

namespace UsuariosApi.Services
{
	public class LoginService
	{
		private SignInManager<IdentityUser<int>> _signInManager;
		private TokenService _tokenService;

		public LoginService(SignInManager<IdentityUser<int>> signInManager, 
			TokenService tokenService)
		{
			_signInManager = signInManager;
			_tokenService = tokenService;
		}

		public Result LogaUsuario(LoginRequest request)
		{
			//IsPersistent = persistencia na tentativa de Login
			//lockoutOnFailure = caso queira travar o login caso tenha o login seja feita de maneira errada
			var resultadoIdentity = _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);

			if (resultadoIdentity.Result.Succeeded) 
			{
				var identityUser = _signInManager.UserManager.Users.FirstOrDefault(x => x.NormalizedUserName == request.Username.ToUpper());

				var token = _tokenService.CreateToken(identityUser);
				return Result.Ok().WithSuccess(token.Value);
			}

			return Result.Fail("Login falhou");
		}
	}
}
