using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Bobuv.Models.Repository
{
    public class Repository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Shoe> Shoes
        {
            get { return context.Shoes; }
        }

        public IEnumerable<Order> Orders
        {
            get
            {
                return context.Orders
                    .Include(o => o.OrderLines.Select(ol => ol.Shoe));
            }
        }

        public void SaveShoe(Shoe shoe)
        {
            if (shoe.ShoeId == 0)
            {
                shoe = context.Shoes.Add(shoe);
            }
            else
            {
                Shoe dbShoe = context.Shoes.Find(shoe.ShoeId);
                if (dbShoe != null)
                {
                    dbShoe.Name = shoe.Name;
                    dbShoe.Description = shoe.Description;
                    dbShoe.Price = shoe.Price;
                    dbShoe.Category = shoe.Category;
                    dbShoe.Img = shoe.Img;
                }
            }
            context.SaveChanges();
        }

        public void DeleteShoe(Shoe shoe)
        {
            IEnumerable<Order> orders = context.Orders
                .Include(o => o.OrderLines.Select(ol => ol.Shoe))
                .Where(o => o.OrderLines
                    .Count(ol => ol.Shoe.ShoeId == shoe.ShoeId) > 0)
                .ToArray();

            foreach (Order order in orders)
            {
                context.Orders.Remove(order);
            }
            context.Shoes.Remove(shoe);
            context.SaveChanges();
        }

        public void SaveOrder(Order order)
        {
            if (order.OrderId == 0)
            {
                order = context.Orders.Add(order);

                foreach (OrderLine line in order.OrderLines)
                {
                    context.Entry(line.Shoe).State
                        = EntityState.Modified;
                }

            }
            else
            {
                Order dbOrder = context.Orders.Find(order.OrderId);
                if (dbOrder != null)
                {
                    dbOrder.Name = order.Name;
                    dbOrder.Address = order.Address;
                    dbOrder.Phone = order.Phone;
                    dbOrder.Email = order.Email;
                    dbOrder.City = order.City;
                    dbOrder.GiftWrap = order.GiftWrap;
                    dbOrder.Dispatched = order.Dispatched;
                }
            }
            context.SaveChanges();
        }
    }
}