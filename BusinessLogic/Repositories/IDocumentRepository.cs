

namespace CuentasPorCobrar.Shared;

public interface IDocumentRepository
{
    Task<Document?> CreateAsync(Document document);
    Task<IEnumerable<Document>> RetrieveAllAsync();
    Task<Document?> RetrieveAsync(int id);
    Task<Document?> UpdateAsync(int id, Document document);
    Task<bool?> DeleteAsync(int id); 

}

