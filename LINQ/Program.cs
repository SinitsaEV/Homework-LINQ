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

            LeaderboardFabric leaderboardFabric = new LeaderboardFabric();
            Leaderboard leaderboard = leaderboardFabric.CreateLeaderboard();

            leaderboard.ShowTopPlayersByLevel();
            leaderboard.ShowTopPlayersByPower();            
        }
    }

    class Leaderboard
    {
        private List<Player> _players;

        public Leaderboard(List<Player> players, int topPlacesCount)
        {
            _players = players;
            TopPlacesCount = topPlacesCount;
        }

        public int TopPlacesCount { get; private set; }

        public void ShowTopPlayersByPower()
        {
            List<Player> bestPlayersByPower = _players.OrderByDescending(player => player.Power).Take(TopPlacesCount).ToList();
            Console.WriteLine($"Топ {TopPlacesCount} по силе:");
            ShowPlayers(bestPlayersByPower);
        }

        public void ShowTopPlayersByLevel()
        {
            List<Player> bestPlayersByLevel = _players.OrderByDescending(player => player.Level).Take(TopPlacesCount).ToList();
            Console.WriteLine($"Топ {TopPlacesCount} по уровню:");
            ShowPlayers(bestPlayersByLevel);
        }

        private void ShowPlayers(List<Player> players)
        {
            foreach(Player player in players)
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

    class LeaderboardFabric
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

        private int _topPlacesCount = 3;

        public Leaderboard CreateLeaderboard()
        {
            return new Leaderboard(CreatePlayers(),_topPlacesCount);
        }

        private List<Player> CreatePlayers()
        {
            List<Player> list = new List<Player>();

            int maxPlayerCount = 30;
            int minPlayerCount = 10;

            int randomPlayerCount = UserUtils.GenerateRandomNumber(minPlayerCount, maxPlayerCount);

            int maxLevel = 100;
            int minLevel = 0;

            int maxPower = 100000;
            int minPower = 0;

            for (int i = 0; i < randomPlayerCount; i++)
            {
                int randomLevel = UserUtils.GenerateRandomNumber(minLevel, maxLevel);
                int randomPower = UserUtils.GenerateRandomNumber(minPower, maxPower);
                int randomNameIndex = UserUtils.GenerateRandomNumber(0, _names.Count);

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
