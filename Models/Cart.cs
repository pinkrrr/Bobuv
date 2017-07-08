using System.Collections.Generic;
using System.Linq;

namespace Bobuv.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Shoe shoe, int quantity)
        {
            CartLine line = lineCollection
                .Where(p => p.Shoe.ShoeId == shoe.ShoeId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Shoe = shoe,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Shoe shoe)
        {
            lineCollection.RemoveAll(l => l.Shoe.ShoeId == shoe.ShoeId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Shoe.Price * e.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public Shoe Shoe { get; set; }
        public int Quantity { get; set; }
    }
}