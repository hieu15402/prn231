using BusinessObjects;

namespace Repositories
{
    public interface ICustomerRepository
    {
        void SaveCustomer(Customer customer);
        Customer GetCustomerById(string id);
        Customer GetCustomerByEmail(string email);
        List<Customer> GetCustomers(
            string? keyword = null,
            int pageIndex = 1,
            int pageSize = 5,
            string orderBy = "CustomerName");
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
    }
}
