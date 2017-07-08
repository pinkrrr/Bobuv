using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.ModelBinding;
using Bobuv.Models;
using Bobuv.Models.Repository;

namespace Bobuv.Pages.Admin
{
    public partial class Orders : System.Web.UI.Page
    {
        private Repository repository = new Repository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                int dispatchID;
                if (int.TryParse(Request.Form["dispatch"], out dispatchID))
                {
                    Order myOrder = repository.Orders.Where(o => o.OrderId == dispatchID).FirstOrDefault();
                    if (myOrder != null)
                    {
                        myOrder.Dispatched = true;
                        repository.SaveOrder(myOrder);
                    }
                }
            }
        }

        public IEnumerable<Order> GetOrders([Control] bool showDispatched)
        {
            if (showDispatched)
            {
                return repository.Orders;
            }
            else
            {
                return repository.Orders.Where(o => !o.Dispatched);
            }
        }

        public decimal Total(IEnumerable<OrderLine> orderLines)
        {
            decimal total = 0;
            foreach (OrderLine ol in orderLines)
            {
                total += ol.Shoe.Price * ol.Quantity;
            }
            return total;
        }
    }
}