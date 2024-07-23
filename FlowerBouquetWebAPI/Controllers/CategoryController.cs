using BusinessObjects;
using FlowerBouquetWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.impl;

namespace FlowerBouquetWebAPI.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/Categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository repository = new CategoryRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories(string? keyword = null,
        int pageIndex = 1,
        int pageSize = 5,
        string orderBy = "CategoryName") => repository.GetCategories(keyword, pageIndex, pageSize, orderBy);

        [HttpGet("{id}")]
        public ActionResult<Category> GetCategory(int id)
        {
            var category = repository.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public IActionResult PostCategory(Category category)
        {
            repository.SaveCategory(category);
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult PutCategory(int id, Category category)
        {
            if (id != category.CategoryID)
            {
                return BadRequest();
            }

            var existingCategory = repository.GetCategoryById(id);
            if (existingCategory == null)
            {
                return NotFound();
            }

            repository.UpdateCategory(category);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = repository.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            repository.DeleteCategory(category);
            return NoContent();
        }
    }
}
