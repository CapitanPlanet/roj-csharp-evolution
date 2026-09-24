using System;

class Program
{
    static void Main()
    {
        string[] questions = { "Kto jest twórcą C#", "Jak nazywany był C# przedbrtcie", "Jaki jest najstarszy język programowania?" };
        int[][] answers = new int[][]
        {
            new int[]{1, 2, 3},
            new int[]{4, 5, 6},
            new int[]{7, 8, 9}
        };

        string[] questionResponses = { 
            "No niestety nie jest to prawda. C# zostało zaprojektowane przez Andersa Hejlsberga i Microsoft w latach 2000.", 
            "To była kwestia marketingu! Prawdziwe nazewnictwo przedbrtcie to 'Cool'.", 
            "Jest tyle języków starych, że nie mogę ci tego powiedzieć natychmiast. Zgadujesz!"
        };

        int[] selectedAnswers = new int[3];

        for (int i = 0; i < questions.Length; i++)
        {
            Console.WriteLine($"Pytanie {i + 1}: {questions[i]}");
            Console.WriteLine("Odpowiedzi:");
            for (int j = 0; j < answers[i].Length; j++)
            {
                if (answers[i][j] == 1)
                    Console.WriteLine("1. Ania");
                else if (answers[i][j] == 2)
                    Console.WriteLine("2. Bartek");
                else if (answers[i][j] == 3)
                    Console.WriteLine("3. Czesiek");
                else if (answers[i][j] == 4)
                    Console.WriteLine("4. Kasia");
                else if (answers[i][j] == 5)
                    Console.WriteLine("5. Dawid");
                else if (answers[i][j] == 6)
                    Console.WriteLine("6. Ewa");
                else if (answers[i][j] == 7)
                    Console.WriteLine("7. Filip");
                else if (answers[i][j] == 8)
                    Console.WriteLine("8. Grzegorz");
                else if (answers[i][j] == 9)
                    Console.WriteLine("9. Henryk");
            }

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > answers[i].Length)
            {
                Console.WriteLine("Niepoprawny wybór! Wybierz liczbę od 1 do {0}", answers[i].Length);
            }
            selectedAnswers[i] = answers[i][choice - 1];

            Console.WriteLine(questionResponses[i]);
            Console.ReadLine();
        }

        Console.WriteLine("\nPodsumowanie:");
        for (int i = 0; i < questions.Length; i++)
        {
            if (selectedAnswers[i] == 3)
                Console.WriteLine($"Pytanie {i + 1}: Wybrałeś Czesiek, a prawidłową odpowiedzią jest Czesiek. Dobry wybór!");
            else
                Console.WriteLine($"Pytanie {i + 1}: Twój wybór: {selectedAnswers[i]}");
        }
    }
}