namespace Expenses.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception exception) : base(message, exception)
        {
        }

        public NotFoundException(string name, object key) : base($"No se encontro la entidad {name} {key}")
        {
        }
    }
}
