using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
	public class CloudMailService: IMailService {
		private string _mailTo = "admin@mycompany.com";
		private string _mailFrom = "noreply@mycompany.com";

		public void Send(string subject, string message)
		{
			// send mail - output to debug window
			// won't show these lines when not in debug mode
			Debug.WriteLine($"Main from {_mailFrom} to {_mailTo}, with CloudMailService.");
			Debug.WriteLine($"Subject: {subject}");
			Debug.WriteLine($"Message: {message}");
		}
	}
}
