public static class CashierActions
{
    public static void PerformActions(StepShopDbContext context)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\nCashier operations:");
            Console.WriteLine("1. Show products");
            Console.WriteLine("2. Show orders");
            Console.WriteLine("0. Exit");
            Console.Write("Your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    var products = context.Products.ToList();
                    Console.WriteLine("\nProducts:");
                    foreach (var product in products)
                    {
                        Console.WriteLine($"Barcode: {product.Barcode},Id: {product.Id}, Name: {product.Name},Price: {product.Price},Quantity: {product.Quantity}");
                    }
                    Console.WriteLine("\nPress any key to continue.");
                    Console.ReadKey();
                    break;

                case "2":
                    Console.Clear();

                    var orders = context.Order.ToList();

                    if (orders.Count == 0)
                    {
                        Console.WriteLine("Currently, there are no orders.");
                    }
                    else
                    {
                        Console.WriteLine("\nOrders:");
                        foreach (var order in orders)
                        {
                            Console.WriteLine($"Id: {order.Id}, Product: {order.ProductName}, Price: {order.Price:C}, Customer: {order.CustomerName}, Date: {order.OrderDate}");
                        }
                    }

                    Console.WriteLine("\nPress any key to continue.");
                    Console.ReadKey();
                    break;

                case "0":
                    return;

                default:
                    Console.Clear();
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }
}
