namespace HrManagementSystem.Core.HttpClients
{
    public interface IGenericHttpClient
    {
        //Task<List<TResponse>> GetAsync<TResponse>(string address);
        Task<T> GetAsync<T>(string address);
        Task<T> GetWithId<T>(string address);

        Task<T> GetWithId<T>(string address, dynamic dynamicRequest);

        Task<T> PostAsync<T>(string address, dynamic dynamicRequest);

        Task<T> PutAsync<T>(string address, dynamic dynamicRequest);

        Task<T> DeletAsync<T>(string address);

    }
}
