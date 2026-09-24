using System;

class Game
{
    static void Main()
    {
        Play();
    }

    public static void Play()
    {
        Console.Clear();

        Console.WriteLine("Witaj w grze tekstowej!");
        Console.WriteLine("Odpowiadaj 1, 2 lub 3 na pytania.");
        Console.WriteLine("Naciśnij Enter aby kontynuować...");
        Console.ReadLine();

        Question q1 = new Question(
            "Kto stworzył .NET Framework?",
            new[] { "Bill Gates", "Steve Jobs", "Bartłomiej Nowak" },
            0);
        
        Question q2 = new Question(
            "Jaki jest najpopularniejszy język programowania?",
            new[] { "C#", "Python", "Java" },
            1);

        Question q3 = new Question(
            "Jak nazywa się najpotężniejsza postać w Marvelu?",
            new[] { "Thor", "Iron Man", "Captain America" },
            0);
        
        Console.Clear();
        Console.WriteLine("Pytanie 1");
        q1.ShowQuestion();
        q1.HandleAnswer();

        Console.Clear();
        Console.WriteLine("Pytanie 2");
        q2.ShowQuestion();
        q2.HandleAnswer();

        Console.Clear();
        Console.WriteLine("Pytanie 3");
        q3.ShowQuestion();
        q3.HandleAnswer();

        ShowSummary(q1, q2, q3);
    }

    public static void ShowSummary(Question q1, Question q2, Question q3)
    {
        Console.Clear();
        Console.WriteLine("Podsumowanie:");
        
        if (q1.IsCorrect) Console.WriteLine("Pytanie 1: Poprawna odpowiedź!");
        else Console.WriteLine("Pytanie 1: Niepoprawna odpowiedź!");

        if (q2.IsCorrect) Console.WriteLine("Pytanie 2: Poprawna odpowiedź!");
        else Console.WriteLine("Pytanie 2: Niepoprawna odpowiedź!");

        if (q3.IsCorrect) Console.WriteLine("Pytanie 3: Poprawna odpowiedź!");
        else Console.WriteLine("Pytanie 3: Niepoprawna odpowiedź!");

        Console.ReadLine();
    }
}

class Question
{
    private string question;
    private string[] answers;
    private int correctAnswerIndex;

    public bool IsCorrect { get; private set; }

    public Question(string question, string[] answers, int correctAnswerIndex)
    {
        this.question = question;
        this.answers = answers;
        this.correctAnswerIndex = correctAnswerIndex;
    }

    public void ShowQuestion()
    {
        Console.WriteLine(question);
        for (int i = 0; i < answers.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {answers[i]}");
        }
    }

    public void HandleAnswer()
    {
        bool isCorrect = false;
        while (!isCorrect)
        {
            Console.Write("Twoja odpowiedź: ");
            string input = Console.ReadLine();
            int index = int.Parse(input) - 1;

            if (index >= 0 && index < answers.Length)
            {
                if (index == correctAnswerIndex)
                {
                    Console.WriteLine("Brawo! To prawidłowa odpowiedź.");
                    IsCorrect = true;
                    isCorrect = true;
                }
                else
                {
                    Console.WriteLine("Niepoprawna odpowiedź. Spróbuj ponownie.");
                }
            }
            else
            {
                Console.WriteLine("Podano nieprawidłową opcję. Spróbuj ponownie.");
            }
        }
    }
}