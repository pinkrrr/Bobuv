using System;
using System.Web.Routing;

namespace Bobuv.Pages.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string OrdersUrl
        {
            get
            {
                return generateURL("admin_orders");
            }
        }

        public string TovaryUrl
        {
            get
            {
                return generateURL("admin_tovary");
            }
        }

        public string Redirect
        {
            get
            {
                return generateURL("redirect");
            }
        }

        private string generateURL(string routeName)
        {
            return RouteTable.Routes.GetVirtualPath(null, routeName, null).VirtualPath;
        }
    }
}