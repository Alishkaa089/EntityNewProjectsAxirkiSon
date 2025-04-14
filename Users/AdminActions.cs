public static class AdminActions
{
    public static void PerformActions(StepShopDbContext context)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\nAdmin operations:");
            Console.WriteLine("1. Show users");
            Console.WriteLine("2. Delete user");
            Console.WriteLine("3. Add product");
            Console.WriteLine("4. Update product");
            Console.WriteLine("5. Delete product");
            Console.WriteLine("6. Show products");
            Console.WriteLine("0. Exit");
            Console.Write("Your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    var users = context.Users.Where(u => !u.IsDeleted).ToList();
                    Console.WriteLine("\nUsers:");
                    foreach (var user in users)
                    {
                        Console.WriteLine($"Id: {user.Id}, Username: {user.Username}, Role: {user.Role}");
                    }
                    Console.WriteLine("\nPress any key to continue.");
                    Console.ReadKey();
                    break;


                case "2":
                    Console.Clear();
                    var userss = context.Users.Where(u => !u.IsDeleted).ToList();
                    Console.WriteLine("\nUsers:");
                    foreach (var user in userss)
                    {
                        Console.WriteLine($"Id: {user.Id}, Username: {user.Username}, Role: {user.Role}");
                    }

                    Console.Write("Enter the Id of the user to delete: ");
                    if (int.TryParse(Console.ReadLine(), out int userId))
                    {
                        var userToDelete = context.Users.Find(userId);
                        if (userToDelete != null && !userToDelete.IsDeleted)
                        {
                            userToDelete.IsDeleted = true;
                            context.SaveChanges();
                            Console.WriteLine($"User '{userToDelete.Username}' marked as deleted!");
                        }
                        else
                        {
                            Console.WriteLine("User not found or already deleted!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Id format!");
                    }
                    Console.WriteLine("\nPress any key to continue.");
                    Console.ReadKey();
                    break;



                case "3":
                    Console.Clear();
                    Console.Write("Enter the product name: ");
                    string productName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(productName))
                    {
                        Console.WriteLine("Product name cannot be empty.");
                        Console.WriteLine("\nPress any key to continue.");
                        Console.ReadKey();
                        break;
                    }

                    Console.Write("Enter the product price: ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal productPrice) || productPrice <= 0)
                    {
                        Console.WriteLine("Invalid price! Product price must be a positive number.");
                        Console.WriteLine("\nPress any key to continue.");
                        Console.ReadKey();
                        break;
                    }

                    Console.Write("Enter the product quantity: ");
                    if (!int.TryParse(Console.ReadLine(), out int productQuantity) || productQuantity <= 0)
                    {
                        Console.WriteLine("Invalid quantity! Quantity must be a positive number.");
                        Console.WriteLine("\nPress any key to continue.");
                        Console.ReadKey();
                        break;
                    }

                    var existingProduct = context.Products.FirstOrDefault(p => p.Name.ToLower() == productName.ToLower());
                    if (existingProduct != null)
                    {
                        existingProduct.Quantity += productQuantity;
                        context.SaveChanges();
                        Console.WriteLine($"Product updated! Name: {existingProduct.Name}, Quantity: {existingProduct.Quantity}");
                    }
                    else
                    {
                        var newProduct = new Product
                        {
                            Name = productName,
                            Price = productPrice,
                            Quantity = productQuantity
                        };
                        newProduct.GenerateBarcode();

                        context.Products.Add(newProduct);
                        context.SaveChanges();

                        Console.WriteLine($"Product added! Barcode: {newProduct.Barcode}, Name: {newProduct.Name}, Price: {newProduct.Price}, Quantity: {newProduct.Quantity}");
                    }

                    Console.WriteLine("\nPress any key to continue.");
                    Console.ReadKey();
                    break;


                case "4":
                    Console.Clear();
                    Console.WriteLine("Available Products:\n");

                    var productss = context.Products.ToList();
                    foreach (var product in productss)
                    {
                        Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, Price: {product.Price:C}, Available Quantity: {product.Quantity}");
                    }
                    Console.Write("Enter the Id of the product to update: ");
                    if (int.TryParse(Console.ReadLine(), out int productId))
                    {
                        var productToUpdate = context.Products.Find(productId);
                        if (productToUpdate != null)
                        {
                            Console.Write($"Enter a new name for {productToUpdate.Name} (leave empty to keep current): ");
                            string newName = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(newName))
                            {
                                productToUpdate.Name = newName;
                            }

                            Console.Write($"Enter a new price for {productToUpdate.Name} (current price: {productToUpdate.Price}): ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal newPrice) && newPrice > 0)
                            {
                                productToUpdate.Price = newPrice;
                            }

                            Console.Write($"Enter a new quantity for {productToUpdate.Name} (current quantity: {productToUpdate.Quantity}): ");
                            if (int.TryParse(Console.ReadLine(), out int newQuantity) && newQuantity >= 0)
                            {
                                productToUpdate.Quantity = newQuantity;
                            }

                            context.SaveChanges();
                            Console.WriteLine($"Product updated! Name: {productToUpdate.Name}, Price: {productToUpdate.Price}, Quantity: {productToUpdate.Quantity}");
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
                    break;

                case "5":
                    Console.Clear();
                    Console.WriteLine("Available Products:\n");

                    var productsss = context.Products.ToList();
                    foreach (var product in productsss)
                    {
                        Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, Price: {product.Price:C}, Available Quantity: {product.Quantity}");
                    }
                    Console.Write("Enter the Id of the product to delete: ");
                    if (int.TryParse(Console.ReadLine(), out int deleteId))
                    {
                        var productToDelete = context.Products.Find(deleteId);
                        if (productToDelete != null)
                        {
                            context.Products.Remove(productToDelete);
                            context.SaveChanges();
                            Console.WriteLine("Product deleted!");
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
                    break;

                case "6":
                    Console.Clear();
                    var products = context.Products.ToList();
                    Console.WriteLine("\nProducts:");
                    foreach (var product in products)
                    {
                        Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, Price: {product.Price:C}, Quantity: {product.Quantity}");
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
