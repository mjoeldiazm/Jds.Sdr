using Jds.Sdr.WebClient.Interfaces;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Jds.Sdr.WebClient.Security
{
	public class UserAccountTokenRenewer : IDisposable
	{
		private readonly ILoginService loginService;
		private Timer timer;

		public UserAccountTokenRenewer(ILoginService loginService)
		{
			this.loginService = loginService;
		}

		public void Initialize()
		{
			timer = new()
			{
				Interval = 1000 * 60 * (JwtAuthenticationProvider.TokenRenewalThresholdInMinutes - 1)
			};
			timer.Elapsed += Timer_Elapsed;
			timer.Start();
		}

		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			loginService.RenewTokenHandler();
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
	}
}
