using System;
using System.Collections.Generic;
using System.Linq;
using Hotel;
using Store;
class Program
{
    static void Main(string[] args)
    {
        StartOnlineStore();
        StartHotel();
    }

    private static void StartHotel()
    {
        var hotelSystem = new HotelManagementSystem();

        hotelSystem.AddRoom(101, "Стандарт", 3000, 2);
        hotelSystem.AddRoom(102, "Делюкс", 5000, 4);
        hotelSystem.AddRoom(103, "Люкс", 8000, 4);

        hotelSystem.RegisterCustomer("Иван Петров", "ivan@mail.com");
        hotelSystem.RegisterCustomer("Анна Смирнова", "anna@mail.com");

        hotelSystem.ListAvailableRooms();

        var customer = hotelSystem.GetCustomerByEmail("ivan@mail.com");
        hotelSystem.BookRoom(customer.Email, 101, DateTime.Now.AddDays(1), DateTime.Now.AddDays(5), customer.Name);

        hotelSystem.ListAvailableRooms();
    }

    static void StartOnlineStore()
    {
        var store = new OnlineStore();

        store.AddProduct("Ноутбук", 50000, "Электроника", 10);
        store.AddProduct("Смартфон", 30000, "Электроника", 5);
        store.AddProduct("Книга", 500, "Книги", 100);

        store.RegisterCustomer("Иван Иванов", "ivan@mail.com");
        store.RegisterCustomer("Анна Смирнова", "anna@mail.com");

        var customer = store.GetCustomerByEmail("ivan@mail.com");
        var cart = new Cart(customer);

        var product = store.GetProductByName("Ноутбук");
        cart.AddToCart(product, 1);

        Console.WriteLine(cart);
        cart.Checkout();

        store.ListProducts();
        store.ListCustomers();
    }
}