var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Urls.Add("http://localhost:5000");

app.MapGet("/hi", () => "Hello pleps!");



new MyContext(app);

app.Run();
