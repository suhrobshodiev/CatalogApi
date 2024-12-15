namespace CatalogApi.Models;

public class CatalogDbSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string CatalogCollectionName { get; set; } = null!;
}