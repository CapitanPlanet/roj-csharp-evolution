using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        var questions = new List<Question>
        {
            new Question("Która jest najstarsza planeta w Solarzynie?",
                "1. Ziemia",
                "2. Słońce",
                "3. Merkuriusz",
                3),
            new Question("Jak nazywa się najlepszy ulubiony kiełbasowy w Polsce?",
                "1. Kielbasa Krakowska",
                "2. Kabanosy warszawskie",
                "3. Frankfurter",
                1),
            new Question("Która jest najpiękniejsza gwiazda w nocy?",
                "1. Miecz Szczytna",
                "2. Złoty Różek",
                "3. Śnieg",
                2)
        };

        int score = 0;

        foreach (var question in questions)
        {
            Console.WriteLine(question.Text);
            Console.WriteLine($"A) {question.Options[0]}");
            Console.WriteLine($"B) {question.Options[1]}");
            Console.WriteLine($"C) {question.Options[2]}\n");

            string answer;
            do
            {
                answer = Console.ReadLine();
            } while (answer != "A" && answer != "B" && answer != "C");

            if (answer == question.Answer)
            {
                score++;
                Console.WriteLine("Odpowiedziłeś dobrze! 😊");
            }
            else
            {
                Console.WriteLine("To nie jest poprawna odpowiedź. 🤔");
            }

            Console.WriteLine();
        }

        Console.WriteLine($"Podsumowanie: Poprawnych odpowiedzi: {score}/{questions.Count}");
    }
}

class Question
{
    public string Text { get; set; }
    public string[] Options { get; set; }
    public int Answer { get; set; }

    public Question(string text, string opt1, string opt2, string opt3, int correctAnswer)
    {
        Text = text;
        Options = new[] { opt1, opt2, opt3 };
        Answer = correctAnswer;
    }
}