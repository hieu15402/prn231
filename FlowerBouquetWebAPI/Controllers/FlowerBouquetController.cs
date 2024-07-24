using BusinessObjects;
using FlowerBouquetWebAPI.BusinessObjects;
using FlowerBouquetWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.impl;

namespace FlowerBouquetWebAPI.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]

    public class FlowerBouquetController : ControllerBase
    {
        private IFlowerBouquetRepository repository = new FlowerBouquetRepository();

        [HttpGet]
        public ActionResult<IEnumerable<FlowerBouquet>> GetFlowerBouquets(
            string? keyword = null,
            int pageIndex = 1,
            int pageSize = 5,
            string orderBy = "FlowerBouquetName",
            int? categoryId = null,
            int? supplierId = null) => Ok(new ResponseObject<IEnumerable<FlowerBouquet>>("Get success", repository.GetFlowerBouquets(keyword: keyword,
                pageIndex: pageIndex,
                pageSize: pageSize,
                orderBy: orderBy,
                categoryId: categoryId,
                supplierId: supplierId)));

        [HttpGet("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public ActionResult<FlowerBouquet> GetFlowerBouquetById(int id) => Ok(new ResponseObject<FlowerBouquet>("Get success", repository.GetFlowerBouquetById(id)));


        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult PostFlowerBouquet(PostFlowerBouquet postFlowerBouquet)
        {
            if (repository.GetFlowerBouquets().FirstOrDefault(f => f.FlowerBouquetName.ToLower().Equals(postFlowerBouquet.FlowerBouquetName.ToLower())) != null)
            {
                return BadRequest();
            }
            var f = new FlowerBouquet
            {
                FlowerBouquetName = postFlowerBouquet.FlowerBouquetName,
                Description = postFlowerBouquet.Description,
                UnitPrice = postFlowerBouquet.UnitPrice,
                UnitsInStock = postFlowerBouquet.UnitsInStock,
                FlowerBouquetStatus = postFlowerBouquet.FlowerBouquetStatus,
                CategoryID = postFlowerBouquet.CategoryID,
                SupplierID = postFlowerBouquet.SupplierID
            };
            repository.SaveFlowerBouquet(f);
            return Ok(new ResponseObject<FlowerBouquet>("Create success", f));
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult DeleteFlowerBouquet(int id)
        {
            var f = repository.GetFlowerBouquetById(id);
            if (f == null)
            {
                return NotFound(new ResponseObject<String>("Not found", ""));
            }
            if (f.OrderDetails != null && f.OrderDetails.Count > 0)
            {
                f.FlowerBouquetStatus = 2;
                repository.UpdateFlowerBouquet(f);
            }
            else
            {
                repository.DeleteFlowerBouquet(f);
            }
            return Ok(new ResponseObject<FlowerBouquet>("Delete success", f));
        }
        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult PutFlowerBouquet(int id, PostFlowerBouquet postFlowerBouquet)
        {
            var fTmp = repository.GetFlowerBouquetById(id);
            if (fTmp == null)
            {
                return NotFound(new ResponseObject<String>("Not found", ""));
            }

            if (!fTmp.FlowerBouquetName.ToLower().Equals(postFlowerBouquet.FlowerBouquetName.ToLower())
                && repository.GetFlowerBouquets().FirstOrDefault(f => f.FlowerBouquetName.ToLower().Equals(postFlowerBouquet.FlowerBouquetName.ToLower())) != null)
            {
                return BadRequest();
            }
            else
            {
                fTmp.FlowerBouquetName = postFlowerBouquet.FlowerBouquetName;
            }

            fTmp.Description = postFlowerBouquet.Description;
            fTmp.UnitPrice = postFlowerBouquet.UnitPrice;
            fTmp.UnitsInStock = postFlowerBouquet.UnitsInStock;
            fTmp.CategoryID = postFlowerBouquet.CategoryID;
            fTmp.SupplierID = postFlowerBouquet.SupplierID;

            repository.UpdateFlowerBouquet(fTmp);
            return Ok(new ResponseObject<FlowerBouquet>("Update success", fTmp));
        }
    }
}
