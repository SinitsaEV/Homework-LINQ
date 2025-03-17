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
            const string AmnestyCriminalCommand = "1";
            const string ExitCommand = "2";

            bool isActive = true;

            while (isActive)
            {
                Console.WriteLine($"{AmnestyCriminalCommand} - амнистия\n{ExitCommand} - выйти");
                Console.Write("Введите команду: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case AmnestyCriminalCommand:
                        AmnestyCriminal();
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

        private void AmnestyCriminal()
        {
            Console.Write("Введите преступление: ");
            string crime = Console.ReadLine();

            List<Criminal> criminals = _criminals.Where(criminal => criminal.Crime.ToLower() == crime.ToLower() && criminal.IsInPrison == true).ToList();

            ShowCriminals(criminals);
            ReleaseCriminals(criminals);

            if (criminals.Count == 0)
            {
                Console.WriteLine("Преступники не найдены.");
            }
            else
            {
                ShowCriminals(criminals);
            }
        }

        private void ReleaseCriminals(List<Criminal> criminals)
        {
            foreach(Criminal criminal in criminals)
            {
                criminal.GetOutOfJail();
            }
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
        public Criminal(string name, string crime, bool isInPrison) : base(name)
        {
            Crime = crime;
            IsInPrison = isInPrison;
        }

        public bool IsInPrison { get; private set; }
        public string Crime {  get; private set; }

        public void Show()
        {
            Console.WriteLine($"Фио: {Name}");
            Console.WriteLine($"Преступление: {Crime}");
            Console.WriteLine($"Находиться в под стражей: {IsInPrison}\n");
        }

        public void GetOutOfJail()
        {
            IsInPrison = false;
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

        private List<string> _crimes = new List<string>
        {
            "Убийство",
            "Антиправительственное",
            "Кража"
        };

        public List<Criminal> CreateCriminals()
        {
            List<Criminal> criminals = new List<Criminal>();
            int maxCriminalsCount = 20;
            int minCriminalsCount = 10;

            int randomCriminalsCount = UserUtils.GenerateRandomNumber(minCriminalsCount, maxCriminalsCount);
            bool[] isInPrison = new bool[] { true, false };

            for(int i = 0; i < randomCriminalsCount; i++)
            {
                int randomNameIndex = UserUtils.GenerateRandomNumber(0, _names.Count);
                int randomIsInPrisonIndex = UserUtils.GenerateRandomNumber(0, isInPrison.Length);
                int randomCrimeIndex = UserUtils.GenerateRandomNumber(0,_crimes.Count);

                criminals.Add(new Criminal(_names[randomNameIndex], _crimes[randomCrimeIndex], isInPrison[randomIsInPrisonIndex]));
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