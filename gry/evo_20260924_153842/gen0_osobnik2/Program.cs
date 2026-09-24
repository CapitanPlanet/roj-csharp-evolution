using System;

class QuizGame
{
    static void Main()
    {
        string[] questions = new string[]
        {
            "Kto stworzył C#?",
            "Jak nazywany jest szereg liczb Fibonacci?",
            "Jaki język programowania jest oparty na skryptach?"
        };

        int[] correctAnswers = new int[] { 3, 1, 2 };

        string[] options = new[]
        {
            "1. Microsoft\n" +
            "2. Apple\n" +
            "3. JetBrains",
            
            "1. Liczby Fibonacciego\n" +
            "2. Liczby Euklidesa\n" +
            "3. Liczby Prastety",
            
            "1. Python\n" +
            "2. JavaScript\n" +
            "3. Java"
        };

        string[] humorResponses = new[]
        {
            "Nie masz szans, Microsoft to tylko nazwa firmy!",
            "Euklides nie ma nic wspólnego z liczbami, to matematyk!",
            "Python i Java są skompilowane, a nie skrypty!"
        };

        int attempts = 3;
        string[] userAnswers = new string[3];
        bool isWinner = true;

        for (int i = 0; i < questions.Length && isWinner; i++)
        {
            Console.WriteLine(questions[i]);
            Console.WriteLine(options[i]);

            while (attempts > 0)
            {
                Console.Write("Wybierz odpowiedź (1, 2 lub 3): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 3)
                {
                    userAnswers[i] = input;
                    break;
                }
                else
                {
                    attempts--;
                    Console.WriteLine("Nie ma takiej opcji! Pozostało prób: " + attempts);
                    if (attempts == 0)
                    {
                        isWinner = false;
                        Console.WriteLine(humorResponses[i]);
                        break;
                    }
                }
            }

            if (isWinner && correctAnswers[i] - 1 == int.Parse(userAnswers[i]))
            {
                Console.WriteLine("Brawo! Poprawna odpowiedź!");
            }
            else
            {
                isWinner = false;
                Console.WriteLine(humorResponses[i]);
            }

            Console.WriteLine();
        }

        if (isWinner)
        {
            Console.WriteLine("Gratulacje, wygrałeś grę!");
        }
        else
        {
            Console.WriteLine("Nie wygrywasz. Spróbuj jeszcze raz.");
        }

        Console.WriteLine("Podsumowanie:");
        for (int i = 0; i < questions.Length; i++)
        {
            Console.WriteLine($"Pytanie {i + 1}: Twój wybór - {userAnswers[i]}, Poprawna odpowiedź - {correctAnswers[i]}");
        }
    }
}