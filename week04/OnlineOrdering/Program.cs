using System;

class Program
{
    static void Main(string[] args)
    {
        // Create the first customer's address
        Address address1 = new Address(
            "123 Main Street",
            "New York",
            "NY",
            "USA"
        );

        // Create the first customer
        Customer customer1 = new Customer("John Smith", address1);

        // Create products for the first order
        Product product1 = new Product("Laptop", "P001", 800, 1);
        Product product2 = new Product("Mouse", "P002", 25, 2);
        Product product3 = new Product("Keyboard", "P003", 50, 1);

        // Create the first order
        Order order1 = new Order(customer1);

        // Add products to the first order
        order1.AddProduct(product1);
        order1.AddProduct(product2);
        order1.AddProduct(product3);

        // Create the second customer's address
        Address address2 = new Address(
            "45 Aba Road",
            "Port Harcourt",
            "Rivers",
            "Nigeria"
        );

        // Create the second customer
        Customer customer2 = new Customer("Mary Johnson", address2);

        // Create products for the second order
        Product product4 = new Product("Phone", "P004", 500, 1);
        Product product5 = new Product("Headphones", "P005", 80, 2);
        Product product6 = new Product("Charger", "P006", 30, 1);

        // Create the second order
        Order order2 = new Order(customer2);

        // Add products to the second order
        order2.AddProduct(product4);
        order2.AddProduct(product5);
        order2.AddProduct(product6);

        // Display the first order
        Console.WriteLine("ORDER 1");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order1.GetTotalCost():F2}");

        Console.WriteLine();

        // Display the second order
        Console.WriteLine("ORDER 2");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order2.GetTotalCost():F2}");
    }
}