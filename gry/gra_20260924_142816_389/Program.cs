using System;
using System.Collections.Generic;
using System.Linq;

class TextAdventureGame
{
    private List<string> _storyBranches = new List<string>();
    private List<Question> _questions = new List<Question>();
    
    public TextAdventureGame()
    {
        // Inicjalizacja pytań
        _questions.Add(new Question(
            "Jesteś na rozgadierce. Idziesz w lewo czy prawo?",
            new List<string>
            {
                "Idź w lewo - napotkasz stąd drogę powrotną.",
                "Idź w prawo - napotkasz zagadkowe drzwi.",
                "Zostaw teren - może wrócisz innej drogą."
            }));

        _questions.Add(new Question(
            "Co robisz z zagadkowymi drzwiami?",
            new List<string>
            {
                "Otwierasz je - w środku czekają zagrożenia.",
                "Dotknij je - drzwi pulsują magicznie.",
                "Ignoruj je - kontynuuuj spacer."
            }));

        _questions.Add(new Question(
            "Co robisz z zagrożeniem?",
            new List<string>
            {
                "Bij go - atakujesz z siły.",
                "Użyj magicznej fajki - rozwiąż zagadkę.",
                "Uciekaj - zlustrowaj scenerię."
            }));
    }

    public void Run()
    {
        int currentQuestionIndex = 0;
        while (currentQuestionIndex < _questions.Count)
        {
            Question currentQuestion = _questions[currentQuestionIndex];
            
            Console.WriteLine("\n" + currentQuestion.QuestionText);
            for (int i = 0; i < currentQuestion.Answers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {currentQuestion.Answers[i]}");
            }

            int choice;
            do
            {
                Console.Write("Wybierz numer (1-3): ");
                if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
                {
                    Console.WriteLine("Błąd: Podaj liczbę od 1 do 3!");
                }
            } while (choice < 1 || choice > 3);

            string result = currentQuestion.Answers[--choice];
            _storyBranches.Add(result);
            Console.WriteLine("\n" + result + "\nKontynuuj enterem...");
            Console.ReadLine();
            currentQuestionIndex++;
        }

        ShowFinalStory();
    }

    private void ShowFinalStory()
    {
        Console.WriteLine("\n--- Historia twoich decyzji ---");
        foreach (string branch in _storyBranches)
        {
            Console.WriteLine(branch);
        }
    }

    class Question
    {
        public string QuestionText { get; }
        public List<string> Answers { get; }

        public Question(string questionText, List<string> answers)
        {
            QuestionText = questionText;
            Answers = answers;
        }
    }

    static void Main(string[] args)
    {
        TextAdventureGame game = new TextAdventureGame();
        game.Run();
    }
}