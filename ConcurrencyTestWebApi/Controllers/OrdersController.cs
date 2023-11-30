using ConcurrencyTestWebApi.Context;
using ConcurrencyTestWebApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcurrencyTestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid productId, CancellationToken cancellationToken)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    Product product = await _context.Set<Product>().FromSql($"select * from Products WITH (UPDLOCK) where id = {productId}").FirstOrDefaultAsync(cancellationToken);
                    //Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
                    if (product.StockQuantity > 0)
                    {
                        product.StockQuantity -= 1;

                        Order order = new(productId, 1);
                        _context.Orders.Add(order);
                        _context.Products.Update(product);

                        await _context.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync(cancellationToken);
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                catch (Exception ex)
                {
                    if (ex is DbUpdateConcurrencyException)
                    {
                        // retry logic can be applied.
                    }
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }
    }
}
