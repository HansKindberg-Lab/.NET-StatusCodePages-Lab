using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Application.Pages
{
	public class IndexModel : PageModel
	{
		#region Fields

		private readonly ILogger _logger;

		#endregion

		#region Constructors

		public IndexModel(ILoggerFactory loggerFactory)
		{
			this._logger = (loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory))).CreateLogger(this.GetType());
		}

		#endregion

		#region Methods

		public void OnGet()
		{
			this._logger.LogDebug("OnGet");
		}

		#endregion
	}
}