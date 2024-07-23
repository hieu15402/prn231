using BusinessObjects;
using System.Linq.Expressions;

namespace DataAccess
{
    public class FlowerBouquetDAO : BaseDAO<FlowerBouquet>
    {
        public FlowerBouquetDAO(MyDBContext context) : base(context)
        {
        }
        public List<FlowerBouquet> GetFlowerBouquets(
            string? keyword = null,
            int pageIndex = 1,
            int pageSize = 5,
            string orderBy = "FlowerBouquetName",
            int? categoryId = null,
            int? supplierId = null)
        {
            Expression<Func<FlowerBouquet, bool>> filter = f =>
                (string.IsNullOrEmpty(keyword) || f.FlowerBouquetName.Contains(keyword)) &&
                (!categoryId.HasValue || f.CategoryID == categoryId.Value) &&
                (!supplierId.HasValue || f.SupplierID == supplierId.Value);

            Func<IQueryable<FlowerBouquet>, IOrderedQueryable<FlowerBouquet>> orderByExpression = null;
            switch (orderBy.ToLower())
            {
                case "flowerbouquetname":
                    orderByExpression = q => q.OrderBy(f => f.FlowerBouquetName);
                    break;
                case "flowerbouquetnamedesc":
                    orderByExpression = q => q.OrderByDescending(f => f.FlowerBouquetName);
                    break;
                // Add other sorting options if needed
                default:
                    orderByExpression = q => q.OrderBy(f => f.FlowerBouquetName);
                    break;
            }

            return Get(
                filter: filter,
                orderBy: orderByExpression,
                pageIndex: pageIndex,
                pageSize: pageSize
            ).ToList();
        }

        public FlowerBouquet FindFlowerBouquetById(int flowerBouquetId)
        {
            return GetByID(flowerBouquetId);
        }

        public void SaveFlowerBouquet(FlowerBouquet flowerBouquet)
        {
            Insert(flowerBouquet);
            _dbContext.SaveChanges();
        }

        public void UpdateFlowerBouquet(FlowerBouquet flowerBouquet)
        {
            Update(flowerBouquet);
            _dbContext.SaveChanges();
        }

        public void DeleteFlowerBouquet(FlowerBouquet flowerBouquet)
        {
            Delete(flowerBouquet);
            _dbContext.SaveChanges();
        }
    }
}
