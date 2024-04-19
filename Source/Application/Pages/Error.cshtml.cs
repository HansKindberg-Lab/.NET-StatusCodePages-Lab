using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Application.Pages
{
	[IgnoreAntiforgeryToken]
	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public class ErrorModel(ILogger<ErrorModel> logger) : PageModel
	{
		#region Fields

		private readonly ILogger<ErrorModel> _logger = logger;

		#endregion

		#region Properties

		public string? RequestId { get; set; }
		public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);

		#endregion

		#region Methods

		public void OnGet()
		{
			this.RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier;
			this._logger.LogError("Error");
		}

		#endregion
	}
}