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
    [Authorize(Roles = "Admin")]
    public class CustomerController : ControllerBase
    {
        private ICustomerRepository repository = new CustomerRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers(
            string? keyword = null,
            int pageIndex = 1,
            int pageSize = 5,
            string orderBy = "CustomerName") => Ok(new ResponseObject<IEnumerable<Customer>>("Get success", repository.GetCustomers(keyword, pageIndex, pageSize, orderBy)));


        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomerById(string id) => Ok(new ResponseObject<Customer>("Get success", repository.GetCustomerById(id)));

        [HttpGet("Email/{email}")]
        public ActionResult<Customer> GetCustomerByEmail(string email) => Ok(new ResponseObject<Customer>("Get success", repository.GetCustomerByEmail(email)));

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(string id)
        {
            var c = repository.GetCustomerById(id);
            if (c == null)
            {
                return NotFound(new ResponseObject<String>("Not found", ""));
            }
            repository.DeleteCustomer(c);
            return Ok(new ResponseObject<Customer>("Delete success", c));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Customer")]
        public IActionResult PutCustomer(string id, PutCustomer putCustomer)
        {
            var cTmp = repository.GetCustomerById(id);
            if (cTmp == null)
            {
                return NotFound(new ResponseObject<String>("Not found", ""));
            }

            cTmp.CustomerName = putCustomer.CustomerName;
            cTmp.City = putCustomer.City;
            cTmp.Country = putCustomer.Country;
            cTmp.Birthday = putCustomer.Birthday;

            if (putCustomer.Password != null && cTmp.PasswordHash != putCustomer.Password)
            {
                cTmp.PasswordHash = putCustomer.Password;
            }

            repository.UpdateCustomer(cTmp);
            return Ok(new ResponseObject<Customer>("Update success", cTmp));
        }
    }
}
