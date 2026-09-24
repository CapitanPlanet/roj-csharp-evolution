using System;
using System.Collections.Generic;
using System.Linq;

namespace TextGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var questions = new List<Question>
            {
                new Question(
                    "Pytanie 1",
                    new List<string> { "Odpowiedź 1", "Odpowiedź 2", "Odpowiedź 3" },
                    new List<string> { "Komunikat humorystyczny 1", "Komunikat humorystyczny 2", "Komunikat humorystyczny 3" }
                ),
                new Question(
                    "Pytanie 2",
                    new List<string> { "Wybór A", "Wybór B", "Wybór C" },
                    new List<string> { "Humor 1", "Humor 2", "Humor 3" }
                ),
                new Question(
                    "Pytanie 3",
                    new List<string> { "Opcja X", "Opcja Y", "Opcja Z" },
                    new List<string> { "Śmieszny komunikat 1", "Śmieszny komunikat 2", "Śmieszny komunikat 3" }
                )
            };

            AskQuestionRecursively(questions, 0);
        }

        static void AskQuestionRecursively(List<Question> questions, int index)
        {
            if (index >= questions.Count)
            {
                Console.WriteLine("\nKoniec gry! Dziękujemy za udział.");
                return;
            }

            var currentQuestion = questions[index];
            Console.WriteLine($"\n{currentQuestion.QuestionText}\n");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"[{i + 1}] {currentQuestion.Answers[i]");
            }
            Console.Write("\nWybierz numer odpowiedzi (1-3): ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 3)
            {
                Console.WriteLine("Błąd: Wybierz numer od 1 do 3.");
                AskQuestionRecursively(questions, index);
                return;
            }

            Console.WriteLine($"Wybrałeś: {currentQuestion.Answers[choice - 1]");
            Console.WriteLine($"Humorystyczna odpowiedź: {currentQuestion.HumorResponses[choice - 1]}\n");
            AskQuestionRecursively(questions, index + 1);
        }
    }

    public class Question
    {
        public string QuestionText { get; set; }
        public List<string> Answers { get; set; }
        public List<string> HumorResponses { get; set; }

        public Question(string questionText, List<string> answers, List<string> humorResponses)
        {
            QuestionText = questionText;
            Answers = answers;
            HumorResponses = humorResponses;
        }
    }
}