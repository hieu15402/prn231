using BusinessObjects;

namespace Repositories
{
    public interface ISupplierRepository
    {
        void SaveSupplier(Supplier supplier);
        Supplier GetSupplierById(int id);
        List<Supplier> GetSuppliers(
            string? keyword = null,
            int pageIndex = 1,
            int pageSize = 5,
            string orderBy = "SupplierName");
        void UpdateSupplier(Supplier supplier);
        void DeleteSupplier(Supplier supplier);
    }
}
