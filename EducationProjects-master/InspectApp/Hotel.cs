namespace Hotel
{

    class Room
    {
        public int RoomNumber { get; private set; }
        public string Type { get; private set; }
        public decimal PricePerNight { get; private set; }
        public bool IsAvailable { get; set; }
        public int Capacity { get; private set; }

        public Room(int roomNumber, string type, decimal pricePerNight, int capacity)
        {
            RoomNumber = roomNumber;
            Type = type;
            PricePerNight = pricePerNight;
            Capacity = capacity;
            IsAvailable = true; 
        }

        public override string ToString()
        {
            return $"Номер: {RoomNumber}, Тип: {Type}, Цена за ночь: {PricePerNight:C}, Вмещает: {Capacity} чел., Доступен: {IsAvailable}";
        }
    }

    class Customer
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public List<Booking> BookingHistory { get; private set; }

        public Customer(string name, string email)
        {
            Name = name;
            Email = email;
            BookingHistory = [];
        }

        public void AddBooking(Booking booking)
        {
            BookingHistory.Add(booking);
        }

        public override string ToString()
        {
            return $"Имя: {Name}, Email: {Email}, Количество бронирований: {BookingHistory.Count}";
        }
    }

    class Booking
    {
        public Guid BookingID { get; private set; }
        public Customer Customer { get; private set; }
        public Room Room { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public decimal TotalCost { get; private set; }
        public string Status { get; private set; }

        public Booking(Customer customer, Room room, DateTime startDate, DateTime endDate)
        {
            BookingID = Guid.NewGuid();
            Customer = customer;
            Room = room;
            StartDate = startDate;
            EndDate = endDate;
            TotalCost = (endDate - startDate).Days * room.PricePerNight;
            Status = "Забронировано";

            room.IsAvailable = false;
        }

        public void CancelBooking()
        {
            if (Status != "Забронировано")
            {
                throw new InvalidOperationException("Бронирование уже отменено или завершено.");
            }

            Status = "Отменено";
            Room.IsAvailable = true;
        }

        public override string ToString()
        {
            return $"ID бронирования: {BookingID}, Номер: {Room.RoomNumber}, Даты: {StartDate.ToShortDateString()} - {EndDate.ToShortDateString()}, Статус: {Status}, Общая стоимость: {TotalCost:C}";
        }
    }

    class HotelManagementSystem
    {
        private List<Customer> _customers;
        private List<Room> _rooms;

        public HotelManagementSystem()
        {
            _customers = [];
            _rooms = [];
        }

        public void AddRoom(int roomNumber, string type, decimal pricePerNight, int capacity)
        {
            _rooms.Add(new Room(roomNumber, type, pricePerNight, capacity));
        }

        public void RegisterCustomer(string name, string email)
        {
            _customers.Add(new Customer(name, email));
        }

        public Customer GetCustomerByEmail(string email)
        {
            return _customers.FirstOrDefault(c => c.Email == email);
        }

        public Room GetAvailableRoom(string type, int capacity)
        {
            return _rooms.FirstOrDefault(r => r.Type == type && r.Capacity >= capacity && r.IsAvailable);
        }

        public void BookRoom(string customerEmail, int roomNumber, DateTime startDate, DateTime endDate, string customerName)
        {
            var customer = GetCustomerByEmail(customerEmail);
            var room = _rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);

            if (customer == null || room == null)
            {
                throw new InvalidOperationException("Клиент или номер не найден.");
            }

            var booking = new Booking(customer, room, startDate, endDate);
            customer.AddBooking(booking);

            Console.WriteLine($"Номер {roomNumber} успешно забронирован для {customer.Name}.");
        }

        public void ListAvailableRooms()
        {
            var availableRooms = _rooms.Where(r => r.IsAvailable);
            foreach (var room in availableRooms)
            {
                Console.WriteLine(room);
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

}
