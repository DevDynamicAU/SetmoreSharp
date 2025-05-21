namespace SetmoreSharp
{
    public class ApiBody<T>
    {
        public T Content { get; }

        public ApiBody(T content) => Content = content;
    }
}