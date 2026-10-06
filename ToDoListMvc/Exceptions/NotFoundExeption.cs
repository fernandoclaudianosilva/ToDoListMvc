namespace ToDoListMvc.Exceptions
{
    public class NotFoundExeption :ApplicationException
    {
        public NotFoundExeption(string message) : base(message)
        {
        }
    }
}
