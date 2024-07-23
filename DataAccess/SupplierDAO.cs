using BusinessObjects;
using System.Linq.Expressions;

namespace DataAccess
{
    public class SupplierDAO : BaseDAO<Supplier>
    {
        public SupplierDAO(MyDBContext dbContext) : base(dbContext)
        {
        }
        public List<Supplier> GetSuppliers(
            string? keyword = null,
            int pageIndex = 1,
            int pageSize = 5,
            string orderBy = "SupplierName")
        {
            // Define filter condition
            Expression<Func<Supplier, bool>> filter = s =>
                string.IsNullOrEmpty(keyword) || s.SupplierName.Contains(keyword);

            // Define ordering expression
            Func<IQueryable<Supplier>, IOrderedQueryable<Supplier>> orderByExpression = q => orderBy.ToLower() switch
            {
                "suppliernamedesc" => q.OrderByDescending(s => s.SupplierName),
                _ => q.OrderBy(s => s.SupplierName) // Default sorting by SupplierName
            };

            // Call the Get method from BaseDAO with filtering, ordering, and pagination
            return Get(
                filter: filter,
                orderBy: orderByExpression,
                pageIndex: pageIndex,
                pageSize: pageSize
            ).ToList();
        }

        public Supplier FindSupplierById(int supplierId)
        {
            return GetByID(supplierId);
        }

        public void SaveSupplier(Supplier supplier)
        {
            Insert(supplier);
            _dbContext.SaveChanges();
        }

        public void UpdateSupplier(Supplier supplier)
        {
            Update(supplier);
            _dbContext.SaveChanges();
        }

        public void DeleteSupplier(Supplier supplier)
        {
            Delete(supplier);
            _dbContext.SaveChanges();
        }
    }
}
