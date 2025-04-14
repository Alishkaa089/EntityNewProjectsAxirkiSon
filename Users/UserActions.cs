using Microsoft.EntityFrameworkCore;

public static class UserActions
{
    public static void PerformActions(StepShopDbContext context, User user)
    {
        var basket = new List<(Product Product, int Quantity)>();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("\nUser operations:");
            Console.WriteLine("1. Show products");
            Console.WriteLine("2. Add product to basket");
            Console.WriteLine("3. View basket");
            Console.WriteLine("4. Remove product from basket");
            Console.WriteLine("5. Complete shopping");
            Console.WriteLine("0. Exit");
            Console.Write("Your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowProducts(context);
                    break;

                case "2":
                    AddProductToBasket(context, basket);
                    break;

                case "3":
                    ViewBasket(basket);
                    break;

                case "4":
                    RemoveProductFromBasket(basket);
                    break;

                case "5":
                    CompleteShopping(context, basket, user);
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Invalid choice! Press any key to try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static void ShowProducts(StepShopDbContext context)
    {
        Console.Clear();
        var products = context.Products.ToList();

        Console.WriteLine("\nProducts:");
        foreach (var product in products)
        {
            Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, Price: {product.Price}, Available Quantity: {product.Quantity}");
        }
        Console.WriteLine("\nPress any key to continue.");
        Console.ReadKey();
    }

    private static void AddProductToBasket(StepShopDbContext context, List<(Product Product, int Quantity)> basket)
    {
        Console.Clear();
        ShowProducts(context);

        Console.Write("\nPlease enter the Id of the product you want to add: ");
        if (int.TryParse(Console.ReadLine(), out int productId))
        {
            var productToAdd = context.Products.Find(productId);
            if (productToAdd != null)
            {
                Console.Write($"Enter the quantity of {productToAdd.Name}: ");
                if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
                {
                    if (quantity > productToAdd.Quantity)
                    {
                        Console.WriteLine($"Error: Only {productToAdd.Quantity} {productToAdd.Name} available.");
                    }
                    else
                    {
                        var existingItem = basket.FirstOrDefault(b => b.Product.Id == productToAdd.Id);
                        if (existingItem.Product != null)
                        {
                            basket.Remove(existingItem);
                            basket.Add((productToAdd, existingItem.Quantity + quantity));
                        }
                        else
                        {
                            basket.Add((productToAdd, quantity));
                        }
                        Console.WriteLine($"{quantity} x {productToAdd.Name} added to basket!");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid quantity!");
                }
            }
            else
            {
                Console.WriteLine("Product not found!");
            }
        }
        else
        {
            Console.WriteLine("Invalid Id format!");
        }
        Console.WriteLine("\nPress any key to continue.");
        Console.ReadKey();
    }

    private static void ViewBasket(List<(Product Product, int Quantity)> basket)
    {
        Console.Clear();
        Console.WriteLine("\nYour basket:");
        decimal totalPrice = 0;

        foreach (var item in basket)
        {
            decimal itemTotal = item.Product.Price * item.Quantity;
            totalPrice += itemTotal;
            Console.WriteLine($"Id: {item.Product.Id}, Name: {item.Product.Name}, Quantity: {item.Quantity}, Total Price: {itemTotal:C}");
        }
        Console.WriteLine($"\nTotal Basket Price: {totalPrice:C}");
        Console.WriteLine("\nPress any key to continue.");
        Console.ReadKey();
    }

    private static void RemoveProductFromBasket(List<(Product Product, int Quantity)> basket)
    {
        Console.Clear();
        Console.Write("Enter the Id of the product you want to remove from the basket: ");
        if (int.TryParse(Console.ReadLine(), out int removeId))
        {
            var productToRemove = basket.FirstOrDefault(b => b.Product.Id == removeId);
            if (productToRemove.Product != null)
            {
                basket.Remove(productToRemove);
                Console.WriteLine("Product removed from basket!");
            }
            else
            {
                Console.WriteLine("Product not found in basket!");
            }
        }
        else
        {
            Console.WriteLine("Invalid Id format!");
        }
        Console.WriteLine("\nPress any key to continue.");
        Console.ReadKey();
    }

    private static void CompleteShopping(StepShopDbContext context, List<(Product Product, int Quantity)> basket, User user)
    {
        Console.Clear();
        if (basket.Count == 0)
        {
            Console.WriteLine("Your basket is empty! Cannot complete shopping.");
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Completing shopping...");

        var invoice = new Invoice
        {
            CustomerId = user.Id,
            CashierId = 2,
            Date = DateTime.Now
        };

        context.Invoices.Add(invoice);

        foreach (var item in basket)
        {
            var product = context.Products.Find(item.Product.Id);
            if (product != null)
            {
                product.Quantity -= item.Quantity;
            }

            var newOrder = new Order
            {
                ProductName = item.Product.Name,
                Price = item.Product.Price * item.Quantity,
                CustomerName = user.Username,
                OrderDate = DateTime.Now,
                Invoice = invoice
            };

            context.Order.Add(newOrder);
        }

        try
        {
            context.SaveChanges();
            Console.WriteLine("Shopping completed! Your basket has been recorded as orders.");
            basket.Clear();
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
            }
        }
        Console.WriteLine("\nPress any key to continue.");
        Console.ReadKey();
    }
}