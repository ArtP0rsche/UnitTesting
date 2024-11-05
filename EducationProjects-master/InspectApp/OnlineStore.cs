namespace Store
{

    class OnlineStore
    {
        private List<Customer> _customers;
        private List<Product> _products;

        public OnlineStore()
        {
            _customers = [];
            _products = [];
        }

        public void RegisterCustomer(string name, string email)
        {
            _customers.Add(new Customer(name, email));
        }

        public void AddProduct(string name, decimal price, string category, int stock)
        {
            _products.Add(new Product(name, price, category, stock));
        }

        public Customer GetCustomerByEmail(string email)
        {
            return _customers.FirstOrDefault(c => c.Email == email);
        }

        public Product GetProductByName(string name)
        {
            return _products.FirstOrDefault(p => p.Name == name);
        }

        public void ListProducts()
        {
            foreach (var product in _products)
            {
                Console.WriteLine(product);
            }
        }

        public void ListCustomers()
        {
            foreach (var customer in _customers)
            {
                Console.WriteLine(customer);
            }
        }
    }
    class Product
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public string Category { get; private set; }
        public int Stock { get; private set; }

        public Product(string name, decimal price, string category, int stock)
        {
            Name = name;
            Price = price;
            Category = category;
            Stock = stock;
        }

        public void Update(int quantity)
        {
            if (Stock - quantity < 0)
            {
                throw new InvalidOperationException("Недостаточно товаров на складе.");
            }
            Stock -= quantity;
        }

        public override string ToString()
        {
            return $"Название: {Name}, Категория: {Category}, Цена: {Price:C}, В наличии: {Stock}";
        }
    }

    class Customer
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public List<Order> OrderHistory { get; private set; }

        public Customer(string name, string email)
        {
            Name = name;
            Email = email;
            OrderHistory = [];
        }

        public void AddOrder(Order order)
        {
            OrderHistory.Add(order);
        }

        public override string ToString()
        {
            return $"Имя: {Name}, Email: {Email}, Заказов: {OrderHistory.Count}";
        }
    }

    class Order
    {
        public Guid OrderId { get; private set; }
        public List<Product> Products { get; private set; }
        public decimal TotalAmount { get; private set; }
        public string Status { get; private set; }
        public Customer Customer { get; private set; }

        public Order(Customer customer)
        {
            OrderId = Guid.NewGuid();
            Products = [];
            Customer = customer;
            Status = "Ожидает оплаты";
        }

        public void AddProduct(Product product, int quantity)
        {
            product.Update(quantity);
            Products.Add(product);
            TotalAmount += product.Price * quantity;
        }

        public void CompletePayment()
        {
            Status = "Оплачено";
        }

        public void ShipOrder()
        {
            Status = "Отправлено";
        }

        public override string ToString()
        {
            return $"ID заказа: {OrderId}, Клиент: {Customer.Name}, Статус: {Status}, Общая сумма: {TotalAmount:C}";
        }
    }

    class Cart
    {
        public Customer Customer { get; private set; }
        private Dictionary<Product, int> CartItems;

        public Cart(Customer customer)
        {
            CartItems = [];
        }

        public void AddToCart(Product product, int quantity)
        {
            if (CartItems.ContainsKey(product))
            {
                CartItems[product] += quantity;
            }
            else
            {
                CartItems[product] = quantity;
            }

            product.Update(quantity);
        }

        public void RemoveFromCart(Product product, int quantity)
        {
            CartItems[product] -= quantity;
            if (CartItems[product] == 0)
            {
                CartItems.Remove(product);
            }

            product.Update(-quantity);
        }

        public decimal GetTotalAmount()
        {
            return CartItems.Sum(item => item.Key.Price * item.Value);
        }

        public void Checkout()
        {
            var order = new Order(Customer);
            foreach (var item in CartItems)
            {
                order.AddProduct(item.Key, item.Value);
            }

            Customer.AddOrder(order);
            Console.WriteLine($"Заказ успешно создан для клиента {Customer.Name}. Общая сумма: {order.TotalAmount:C}");
            CartItems.Clear();
        }

        public override string ToString()
        {
            return $"Корзина клиента {Customer.Name}. Товаров в корзине: {CartItems.Count}. Общая сумма: {GetTotalAmount():C}";
        }
    }
}