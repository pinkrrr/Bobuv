using System;
using System.Collections.Generic;
using System.Linq;
using Bobuv.Models;
using Bobuv.Models.Repository;
using Bobuv.Pages.Helpers;
using System.Web.Routing;

namespace Bobuv.Pages
{
    public partial class CartView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Repository repository = new Repository();
                int shoeId;
                if (int.TryParse(Request.Form["remove"], out shoeId))
                {
                    Shoe shoeToRemove = repository.Shoes
                        .Where(g => g.ShoeId == shoeId).FirstOrDefault();
                    if (shoeToRemove != null)
                    {
                        SessionHelper.GetCart(Session).RemoveLine(shoeToRemove);
                    }
                }
            }
        }

        public IEnumerable<CartLine> GetCartLines()
        {
            return SessionHelper.GetCart(Session).Lines;
        }

        public decimal CartTotal
        {
            get
            {
                return SessionHelper.GetCart(Session).ComputeTotalValue();
            }
        }

        public string ReturnUrl
        {
            get
            {
                return SessionHelper.Get<string>(Session, SessionKey.RETURN_URL);
            }
        }

        public string CheckoutUrl
        {
            get
            {
                return RouteTable.Routes.GetVirtualPath(null, "checkout",
                    null).VirtualPath;
            }
        }
    }
}