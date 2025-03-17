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

            CriminalsFabric criminalsFabric = new CriminalsFabric();
            Detective detective = new Detective("Шерлок", criminalsFabric.CreateCriminals());

            detective.Work();
        }       
    }

    abstract class Person
    {

        public Person(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }       
    }

    class Detective : Person
    {
        private List<Criminal> _criminals;
        public Detective(string name, List<Criminal> criminals) : base(name)
        {
            _criminals = criminals;
        }

        public void Work()
        {
            const string FindCriminalCommand = "1";
            const string ExitCommand = "2";

            bool isActive = true;

            while (isActive)
            {
                Console.WriteLine($"{FindCriminalCommand} - найти преступника\n{ExitCommand} - выйти");
                Console.Write("Введите команду: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case FindCriminalCommand:
                        FindCriminal();
                        break;

                    case ExitCommand:
                        Console.WriteLine("Вы вышли");
                        isActive = false;
                        break;

                    default:
                        Console.WriteLine("Неверный ввод.");
                        break;
                }
            }
        }

        private void FindCriminal()
        {
            int height = ReadInt("Введите рост: ");
            int weight = ReadInt("Введите вес: ");
            Console.Write("Введите национальность: ");
            string nationality = Console.ReadLine();

            List<Criminal> criminals = _criminals.Where( criminal => criminal.Height == height && criminal.Weight == weight
            && criminal.Nationality.ToLower() == nationality.ToLower() && criminal.IsInPrison == false).ToList();

            if (criminals.Count == 0)
            {
                Console.WriteLine("Преступники не найдены.");
            }
            else
            {
                ShowCriminals(criminals);
            }
        }

        private int ReadInt(string message)
        {
            Console.Write(message);
            int input = 0;

            while (int.TryParse(Console.ReadLine(), out input) == false || input < 0)
            {
                Console.WriteLine("Неверный ввод.");
            }

            return input;
        }

        private void ShowCriminals(List<Criminal> criminals)
        {
            foreach (Criminal criminal in criminals)
            {
                criminal.Show();
            }
        }
    }

    class Criminal : Person
    {
        public Criminal(string name, int height, int weight, string nationality, bool isInPrison) : base(name)
        {
            Height = height;
            Weight = weight;
            Nationality = nationality;
            IsInPrison = isInPrison;
        }

        public bool IsInPrison { get; private set; }
        public int Height { get; private set; }
        public int Weight { get; private set; }
        public string Nationality { get; private set; }

        public void Show()
        {
            Console.WriteLine($"Фио: {Name}");
            Console.WriteLine($"Рост: {Height}");
            Console.WriteLine($"Вес: {Weight}");
            Console.WriteLine($"Национальность: {Nationality}");
            Console.WriteLine($"Находиться в под стражей: {IsInPrison}\n");
        }
    }

    class CriminalsFabric
    {
        private List<string> _names = new List<string>
        {
            "Синица Евгений Владимирович",
            "Бурван Илья Викторович",
            "Наркевич Владимир Николаевич",
            "Кореневский Ян Александрович",
            "Половнев Павел Анатольевич",
            "Жилан Александр Викторович"
        };

        private List<string> _nationalitys = new List<string>
        {
            "Белорус",
            "Русский",
            "Поляк",
            "Американец",
            "Латыш",
            "Испанец"
        };

        public List<Criminal> CreateCriminals()
        {
            List<Criminal> criminals = new List<Criminal>();
            int maxCriminalsCount = 20;
            int minCriminalsCount = 10;

            int maxHeight = 220;
            int minHeight = 150;
            int maxWeight = 150;
            int minWeight = 50;

            int randomCriminalsCount = UserUtils.GenerateRandomNumber(minCriminalsCount, maxCriminalsCount);
            bool[] isInPrison = new bool[] { true, false };

            for(int i = 0; i < randomCriminalsCount; i++)
            {
                int randomNameIndex = UserUtils.GenerateRandomNumber(0, _names.Count);
                int randomHeight = UserUtils.GenerateRandomNumber(minHeight,maxHeight);
                int randomWeight = UserUtils.GenerateRandomNumber(minWeight, maxWeight);
                int randomNationalityIndex = UserUtils.GenerateRandomNumber(0, _nationalitys.Count);
                int randomIsInPrisonIndex = UserUtils.GenerateRandomNumber(0, isInPrison.Length);

                criminals.Add(new Criminal(_names[randomNameIndex], randomHeight, randomWeight, _nationalitys[randomNationalityIndex], isInPrison[randomIsInPrisonIndex]));
            }

            return criminals;
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