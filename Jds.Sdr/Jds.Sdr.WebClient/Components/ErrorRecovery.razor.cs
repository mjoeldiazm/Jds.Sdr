using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Jds.Sdr.WebClient.Components
{
	public partial class ErrorRecovery
	{
		[CascadingParameter]
		protected ErrorBoundary ErrorBoundary { get; set; }

		protected void OnErrorRecoverClick()
		{
			ErrorBoundary?.Recover();
		}
	}
}
