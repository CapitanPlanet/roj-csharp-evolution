using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureGame
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Question> questions = new List<Question>
            {
                new Question(
                    "Pytanie 1: Jaka jest twoja ulubiona kolorowa zwierzyna? (1,2,3)",
                    new List<Answer>
                    {
                        new Answer("Kot", "Ojej, koty są najlepsze!"),
                        new Answer("Pies", "Pies to najlepszy przyjaciel!"),
                        new Answer("Słoń", "Słoń? A może ty chcesz być olśniewany?")
                    }
                ),
                new Question(
                    "Pytanie 2: Jaką masz ulubioną postać z bajki? (1,2,3)",
                    new List<Answer>
                    {
                        new Answer("Czarna Karawana", "Hmm, Czarna Karawana? Może masz gusta na tematy z niepewnością."),
                        new Answer("Czarna Kula", "Czarna Kula? To coś z kosmosu? Może jesteś astronomem."),
                        new Answer("Czarna Owca", "Czarna Owca... Może masz duszę artysty.")
                    }
                ),
                new Question(
                    "Pytanie 3: Co jest lepsze? (1,2,3)",
                    new List<Answer>
                    {
                        new Answer("Ciepło", "Ciepło? Może masz lęk przed zimą."),
                        new Answer("Zimno", "Zimno? To znak, że masz ogień w duszy."),
                        new Answer("Średnio", "Średnio? To znak, że jesteś równowagowcem.")
                    }
                )
            };

            int score = 0;

            foreach (var question in questions)
            {
                Console.WriteLine(question.QuestionText);
                for (int i = 0; i < question.Answers.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {question.Answers[i].Text}");
                }

                while (true)
                {
                    Console.Write("Wybierz odpowiedź (1-3): ");
                    string input = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(input)) continue;

                    if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 3)
                    {
                        Console.WriteLine(question.Answers[choice - 1].HumorResponse);
                        score += choice;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Nieprawidłowy wybór. Sprobuj jeszcze raz.");
                    }
                }

                Console.WriteLine("\nNaciśnij Enter, aby kontynuować...");
                Console.ReadLine();
                Console.Clear();
            }

            Console.WriteLine("\nPodsumowanie:");
            Console.WriteLine($"Odpowiedzi: {score} punktów");
            Console.WriteLine($"Twoje humorystyczne odpowiedzi: {Environment.NewLine}{string.Join(Environment.NewLine, questions.Select(q => q.Answers[0].HumorResponse)))");
            Console.WriteLine("\nDziękujemy za rozgrywkę! Naciśnij dowolny klawisz, aby zakończyć.");
            Console.ReadKey();
        }
    }

    public class Question
    {
        public string QuestionText { get; set; }
        public List<Answer> Answers { get; set; }

        public Question(string questionText, List<Answer> answers)
        {
            QuestionText = questionText;
            Answers = answers;
        }
    }

    public class Answer
    {
        public string Text { get; set; }
        public string HumorResponse { get; set; }

        public Answer(string text, string humorResponse)
        {
            Text = text;
            HumorResponse = humorResponse;
        }
    }
}