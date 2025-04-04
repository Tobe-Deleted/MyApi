using System.Security.Principal;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private static List<Product> _products = new()
    {
        new Product{Id = 1, Name = "3x3x3 Rubik's Cube", Price = 89.00M},
        new Product{Id = 2, Name = "2x2x2 Rubik's Cube", Price = 59.90M},
        new Product{Id = 3, Name = "Mirror Cube", Price = 99.00M}
    };

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_products);
    }

    [HttpGet("{Id}")]
    public IActionResult GetById(int id)
    {
        var product = _products.FirstOrDefault(x => x.Id == id);
        return product != null ? Ok(product) : NotFound();
    }

    [HttpPost]
    public IActionResult Create([FromBody] Product product)
    {
        product.Id = _products.Max(x => x.Id) +1;
        _products.Add(product);
        return CreatedAtAction(nameof(GetById), new {id = product.Id}, product);
    }

    [HttpPut("{Id}")]
    public IActionResult Update(int id, [FromBody] Product updatedProduct)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if(product == null)return NotFound();

        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;
        return NoContent();
    }

    [HttpDelete("{Id}")]
    public IActionResult Delete(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();
        _products.Remove(product);
        return NoContent();
    }
}