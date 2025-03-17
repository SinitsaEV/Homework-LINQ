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

            BattalionFabric battalionFabric = new BattalionFabric();
            Battalion battalion = battalionFabric.CreateBattalion();

            battalion.ShowSoldiers();
        }
    }

    class Battalion
    {
        private List<Soldier> _soldiers;

        public Battalion(List<Soldier> soldiers)
        {
            _soldiers = soldiers;
        }

        public void ShowSoldiers()
        {
            var soldiers = _soldiers.Select(soldier => new { soldier.Name, soldier.Rank }).ToList();

            foreach (var soldier in soldiers)
            {
                Console.WriteLine($"{soldier.Name} {soldier.Rank}\n");
            }
        }
    }

    class Soldier
    {
        public Soldier(string name, string weapon, string rank, int armyServiceMonths)
        {
            Name = name;
            Weapon = weapon;
            Rank = rank;
            ArmyServiceMonths = armyServiceMonths;
        }

        public string Name { get; private set; }
        public string Weapon {  get; private set; }
        public string Rank {  get; private set; }
        public int ArmyServiceMonths {  get; private set; }
    }

    class BattalionFabric
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

        private List<string> _militaryRanks = new List<string>
        {
            "Рядовой",
            "Ефрейтор",
            "Младший сержант",
            "Сержант",
            "Старший сержант",
            "Старшина",
            "Прапорщик",
            "Старший прапорщик",
            "Младший лейтенант",
            "Лейтенант",
            "Старший лейтенант",
            "Капитан",
            "Майор",
            "Подполковник",
            "Полковник"
        };

        private List<string> _weaponTypes = new List<string>
        {
            "АК-74",
            "АК-12",
            "Пистолет Макарова (ПМ)",
            "Снайперская винтовка Драгунова (СВД)",
            "Ручной пулемет Калашникова (РПК)",
            "Гранатомет РПГ-7",
            "Пистолет-пулемет ПП-2000",
            "Автоматический гранатомет АГС-30",
            "Карабин СКС",
            "Пулемет ПКМ"
        };

        public Battalion CreateBattalion()
        {
            return new Battalion(CreateSoldiers());
        }

        private List<Soldier> CreateSoldiers()
        {
            List<Soldier> products = new List<Soldier>();

            int maxSoldiersCount = 15;
            int minSoldiersCount = 10;

            int maxArmyServiceMonths = 360;
            int minArmyServiceMonths = 1;


            int randomSoldiersCount = UserUtils.GenerateRandomNumber(minSoldiersCount, maxSoldiersCount);

            for (int i = 0; i < randomSoldiersCount; i++)
            {
                int randomArmyServiceMonths = UserUtils.GenerateRandomNumber(minArmyServiceMonths, maxArmyServiceMonths);
                int randomNameIndex = UserUtils.GenerateRandomNumber(0,_names.Count);
                int randomRankIndex = UserUtils.GenerateRandomNumber(0,_militaryRanks.Count);
                int randomWeaponTypesIndex = UserUtils.GenerateRandomNumber(0, _weaponTypes.Count);

                products.Add(new Soldier(_names[randomNameIndex], _weaponTypes[randomWeaponTypesIndex], _militaryRanks[randomRankIndex], randomArmyServiceMonths));
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