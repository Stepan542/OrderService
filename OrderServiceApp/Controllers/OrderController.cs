using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderServiceApp.Data;
using OrderServiceApp.Models;

namespace OrderServiceApp.Controllers
{
    public class OrderController : Controller
    {
       private readonly ApplicationDbContext _dbContext;

        public OrderController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _dbContext.Orders.ToListAsync();
            return View(orders);
        }

        public IActionResult Create()
        {
            ViewBag.Action = "create";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                if (order.OrderDate.HasValue)
                {
                    order.OrderDate = DateTime.SpecifyKind(order.OrderDate.Value, DateTimeKind.Utc);
                }

                _dbContext.Add(order);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "create";
            return View(order);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            ViewBag.Action = "edit";
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (order.OrderDate.HasValue)
                {
                    order.OrderDate = DateTime.SpecifyKind(order.OrderDate.Value, DateTimeKind.Utc);
                }

                _dbContext.Orders.Update(order);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "edit";
            return View(order);
        }

        //[HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _dbContext.Orders.FindAsync(id);
            if(order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        //ActionName("DeleteConfirmed")
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _dbContext.Orders.FindAsync(id);

            if(order == null)
            {
                return NotFound();
            }

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
