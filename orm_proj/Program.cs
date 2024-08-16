using orm_proj.Enums;
using orm_proj.Helpers;
using orm_proj.Repositories.Implementations;
using orm_proj.Services;
using orm_proj.Services.Implementations;
using orm_proj.Services.Interfaces;
using ClosedXML.Excel;
using System.Net.Http.Headers;

public class Program
{
    private static IProductService _productService;
    private static IUserService _userService;
    private static IOrderService _orderService;
    private static IPaymentService _paymentService;
    private static IOrderDetailService _orderDetailService;
    private static UserGetDto _currentUser;
    public static UserGetDto CurrentUser => _currentUser;  //public static UserGetDto CurrentUser = null;
    public static async Task Main(string[] args)
    {
        InitializeServices();
        ExcelExports();
        while (true)
        {
            try
            {
                TextColor.WriteLine($"Current user: {CurrentUser?.UserName ?? "None"}, IsAdmin: {CurrentUser?.IsAdmin ?? false}", ConsoleColor.DarkYellow);
                TextColor.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~", ConsoleColor.Blue);

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
                TextColor.WriteLine($"An unexpected error occurred: {ex.Message}", ConsoleColor.Red);
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
        var orderDetailRepository = new OrderDetailRepository(dbContext);

        _productService = new ProductService(productRepository);
        _userService = new UserService(userRepository, orderRepository);
        _orderService = new OrderService(orderRepository, userRepository, productRepository);
        _paymentService = new PaymentService(paymentRepository, orderRepository);
        _orderDetailService = new OrderDetailService(orderDetailRepository, productRepository, orderRepository);  
    }
    private static async Task FirstMenuAsync()
    {
        while (true)
        {
            TextColor.WriteLine("| Menu |", ConsoleColor.DarkMagenta);
            TextColor.WriteLine("1. Admin Registration", ConsoleColor.Blue);
            TextColor.WriteLine("2. User Registration", ConsoleColor.Blue);
            TextColor.WriteLine("3. Login", ConsoleColor.Blue);
            TextColor.WriteLine("4. Exit", ConsoleColor.Blue);
            TextColor.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~", ConsoleColor.Blue);
            TextColor.WriteLine("Select an option: ", ConsoleColor.DarkYellow);

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
                    return;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    TextColor.WriteLine("Invalid option. Please try again.", ConsoleColor.Red);
                    break;
            }
        }
    }
    private static async Task AdminMenuAsync()
    {
        while (true)
        {
            TextColor.WriteLine("| Admin Menu |", ConsoleColor.DarkMagenta);
            TextColor.WriteLine("1. Manage Products", ConsoleColor.Blue);
            TextColor.WriteLine("2. Manage Users", ConsoleColor.Blue);
            TextColor.WriteLine("3. Manage Orders", ConsoleColor.Blue);
            TextColor.WriteLine("4. View Payments", ConsoleColor.Blue);
            TextColor.WriteLine("5. Log out", ConsoleColor.Blue);
            TextColor.WriteLine("6. Exit", ConsoleColor.Blue);
            TextColor.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~", ConsoleColor.Blue);
            TextColor.WriteLine("Select an option: ", ConsoleColor.DarkYellow);

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
                    await ViewPaymentsAsync();
                    break;
                case "5":
                    _currentUser = null;
                    TextColor.WriteLine("Logged out successfully.", ConsoleColor.Green);
                    return;
                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    TextColor.WriteLine("Invalid option. Please try again.", ConsoleColor.Red);
                    break;
            }
        }
    }
    private static async Task UserMenuAsync()
    {
        while (true)
        {
            TextColor.WriteLine("| User Menu |", ConsoleColor.DarkMagenta);
            TextColor.WriteLine("1. View All Products", ConsoleColor.Blue);
            TextColor.WriteLine("2. Search From Products", ConsoleColor.Blue);
            TextColor.WriteLine("3. Manage Orders", ConsoleColor.Blue);
            TextColor.WriteLine("4. View All Payments", ConsoleColor.Blue);
            TextColor.WriteLine("5. Update My Information", ConsoleColor.Blue);
            TextColor.WriteLine("6. Logout", ConsoleColor.Blue);
            TextColor.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~", ConsoleColor.Blue);
            TextColor.WriteLine("Select an option: ", ConsoleColor.DarkYellow);

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
                    await ViewPaymentsAsync();
                    break;
                case "5":
                    await UpdateUserInformationAsync();
                    break;
                case "6":
                    _currentUser = null;
                    TextColor.WriteLine("Logged out successfully.", ConsoleColor.Green);
                    return;
                default:
                    TextColor.WriteLine("Invalid option. Please try again.", ConsoleColor.Red);
                    break;
            }
        }
    }

    private static async Task ManageProductsAsync()
    {
        TextColor.WriteLine("Product Management", ConsoleColor.DarkMagenta);
        TextColor.WriteLine("1. Add Product", ConsoleColor.Blue);
        TextColor.WriteLine("2. Update Product", ConsoleColor.Blue);
        TextColor.WriteLine("3. Delete Product", ConsoleColor.Blue);
        TextColor.WriteLine("4. View All Products", ConsoleColor.Blue);
        TextColor.WriteLine("5. Search Products", ConsoleColor.Blue);
        TextColor.WriteLine("6. Back to Main Menu", ConsoleColor.Blue);
        TextColor.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~", ConsoleColor.Blue);
        TextColor.WriteLine("Select an option: ", ConsoleColor.DarkYellow);

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
                TextColor.WriteLine("Invalid option. Please try again.", ConsoleColor.Red);
                break;
        }
    }

    private static async Task ManageUsersAsync()
    {
        TextColor.WriteLine("User Management", ConsoleColor.DarkMagenta);
        TextColor.WriteLine("1. Register User", ConsoleColor.Blue);
        TextColor.WriteLine("2. Register Admin", ConsoleColor.Blue);
        TextColor.WriteLine("3. Update User Information", ConsoleColor.Blue);
        TextColor.WriteLine("4. View User Orders", ConsoleColor.Blue);
        TextColor.WriteLine("5. Back to Main Menu", ConsoleColor.Blue);
        TextColor.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~", ConsoleColor.Blue);
        TextColor.WriteLine("Select an option: ", ConsoleColor.DarkYellow);

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
                TextColor.WriteLine("Invalid option. Please try again.", ConsoleColor.Red);
                break;
        }
    }

    private static async Task ManageOrdersAsync()
    {
        TextColor.WriteLine("Order Management", ConsoleColor.DarkMagenta);
        TextColor.WriteLine("1. Create Order", ConsoleColor.Blue);
        TextColor.WriteLine("2. Cancel Order", ConsoleColor.Blue);
        TextColor.WriteLine("3. Complete Order", ConsoleColor.Blue);
        TextColor.WriteLine("4. View Orders", ConsoleColor.Blue);
        TextColor.WriteLine("5. Back to Main Menu", ConsoleColor.Blue);
        TextColor.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~", ConsoleColor.Blue);
        TextColor.WriteLine("Select an option: ", ConsoleColor.DarkYellow);

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
                await CompleteOrderAndMakePaymentAsync();
                break;
            case "4":
                await ViewUserOrdersAsync();
                break;
            case "5":
                break;
            default:
                TextColor.WriteLine("Invalid option. Please try again.", ConsoleColor.Red);
                break;
        }
    }

    private static async Task AddProductAsync()
    {

        TextColor.WriteLine("| Create product |", ConsoleColor.DarkMagenta);

        string name;
        string description;
        decimal price;
        int stock;

        try
        {
            while (true)
            {
                TextColor.WriteLine("Enter name: ", ConsoleColor.Blue);
                name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name))
                    break;
                TextColor.WriteLine("Name cannot be empty. Please try again.", ConsoleColor.Red);
            }

            while (true)
            {
                TextColor.WriteLine("Enter description: ", ConsoleColor.Blue);
                description = Console.ReadLine();
                if (!string.IsNullOrEmpty(description))
                    break;
                TextColor.WriteLine("Description cannot be empty. Please try again.", ConsoleColor.Red);
            }

            while (true)
            {
                TextColor.WriteLine("Enter price: ", ConsoleColor.Blue);
                price = decimal.Parse(Console.ReadLine());
                if (price != null)
                    break;
                TextColor.WriteLine("Price cannot be empty. Please try again.", ConsoleColor.Red);
            }

            while (true)
            {
                TextColor.WriteLine("Enter stock amount: ", ConsoleColor.Blue);
                stock = int.Parse(Console.ReadLine());
                if (stock != null)
                    break;
                TextColor.WriteLine("Stock cannot be empty. Please try again.", ConsoleColor.Red);
            }

            var productPost = new ProductPostDto
            {
                Name = name,
                Description = description,
                Price = price,
                Stock = stock,
            };

            await _productService.AddProductAsync(productPost);

            TextColor.WriteLine($"Product '{name}' created successfully.", ConsoleColor.Green);
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            TextColor.WriteLine($"Error: {ex.Message}", ConsoleColor.Red);
        }
    }

    private static async Task UpdateProductAsync()
    {
        TextColor.WriteLine("| Update Product |", ConsoleColor.DarkMagenta);

        try
        {
            TextColor.WriteLine("Enter product ID:", ConsoleColor.Blue);
            int productId = int.Parse(Console.ReadLine());

            var product = await _productService.GetProductById(productId);

            if (product == null)
            {
                TextColor.WriteLine("Product not found.", ConsoleColor.Red);
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


            TextColor.WriteLine($"Current Name: {product.Name}", ConsoleColor.DarkYellow);
            TextColor.WriteLine("Enter new Name (leave empty to keep current):", ConsoleColor.Blue);
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                newProduct.Name = newName;
            }

            TextColor.WriteLine($"Current Price: {product.Price}", ConsoleColor.DarkYellow);
            TextColor.WriteLine("Enter new Price (leave empty to keep current):", ConsoleColor.Blue);
            string priceInput = Console.ReadLine();
            if (decimal.TryParse(priceInput, out decimal newPrice))
            {
                newProduct.Price = newPrice;
            }

            TextColor.WriteLine($"Current Stock: {product.Stock}", ConsoleColor.DarkYellow);
            TextColor.WriteLine("Enter new Stock (leave empty to keep current):", ConsoleColor.Blue);
            string stockInput = Console.ReadLine();
            if (int.TryParse(stockInput, out int newStock))
            {
                newProduct.Stock = newStock;
            }

            TextColor.WriteLine($"Current Description: {product.Description}", ConsoleColor.DarkYellow);
            TextColor.WriteLine("Enter new Description (leave empty to keep current):", ConsoleColor.Blue);
            string newDescription = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newDescription))
            {
                newProduct.Description = newDescription;
            }

            await _productService.UpdateProductAsync(newProduct);

            TextColor.WriteLine("Product updated successfully.", ConsoleColor.Green);
        }
        catch (Exception ex)
        {
            TextColor.WriteLine($"Error: {ex.Message}", ConsoleColor.Red);
        }
    }

    private static async Task DeleteProductAsync()
    {
        try
        {
            TextColor.WriteLine("Select product from list and enter product ID:", ConsoleColor.DarkYellow);
            await ViewAllProductsAsync();
            var id = int.Parse(Console.ReadLine());
            await _productService.DeleteProductAsync(id);
            TextColor.WriteLine("Product deleted succesfully", ConsoleColor.Green);
        }
        catch (Exception ex)
        {
            TextColor.WriteLine($"Error:{ex.Message}", ConsoleColor.Red);
        }
    }

    private static async Task ViewAllProductsAsync()
    {
        try
        {
            var products = await _productService.GetAllProducts();

            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }
        catch (Exception ex)
        {
            TextColor.WriteLine($"Error:{ex.Message}", ConsoleColor.Red);
        }
    }

    private static async Task SearchProductsAsync()
    {
        try
        {
            TextColor.WriteLine("Enter product name (or substring of name):", ConsoleColor.DarkYellow);
            var name = Console.ReadLine();
            var products = await _productService.SearchProducts(name);

            if (products == null || !products.Any())
            {
                TextColor.WriteLine("Products not found.", ConsoleColor.Red);
            }
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }
        catch (Exception ex)
        {
            TextColor.WriteLine(ex.Message, ConsoleColor.Red);
        }
    }

    private static async Task RegisterAdminAsync()
    {
        TextColor.WriteLine("| Admin Registration |", ConsoleColor.DarkMagenta);

        string username = string.Empty;
        string email = string.Empty;
        string password = string.Empty;
        string address = string.Empty;

        try
        {
            while (true)
            {
                TextColor.WriteLine("Enter username: ", ConsoleColor.Blue);
                username = Console.ReadLine();
                if (!string.IsNullOrEmpty(username))
                    break;
                TextColor.WriteLine("Full name cannot be empty. Please try again.", ConsoleColor.Red);
            }

            while (true)
            {
                TextColor.WriteLine("Enter email: ", ConsoleColor.Blue);
                email = Console.ReadLine();
                if (!string.IsNullOrEmpty(email))
                    break;
                TextColor.WriteLine("Email cannot be empty. Please try again.", ConsoleColor.Red);
            }

            while (true)
            {
                TextColor.WriteLine("Enter password: ", ConsoleColor.Blue);
                password = Console.ReadLine();
                if (!string.IsNullOrEmpty(password))
                    break;
                TextColor.WriteLine("Password cannot be empty. Please try again.", ConsoleColor.Red);
            }

            while (true)
            {
                TextColor.WriteLine("Enter address: ", ConsoleColor.Blue);
                address = Console.ReadLine();
                if (!string.IsNullOrEmpty(address))
                    break;
                TextColor.WriteLine("Address cannot be empty. Please try again.", ConsoleColor.Red);
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
        TextColor.WriteLine("| User Registration |", ConsoleColor.DarkMagenta);

        string username = string.Empty;
        string email = string.Empty;
        string password = string.Empty;
        string address = string.Empty;

        try
        {
            while (true)
            {
                TextColor.WriteLine("Enter full name: ", ConsoleColor.Blue);
                username = Console.ReadLine();
                if (!string.IsNullOrEmpty(username))
                    break;
                TextColor.WriteLine("Full name cannot be empty. Please try again.", ConsoleColor.Red);
            }

            while (true)
            {
                TextColor.WriteLine("Enter email: ", ConsoleColor.Blue);
                email = Console.ReadLine();
                if (!string.IsNullOrEmpty(email))
                    break;
                TextColor.WriteLine("Email cannot be empty. Please try again.", ConsoleColor.Red);
            }

            while (true)
            {
                TextColor.WriteLine("Enter password: ", ConsoleColor.Blue);
                password = Console.ReadLine();
                if (!string.IsNullOrEmpty(password))
                    break;
                TextColor.WriteLine("Password cannot be empty. Please try again.", ConsoleColor.Red);
            }

            while (true)
            {
                TextColor.WriteLine("Enter address: ", ConsoleColor.Blue);
                address = Console.ReadLine();
                if (!string.IsNullOrEmpty(address))
                    break;
                TextColor.WriteLine("Address cannot be empty. Please try again.", ConsoleColor.Red);
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

            TextColor.WriteLine($"User {username} created successfully.", ConsoleColor.Green);
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            TextColor.WriteLine($"Error: {ex.Message}", ConsoleColor.Red);
        }
    }

    private static async Task LoginAsync()
    {
        TextColor.WriteLine("| Login |", ConsoleColor.DarkMagenta);
        TextColor.WriteLine("Enter email:", ConsoleColor.Blue);
        string email = Console.ReadLine();

        TextColor.WriteLine("Enter password:", ConsoleColor.Blue);
        string password = Console.ReadLine();

        try
        {
            var user = await _userService.LoginUserAsync(email, password);

            if (user != null)
            {
                _currentUser = user;
                //CurrentUser = user;
                TextColor.WriteLine($"User {user.UserName} logged in successfully.", ConsoleColor.Green);
            }
        }
        catch (UserAuthenticationException ex)
        {
            TextColor.WriteLine($"Login failed: {ex.Message}", ConsoleColor.Red);
        }
    }

    private static async Task UpdateUserInformationAsync()
    {
        TextColor.WriteLine("| Update User Information |", ConsoleColor.DarkMagenta);

        try
        {
            int userId = CurrentUser.IsAdmin ? PromptForUserId() : CurrentUser.Id;

            TextColor.WriteLine("Enter new username (leave blank to keep current): ", ConsoleColor.Blue);
            string newUserName = Console.ReadLine();

            TextColor.WriteLine("Enter new address (leave blank to keep current): ", ConsoleColor.Blue);
            string newAddress = Console.ReadLine();

            TextColor.WriteLine("Do you want to update the password? (y/n): ", ConsoleColor.Blue);
            string updatePassword = Console.ReadLine();

            string newPassword = null;
            if (updatePassword.ToLower() == "y")
            {
                TextColor.WriteLine("Enter new password: ", ConsoleColor.Blue);
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
            TextColor.WriteLine("User not found.", ConsoleColor.Red);
        }
        catch (Exception ex)
        {
            TextColor.WriteLine($"Error: {ex.Message}", ConsoleColor.Red);
        }
    }

    private static async Task ViewUserOrdersAsync()
    {
        TextColor.WriteLine("| View Orders |", ConsoleColor.DarkMagenta);

        try
        {
            List<OrderGetDto> orders;

            if (CurrentUser.IsAdmin)
            {
                TextColor.WriteLine("Admin view: orders for all users.", ConsoleColor.Blue);
                orders = await _orderService.GetAllOrders();
            }
            else
            {
                TextColor.WriteLine("User view: orders for the current user.", ConsoleColor.Blue);
                orders = await _orderService.GetUserOrdersAsync(CurrentUser.Id);
            }

            if (orders.Count == 0)
            {
                TextColor.WriteLine("No orders found.", ConsoleColor.Red);
                return;
            }

            foreach (var order in orders)
            {
                Console.WriteLine(order);
            }
        }
        catch (Exception ex)
        {
            TextColor.WriteLine($"Error: {ex.Message}", ConsoleColor.Red);
        }
    }

    private static async Task CreateOrderAsync()
    {
        TextColor.WriteLine("| Create Order |", ConsoleColor.DarkMagenta);

        try
        {
            int userId = CurrentUser.IsAdmin ? PromptForUserId() : CurrentUser.Id;

            if (userId == 0)
            {
                TextColor.WriteLine("Invalid user ID.", ConsoleColor.Red);
                return;
            }

            var orderItems = new List<OrderDetail>();
            decimal totalAmount = 0;

            while (true)
            {
                TextColor.WriteLine("Enter product ID (or type 'done' to finish):", ConsoleColor.Blue);
                var input = Console.ReadLine();

                if (input?.ToLower() == "done")
                    break;

                if (int.TryParse(input.Trim(), out var productId))
                {
                    TextColor.WriteLine($"Enter quantity for product ID {productId}:", ConsoleColor.Blue);
                    if (int.TryParse(Console.ReadLine(), out var quantity) && quantity > 0)
                    {
                        decimal pricePerUnit = await _productService.GetProductPrice(productId);

                        if (pricePerUnit <= 0)
                        {
                            TextColor.WriteLine("Invalid product price.", ConsoleColor.Red);
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
                        TextColor.WriteLine("Invalid quantity. Please enter a positive number.", ConsoleColor.Red);
                    }
                }
                else
                {
                    TextColor.WriteLine("Invalid product ID. Please enter a valid number.", ConsoleColor.Red);
                }
            }

            if (!orderItems.Any())
            {
                TextColor.WriteLine("No products specified.", ConsoleColor.Red);
                return;
            }

            var orderDto = new OrderPostDto
            {
                UserId = userId,
                TotalAmount = totalAmount,
                Details = orderItems
            };

            await _orderService.CreateOrderAsync(orderDto);

            TextColor.WriteLine("Order created successfully.", ConsoleColor.Green);
        }
        catch (Exception ex)
        {
            TextColor.WriteLine($"Error: {ex.Message}", ConsoleColor.Red);
        }
    }

    private static async Task CancelOrderAsync()
    {
        TextColor.WriteLine("| Cancel Order |", ConsoleColor.DarkMagenta);

        try
        {
            int userId = CurrentUser.IsAdmin ? PromptForUserId() : CurrentUser.Id;

            if (userId == 0)
            {
                TextColor.WriteLine("Invalid user ID.", ConsoleColor.Red);
                return;
            }

            List<OrderGetDto> orders;

            if (CurrentUser.IsAdmin)
            {
                orders = await _orderService.GetUserOrdersAsync(userId);
            }
            else
            {
                orders = await _orderService.GetUserOrdersAsync(CurrentUser.Id);
            }

            if (!orders.Any())
            {
                TextColor.WriteLine("No orders found for the specified user.", ConsoleColor.Red);
                return;
            }

            TextColor.WriteLine("Orders:", ConsoleColor.DarkYellow);
            foreach (var order in orders)
            {
                Console.WriteLine(order);
            }

            TextColor.WriteLine("Enter the ID of the order to cancel: ", ConsoleColor.Blue);
            if (!int.TryParse(Console.ReadLine(), out int orderId))
            {
                TextColor.WriteLine("Invalid order ID.", ConsoleColor.Red);
                return;
            }

            var orderToCancel = orders.FirstOrDefault(o => o.Id == orderId);

            if (orderToCancel == null)
            {
                TextColor.WriteLine("Order not found.", ConsoleColor.Red);
                return;
            }

            if (orderToCancel.Status != OrderStatus.Pending)
            {
                TextColor.WriteLine("Only pending orders can be cancelled.", ConsoleColor.Red);
                return;
            }

            await _orderService.CancelOrderAsync(orderId);

            TextColor.WriteLine("Order cancelled successfully.", ConsoleColor.Green);
        }
        catch (Exception ex)
        {
            TextColor.WriteLine($"Error: {ex.Message}", ConsoleColor.Red);
        }
    }

    private static async Task CompleteOrderAndMakePaymentAsync()
    {
        TextColor.WriteLine("| Complete Order and Make Payment |", ConsoleColor.DarkMagenta);

        try
        {
            if (CurrentUser == null)
            {
                TextColor.WriteLine("You need to log in first.", ConsoleColor.Red);
                return;
            }

            int userId = CurrentUser.IsAdmin ? PromptForUserId() : CurrentUser.Id;

            if (userId == 0)
            {
                TextColor.WriteLine("Invalid user ID.", ConsoleColor.Red);
                return;
            }

            var orders = await _orderService.GetUserOrdersAsync(userId);

            if (!orders.Any())
            {
                TextColor.WriteLine("No pending orders found.", ConsoleColor.Red);
                return;
            }

            TextColor.WriteLine("Select the order ID to complete and make payment for:", ConsoleColor.DarkYellow);
            foreach (var order in orders.Where(o=>o.Status==OrderStatus.Pending))
            {
                Console.WriteLine(order);
            }

            if (!int.TryParse(Console.ReadLine(), out int orderId) || !orders.Any(o => o.Id == orderId))
            {
                TextColor.WriteLine("Invalid order ID.", ConsoleColor.Red);
                return;
            }

            var selectedOrder = orders.FirstOrDefault(o => o.Id == orderId);

            if (selectedOrder == null)
            {
                TextColor.WriteLine("Order not found.", ConsoleColor.Red);
                return;
            }

            if (selectedOrder.Status != OrderStatus.Pending)
            {
                TextColor.WriteLine("Only pending orders can be completed.", ConsoleColor.Red);
                return;
            }

            var paymentDto = new PaymentPostDto
            {
                OrderId = selectedOrder.Id,
                Amount = selectedOrder.TotalAmount,
                PaymentDate = DateTime.UtcNow
            };

            await _paymentService.MakePaymentAsync(paymentDto);
            await _orderService.CompleteOrderAsync(orderId);

            TextColor.WriteLine("Payment successful. Your order has been completed.", ConsoleColor.Green);
        }
        catch (Exception ex)
        {
            TextColor.WriteLine($"Error: {ex.Message}", ConsoleColor.Red);
        }
    }

    private static async Task ViewPaymentsAsync()
    {
        TextColor.WriteLine("| View Payments |", ConsoleColor.DarkMagenta);

        try
        {
            int userId = CurrentUser.IsAdmin ? PromptForUserId() : CurrentUser.Id;

            var payments = await _paymentService.GetPaymentByUserId(userId);

            if (!payments.Any())
            {
                TextColor.WriteLine("No payments found.", ConsoleColor.Red);
                return;
            }

            foreach (var payment in payments)
            {
                Console.WriteLine(payment);
            }
        }
        catch (Exception ex)
        {
            TextColor.WriteLine($"Error: {ex.Message}", ConsoleColor.Red);
        }
    }
    private static int PromptForUserId()
    {
        TextColor.WriteLine("Enter the ID of the user: ", ConsoleColor.Blue);
        return int.TryParse(Console.ReadLine(), out var userId) ? userId : 0;
    }
    public bool Export<T>(List<T> list, string file, string sheetName ) 
    {
        bool exported = false;
        using(IXLWorkbook workbook = new XLWorkbook())
        {
            workbook.AddWorksheet(sheetName).FirstCell().InsertTable<T>(list, false);
         
            workbook.SaveAs(file);
            exported = true;    
        }
        return exported;
    }
 
    public async static Task ExcelExports()
    {
        var products = await _productService.GetAllProducts();
        var orders = await _orderService.GetAllOrders();
        var payments = await _paymentService.GetAllPayments();
        var users = await _userService.GetAllUsers();

        using (var workbook = new XLWorkbook())
        {
            var productsSheet = workbook.AddWorksheet("Products");
            productsSheet.FirstCell().InsertTable(products);

            var ordersSheet = workbook.AddWorksheet("Orders");
            ordersSheet.FirstCell().InsertTable(orders);

            var paymentsSheet = workbook.AddWorksheet("Payments");
            paymentsSheet.FirstCell().InsertTable(payments);

            var usersSheet = workbook.AddWorksheet("Users");
            usersSheet.FirstCell().InsertTable(users);

            workbook.SaveAs(@"C:\Users\NafisatAkiyeva\Desktop\Excels\Test.xlsx");
        }
    }
}