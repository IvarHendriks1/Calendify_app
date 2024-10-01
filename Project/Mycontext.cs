namespace NewContext;
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
        app.MapPost("/Login/{user}/{pass}", UserLogin);
        app.MapGet("/Login/{user}", IsUserLogged);
    }

    public IResult UserLogin(string user, string pass) {
        if (user is null || pass is null){
            //if (usernotfound) Results.NotFound($"No user found with username: {user}")
            string user_pass = "hi"; //get password of the user from database
            if (pass == user_pass){
                Results.Ok("Login Succes");
                //set user to logged in
            }
            return Results.Ok("Login failed incorrect password");

        }
        return Results.NoContent();
    }

    public IResult IsUserLogged(string user){
        if (user is null){
            bool isuserlogged = false; //check in the database if user is logged
            
            return Results.Ok(isuserlogged);
            
        }
        return Results.NoContent();
    }

}


