using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practika3
{
    internal class Program
    {
        public class Smartphone
        {
            public string Model { get; set; }
            public double Price { get; set; }

            public Smartphone(string model, double price)
            {
                Model = model;
                Price = price;
            }

            public override string ToString()
            {
                return $"Модель: {Model}, Цена: {Price}";
            }
        }
        static async Task Main(string[] args) // выбор задачки
        {
            Console.OutputEncoding = Encoding.UTF8;
            while (true) // Бесконечный цикл
            {
                Console.WriteLine("Выберите задачу для выполнения:");
                Console.WriteLine("1 - Задача 1");
                Console.WriteLine("2 - Задача 2");
                Console.WriteLine("3 - Задача 3");
                Console.WriteLine("4 - Задача 4");
                Console.WriteLine("0 - Выйти");

                string choice = Console.ReadLine();
                Task selectedTask = null;

                switch (choice)
                {
                    case "1":
                        selectedTask = Task1();
                        break;
                    case "2":
                        selectedTask = Task2();
                        break;
                    case "3":
                        selectedTask = Task3();
                        break;
                    case "4":
                        selectedTask = Task4();
                        break;
                    case "0":
                        Console.WriteLine("Выход из программы.");
                        return;
                    default:
                        Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
                        break;
                }

                if (selectedTask != null)
                {
                    await selectedTask;
                    Console.WriteLine("Задача завершена.");
                }

                Console.WriteLine("");
            }
        }
        static async Task Task1() // задача 1
        {
            // Создание необобщенной коллекции ArrayList
            ArrayList myCollection = new ArrayList();

            // a) Заполнение 5 случайными числами
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                myCollection.Add(random.Next(1, 100)); // Случайные числа между 1 и 99
            }

            // b) Создание строки
            myCollection.Add("Привет!");

            // c) Удаление заданного элемента
            myCollection.RemoveAt(0);

            // d) Вывод кол-во элементов и коллекции на консоль
            Console.WriteLine($"Кол-во элементов: {myCollection.Count}");
            Console.WriteLine("Коллекций: ");
            foreach (var item in myCollection)
            {
                Console.WriteLine(item);
            }

            // e) Выполнение поиска в коллекции заданного значения
            int index = myCollection.IndexOf("Привет!");
            if (index >= 0)
            {
                Console.WriteLine($"Привет! , ваш индекс: {index}");
            }
            else
            {
                Console.WriteLine("Привет , ваша коллекция ");
            }
        }
        static async Task Task2() // Задача 2
        {
            // a) Создаем и заполняем коллекцию
            Dictionary<string, Stack<string>> dictionary = new Dictionary<string, Stack<string>>();

            // Добавляем данные в коллекцию
            dictionary.Add("First", new Stack<string>(new[] { "A", "B", "C" }));
            dictionary.Add("Second", new Stack<string>(new[] { "D", "E" }));
            dictionary.Add("Third", new Stack<string>(new[] { "F", "G", "H", "I" }));

            // Выводим коллекцию на консоль
            Console.WriteLine("Первая коллекция:");
            PrintDictionary(dictionary);

            // b) Удаляем n элементов
            int n = 2; // количество элементов для удаления
            RemoveItems(dictionary, n);

            // c) Добавляем другие элементы
            AddItems(dictionary);

            // d) Создаем вторую коллекцию и заполняем ее данными из первой
            Dictionary<string, Stack<string>> secondDictionary = new Dictionary<string, Stack<string>>();
            FillSecondDictionary(dictionary, secondDictionary);

            // e) Выводим вторую коллекцию на консоль
            Console.WriteLine("\nВторая коллекция:");
            PrintDictionary(secondDictionary);

            // f) Ищем заданное значение во второй коллекции
            Console.WriteLine("Введите слово для поиска: ");
            string searchValue = Console.ReadLine();
            //string searchValue = "F"; // значение для поиска
            FindValueInDictionary(secondDictionary, searchValue);
        }

        static void PrintDictionary(Dictionary<string, Stack<string>> dictionary)
        {
            foreach (var pair in dictionary)
            {
                Console.Write($"{pair.Key}: ");
                foreach (var item in pair.Value)
                {
                    Console.Write($"{item} ");
                }
                Console.WriteLine();
            }
        }

        static void RemoveItems(Dictionary<string, Stack<string>> dictionary, int n)
        {
            int removedCount = 0;

            foreach (var key in new List<string>(dictionary.Keys))
            {
                if (removedCount >= n) break;
                if (dictionary[key].Count > 0)
                {
                    dictionary[key].Pop();
                    removedCount++;
                }
            }

            Console.WriteLine($"\nУдалено {removedCount} элемента(ов) из коллекции.");
        }

        static void AddItems(Dictionary<string, Stack<string>> dictionary)
        {
            // Используем разные методы добавления
            dictionary["First"].Push("J");
            dictionary["Second"].Push("K");
            dictionary["Third"].Push("L");

            // Добавляем новый элемент с новым ключом
            dictionary.Add("Fourth", new Stack<string>(new[] { "M", "N" }));

            Console.WriteLine("\nЭлементы добавлены в коллекцию.");
        }

        static void FillSecondDictionary(Dictionary<string, Stack<string>> source, Dictionary<string, Stack<string>> target)
        {
            int index = 1;

            foreach (var pair in source)
            {
                Stack<string> newStack = new Stack<string>(pair.Value);
                target.Add(pair.Key, newStack);
            }

            // Если в source меньше элементов, чем в target, генерируем ключи
            while (target.Count < source.Count)
            {
                target.Add($"NewKey{index++}", new Stack<string>());
            }

            // Если в source больше элементов, чем в target, оставляем TValue
            foreach (var key in new List<string>(target.Keys))
            {
                if (!source.ContainsKey(key))
                {
                    // Удаляем ключ, если его нет в source
                    target.Remove(key);
                }
            }
        }

        static void FindValueInDictionary(Dictionary<string, Stack<string>> dictionary, string value)
        {
            bool found = false;
            foreach (var pair in dictionary)
            {
                if (pair.Value.Contains(value))
                {
                    Console.WriteLine($"Значение '{value}' найдено в ключе '{pair.Key}'.");
                    found = true;
                }
            }
            if (!found)
            {
                Console.WriteLine($"Значение '{value}' не найдено.");
            }
        }
        static async Task Task3() // Задача 3
        {
            /*static void Main(string[] args)
            {*/
            // Создаем коллекцию Dictionary
            Dictionary<string, Smartphone> smartphoneDict = new Dictionary<string, Smartphone>
        {
            { "Smartphone1", new Smartphone("Model A", 300) },
            { "Smartphone2", new Smartphone("Model B", 600) },
            { "Smartphone3", new Smartphone("Model C", 450) }
        };

            // Выводим коллекцию на консоль
            Console.WriteLine("Коллекция смартфонов:");
            foreach (var kvp in smartphoneDict)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }

            // Удаляем из коллекции 1 элемент
            smartphoneDict.Remove("Smartphone2");

            // Добавляем другой элемент
            smartphoneDict.Add("Smartphone4", new Smartphone("Model D", 800));

            // Создаем вторую коллекцию и копируем данные
            Dictionary<string, Smartphone> secondSmartphoneDict = new Dictionary<string, Smartphone>();

            int index = 1;
            foreach (var kvp in smartphoneDict)
            {
                secondSmartphoneDict.Add($"NewSmartphone{index++}", kvp.Value);
            }

            // Выводим вторую коллекцию на консоль
            Console.WriteLine("Вторая коллекция смартфонов:");
            foreach (var kvp in secondSmartphoneDict)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }

            // Находим во второй коллекции заданное значение
            double priceToFind = 450;
            bool found = false;
            foreach (var smartphone in secondSmartphoneDict.Values)
            {
                if (smartphone.Price == priceToFind)
                {
                    found = true;
                    break;
                }
            }

            Console.WriteLine(found ? $"Смартфон с ценой {priceToFind} найден." : $"Смартфон с ценой {priceToFind} не найден.");
        }
        //}
        static async Task Task4() // Задача 4
        {
            // Создаем ObservableCollection<Smartphone>
            ObservableCollection<Smartphone> smartphones = new ObservableCollection<Smartphone>();

            // Подписываемся на событие CollectionChanged
            smartphones.CollectionChanged += Smartphones_CollectionChanged;

            // Добавляем элементы в коллекцию
            Console.WriteLine("Добавляем смартфоны (для завершения нажмите еще раз Enter):");

            while (true)
            {
                Console.Write("Введите название смартфона: ");
                string name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    break;
                }

                Console.Write("Введите цену смартфона: ");
                string priceInput = Console.ReadLine();
                if (double.TryParse(priceInput, out double price))
                {
                    smartphones.Add(new Smartphone(name, price));
                }
                else
                {
                    Console.WriteLine("Некорректная цена. Попробуйте еще раз.");
                }
            }

            // Удаляем элемент из коллекции
            Console.WriteLine("Удаляем смартфон:");
            if (smartphones.Count > 0)
            {
                Console.WriteLine("Введите индекс смартфона для удаления (0 - {0}):", smartphones.Count - 1);
                if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < smartphones.Count)
                {
                    smartphones.RemoveAt(index); // Удаляем смартфон по индексу
                }
                else
                {
                    Console.WriteLine("Некорректный индекс.");
                }
            }
            else
            {
                Console.WriteLine("Список смартфонов пуст.");
            }

            Console.ReadLine();
        }

        private static void Smartphones_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Smartphone newItem in e.NewItems)
                {
                    Console.WriteLine(string.Format("Добавлен: {0}", newItem));
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Smartphone oldItem in e.OldItems)
                {
                    Console.WriteLine(string.Format("Удален: {0}", oldItem));
                }
            }
        }
    }
}

