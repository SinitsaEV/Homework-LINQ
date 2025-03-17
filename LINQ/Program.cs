using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            ShopFabric shopFabric = new ShopFabric();
            Shop shop = shopFabric.CreateShop();

            shop.FindSpoiledProducts();
        }
    }

    class Shop
    {
        private List<Product> _products;

        public Shop(List<Product> products, int currentYear)
        {
            _products = products;
            CurrentYear = currentYear;
        }

        public int CurrentYear {  get; private set; }

        public void FindSpoiledProducts()
        {
            List<Product> spoiledProducts = _products.Where(product => CurrentYear - product.ManufactureYear > product.ExpirationDate).ToList();
            ShowProducts(spoiledProducts);
        }

        private void ShowProducts(List<Product> products)
        {
            foreach(Product product in products)
            {
                product.Show();
            }
        }
    }

    class Product
    {
        public Product(string name, int manufactureYear, int expirationDate)
        {
            Name = name;
            ManufactureYear = manufactureYear;
            ExpirationDate = expirationDate;
        }

        public string Name { get; private set; }
        public int ManufactureYear { get; private set; }
        public int ExpirationDate { get; private set; }

        public void Show()
        {
            Console.WriteLine($"Имя: {Name}");
            Console.WriteLine($"Год производства: {ManufactureYear}");
            Console.WriteLine($"Срок годности: {ExpirationDate}");
        }
    }

    class ShopFabric
    {
        private List<string> _names = new List<string>
        {
            "Фасоль",
            "Огурцы",
            "Помидоры"
        };

        public int CurrentYear = 2025;

        public Shop CreateShop()
        {
            return new Shop(CreateProducts(),CurrentYear);
        }

        private List<Product> CreateProducts()
        {
            List<Product> products = new List<Product>();

            int maxProductsCount = 15;
            int minProductsCount = 10;

            int maxManufactureYear = 2025;
            int minManufactureYear = 2000;

            int maxExpirationDate = 5;
            int minExpirationDate = 1;

            int randomProductsCount = UserUtils.GenerateRandomNumber(minProductsCount, maxProductsCount);

            for (int i = 0; i < randomProductsCount; i++)
            {
                int randomNameIndex = UserUtils.GenerateRandomNumber(0, _names.Count);
                int randomManufactureYear = UserUtils.GenerateRandomNumber(minManufactureYear, maxManufactureYear);
                int randomExpirationDate = UserUtils.GenerateRandomNumber(minExpirationDate, maxExpirationDate);

                products.Add(new Product(_names[randomNameIndex], randomManufactureYear, randomExpirationDate));
            }

            return products;
        }
    }

    class UserUtils
    {
        private static Random s_random = new Random();

        public static int GenerateRandomNumber(int min, int max)
        {
            return s_random.Next(min, max);
        }
    }
}