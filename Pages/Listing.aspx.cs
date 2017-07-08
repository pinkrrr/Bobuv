using System;
using System.Collections.Generic;
using Bobuv.Models;
using Bobuv.Models.Repository;
using System.Linq;
using Bobuv.Pages.Helpers;
using System.Web.Routing;

namespace Bobuv.Pages
{
    public partial class Listing : System.Web.UI.Page
    {
        private Repository repository = new Repository();
        private int pageSize = 4;

        protected int CurrentPage
        {
            get {
                int page;
                page = GetPageFromRequest();
                return page > MaxPage ? MaxPage : page;
            }
        }

        protected int MaxPage
        {
            get {
                int prodCount = FilterShoes().Count();
                return (int)Math.Ceiling((decimal)prodCount / pageSize);
            }
        }

        private int GetPageFromRequest()
        {
            int page;
            string reqValue = (string)RouteData.Values["page"] ??
                Request.QueryString["page"];
            return reqValue != null && int.TryParse(reqValue, out page) ? page : 1;
        }

        public IEnumerable<Shoe> GetShoes()
        {
            return FilterShoes()
                .OrderBy(g => g.ShoeId)
                .Skip((CurrentPage - 1) * pageSize)
                .Take(pageSize);
        }

        private IEnumerable<Shoe> FilterShoes()
        {
            IEnumerable<Shoe> shoes = repository.Shoes;
            string currentCategory = (string)RouteData.Values["category"] ??
                Request.QueryString["category"];
            return currentCategory == null ? shoes :
                shoes.Where(p => p.Category == currentCategory);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                int selectedShoeId;
                if (int.TryParse(Request.Form["add"], out selectedShoeId))
                {
                    Shoe selectedShoe = repository.Shoes
                        .Where(g => g.ShoeId == selectedShoeId).FirstOrDefault();

                    if (selectedShoe != null)
                    {
                        SessionHelper.GetCart(Session).AddItem(selectedShoe, 1);
                        SessionHelper.Set(Session, SessionKey.RETURN_URL,
                            Request.RawUrl);

                        Response.Redirect(RouteTable.Routes
                            .GetVirtualPath(null, "cart", null).VirtualPath);
                    }
                }
            }
        }
    }
}