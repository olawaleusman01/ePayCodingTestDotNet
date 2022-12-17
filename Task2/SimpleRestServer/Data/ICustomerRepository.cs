using SimpleRestServer.Models;

namespace SimpleRestServer.Data
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomers();
        bool CustomerExist(int id);
        void InsertCustomers(List<Customer> customers);
    }
}