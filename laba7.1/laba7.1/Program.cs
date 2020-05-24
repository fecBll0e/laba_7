using System;
using System.Collections.Generic;
using System.IO;

namespace laba7._1
{
    class Program
    {
        enum Pos {П, С, А, None}

        struct Worker
        {
            public string surname;
            public Pos position;
            public int year;
            public decimal salary;

            public void showTable()
            {
                Console.WriteLine("{0, -22} {1, -21} {2, -21} {3, -25}", surname, position, year, salary);
                Console.WriteLine();
            }
        }
        struct Log
        {
            public DateTime time;
            public string operation;
            public string name;

            public void logOutput()
            {
                Console.WriteLine("{0, -20} : {1, -15} {2, -15}", time, operation, name);
            }
        }

        public static void CreateFile(string directory, string path)
        {
            var directoryInfo = new DirectoryInfo(directory);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            FileStream file = new FileStream(path, FileMode.OpenOrCreate);
            file.Close();
        }
        static void Main(string[] args)
        {
            var table = new List<Worker>();

            string directory = @"C:\laba7";
            string path = directory + "\\lab.dat";
            CreateFile(directory, path);

            using (StreamReader readFile = new StreamReader(path))
            {
                while (!readFile.EndOfStream)
                {
                    string surname = readFile.ReadLine();
                    string pos = readFile.ReadLine();
                    var position = Pos.None;
                    if (pos == "П")
                    {
                        position = Pos.П;
                    }
                    else if (pos == "С")
                    {
                        position = Pos.С;
                    }
                    else if (pos == "А")
                    {
                        position = Pos.А;
                    }
                    int year = int.Parse(readFile.ReadLine());
                    decimal salary = decimal.Parse(readFile.ReadLine());
                    Worker newWorker;
                    newWorker.surname = surname;
                    newWorker.position = position;
                    newWorker.year = year;
                    newWorker.salary = salary;
                    table.Add(newWorker);
                }
            }

            var logOfSession = new List<Log>(50);
            DateTime firstTime = DateTime.Now;
            DateTime secondTime = DateTime.Now;
            TimeSpan downtime = secondTime - firstTime;

            bool working = true;
            bool error = true;
            do
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Просмотр таблицы");
                Console.WriteLine("2 - Добавить запись");
                Console.WriteLine("3 - Удалить запись");
                Console.WriteLine("4 - Обновить запись");
                Console.WriteLine("5 - Поиск записей");
                Console.WriteLine("6 - Просмотреть лог");
                Console.WriteLine("7 - Сортировка по дате рождения");
                Console.WriteLine("8 - Выход");
                int selector = int.Parse(Console.ReadLine());
                if (selector == 1)
                {
                    Console.WriteLine("Фамилия{0, -13} Должность{0, -12} Год рождения{0, -10} Зарплата{0, -14}", "");
                    for (int i = 0; i < table.Count; i++)
                    {
                        table[i].showTable();
                    }
                    Console.WriteLine();
                }
                if (selector == 2)
                {
                    Console.Write("Введите фамилию: ");
                    string surname = Console.ReadLine();
                    var position = Pos.А;
                    int year = 0;
                    decimal salary = 0;
                    do
                    {
                        Console.Write("Введите должность(П, С, А): ");
                        string pos = Console.ReadLine();
                        if (pos == "П")
                        {
                            position = Pos.П;
                            error = false;
                        }
                        else if (pos == "С")
                        {
                            position = Pos.С;
                            error = false;
                        }
                        else if (pos == "А")
                        {
                            position = Pos.А;
                            error = false;
                        }
                        else
                        {
                            Console.WriteLine("Введите правильно!");
                            Console.WriteLine();
                        }
                    }
                    while (error);
                    error = true;
                    do
                    {
                        Console.Write("Введите год рождения: ");
                        try
                        {
                            year = int.Parse(Console.ReadLine());
                            error = false;
                        }
                        catch (FormatException)
                        {
                            year = 0;
                            Console.WriteLine("Введите правильно!");
                        }
                    }
                    while (error);
                    error = true;
                    do
                    {
                        Console.Write("Введите зарплату: ");
                        try
                        {
                            salary = decimal.Parse(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
                            error = false;
                        }
                        catch (FormatException)
                        {
                            salary = 0;
                            Console.WriteLine("Введите правильно!");
                        }
                    }
                    while (error);
                    error = true;

                    Worker newWorker;
                    newWorker.surname = surname;
                    newWorker.position = position;
                    newWorker.year = year;
                    newWorker.salary = salary;
                    table.Add(newWorker);

                    Log newLog;
                    newLog.time = DateTime.Now;
                    newLog.operation = "Запись добавлена";
                    newLog.name = surname;
                    logOfSession.Add(newLog);

                    firstTime = newLog.time;
                    TimeSpan secondDowntime = firstTime - secondTime;
                    if (downtime < secondDowntime)
                    {
                        downtime = secondDowntime;
                    }
                    secondTime = newLog.time;
                    Console.WriteLine();
                }
                if (selector == 3)
                {
                    int number = 0;
                    string surname = string.Empty;
                    do
                    {
                        Console.WriteLine("Выберите номер строки для удаления: ");
                        number = int.Parse(Console.ReadLine());
                        if (number > 0 && number <= table.Count)
                        {
                            surname = table[number - 1].surname;
                            table.RemoveAt(number - 1);
                            error = false;
                        }
                        else
                        {
                            Console.WriteLine("Введите правильно!");
                        }
                    }
                    while (error);
                    error = true;

                    Log newLog;
                    newLog.time = DateTime.Now;
                    newLog.operation = "Запись удалена";
                    newLog.name = surname;
                    logOfSession.Add(newLog);

                    firstTime = newLog.time;
                    TimeSpan secondDowntime = firstTime - secondTime;
                    if (downtime < secondDowntime)
                    {
                        downtime = secondDowntime;
                    }
                    secondTime = newLog.time;
                    Console.WriteLine();
                }
                if (selector == 4)
                {
                    string oldSurname = string.Empty;
                    string surname = string.Empty;
                    var position = Pos.А;
                    int year = 0;
                    decimal salary = 0;
                    int number = 0;
                    do
                    {
                        Console.WriteLine("Выберите номер строки для обновления: ");
                        number = int.Parse(Console.ReadLine());
                        if (number > 0 && number <= table.Count)
                        {
                            oldSurname = table[number - 1].surname;
                            Console.Write("Введите фамилию: ");
                            surname = Console.ReadLine();
                            do
                            {
                                Console.Write("Введите должность(П, С, А): ");
                                string pos = Console.ReadLine();
                                if (pos == "П")
                                {
                                    position = Pos.П;
                                    error = false;
                                }
                                else if (pos == "С")
                                {
                                    position = Pos.С;
                                    error = false;
                                }
                                else if (pos == "А")
                                {
                                    position = Pos.А;
                                    error = false;
                                }
                                else
                                {
                                    Console.WriteLine("Введите правильно!");
                                    Console.WriteLine();
                                }
                            }
                            while (error);
                            error = true;
                            do
                            {
                                Console.Write("Введите год рождения: ");
                                try
                                {
                                    year = int.Parse(Console.ReadLine());
                                    error = false;
                                }
                                catch (FormatException)
                                {
                                    year = 0;
                                    Console.WriteLine("Введите правильно!");
                                }
                            }
                            while (error);
                            error = true;
                            do
                            {
                                Console.Write("Введите зарплату: ");
                                try
                                {
                                    salary = decimal.Parse(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
                                    error = false;
                                }
                                catch (FormatException)
                                {
                                    salary = 0;
                                    Console.WriteLine("Введите правильно!");
                                }
                            }
                            while (error);
                        }
                        else
                        {
                            Console.WriteLine("Введите правильно!");
                        }
                    }
                    while (error);
                    error = true;

                    Worker editWorker;
                    editWorker.surname = surname;
                    editWorker.position = position;
                    editWorker.year = year;
                    editWorker.salary = salary;
                    table.Insert(number - 1, editWorker);
                    table.Remove(table[number]);

                    Log newLog;
                    newLog.time = DateTime.Now;
                    newLog.operation = "Запись обновлена";
                    newLog.name = oldSurname + " ==> " + surname;
                    logOfSession.Add(newLog);

                    firstTime = newLog.time;
                    TimeSpan secondDowntime = firstTime - secondTime;
                    if (downtime < secondDowntime)
                    {
                        downtime = secondDowntime;
                    }
                    secondTime = newLog.time;
                    Console.WriteLine();
                }
                if (selector == 5)
                {
                    var pos = Pos.П;
                    do
                    {
                        Console.WriteLine("П - Преподаватель");
                        Console.WriteLine("С - Студент");
                        Console.WriteLine("А - Аспирант");
                        Console.WriteLine("Введите кого вы хотите найти: ");
                        string select = Console.ReadLine();
                        Console.WriteLine();
                        if (select == "П" || select == "С" || select == "А")
                        {
                            if (select == "П")
                                pos = Pos.П;
                            if (select == "С")
                                pos = Pos.С;
                            if (select == "А")
                                pos = Pos.А;
                            for (int i = 0; i < table.Count; i++)
                            {
                                if (table[i].position == pos)
                                {
                                    table[i].showTable();
                                }
                            }
                            error = false;
                        }
                        else
                        {
                            Console.WriteLine("Введите праильно!");
                        }
                    }
                    while (error);
                    error = true;
                    Console.WriteLine();
                }
                if (selector == 6)
                {
                    for (int i = 0; i < logOfSession.Count; i++)
                    {
                        logOfSession[i].logOutput();
                    }
                    Console.WriteLine();
                    Console.WriteLine(downtime + " - Самый долгий период бездействия пользователя");
                    Console.WriteLine();
                }
                if (selector == 7)
                {
                    for (int i = 0; i < table.Count; i++)
                    {
                        int j = i - 1;
                        var temp = table[i];
                        while (j >= 0 && temp.year < table[j].year)
                        {
                            table[j + 1] = table[j];
                            j--;
                        }
                        table[j + 1] = temp;
                    }
                    Console.WriteLine();
                    Console.WriteLine("Таблица отсортирована");
                    Console.WriteLine();
                }
                if (selector == 8)
                {
                    using (StreamWriter newText = new StreamWriter(path, false))
                    {
                        for (int i = 0; i < table.Count; i++)
                        {
                            newText.WriteLine("{0}\n{1}\n{2}\n{3}", table[i].surname, table[i].position, table[i].year, table[i].salary);
                        }
                    }
                    working = false;
                }
                if (selector < 1 || selector > 8)
                {
                    Console.WriteLine("Введите правильно!");
                    Console.WriteLine();
                }
            }
            while (working);
            Console.WriteLine();
        }
    }
}

