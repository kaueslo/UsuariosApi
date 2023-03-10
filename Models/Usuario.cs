using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Models
{
	public class Usuario
	{
		[Key]
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
	}
}
