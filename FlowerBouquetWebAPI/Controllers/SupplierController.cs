using BusinessObjects;
using FlowerBouquetWebAPI.BusinessObjects;
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
            string orderBy = "SupplierName") => Ok(new ResponseObject<IEnumerable<Supplier>>("Get success", repository.GetSuppliers(
                keyword: keyword,
                pageIndex: pageIndex,
                pageSize: pageSize,
                orderBy: orderBy)));

        [HttpGet("{id}")]
        public ActionResult<Supplier> GetById(int id)
        {
            var s = repository.GetSupplierById(id);
            return s != null ? Ok(new ResponseObject<Supplier>("Get success", s)) : NotFound(new ResponseObject<String>("Not found", ""));
        }
        [HttpPost]
        public IActionResult PostSupplier(Supplier supplier)
        {
            repository.SaveSupplier(supplier);
            return Ok(new ResponseObject<Supplier>("Create success", supplier));
        }
    }
}
