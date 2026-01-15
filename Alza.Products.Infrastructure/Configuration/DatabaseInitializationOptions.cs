namespace Alza.Products.Infrastructure.Configuration
{
    public sealed class DatabaseInitializationOptions
    {
        public bool EnsureDeleted { get; init; }
        public bool EnsureCreated { get; init; }
        public bool SeedData { get; init; }
    }
}
