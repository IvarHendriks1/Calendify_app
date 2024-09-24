class MyContext
{

    public WebApplication app { get; set; }
    public MyContext(WebApplication app)
    {
        this.app = app;
        DefineEndPoints();
    }
    private void DefineEndPoints()
    {
        app.MapPost("/Login", (string user, string pass) => UserLogin);
    }

}


// def UserLogin