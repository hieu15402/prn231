using BusinessObjects;

namespace Repositories
{
    public interface IFlowerBouquetRepository
    {
        void SaveFlowerBouquet(FlowerBouquet flowerBouquet);
        FlowerBouquet GetFlowerBouquetById(int id);
        List<FlowerBouquet> GetFlowerBouquets(string? keyword = null,
        int pageIndex = 1,
        int pageSize = 5,
        string orderBy = "FlowerBouquetName",
        int? categoryId = null,
        int? supplierId = null);
        void UpdateFlowerBouquet(FlowerBouquet flowerBouquet);
        void DeleteFlowerBouquet(FlowerBouquet flowerBouquet);
    }
}
