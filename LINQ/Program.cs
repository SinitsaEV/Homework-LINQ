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

            PlayersFabric playersFabric = new PlayersFabric();

            List<Player> players = playersFabric.CreatePlayers();
            int topPlacesCount = 3;

            var bestPlayersByLevel = players.OrderByDescending(player => player.Level).Take(topPlacesCount).ToList();
           
            foreach( Player player in bestPlayersByLevel)
            {
                player.Show();
            }

            var bestPlayersByPower = players.OrderByDescending(player => player.Power).Take(topPlacesCount).ToList();

            foreach (Player player in bestPlayersByPower)
            {
                player.Show();
            }
        }       
    }

    class Player
    {
        public Player(string name, int level, int power)
        {
            Name = name;
            Level = level;
            Power = power;
        }

        public string Name { get; private set; }
        public int Level { get; private set; }
        public int Power { get; private set; }

        public void Show()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"Power: {Power}");
        }
    }

    class PlayersFabric
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
        public List<Player> CreatePlayers()
        {
            List<Player> list = new List<Player>();

            int maxPlayerCount = 30;
            int minPlayerCount = 10;

            int randomPlayerCount = UserUtils.GenerateRandomNumber(minPlayerCount, maxPlayerCount);

            int maxLevel = 100;
            int minLevel = 0;

            int maxPower = 100000;
            int minPower = 0;

            for(int i = 0; i < maxPlayerCount; i++)
            {
                int randomLevel = UserUtils.GenerateRandomNumber(minLevel, maxLevel);
                int randomPower = UserUtils.GenerateRandomNumber(minPower, maxPower);
                int randomNameIndex = UserUtils.GenerateRandomNumber(0,_names.Count);

                list.Add(new Player(_names[randomNameIndex], randomLevel, randomPower));
            }

            return list;
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