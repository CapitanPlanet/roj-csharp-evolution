using System;

namespace TextBasedGameV3
{
    class Program
    {
        static void Main(string[] args)
        {
            new Game().Run();
        }
    }

    class Game
    {
        private string[] questions = {
            "1. Jak nazywa się najstarszy smok w Twoim domu?\n" +
            "(1) Smok\n(2) Szymon\n(3) Szczurek",
            "2. Gdzie jest twoja ulubiona butelka wina?\n" +
            "(1) W piwnicy\n(2) Na polce kuchennej\n(3) W skrzyni pod łóżkiem",
            "3. Kto jest Twoim ulubionym herosem superboju?\n" +
            "(1) Spider-Man\n(2) Batman\n(3) Superman"
        };

        private string[] responses = {
            "Hehe, to było proste! Smok to nazwa każdego smoka.\n",
            "No no, szynkas. Szymon nie jest smokiem...\n",
            "Szczurek to dobra odpowiedź na te pytanie, ale najstarszy smok ma bardziej dorosłą imię.\n",
            
            "Piwnica jest idealnym miejscem dla butelki wina!\n",
            "Wszystko w swoim miejscu - na polce kuchennej!\n",
            "Skrzynia pod łóżkiem to miejsce, gdzie trzymamy wszystko, co nie pasuje do garażu.\n",
            
            "Spider-Man ma silne nici! Ciekawie by było z nim bojeć się przeciwników.\n",
            "Batman ma mnóstwo narzędzi do walki. Wygląda na to, że jesteś wielkim fanem kryminału.\n",
            "Superman jest niezrównana postacią. Dajemy mu zwycięstwo!\n"
        };

        public void Run()
        {
            for (int i = 0; i < questions.Length; i++)
            {
                Console.WriteLine(questions[i]);
                int response;
                while (!int.TryParse(Console.ReadLine(), out response) || response < 1 || response > 3)
                {
                    Console.WriteLine("Proszę wybrac liczbę od 1 do 3.");
                }

                Console.WriteLine(responses[response - 1]);
                Console.WriteLine();
            }

            Console.WriteLine("Podsumowanie:");
            for (int i = 0; i < questions.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {questions[i]} Wybrano: {(char)(response - 1)}");
            }
        }
    }
}