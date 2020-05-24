using System;
using System.IO;

namespace laba7._2
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            TimeSpan selectionWorkTime;
            TimeSpan insertionWorkTime;
            TimeSpan bubbleWorkTime;
            TimeSpan shakerWorkTime;
            TimeSpan shellWorkTime;
            ulong countOfTranspositions = 0, countOfComparisons = 0;
            const int length = 100;
            int[] originalArray = new int[length];
            int[] originalSortedArray = new int[length];
            int[] originalReversSortedArray = new int[length];
            int[] array = new int[length];
            int[] sortedArray = new int[length];
            int[] reverseSortedArray = new int[length];
            int[] arrayForCheck = new int[length];
            for (int i = 0; i < length; i++)
            {
                originalArray[i] = rand.Next(-999, 999);
                array[i] = originalArray[i];
                originalSortedArray[i] = array[i];
                originalReversSortedArray[i] = array[i];
            }
            Console.WriteLine("[SUCCESS] Создан первый массив\n");
            shellSort(originalSortedArray, length, out shellWorkTime, out countOfComparisons, out countOfTranspositions);
            Array.Reverse(originalSortedArray);
            Console.WriteLine("[SUCCESS] Создан отсортированный массив\n");
            shellSort(originalReversSortedArray, length, out shellWorkTime, out countOfComparisons, out countOfTranspositions);
            string directory = @"C:\laba7";
            string path = directory + "\\sorted.dat";
            var directoryInfo = new DirectoryInfo(directory);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            using (StreamReader readArray = new StreamReader(new FileStream(path, FileMode.OpenOrCreate)))
            {
                try
                {
                    try
                    {
                        Console.WriteLine("Чтение массива из файла");
                        for (int i = 0; i < arrayForCheck.Length; i++)
                        {
                            arrayForCheck[i] = int.Parse(readArray.ReadLine());
                        }
                        Console.WriteLine("Проверка массива");
                        shellSort(arrayForCheck, length, out shellWorkTime, out countOfComparisons, out countOfTranspositions);
                        if (countOfTranspositions == 0)
                            Console.WriteLine("[SUCCESS] Массив отсортирован\n");
                        else
                            Console.WriteLine("[ERROR] Массив не отсортирован\n");
                    }
                    catch (ArgumentNullException)
                    {
                        bool isEmpty = true;
                        foreach (int number in arrayForCheck)
                        {
                            if (number != 0)
                                isEmpty = false;
                        }
                        if (isEmpty)
                            Console.WriteLine("[ERROR] Файл пуст!\n");
                        else
                            Console.WriteLine("[ERROR] Массив не заполнен\n");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("[ERROR] Массив сломан\n");
                }
            }
            using (StreamWriter writeArray = new StreamWriter(new FileStream(path, FileMode.Create)))
            {
                Console.WriteLine("Запись массива в файл");
                foreach (int number in originalReversSortedArray)
                {
                    writeArray.Write(number + "\n");
                }
                Console.WriteLine("[SUCCESS] Массив записан\n");
            }
            Console.WriteLine("[SUCCESS] Создан обратный массив\n");
            int select = 0;
            while (select != 4)
            {
                Console.WriteLine("Выберите массив:\n1 - Случайно сгенерированный массив\n2 - Отсортированный по возрастанию массив\n3 - По убыванию отсортированный массив\n4 - Выход\n");
                select = int.Parse(Console.ReadLine());
                switch (select)
                {
                    case 1:
                        Console.WriteLine("Случайно сгенерированный массив:\n");

                        selectionSort(array, length, out selectionWorkTime, out countOfComparisons, out countOfTranspositions);
                        Console.WriteLine("Сортировка выбором {0}\nКоличество перестоновок: {1}\nКоличество сравнений: {2}\n", selectionWorkTime, countOfTranspositions, countOfComparisons);
                        Array.Copy(originalArray, array, length);

                        insertionSort(array, length, out insertionWorkTime, out countOfComparisons, out countOfTranspositions);
                        Console.WriteLine("Сортировка вставками {0}\nКоличество перестоновок: {1}\nКоличество сравнений: {2}\n", insertionWorkTime, countOfTranspositions, countOfComparisons);
                        Array.Copy(originalArray, array, length);

                        bubbleSort(array, length, out bubbleWorkTime, out countOfComparisons, out countOfTranspositions);
                        Console.WriteLine("Сортировка пузырьком {0}\nКоличество перестоновок: {1}\nКоличество сравнений: {2}\n", bubbleWorkTime, countOfTranspositions, countOfComparisons);
                        Array.Copy(originalArray, array, length);

                        shakerSort(array, length, out shakerWorkTime, out countOfComparisons, out countOfTranspositions);
                        Console.WriteLine("Шейкерная сортировка {0}\nКоличество перестоновок: {1}\nКоличество сравнений: {2}\n", shakerWorkTime, countOfTranspositions, countOfComparisons);
                        Array.Copy(originalArray, array, length);

                        shellSort(array, length, out shellWorkTime, out countOfComparisons, out countOfTranspositions);
                        Console.WriteLine("Сортировка Шелла {0}\nКоличество перестоновок: {1}\nКоличество сравнений: {2}\n", shellWorkTime, countOfTranspositions, countOfComparisons);
                        Array.Copy(originalArray, array, length);
                        break;
                    case 2:
                        Array.Copy(originalSortedArray, sortedArray, length);
                        Console.WriteLine("Отсортированный по возрастанию массив:\n");

                        selectionSort(sortedArray, length, out selectionWorkTime, out countOfComparisons, out countOfTranspositions);
                        Console.WriteLine("Сортировка выбором {0}\nКоличество перестоновок: {1}\nКоличество сравнений: {2}\n", selectionWorkTime, countOfTranspositions, countOfComparisons);
                        Array.Copy(originalSortedArray, sortedArray, length);

                        insertionSort(sortedArray, length, out insertionWorkTime, out countOfComparisons, out countOfTranspositions);
                        Console.WriteLine("Сортировка вставками {0}\nКоличество перестоновок: {1}\nКоличество сравнений: {2}\n", insertionWorkTime, countOfTranspositions, countOfComparisons);
                        Array.Copy(originalSortedArray, sortedArray, length);

                        bubbleSort(sortedArray, length, out bubbleWorkTime, out countOfComparisons, out countOfTranspositions);
                        Console.WriteLine("Сортировка пузырьком {0}\nКоличество перестоновок: {1}\nКоличество сравнений: {2}\n", bubbleWorkTime, countOfTranspositions, countOfComparisons);
                        Array.Copy(originalSortedArray, sortedArray, length);

                        shakerSort(sortedArray, length, out shakerWorkTime, out countOfComparisons, out countOfTranspositions);
                        Console.WriteLine("Шейкерная сортировка {0}\nКоличество перестоновок: {1}\nКоличество сравнений: {2}\n", shakerWorkTime, countOfTranspositions, countOfComparisons);
                        Array.Copy(originalSortedArray, sortedArray, length);

                        shellSort(sortedArray, length, out shellWorkTime, out countOfComparisons, out countOfTranspositions);
                        Console.WriteLine("Сортировка Шелла {0}\nКоличество перестоновок: {1}\nКоличество сравнений: {2}\n", shellWorkTime, countOfTranspositions, countOfComparisons);
                        Array.Copy(originalSortedArray, sortedArray, length);
                        break;
                    case 3:
                        Array.Copy(originalReversSortedArray, reverseSortedArray, length);
                        Console.WriteLine("По убыванию отсортированный массивn\n");

                        selectionSort(reverseSortedArray, length, out selectionWorkTime, out countOfComparisons, out countOfTranspositions);
                        Console.WriteLine("Сортировка выбором {0}\nКоличество перестоновок: {1}\nКоличество сравнений: {2}\n", selectionWorkTime, countOfTranspositions, countOfComparisons);
                        Array.Copy(originalReversSortedArray, reverseSortedArray, length);

                        insertionSort(reverseSortedArray, length, out insertionWorkTime, out countOfComparisons, out countOfTranspositions);
                        Console.WriteLine("Сортировка вставками {0}\nКоличество перестоновок: {1}\nКоличество сравнений: {2}\n", insertionWorkTime, countOfTranspositions, countOfComparisons);
                        Array.Copy(originalReversSortedArray, reverseSortedArray, length);

                        bubbleSort(reverseSortedArray, length, out bubbleWorkTime, out countOfComparisons, out countOfTranspositions);
                        Console.WriteLine("Сортировка пузырьком {0}\nКоличество перестоновок: {1}\nКоличество сравнений: {2}\n", bubbleWorkTime, countOfTranspositions, countOfComparisons);
                        Array.Copy(originalReversSortedArray, reverseSortedArray, length);

                        shakerSort(reverseSortedArray, length, out shakerWorkTime, out countOfComparisons, out countOfTranspositions);
                        Console.WriteLine("Шейкерная сортировка {0}\nКоличество перестоновок: {1}\nКоличество сравнений: {2}\n", shakerWorkTime, countOfTranspositions, countOfComparisons);
                        Array.Copy(originalReversSortedArray, reverseSortedArray, length);

                        shellSort(reverseSortedArray, length, out shellWorkTime, out countOfComparisons, out countOfTranspositions);
                        Console.WriteLine("Сортировка Шелла {0}\nКоличество перестоновок: {1}\nКоличество сравнений: {2}\n", shellWorkTime, countOfTranspositions, countOfComparisons);
                        Array.Copy(originalReversSortedArray, reverseSortedArray, length);
                        break;
                }
            }
        }
        static void Swap(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }
        static void selectionSort(int[] array, int length, out TimeSpan selectionWorkTime, out ulong countOfComparisons, out ulong countOfTranspositions)
        {
            countOfComparisons = 0;
            countOfTranspositions = 0;
            DateTime startTime = DateTime.Now;
            for (int i = length - 1; i >= 0; i--)
            {
                int min = i;
                for (int j = i - 1; j >= 0; j--)
                {
                    if (array[j] < array[min])
                        min = j;
                    countOfComparisons++;
                }
                Swap(ref array[i], ref array[min]);
                if (i != min)
                    countOfTranspositions++;
            }
            DateTime endTime = DateTime.Now;
            selectionWorkTime = endTime - startTime;
        }
        static void insertionSort(int[] array, int length, out TimeSpan insertionWorkTime, out ulong countOfComparisons, out ulong countOfTranspositions)
        {
            countOfComparisons = 0;
            countOfTranspositions = 0;
            DateTime startTime = DateTime.Now;
            for (int i = 1; i < length; i++)
            {
                int j = i;
                int temp = array[i];
                while (j > 0 && temp > array[j - 1])
                {
                    countOfComparisons++;
                    array[j] = array[j - 1];
                    j--;
                }
                countOfComparisons++;
                array[j] = temp;
                if (j != i)
                    countOfTranspositions++;
            }
            if (countOfTranspositions > 0)
                countOfTranspositions--;
            DateTime endTime = DateTime.Now;
            insertionWorkTime = endTime - startTime;
        }
        static void bubbleSort(int[] array, int length, out TimeSpan bubbleWorkTime, out ulong countOfComparisons, out ulong countOfTranspositions)
        {
            countOfComparisons = 0;
            countOfTranspositions = 0;
            DateTime startTime = DateTime.Now;
            for (int i = 0; i < length; i++)
            {
                for (int j = length - 1; j > i; j--)
                {
                    if (array[j - 1] < array[j])
                    {
                        Swap(ref array[j - 1], ref array[j]);
                        countOfTranspositions++;
                    }
                    countOfComparisons++;
                }
            }
            DateTime endTime = DateTime.Now;
            bubbleWorkTime = endTime - startTime;
        }
        static void shakerSort(int[] array, int length, out TimeSpan shakerWorkTime, out ulong countOfComparisons, out ulong countOfTranspositions)
        {
            countOfComparisons = 0;
            countOfTranspositions = 0;
            int startIndex = 0;
            DateTime startTime = DateTime.Now;
            do
            {
                for (int i = startIndex; i < length - 1; i++)
                {
                    if (array[i] < array[i + 1])
                    {
                        Swap(ref array[i], ref array[i + 1]);
                        countOfTranspositions++;
                    }
                    countOfComparisons++;
                }
                length--;

                for (int i = length - 1; i > startIndex; i--)
                {
                    if (array[i] > array[i - 1])
                    {
                        Swap(ref array[i], ref array[i - 1]);
                        countOfTranspositions++;
                    }
                    countOfComparisons++;
                }
                startIndex++;
            }
            while (startIndex <= length - 1);
            DateTime endTime = DateTime.Now;
            shakerWorkTime = endTime - startTime;
        }
        static void shellSort(int[] array, int length, out TimeSpan shellWorkTime, out ulong countOfComparisons, out ulong countOfTranspositions)
        {
            countOfComparisons = 0;
            countOfTranspositions = 0;
            DateTime startTime = DateTime.Now;
            int[] steps = { 57, 23, 10, 4, 1 };
            foreach (int step in steps)
            {
                for (int i = step; i < length; i++)
                {
                    int j = i;
                    int temp = array[i];
                    while (j >= step && temp > array[j - step])
                    {
                        countOfComparisons++;
                        array[j] = array[j - step];
                        j -= step;
                    }
                    countOfComparisons++;
                    array[j] = temp;
                    if (j != i)
                        countOfTranspositions++;
                }
            }
            if (countOfTranspositions > 0)
                countOfTranspositions--;
            DateTime endTime = DateTime.Now;
            shellWorkTime = endTime - startTime;
        }
    }
}
