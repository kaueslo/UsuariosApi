using MailKit.Net.Smtp;
using MimeKit;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
	public class EmailService
	{
		public void EnviarEmail(string[] destinatario, string assunto, int usuarioid, string code)
		{
			var mensagem = new Mensagem(destinatario, assunto, usuarioid, code);

			var mensagemDeEmail = CriaCorpoDoEmail(mensagem);

			Enviar(mensagemDeEmail);
		}

		private MimeMessage CriaCorpoDoEmail(Mensagem mensagem)
		{
			var mensagemDeEmail = new MimeMessage();
			mensagemDeEmail.From.Add(new MailboxAddress(default, "Adicionar o remetente"));
			mensagemDeEmail.To.AddRange(mensagem.Destinatario);
			mensagemDeEmail.Subject = mensagem.Assunto;
			mensagemDeEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text)
			{
				Text = mensagem.Conteudo
			};

			return mensagemDeEmail;
		}

		private void Enviar(MimeMessage mensagemDeEmail)
		{
			using (var client = new SmtpClient())
			{
				try
				{
					client.Connect("Conexão a fazer");
					//To Do auth de email

					client.Send(mensagemDeEmail);
				}
				catch(Exception ex)
				{
					throw ex;
				}
				finally
				{
					client.Disconnect(true);
					client.Dispose();
				}
			}
		}
	}
}
