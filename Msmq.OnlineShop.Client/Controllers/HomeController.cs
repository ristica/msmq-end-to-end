using System.Web.Mvc;
using Msmq.OnlineShop.Entities;
using Msmq.OnlineShop.ViewModels;

namespace Msmq.OnlineShop.Client.Controllers
{
    public class HomeController : Controller
    {
        private int _stock;

        [HttpGet]
        public ActionResult List()
        {
            var products = Db.Repository.ProductRepository.Get();
            return View(products);
        }

        [HttpGet]
        public ActionResult PlaceOrder()
        {
            return View(new ProductViewModel());
        }

        [HttpPost]
        public ActionResult PlaceOrder(ProductViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (!this.ProductInStock(vm.ProductId, vm.Quantity, out this._stock))
            {
                ModelState.AddModelError("", "Available products in stock: "+ this._stock + ".");
                return View(vm);
            }

            this.PlaceNewOrder(vm);

            Db.Repository.ProductRepository.Create(new Product
            {
                ProductName = vm.ProductName
            });

            return RedirectToAction("List");
        }

        #region

        private bool ProductInStock(int productId, int quantity, out int stock)
        {
            var service = new ApplicationServices.IsProductInStockService();
            stock = service.GetCurrentStock(productId);

            if (stock < quantity)
                return false;

            return true;
        }

        private void PlaceNewOrder(ProductViewModel vm)
        {
            var service = new ApplicationServices.PlaceNewOrderService();
            service.PlaceOrder(vm);
        }

        #endregion

    }
}