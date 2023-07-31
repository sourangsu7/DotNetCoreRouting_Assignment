using RoutingAssignment;
using System.Diagnostics.Metrics;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("Countries", async (httpContext) =>
    {
        List<string>? allCountries = new CountryCollection().Countries.Select(x => x.Value).ToList();
        foreach (var country in allCountries)
        {
            await httpContext.Response.WriteAsync($"{country}\n");
        }
    });
    endpoints.MapGet("Countries/{id:range(1,5):int}", async (httpContext) =>
    {
        if (httpContext.Request.RouteValues.ContainsKey("id"))
        {
            var countryId = Convert.ToInt32(httpContext.Request.RouteValues["id"]);
            new CountryCollection().Countries.TryGetValue(countryId, out var country);
            await httpContext.Response.WriteAsync(country);
        }
       
    });
    endpoints.MapGet("Countries/{id:range(6,99):int}", async (httpContext) =>
    {
        if (httpContext.Request.RouteValues.ContainsKey("id"))
        {
            httpContext.Response.StatusCode = 404;
            await httpContext.Response.WriteAsync("No Country");
        }

    });
    endpoints.MapGet("Countries/{id:min(100):int}", async (httpContext) =>
    {
        if (httpContext.Request.RouteValues.ContainsKey("id"))
        {
            httpContext.Response.StatusCode = 400;
            await httpContext.Response.WriteAsync("The CountryID should be between 1 and 100");
        }

    });
});
app.Run(async (httpContext) =>
{
    await httpContext.Response.WriteAsync($"Server requests which doesn't" +
        $" match with matching routingpath is collected here {httpContext.Request.Path}");
});
app.Run();
