using System;

class Program
{
    static void Main(string[] args)
    {
        string[] questions = new string[]
        {
            "Czy kogut ma białe skrzydła? 1 - TAK, 2 - NIE, 3 - ZALEZY OD WIELKosci",
            "Jak się nazywa królewski pies w Ciezarniach? 1 - Paweł, 2 - Król, 3 - Kucharz",
            "Czy pączek ma krem w środku? 1 - Zawsze, 2 - Nie zawsze, 3 - To取决于你的选择，以下是一个完整的C#代码示例。这个程序包含了三个问题，每个问题有三个选项供玩家选择，并且在每次选择后都会给出一个幽默的回应。最后会显示结果总结。"
        
        string[] answers = new string[]
        {
            "TAK, bo koguty nie są czarne :)", 
            "Kucharz, bo tam jest pączek :P", 
            "Nie zawsze, niektóre pączki mogą być ciasto na wypychanie :D"
        };

        int[] correctAnswers = new int[] { 1, 3, 2 };

        Console.WriteLine("Witaj w grze tekstowej! Zaproponowaliśmy Ci trzy pytania z trzema odpowiedziami. Wybierz 1, 2 lub 3 i dostaniesz humorystyczną odpowiedź.");
        for (int i = 0; i < questions.Length; i++)
        {
            Console.WriteLine(questions[i]);
            int playerChoice;
            while (!int.TryParse(Console.ReadLine(), out playerChoice) || playerChoice < 1 || playerChoice > 3)
            {
                Console.WriteLine("Proszę wprowadzić liczbę 1, 2 lub 3.");
            }

            if (playerChoice == correctAnswers[i])
            {
                Console.WriteLine($"Brawo! {answers[i]}");
            }
            else
            {
                Console.WriteLine($"Niepoprawne! Poprawna odpowiedź to: {correctAnswers[i]}. {answers[i - 1]}");
            }

            Console.WriteLine();
        }

        Console.WriteLine("Podsumowanie gry:");
        for (int i = 0; i < questions.Length; i++)
        {
            if (playerChoice == correctAnswers[i])
            {
                Console.WriteLine($"Pytanie {i + 1}: Poprawna odpowiedź!");
            }
            else
            {
                Console.WriteLine($"Pytanie {i + 1}: Niepoprawna odpowiedź.");
            }
        }

        Console.ReadLine();
    }
}