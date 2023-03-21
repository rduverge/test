using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Concurrent;


namespace CuentasPorCobrar.Shared;

public class DocumentRepository: IDocumentRepository
{
    //Use a static thread safe dictionary field to cache the customer 

    private static ConcurrentDictionary<int, Document>? documentCache;


    /*Use an instance of data context field because it should not
     * be cached due to their internal caching
     
     */
    private CuentasporcobrardbContext db;
    private IValidator<Document> validator; 

    public DocumentRepository(CuentasporcobrardbContext db, IValidator<Document> validator)
    {
        this.validator=validator; 
        this.db=db;

        if(documentCache is null)
        {
            documentCache=new ConcurrentDictionary <int, Document>(db.Documents.ToDictionary(d=>d.DocumentId)); 
        }
    }
    public Task<IEnumerable<Document>> RetrieveAllAsync()
    {
        //for performance get from cache 

        return Task.FromResult
            (documentCache is null ? Enumerable.Empty<Document>()
            : documentCache.Values);
    }

    public Task<Document?>RetrieveAsync(int id)
    {
        // for performance get from cache 

        if (documentCache is null) return null!;

        documentCache.TryGetValue(id, out Document? document);
        return Task.FromResult(document); 

    }


    public async Task<Document?> CreateAsync(Document document)
    {

     
        EntityEntry<Document> added = await db.Documents.AddAsync(document);
        int affected = await db.SaveChangesAsync();

    

        if(affected == 1)
        {
            if (documentCache is null) return document;


            //if the Documen is new  , add it to cache 

            return documentCache.AddOrUpdate(document.DocumentId, document, UpdateCache);
        }
        else
        {
            return null; 
        }

    }
    

    private Document UpdateCache(int id, Document document)
    {
        Document? old; 
        if(documentCache is not null)
        {
            if(documentCache.TryGetValue(id, out old))
            {
                if (documentCache.TryUpdate(id, document, old))
                {
                    return document; 
                }
            }
        }
        return null!;
    }
    public async Task<Document?> UpdateAsync(int id, Document document)
    {
        //update in database 
        db.Documents.Update(document);
        int affected = await db.SaveChangesAsync();

        if (affected==1)
        {
            //update in cache
            return UpdateCache(id, document);
        }
        return null;


    }

    public async Task<bool?> DeleteAsync(int id)
    {
        //remove from database 
        Document? document = db.Documents.Find(id);
        if (document is null) return null;
        db.Documents.Remove(document);
        int affected = await db.SaveChangesAsync();

        if (affected==1)
        {
            if (documentCache is null) return null;

            //remove from cache 
            return documentCache.TryRemove(id, out document); 

        }
        else
        {
            return null;
        }
    }

   
}
