using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Notification_System
{
    static  class ExtensionClassOrder
    {
        public static string FormatMessage(this OrderService order )
        {
            return $"{order.OrderId} , {order.OrderName} : " ;
        }
    }
    class OrderService
    {
       public int OrderId { get; set; }
       public int Amount { get; set; }
      public  string OrderName { get; set; }
        public OrderService(int OrderId , string OrderName,int Amount) 
        {
            this.OrderId = OrderId;
            this.OrderName = OrderName;
            this.Amount = Amount;

        }


        public event Action Notify;

            public void PlaceOrder()
            {
                Console.WriteLine("Order Placed : \n");
                Notify?.Invoke();
            }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            OrderService order= new OrderService(1,"Noon",4);
            order.Notify += EmailService;
            order.Notify += SMSService;
            order.Notify += new Action(() => Console.WriteLine("Wattsapp Message send to Customer"));
            if(FilterAmountOrderAndIDOrder(FilterAmountOrder,order)&& FilterAmountOrderAndIDOrder(FilterIDOrder, order))
            {
                Console.WriteLine(order.FormatMessage());
                order.PlaceOrder();
            }
            else
            {
                 Console.WriteLine("Order is not valid");
            }
        }

        public static void EmailService()
        {
            string message = "Email send to Customer";
            Console.WriteLine(message);
        }

        public static void SMSService()
        {
            string message= "SMS send to Customer";
            Console.WriteLine(message);
        }
        public static bool FilterAmountOrderAndIDOrder(Predicate<OrderService> filterAmount , OrderService order)
        {
            return filterAmount(order);
        }

        public static bool FilterAmountOrder(OrderService order)
        {
            return order.Amount > 0;
        }
        public static bool FilterIDOrder(OrderService order)
        {
            return order.OrderId > 0;
        }
    }
}
