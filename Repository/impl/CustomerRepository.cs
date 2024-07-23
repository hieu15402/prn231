using BusinessObjects;
using DataAccess;

namespace Repositories.impl
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MyDBContext _context;
        private readonly CustomerDAO _customerDAO;

        public CustomerRepository()
        {
            _context = new MyDBContext();
            _customerDAO = new CustomerDAO(_context);
        }
        public void SaveCustomer(Customer customer) => _customerDAO.SaveCustomer(customer);
        public List<Customer> GetCustomers(
            string? keyword = null,
            int pageIndex = 1,
            int pageSize = 5,
            string orderBy = "CustomerName")
        {
            return _customerDAO.GetCustomers(keyword, pageIndex, pageSize, orderBy);
        }
        public Customer GetCustomerById(string id) => _customerDAO.FindCustomerById(id);
        public Customer GetCustomerByEmail(string email) => _customerDAO.FindCustomerByEmail(email);
        public void UpdateCustomer(Customer customer) => _customerDAO.UpdateCustomer(customer);
        public void DeleteCustomer(Customer customer) => _customerDAO.DeleteCustomer(customer);
    }
}
