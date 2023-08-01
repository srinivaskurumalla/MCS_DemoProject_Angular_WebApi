namespace MCS_DemoProject_Angular_WebApi.Interfaces
{
    public interface IUsers<T> where T : class
    {
        Task<bool> Create(T obj);
        Task<T?> Update(int id, T obj);
        Task<T?> Delete(int id);

        Task<IEnumerable<T>> GetAll();

        Task<T?> GetUserById(int id);
        Task<T?> GetUserByEmail(string email);
    }
}
