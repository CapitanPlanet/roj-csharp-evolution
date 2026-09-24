using System;
using System.Collections.Generic;
using System.Linq;

namespace FunnyTriviaGame
{
    public class Game
    {
        private readonly List<Question> _questions;

        public Game()
        {
            _questions = new List<Question>
            {
                new Question(
                    "Pytanie 1: Jaki jest Twój ulubiony kolor?",
                    new List<string> { "Czerwony", "Niebieski", "Zielony" },
                    new List<string> {
                        "Hej, czerwony to kolor pasa ostroga! Ale to twoja osoba.",
                        "Niebieski? To kolor nieba, a ty jesteś jak niebo.",
                        "Zielony? To kolor lasu, a ty chcesz być w lesie? Może."
                    }
                ),
                new Question(
                    "Pytanie 2: Jaką muzykę słuchasz najczęściej?",
                    new List<string> { "Rock", "Pop", "Klasyczna" },
                    new List<string> {
                        "Rock? Więc jesteś jak moja stara tablica nożnej!",
                        "Pop? Więc jesteś jak ta sama ścieżka jak ja w parku.",
                        "Klasyczna? Więc jesteś jak biblioteka w sobotę."
                    }
                ),
                new Question(
                    "Pytanie 3: Jakie jest Twoje ulubione jedzenie?",
                    new List<string> { "Pizza", "Sushi", "Pasta" },
                    new List<string> {
                        "Pizza? Więc jesteś jak moja pokojówka, która ma też miłość.",
                        "Sushi? Więc jesteś jak rybak, który zaraz ma łowić jajko kajak.",
                        "Pasta? Więc jesteś jak moja mama, która kocha cię jak pasta."
                    }
                )
            ];
        }

        public void Run()
        {
            Console.WriteLine("Witaj w Humorystycznej Grajce! Podróżujemy przez świat zabawnych pytań.");
            Console.WriteLine("Wybierz odpowiedź, podając numer (1, 2 lub 3). Naciśnij Enter, aby kontynuować.");
            Console.WriteLine();

            for (int i = 0; i < _questions.Count; i++)
            {
                Console.WriteLine($"Pytanie {i + 1}:");
                _questions[i].Display();
                int choice;
                do
                {
                    Console.Write("Twoj wybor: ");
                    Console.Write($"Odpowiedź 1: {_questions[i].Answers[0]}\n");
                    Console.Write($"Odpowiedź 2: {_questions[i].Answers[1]}\n");
                    Console.Write($"Odpowiedź 3: {_questions[i].Answers[2]}\n");
                    Console.Write("Wybierz (1-3): ");
                    
                    if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
                    {
                        Console.WriteLine("Nieprawidłowy wybór! Spróbuj jeszcze raz.");
                    }
                } while (choice < 1 || choice > 3);

                Console.WriteLine();
                Console.WriteLine($"Odpowiedź: {_questions[i].Answers[choice - 1]}");
                Console.WriteLine();
                Console.WriteLine($"Humorystyczna odpowiedź: {_questions[i].FunnyAnswers[choice - 1]}");
                Console.WriteLine();
                Console.WriteLine("Aby przejść do kolejnego pytania, naciśnij Enter.");
                Console.ReadLine();
                Console.WriteLine();
            }

            Console.WriteLine("\nPodsumowanie:");
            Console.WriteLine("Dziękujemy za udział w grze! To była zabawna przygoda.");
            Console.WriteLine("Do następnego razu!");
        }
    }

    public class Question
    {
        public string QuestionText { get; }
        public List<string> Answers { get; }
        public List<string> FunnyAnswers { get; }

        public Question(string questionText, List<string> answers, List<string> funnyAnswers)
        {
            QuestionText = questionText;
            Answers = answers;
            FunnyAnswers = funnyAnswers;
        }

        public void Display()
        {
            Console.WriteLine(QuestionText);
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Run();
        }
    }
}