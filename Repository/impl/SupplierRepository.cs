using BusinessObjects;
using DataAccess;

namespace Repositories.impl
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly MyDBContext _context;
        private readonly SupplierDAO _supplierDAO;

        public SupplierRepository()
        {
            _context = new MyDBContext();
            _supplierDAO = new SupplierDAO(_context);
        }
        public void SaveSupplier(Supplier supplier) => _supplierDAO.SaveSupplier(supplier);
        public Supplier GetSupplierById(int id) => _supplierDAO.FindSupplierById(id);
        public List<Supplier> GetSuppliers(
            string? keyword = null,
            int pageIndex = 1,
            int pageSize = 5,
            string orderBy = "SupplierName")
        {
            return _supplierDAO.GetSuppliers(
                keyword: keyword,
                pageIndex: pageIndex,
                pageSize: pageSize,
                orderBy: orderBy
            );
        }
        public void UpdateSupplier(Supplier supplier) => _supplierDAO.UpdateSupplier(supplier);
        public void DeleteSupplier(Supplier supplier) => _supplierDAO.DeleteSupplier(supplier);
    }
}
