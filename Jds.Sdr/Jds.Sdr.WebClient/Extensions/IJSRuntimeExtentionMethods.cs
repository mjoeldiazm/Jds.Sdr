using Microsoft.JSInterop;

namespace Jds.Sdr.WebClient.Extensions
{
	public static class IJSRuntimeExtentionMethods
	{
		public static ValueTask<object> SaveInLocalStorageAsync(this IJSRuntime jSRuntime, string key, string content)
		{
			return jSRuntime.InvokeAsync<object>("localStorage.setItem", key, content);
		}

		public static ValueTask<object> GetFromLocalStorageAsync(this IJSRuntime jSRuntime, string key)
		{
			return jSRuntime.InvokeAsync<object>("localStorage.getItem", key);
		}

		public static ValueTask<object> RemoveFromLocalStorageAsync(this IJSRuntime jSRuntime, string key)
		{
			return jSRuntime.InvokeAsync<object>("localStorage.removeItem", key);
		}

		public static ValueTask ShowToastMessage(this IJSRuntime jSRuntime)
		{
			return jSRuntime.InvokeVoidAsync("showToastMessage");
		}
	}
}
