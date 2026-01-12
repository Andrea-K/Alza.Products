namespace Alza.Products.Domain.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset DateLastModified { get; set; } = DateTimeOffset.UtcNow;

        public void UpdateLastModified()
        {
            this.DateLastModified = DateTimeOffset.UtcNow;
        }
    }
}
