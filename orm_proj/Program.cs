using orm_proj.Enums;
using orm_proj.Repositories.Implementations;
using orm_proj.Services;
using orm_proj.Services.Implementations;
using orm_proj.Services.Interfaces;

public class Program
{
    private static IProductService _productService;
    private static IUserService _userService;
    private static IOrderService _orderService;
    private static IPaymentService _paymentService;
    private static UserGetDto _currentUser;
    public static UserGetDto CurrentUser => _currentUser;
    public static async Task Main(string[] args)
    {
        InitializeServices();

        //// Simulate user login for testing
        //Console.WriteLine("Testing login:");
        //await LoginAsync();

        while (true)
        {
            try
            {
                Console.WriteLine($"Current user: {CurrentUser?.UserName ?? "None"}, IsAdmin: {CurrentUser?.IsAdmin ?? false}");

                if (CurrentUser == null)
                {
                    await FirstMenuAsync();
                }
                else if (CurrentUser.IsAdmin)
                {
                    await AdminMenuAsync();
                }
                else if (!CurrentUser.IsAdmin)
                {
                    await UserMenuAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }


    private static void InitializeServices()
    {
        var dbContext = new AppDBContext();

        var productRepository = new ProductRepository(dbContext);
        var userRepository = new UserRepository(dbContext);
        var orderRepository = new OrderRepository(dbContext);
        var paymentRepository = new PaymentRepository(dbContext);

        _productService = new ProductService(productRepository);
        _userService = new UserService(userRepository, orderRepository);
        _orderService = new OrderService(orderRepository, userRepository);
        _paymentService = new PaymentService(paymentRepository, orderRepository);
    }
    private static async Task FirstMenuAsync()
    {
        while (true)
        {
            Console.WriteLine("| Menu |");
            Console.WriteLine("1. Admin Registration");
            Console.WriteLine("2. User Registration");
            Console.WriteLine("3. Login");
            Console.WriteLine("4. Exit");
            Console.Write("Select an option: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await RegisterAdminAsync();
                    break;
                case "2":
                    await RegisterUserAsync();
                    break;
                case "3":
                    await LoginAsync();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
    private static async Task AdminMenuAsync()
    {
        while (true)
        {
            Console.WriteLine("| Admin Menu |");
            Console.WriteLine("1. Manage Products");
            Console.WriteLine("2. Manage Users");
            Console.WriteLine("3. Manage Orders");
            Console.WriteLine("4. Manage Payments");
            Console.WriteLine("5. Log out");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await ManageProductsAsync();
                    break;
                case "2":
                    await ManageUsersAsync();
                    break;
                case "3":
                    await ManageOrdersAsync();
                    break;
                case "4":
                    await ManagePaymentsAsync();
                    break;
                case "5":
                    _currentUser = null;
                    Console.WriteLine("Logged out successfully.");
                    return;
                    break;
                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
    private static async Task UserMenuAsync()
    {
        while (true)
        {
            Console.WriteLine("| User Menu |");
            Console.WriteLine("1. View Products");
            Console.WriteLine("2. Search Products");
            Console.WriteLine("3. Orders Menu");
            Console.WriteLine("4. Payments Menu");
            Console.WriteLine("5. Update my information");
            Console.WriteLine("6. Logout");
            Console.Write("Select an option: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await ViewAllProductsAsync();
                    break;
                case "2":
                    await SearchProductsAsync();
                    break;
                case "3":
                    await ManageOrdersAsync();
                    break;
                case "4":
                    await ManagePaymentsAsync();
                    break;
                case "5":
                    await UpdateUserInformationAsync();
                    break;
                case "6":
                    _currentUser = null;
                    Console.WriteLine("Logged out successfully.");
                    return;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private static async Task ManageProductsAsync()
    {
        Console.WriteLine("Product Management");
        Console.WriteLine("1. Add Product");
        Console.WriteLine("2. Update Product");
        Console.WriteLine("3. Delete Product");
        Console.WriteLine("4. View All Products");
        Console.WriteLine("5. Search Products");
        Console.WriteLine("6. Back to Main Menu");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                await AddProductAsync();
                break;
            case "2":
                await UpdateProductAsync();
                break;
            case "3":
                await DeleteProductAsync();
                break;
            case "4":
                await ViewAllProductsAsync();
                break;
            case "5":
                await SearchProductsAsync();
                break;
            case "6":
                break;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }
    }

    private static async Task ManageUsersAsync()
    {
        Console.WriteLine("User Management");
        Console.WriteLine("1. Register User");
        Console.WriteLine("2. Register Admin");
        Console.WriteLine("3. Update User Information");
        Console.WriteLine("4. View User Orders");
        Console.WriteLine("5. Back to Main Menu");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                await RegisterUserAsync();
                break;
            case "2":
                await RegisterAdminAsync();
                break;
            case "3":
                await UpdateUserInformationAsync();
                break;
            case "4":
                await ViewUserOrdersAsync();
                break;
            case "5":
                break;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }
    }

    private static async Task ManageOrdersAsync()
    {
        Console.WriteLine("Order Management");
        Console.WriteLine("1. Create Order");
        Console.WriteLine("2. Cancel Order");
        Console.WriteLine("3. Complete Order");
        Console.WriteLine("4. View All Orders");
        Console.WriteLine("5. Back to Main Menu");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                await CreateOrderAsync();
                break;
            case "2":
                await CancelOrderAsync();
                break;
            case "3":
                await CompleteOrderAsync();
                break;
            case "4":
                await ViewOrdersAsync();
                break;
            case "5":
                break;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }
    }

    private static async Task ManagePaymentsAsync()
    {
        Console.WriteLine("Payment Management");
        Console.WriteLine("1. Make Payment");
        Console.WriteLine("2. View All Payments");
        Console.WriteLine("3. Back to Main Menu");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                await MakePaymentAsync();
                break;
            case "2":

                await ViewPaymentsAsync();
                break;
            case "3":
                return;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }
    }

    private static async Task AddProductAsync()
    {

        Console.WriteLine("| Create product |");

        string name;
        string description;
        decimal price;
        int stock;

        try
        {
            while (true)
            {
                Console.Write("Enter name: ");
                name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name))
                    break;
                Console.WriteLine("Name cannot be empty. Please try again.");
            }

            while (true)
            {
                Console.Write("Enter description: ");
                description = Console.ReadLine();
                if (!string.IsNullOrEmpty(description))
                    break;
                Console.WriteLine("Description cannot be empty. Please try again.");
            }

            while (true)
            {
                Console.Write("Enter price: ");
                price = decimal.Parse(Console.ReadLine());
                if (price != null)
                    break;
                Console.WriteLine("Price cannot be empty. Please try again.");
            }

            while (true)
            {
                Console.Write("Enter stock amount: ");
                stock = int.Parse(Console.ReadLine());
                if (stock != null)
                    break;
                Console.WriteLine("Stock cannot be empty. Please try again.");
            }

            var productPost = new ProductPostDto
            {
                Name = name,
                Description = description,
                Price = price,
                Stock = stock,
            };

            await _productService.AddProductAsync(productPost);

            Console.WriteLine($"Product '{name}' created successfully.");
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static async Task UpdateProductAsync()
    {
        Console.WriteLine("| Update Product |");

        try
        {
            Console.WriteLine("Enter product ID:");
            int productId = int.Parse(Console.ReadLine());

            var product = await _productService.GetProductById(productId);

            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            var newProduct = new ProductPutDto
            {
                Id = productId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock
            };

            Console.WriteLine($"Current Name: {product.Name}");
            Console.WriteLine("Enter new Name (leave empty to keep current):");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                newProduct.Name = newName;
            }

            Console.WriteLine($"Current Price: {product.Price}");
            Console.WriteLine("Enter new Price (leave empty to keep current):");
            string priceInput = Console.ReadLine();
            if (decimal.TryParse(priceInput, out decimal newPrice))
            {
                newProduct.Price = newPrice;
            }

            Console.WriteLine($"Current Stock: {product.Stock}");
            Console.WriteLine("Enter new Stock (leave empty to keep current):");
            string stockInput = Console.ReadLine();
            if (int.TryParse(stockInput, out int newStock))
            {
                newProduct.Stock = newStock;
            }

            Console.WriteLine($"Current Description: {product.Description}");
            Console.WriteLine("Enter new Description (leave empty to keep current):");
            string newDescription = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newDescription))
            {
                newProduct.Description = newDescription;
            }

            await _productService.UpdateProductAsync(newProduct);

            Console.WriteLine("Product updated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }


    private static async Task DeleteProductAsync()
    {
        Console.WriteLine("Select product from list and enter product ID:");
        await ViewAllProductsAsync();
        var id = int.Parse(Console.ReadLine());
        await _productService.DeleteProductAsync(id);
        Console.WriteLine("Product deleted succesfully");
    }

    private static async Task ViewAllProductsAsync()
    {
        var products = await _productService.GetAllProducts();

        foreach (var product in products)
        {
            Console.WriteLine(product);
        }
    }

    private static async Task SearchProductsAsync()
    {
        Console.WriteLine("Enter product name (or substring of name):");
        var name = Console.ReadLine();
        var products = await _productService.SearchProducts(name);
        foreach (var product in products)
        {
            Console.WriteLine(product);
        }
    }

    private static async Task RegisterAdminAsync()
    {
        Console.WriteLine("| Admin Registration |");

        string username = string.Empty;
        string email = string.Empty;
        string password = string.Empty;
        string address = string.Empty;

        try
        {
            while (true)
            {
                Console.Write("Enter username: ");
                username = Console.ReadLine();
                if (!string.IsNullOrEmpty(username))
                    break;
                Console.WriteLine("Full name cannot be empty. Please try again.");
            }

            while (true)
            {
                Console.Write("Enter email: ");
                email = Console.ReadLine();
                if (!string.IsNullOrEmpty(email))
                    break;
                Console.WriteLine("Email cannot be empty. Please try again.");
            }

            while (true)
            {
                Console.Write("Enter password: ");
                password = Console.ReadLine();
                if (!string.IsNullOrEmpty(password))
                    break;
                Console.WriteLine("Password cannot be empty. Please try again.");
            }

            while (true)
            {
                Console.Write("Enter address: ");
                address = Console.ReadLine();
                if (!string.IsNullOrEmpty(address))
                    break;
                Console.WriteLine("Address cannot be empty. Please try again.");
            }

            var userPost = new UserPostDto
            {
                UserName = username,
                Email = email,
                Password = password,
                Address = address,
                IsAdmin = true
            };

            await _userService.RegisterAdminAsync(userPost);

            Console.WriteLine($"User {username} created successfully.");
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static async Task RegisterUserAsync()
    {
        Console.WriteLine("| User Registration |");

        string username = string.Empty;
        string email = string.Empty;
        string password = string.Empty;
        string address = string.Empty;

        try
        {
            while (true)
            {
                Console.Write("Enter full name: ");
                username = Console.ReadLine();
                if (!string.IsNullOrEmpty(username))
                    break;
                Console.WriteLine("Full name cannot be empty. Please try again.");
            }

            while (true)
            {
                Console.Write("Enter email: ");
                email = Console.ReadLine();
                if (!string.IsNullOrEmpty(email))
                    break;
                Console.WriteLine("Email cannot be empty. Please try again.");
            }

            while (true)
            {
                Console.Write("Enter password: ");
                password = Console.ReadLine();
                if (!string.IsNullOrEmpty(password))
                    break;
                Console.WriteLine("Password cannot be empty. Please try again.");
            }

            while (true)
            {
                Console.Write("Enter address: ");
                address = Console.ReadLine();
                if (!string.IsNullOrEmpty(address))
                    break;
                Console.WriteLine("Address cannot be empty. Please try again.");
            }

            var userPost = new UserPostDto
            {
                UserName = username,
                Email = email,
                Password = password,
                Address = address,
                IsAdmin = false
            };

            await _userService.RegisterUserAsync(userPost);

            Console.WriteLine($"User {username} created successfully.");
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static async Task LoginAsync()
    {
        Console.WriteLine("Enter email:");
        string email = Console.ReadLine();

        Console.WriteLine("Enter password:");
        string password = Console.ReadLine();

        try
        {
            var user = await _userService.LoginUserAsync(email, password);

            if (user != null)
            {
                _currentUser = user;
                Console.WriteLine($"User {user.UserName} logged in successfully.");
            }
        }
        catch (UserAuthenticationException ex)
        {
            Console.WriteLine($"Login failed: {ex.Message}");
        }
    }

    private static async Task UpdateUserInformationAsync()
    {
        Console.WriteLine("| Update User Information |");

        try
        {
            int userId = CurrentUser.IsAdmin ? PromptForUserId() : CurrentUser.Id;

            Console.Write("Enter new username (leave blank to keep current): ");
            string newUserName = Console.ReadLine();

            Console.Write("Enter new address (leave blank to keep current): ");
            string newAddress = Console.ReadLine();

            Console.Write("Do you want to update the password? (y/n): ");
            string updatePassword = Console.ReadLine();

            string newPassword = null;
            if (updatePassword.ToLower() == "y")
            {
                Console.Write("Enter new password: ");
                newPassword = Console.ReadLine();
            }

            var userDto = new UserPutDto
            {
                UserName = newUserName,
                Address = newAddress,
                Password = newPassword
            };

            await _userService.UpdateUserInfoAsync(userId, userDto);

            Console.WriteLine("User information updated successfully.");
        }
        catch (NotFoundException)
        {
            Console.WriteLine("User not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static async Task ViewUserOrdersAsync()
    {
        Console.WriteLine("| View Orders |");

        try
        {
            List<OrderGetDto> orders;

            if (CurrentUser.IsAdmin)
            {
                Console.WriteLine("Admin view: orders for all users.");
                orders = await _orderService.GetAllOrders();
            }
            else
            {
                Console.WriteLine("User view: orders for the current user.");
                orders = await _orderService.GetUserOrdersAsync(CurrentUser.Id);
            }

            if (orders.Count == 0)
            {
                Console.WriteLine("No orders found.");
                return;
            }

            foreach (var order in orders)
            {
                Console.WriteLine(order);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static async Task CreateOrderAsync()
    {
        Console.WriteLine("| Create Order |");

        try
        {
            if (CurrentUser == null)
            {
                Console.WriteLine("You need to log in first.");
                return;
            }

            int userId = CurrentUser.IsAdmin ? PromptForUserId() : CurrentUser.Id;

            if (userId == 0)
            {
                Console.WriteLine("Invalid user ID.");
                return;
            }

            var orderItems = new List<OrderDetail>();
            decimal totalAmount = 0;

            while (true)
            {
                Console.WriteLine("Enter product ID (or type 'done' to finish):");
                var input = Console.ReadLine();

                if (input?.ToLower() == "done")
                    break;

                if (int.TryParse(input.Trim(), out var productId))
                {
                    Console.WriteLine("Enter quantity for product ID {0}:", productId);
                    if (int.TryParse(Console.ReadLine(), out var quantity) && quantity > 0)
                    {
                        decimal pricePerUnit = await _productService.GetProductPrice(productId);

                        if (pricePerUnit <= 0)
                        {
                            Console.WriteLine("Invalid product price.");
                            continue;
                        }

                        var orderDetail = new OrderDetail
                        {
                            ProductId = productId,
                            Quantity = quantity,
                            PricePerItem = pricePerUnit
                        };

                        orderItems.Add(orderDetail);
                        totalAmount += pricePerUnit * quantity;
                    }
                    else
                    {
                        Console.WriteLine("Invalid quantity. Please enter a positive number.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid product ID. Please enter a valid number.");
                }
            }

            if (!orderItems.Any())
            {
                Console.WriteLine("No products specified.");
                return;
            }

            var orderDto = new OrderPostDto
            {
                UserId = userId,
                TotalAmount = totalAmount,
                Details = orderItems
            };

            await _orderService.CreateOrderAsync(orderDto);

            Console.WriteLine("Order created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static int PromptForUserId()
    {
        Console.Write("Enter the ID of the user to create the order for: ");
        return int.TryParse(Console.ReadLine(), out var userId) ? userId : 0;
    }

    private static async Task CancelOrderAsync()
    {
        Console.WriteLine("| Cancel Order |");

        try
        {
            if (CurrentUser == null)
            {
                Console.WriteLine("You need to log in first.");
                return;
            }

            int userId = CurrentUser.IsAdmin ? PromptForUserId() : CurrentUser.Id;

            if (userId == 0)
            {
                Console.WriteLine("Invalid user ID.");
                return;
            }

            List<OrderGetDto> orders;

            if (CurrentUser.IsAdmin)
            {
                orders = await _orderService.GetUserOrdersAsync(userId); // Get all orders for the specified user
            }
            else
            {
                orders = await _orderService.GetUserOrdersAsync(CurrentUser.Id); // Get only the current user's orders
            }

            if (!orders.Any())
            {
                Console.WriteLine("No orders found for the specified user.");
                return;
            }

            Console.WriteLine("Orders:");
            foreach (var order in orders)
            {
                Console.WriteLine(order);
            }

            Console.Write("Enter the ID of the order to cancel: ");
            if (!int.TryParse(Console.ReadLine(), out int orderId))
            {
                Console.WriteLine("Invalid order ID.");
                return;
            }

            var orderToCancel = orders.FirstOrDefault(o => o.Id == orderId);

            if (orderToCancel == null)
            {
                Console.WriteLine("Order not found.");
                return;
            }

            if (orderToCancel.Status != OrderStatus.Pending)
            {
                Console.WriteLine("Only pending orders can be cancelled.");
                return;
            }

            await _orderService.CancelOrderAsync(orderId);

            Console.WriteLine("Order cancelled successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static async Task CompleteOrderAsync()
    {
        Console.WriteLine("| Complete Order |");

        try
        {
            if (CurrentUser == null)
            {
                Console.WriteLine("You need to log in first.");
                return;
            }

            int userId = CurrentUser.IsAdmin ? PromptForUserId() : CurrentUser.Id;

            if (userId == 0)
            {
                Console.WriteLine("Invalid user ID.");
                return;
            }

            List<OrderGetDto> orders;

            if (CurrentUser.IsAdmin)
            {
                orders = await _orderService.GetUserOrdersAsync(userId); // Get all orders for the specified user
            }
            else
            {
                orders = await _orderService.GetUserOrdersAsync(CurrentUser.Id); // Get only the current user's orders
            }

            if (!orders.Any())
            {
                Console.WriteLine("No orders found for the specified user.");
                return;
            }

            Console.WriteLine("Orders:");
            foreach (var order in orders)
            {
                Console.WriteLine($"ID: {order.Id}, Date: {order.OrderDate}, Status: {order.Status}, Total Amount: {order.TotalAmount}");
            }

            Console.Write("Enter the ID of the order to complete: ");
            if (!int.TryParse(Console.ReadLine(), out int orderId))
            {
                Console.WriteLine("Invalid order ID.");
                return;
            }

            var orderToComplete = orders.FirstOrDefault(o => o.Id == orderId);

            if (orderToComplete == null)
            {
                Console.WriteLine("Order not found.");
                return;
            }

            if (orderToComplete.Status != OrderStatus.Pending)
            {
                Console.WriteLine("Only pending orders can be completed.");
                return;
            }

            await _orderService.CompleteOrderAsync(orderId);

            Console.WriteLine("Order completed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }


    private static async Task ViewOrdersAsync()
    {
        Console.WriteLine("| View Orders |");

        try
        {
            if (CurrentUser == null)
            {
                Console.WriteLine("You need to log in first.");
                return;
            }

            int userId = CurrentUser.IsAdmin ? 0 : CurrentUser.Id;

            var orders = await _orderService.GetUserOrdersAsync(userId);

            if (!orders.Any())
            {
                Console.WriteLine("No orders found.");
                return;
            }

            foreach (var order in orders)
            {
                Console.WriteLine(order);

                foreach (var detail in order.Details)
                {
                    Console.WriteLine(detail);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    private static async Task MakePaymentAsync()
    {
        Console.WriteLine("| Make Payment |");

        try
        {
            if (CurrentUser == null)
            {
                Console.WriteLine("You need to log in first.");
                return;
            }

            int userId = CurrentUser.IsAdmin ? PromptForUserId() : CurrentUser.Id;

            if (userId == 0)
            {
                Console.WriteLine("Invalid user ID.");
                return;
            }

            var orders = await _orderService.GetUserOrdersAsync(userId);

            if (!orders.Any())
            {
                Console.WriteLine("No pending orders found.");
                return;
            }

            Console.WriteLine("Select the order ID to make payment for:");
            foreach (var order in orders)
            {
                Console.WriteLine(order);
            }

            if (!int.TryParse(Console.ReadLine(), out int orderId) || !orders.Any(o => o.Id == orderId))
            {
                Console.WriteLine("Invalid order ID.");
                return;
            }

            Console.WriteLine("Enter payment amount:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal paymentAmount) || paymentAmount <= 0)
            {
                Console.WriteLine("Invalid payment amount.");
                return;
            }

            var selectedOrder = orders.FirstOrDefault(o => o.Id == orderId);

            if (selectedOrder == null)
            {
                Console.WriteLine("Order not found.");
                return;
            }

            if (paymentAmount < selectedOrder.TotalAmount)
            {
                Console.WriteLine("Payment amount is less than the total amount. Please pay the full amount.");
                return;
            }

            var paymentDto = new PaymentPostDto
            {
                OrderId = selectedOrder.Id,
                Amount = paymentAmount,
                PaymentDate = DateTime.UtcNow
            };

            await _paymentService.MakePaymentAsync(paymentDto);

            Console.WriteLine("Payment successful. Your order has been completed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static async Task ViewPaymentsAsync()
    {
        Console.WriteLine("| View Payments |");

        try
        {
            if (CurrentUser == null)
            {
                Console.WriteLine("You need to log in first.");
                return;
            }

            int userId = CurrentUser.IsAdmin ? PromptForUserId() : CurrentUser.Id;

            var payments = await _paymentService.GetPaymentByUserId(userId);

            if (!payments.Any())
            {
                Console.WriteLine("No payments found.");
                return;
            }

            foreach (var payment in payments)
            {
                Console.WriteLine(payment);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }


}