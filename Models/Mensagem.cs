using MimeKit;

namespace UsuariosApi.Models
{
	public class Mensagem
	{
		//MailboxAddress é de um pacote chamado mimekit, precisa baixar outro pacote tbm, ver nas dependencias do projeto
		public List<MailboxAddress> Destinatario { get; set; }
		public string Assunto { get; set; }
		public string Conteudo { get; set; }

		public Mensagem(IEnumerable<string> destinatario, string assunto, int usuarioId, string codigo)
		{
			Destinatario = new List<MailboxAddress>();
			Destinatario.AddRange(destinatario.Select(x => new MailboxAddress(default,x)));
			Assunto = assunto;
			Conteudo = $"http://localhost:7082/AtivaContaUsuario?UsuarioId={usuarioId}&CodigoDeAtivacao={codigo}";
		}
	}
}
