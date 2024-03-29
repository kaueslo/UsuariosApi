﻿using MailKit.Net.Smtp;
using MimeKit;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
	public class EmailService
	{
		private IConfiguration _configuration;

		public EmailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void EnviarEmail(string[] destinatario, string assunto, int usuarioid, string code)
		{
			var mensagem = new Mensagem(destinatario, assunto, usuarioid, code);

			var mensagemDeEmail = CriaCorpoDoEmail(mensagem);

			Enviar(mensagemDeEmail);
		}

		private MimeMessage CriaCorpoDoEmail(Mensagem mensagem)
		{
			var mensagemDeEmail = new MimeMessage();
			mensagemDeEmail.From.Add(new MailboxAddress(default, _configuration.GetValue<string>("EmailSettings:From")));
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
					//Smtp do Email / Porta / SSL sim ou não
					client.Connect(_configuration.GetValue<string>("EmailSettings:SmtpServer"), 
						_configuration.GetValue<int>("EmailSettings:Port"), true);
					client.AuthenticationMechanisms.Remove("XOUATH2");
					client.Authenticate(_configuration.GetValue<string>("EmailSettings:From"),
						_configuration.GetValue<string>("EmailSettings:Password"));

					client.Send(mensagemDeEmail);
				}
				catch
				{
					throw;
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
