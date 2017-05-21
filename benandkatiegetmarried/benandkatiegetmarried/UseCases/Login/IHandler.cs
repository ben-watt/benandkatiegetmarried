namespace benandkatiegetmarried.UseCases.Login
{
    public interface IHandler<Request, Response>
    {
        Response Handle(Request request);
    }
}