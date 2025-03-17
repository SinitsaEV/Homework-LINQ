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

            HospitalFabric hospitalFabric = new HospitalFabric();
            Hospital hospital = hospitalFabric.CreateHospital();

            hospital.Work();
        }       
    }

    class Patient
    {
        public Patient(string name, int age, string disease)
        {
            Name = name;
            Age = age;
            Disease = disease;
        }

        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Disease { get; private set; }

        public void Show()
        {
            Console.WriteLine($"Фио: {Name}");
            Console.WriteLine($"Возраст: {Age}");
            Console.WriteLine($"Заболевание: {Disease}");
        }
    }

    class Hospital
    {
        private List<Patient> _patients;

        public Hospital(List<Patient> patients, string name)
        {
            _patients = patients;
            Name = name;
        }

        public string Name { get; private set; }

        public void Work()
        {
            const string OrderByNameCommand = "1";
            const string OrderByAgeCommand = "2";
            const string FilterByDiseaseCommand = "3";
            const string ExitCommand = "4";

            bool isActive = true;

            while (isActive)
            {
                Console.WriteLine($"{OrderByNameCommand} - сортировать по имени\n" +
                    $"{OrderByAgeCommand} - сортировать по возрасту\n" +
                    $"{FilterByDiseaseCommand} - отфильтровать по болезни\n" +
                    $"{ExitCommand} - выйти");
                Console.Write("Введите команду: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case OrderByNameCommand:
                        OrderByName();
                        break;

                    case OrderByAgeCommand:
                        OrderByAge();
                        break;

                    case FilterByDiseaseCommand:
                        FilterByDisease();
                        break;

                    case ExitCommand:
                        isActive = false;
                        Console.WriteLine("Вы вышли.");
                        break;

                    default:
                        Console.WriteLine("Неверный ввод.");
                        break;
                }
            }
        }

        private void OrderByName()
        {
            List<Patient> orderedPatients = _patients.OrderBy(patient => patient.Name).ToList();
            ShowPatients(orderedPatients);
        }

        private void OrderByAge()
        {
            List<Patient> orderedPatients = _patients.OrderBy(patient => patient.Age).ToList();
            ShowPatients(orderedPatients);
        }

        private void FilterByDisease()
        {
            Console.Write("Введите болезнь: ");
            string disease = Console.ReadLine();
            List<Patient> filteredPatients = _patients.Where(patient => patient.Disease.ToLower() == disease.ToLower()).ToList();
            ShowPatients(filteredPatients);
        }

        private void ShowPatients(List<Patient> patients)
        {
            foreach (Patient patient in patients)
            {
                patient.Show();
            }
        }
    }

    class HospitalFabric
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

        private List<string> _diseases = new List<string>
        {
            "Перелом",
            "ОРВИ",
            "Мигрень"
        };

        public Hospital CreateHospital()
        {
            return new Hospital(CreatePatients(), "14-областная");
        }

        private List<Patient> CreatePatients()
        {
            List<Patient> patients = new List<Patient>();

            int maxPatientsCount = 20;
            int minPatientsCount = 10;

            int maxAge = 70;
            int minAge = 10;

            int randomPatientsCount = UserUtils.GenerateRandomNumber(minPatientsCount, maxPatientsCount);

            for (int i = 0; i < randomPatientsCount; i++)
            {
                int randomAge = UserUtils.GenerateRandomNumber(minAge, maxAge);
                int randoNameIndex = UserUtils.GenerateRandomNumber(0, _names.Count);
                int randoDiseaseIndex = UserUtils.GenerateRandomNumber(0, _diseases.Count);

                patients.Add(new Patient(_names[randoNameIndex], randomAge, _diseases[randoDiseaseIndex]));
            }

            return patients;
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