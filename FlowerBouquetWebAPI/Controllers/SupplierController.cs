using BusinessObjects;
using FlowerBouquetWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.impl;

namespace FlowerBouquetWebAPI.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]s")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private ISupplierRepository repository = new SupplierRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Supplier>> GetSuppliers(
            string? keyword = null,
            int pageIndex = 1,
            int pageSize = 5,
            string orderBy = "SupplierName") => repository.GetSuppliers(
                keyword: keyword,
                pageIndex: pageIndex,
                pageSize: pageSize,
                orderBy: orderBy);

        [HttpPost]
        public IActionResult PostSupplier(Supplier supplier)
        {
            repository.SaveSupplier(supplier);
            return NoContent();
        }
    }
}
