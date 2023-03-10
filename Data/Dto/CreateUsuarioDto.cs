using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Dto
{
	public class CreateUsuarioDto
	{
		[Required(ErrorMessage ="É necessário informar o nome do usuário")]
		public string UserName { get; set; }
		[Required(ErrorMessage ="É necessário informar o E-mail")]
		public string Email { get; set; }
		[Required(ErrorMessage ="É necessário informar a senha")]
		public string Password { get; set; }
	}
}
