using System;
using System.Collections.Generic;
using System.Linq;

public class Question
{
    public string Text { get; set; }
    public List<string> Options { get; set; }
    public List<string> CorrectFeedback { get; set; }
    public List<string> WrongFeedback { get; set; }
}

public class Program
{
    static List<Question> questions = new List<Question>
    {
        new Question
        {
            Text = "Jakie jest największe число?",
            Options = new List<string> { "100", "200", "300" },
            CorrectFeedback = new List<string> { "Brawo! Masz rację.", "Super! To prawda.", "Dobrze robisz!" },
            WrongFeedback = new List<string> { "Zła odpowiedź.", "Spróbuj jeszcze raz.", "Błąd." }
        },
        // Dodaj kolejne pytania
    };

    static void Main()
    {
        int score = 0;
        int totalQuestions = questions.Count;
        int correctAnswers = 0;

        foreach (var question in questions)
        {
            Console.WriteLine(question.Text);
            for (int i = 0; i < question.Options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {question.Options[i]}");
            }

            Console.Write("Wybierz odpowiedź: ");
            int userChoice = int.Parse(Console.ReadLine()) - 1;

            if (userChoice >= 0 && userChoice < question.Options.Count)
            {
                bool isCorrect = userChoice == 0; // Zakładamy, że pierwsza odpowiedź jest poprawna
                Console.WriteLine(isCorrect ? 
                    question.CorrectFeedback[new Random().Next(question.CorrectFeedback.Count)] :
                    question.WrongFeedback[new Random().Next(question.WrongFeedback.Count)]);
            }
            else
            {
                Console.WriteLine("Nieprawidłowy wybór.");
            }
        }

        Console.WriteLine("\nWynik:");
        Console.WriteLine($"Poprawnych odpowiedzi: {correctAnswers} z {totalQuestions}");
        Console.WriteLine($"Ocena: {(correctAnswers / (double)totalQuestions) * 100}%");
    }
}