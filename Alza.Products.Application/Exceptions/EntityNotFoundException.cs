namespace Alza.Products.Application.Exceptions
{
    public sealed class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityName, object id) : base($"{entityName} with id '{id}' was not found.")
        {
        }
    }
}
