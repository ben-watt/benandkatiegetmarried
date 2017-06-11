namespace benandkatiegetmarried.UseCases
{
    public interface IHandler<Request, Response>
    {
        Response Handle(Request request);
    }
}