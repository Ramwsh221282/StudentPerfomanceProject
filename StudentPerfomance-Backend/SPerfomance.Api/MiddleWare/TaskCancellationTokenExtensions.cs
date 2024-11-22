namespace SPerfomance.Api.MiddleWare;

public class TaskCancellationTokenExtensions(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception _) when (_ is OperationCanceledException or TaskCanceledException)
        {
            Console.WriteLine("The request was canceled.");
        }
    }
}
