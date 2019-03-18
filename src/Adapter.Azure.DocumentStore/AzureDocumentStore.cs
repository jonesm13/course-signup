namespace Adapter.Azure.DocumentStore
{
    using System.Threading.Tasks;
    using Domain.Ports;

    public class AzureDocumentStore : IDocumentStore
    {
        public Task StoreAsync<TDocument>(string key, TDocument toStore)
        {
            // TODO serialize toStore, store it in blob storage
            throw new System.NotImplementedException();
        }

        public Task<TDocument> GetAsync<TDocument>(string key)
        {
            // TODO read from blob store, deserialize to the target type
            throw new System.NotImplementedException();
        }

        public Task<bool> ExistsAsync(string key)
        {
            // TODO see if we have a blob with this key
            throw new System.NotImplementedException();
        }
    }
}