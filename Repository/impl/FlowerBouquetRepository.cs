using BusinessObjects;
using DataAccess;

namespace Repositories.impl
{
    public class FlowerBouquetRepository : IFlowerBouquetRepository
    {
        private readonly MyDBContext _context;
        private readonly FlowerBouquetDAO _flowerBouquetDAO;

        public FlowerBouquetRepository()
        {
            _context = new MyDBContext();
            _flowerBouquetDAO = new FlowerBouquetDAO(_context);
        }
        public void SaveFlowerBouquet(FlowerBouquet flowerBouquet) => _flowerBouquetDAO.SaveFlowerBouquet(flowerBouquet);
        public FlowerBouquet GetFlowerBouquetById(int id) => _flowerBouquetDAO.FindFlowerBouquetById(id);
        public List<FlowerBouquet> GetFlowerBouquets(
        string? keyword = null,
        int pageIndex = 1,
        int pageSize = 5,
        string orderBy = "FlowerBouquetName",
        int? categoryId = null,
        int? supplierId = null)
        {
            return _flowerBouquetDAO.GetFlowerBouquets(
                keyword: keyword,
                pageIndex: pageIndex,
                pageSize: pageSize,
                orderBy: orderBy,
                categoryId: categoryId,
                supplierId: supplierId
            );
        }
        public void UpdateFlowerBouquet(FlowerBouquet flowerBouquet) => _flowerBouquetDAO.UpdateFlowerBouquet(flowerBouquet);
        public void DeleteFlowerBouquet(FlowerBouquet flowerBouquet) => _flowerBouquetDAO.DeleteFlowerBouquet(flowerBouquet);
    }
}
