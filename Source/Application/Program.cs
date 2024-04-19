using System.Globalization;
using System.Net.Mime;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using RegionOrebroLan.Localization.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPathBasedLocalization(options => { options.FileResourcesDirectoryPath = "Resources"; }, true);
builder.Services.AddRazorPages();

var app = builder.Build();

var stringLocalizer = app.Services.GetRequiredService<IStringLocalizer>();

//if(!app.Environment.IsDevelopment())
app.UseExceptionHandler("/Error");
app.UseStaticFiles();
app.UseRouting();
app.UseRequestLocalization(options =>
{
	options.ApplyCurrentCultureToResponseHeaders = true;
	options.DefaultRequestCulture = new RequestCulture("en-GB", "en");
	options.FallBackToParentCultures = true;
	options.FallBackToParentUICultures = true;
	options.SupportedCultures!.Clear();
	options.SupportedCultures!.Add(CultureInfo.GetCultureInfo("de-DE"));
	options.SupportedCultures!.Add(CultureInfo.GetCultureInfo("en-GB"));
	options.SupportedCultures!.Add(CultureInfo.GetCultureInfo("en-US"));
	options.SupportedCultures!.Add(CultureInfo.GetCultureInfo("sv-SE"));
	options.SupportedUICultures!.Clear();
	options.SupportedUICultures!.Add(CultureInfo.GetCultureInfo("de"));
	options.SupportedUICultures!.Add(CultureInfo.GetCultureInfo("en"));
	options.SupportedUICultures!.Add(CultureInfo.GetCultureInfo("sv"));

	options.RequestCultureProviders.RemoveAt(2);
});
app.UseStatusCodePages(context =>
{
	var requestCultureFeature = context.HttpContext.Features.Get<IRequestCultureFeature>();
	var statusCode = context.HttpContext.Response.StatusCode;
	string statusCodePage;

	using(var streamReader = new StreamReader(typeof(Program).Assembly.GetManifestResourceStream("Application.Resources.StatusCodePage.html")!))
	{
		statusCodePage = streamReader.ReadToEnd();
	}

	var name = stringLocalizer[$"statusCodes/_{statusCode}/name"].Value;
	var text = stringLocalizer[$"statusCodes/_{statusCode}/text"].Value;

	var body = string.Format(requestCultureFeature!.RequestCulture.UICulture, statusCodePage, requestCultureFeature.RequestCulture.UICulture, statusCode + " - " + name, text);

	context.HttpContext.Response.ContentType = MediaTypeNames.Text.Html;

	return context.HttpContext.Response.WriteAsync(body);
});
app.UseAuthorization();
app.MapRazorPages();

app.Run();