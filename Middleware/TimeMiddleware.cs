using System.Security.Cryptography.X509Certificates;

public class TimeMiddleware
{
    //nos permite invocar el request que sigue dentro del ciclo.
    readonly RequestDelegate next;

    public TimeMiddleware(RequestDelegate nextRequest)   
    {
        next = nextRequest;
    }

    public async Task Invoke(HttpContext context)
    {        
        await next(context);    

        if(context.Request.Query.Any(p=> p.Key == "time")) 
        {
            //devuelve la fecha y hora actual.
            await context.Response.WriteAsync(DateTime.Now.ToShortTimeString());
        }
        
    }
}

public static class TimeMiddlewareExtension
{
    public static IApplicationBuilder UseTimeMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TimeMiddleware>();
    }
}