using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Bobuv.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage="Пожалуйста введите свое имя")]
        public string Name { get; set; }

        [Required(ErrorMessage="Вы должны указать хотя бы один адрес доставки")]
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage="Пожалуйста укажите город, куда нужно доставить заказ")]
        public string City { get; set; }
        public bool GiftWrap { get; set; }
        public bool Dispatched { get; set; }
        public virtual List<OrderLine> OrderLines { get; set; }
    }

    public class OrderLine
    {
        public int OrderLineId { get; set; }
        public Order Order { get; set; }
        public Shoe Shoe { get; set; }
        public int Quantity { get; set; }
    }
}