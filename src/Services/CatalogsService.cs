using CatalogApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CatalogApi.Services;

public class CatalogsService
{
    private readonly IMongoCollection<Product> _productsCollection;

    public CatalogsService(IOptions<CatalogDbSettings> catalogDbSettings)
    {
        if (catalogDbSettings == null)
            throw new ArgumentNullException(nameof(catalogDbSettings));
        if (string.IsNullOrEmpty(catalogDbSettings.Value.ConnectionString))
            throw new ArgumentException("Connection string is not configured.");

        var mongoClient = new MongoClient(catalogDbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(catalogDbSettings.Value.DatabaseName);

        _productsCollection = mongoDatabase.GetCollection<Product>(
            catalogDbSettings.Value.CatalogCollectionName);
    }

    public async Task<List<Product>> GetProductsAsync()
    {
       return await _productsCollection.Find(p => true).ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(string id)
    {
        return await _productsCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateProductAsync(Product product)
    {
        await _productsCollection.InsertOneAsync(product);
    }

    public async Task UpdateProductAsync(string id, Product product)
    {
        await _productsCollection.ReplaceOneAsync(p => p.Id == id, product);
    }

    public async Task RemoveProductAsync(string id)
    {
        await _productsCollection.DeleteOneAsync(p => p.Id == id);
    }
}