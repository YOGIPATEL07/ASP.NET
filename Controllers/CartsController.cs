using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using assignments.Models;
using assignments.Services;

namespace assignments.Controllers
{
    public class CartsController : Controller
    {
       
        private readonly ApplicationDbContext _context;
        private readonly CartService _cartService;

        public CartsController(CartService cartService, ApplicationDbContext context)
        {
            _context = context;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var cart = _cartService.GetCart();

            if (cart == null)
            {
                return NotFound();
            }

            if (cart.CartItems.Count > 0)
            {
                foreach (var cartItem in cart.CartItems)
                {
                    var product = await _context.Products
                        .Include(p => p.Game)
                        .FirstOrDefaultAsync(p => p.SKU == cartItem.ProductId);

                    if (product != null)
                    {
                        cartItem.Product = product;
                    }
                }
            }

            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var cart = _cartService.GetCart();

            if (cart == null)
            {
                return NotFound();
            }

            var cartItem = cart.CartItems.Find(cartItem => cartItem.ProductId == productId);

            if (cartItem != null && cartItem.Product != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.SKU == productId);

                if (product == null)
                {
                    return NotFound();
                }

                cartItem = new CartItem { ProductId = productId, Quantity = quantity, Product = product };
                cart.CartItems.Add(cartItem);
            }


            _cartService.SaveCart(cart);

            return RedirectToAction("Index");
        }

        [HttpPost] 
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = _cartService.GetCart();

            if (cart == null)
            {
                return NotFound();
            }

            var cartItem = cart.CartItems.Find(cartItem => cartItem.ProductId == productId);

            if (cartItem != null)
            {
                cart.CartItems.Remove(cartItem);

                _cartService.SaveCart(cart);
            }

            return RedirectToAction("Index");
        }
    }

}