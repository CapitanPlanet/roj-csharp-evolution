
using System;
using System.Collections.Generic;
class Program {
    class Q { public string Text; public string[] Options = new string[3]; public string[] Funny = new string[3]; }
    static void Main(){
        var pytania = new List<Q>{new Q{ Text="Jak uczynić z babcią psa?", Options=new[]{"w skrzynce do butów","na papierosewo","na kanapa poduszki"}, Funny=new[]{"Babcia na nogach!","Papierosy dla dziadka!","Poduszkowy spa!(?)"} },
new Q{ Text="Jak uruchomić komputer za pomocą kielisza wody?", Options=new[]{"przez skanowanie odcisków palców na kieliszku","poprzez podłączenie go do gniazda elektrostatycznego","odrzucając go z okna i czekając, aż sam uruchomi się"}, Funny=new[]{"Magia w działaniu!","Energetyka komputerowa!","Uruchamianie za pomocą grawitacji!"} },
new Q{ Text="Gdzie umieszczyć kogut w naczyniu z wodą, aby nie umarł oduptools?", Options=new[]{"w wannie z limonką","na stole z bułką","na stropie z medalem"}, Funny=new[]{"W wannie z limonką!","Na stole z bułką!","Na stropie z medalem!"} }};
        Console.WriteLine("=== GRA TEKSTOWA v4.3 FINAL ===\n");
        foreach(var q in pytania){
            Console.WriteLine(q.Text);
            for(int i=0;i<q.Options.Length;i++) Console.WriteLine($" {i+1}) {q.Options[i]}");
            int wybor;
            while(true){
                Console.Write("\nWybierz 1-3: ");
                if(int.TryParse(Console.ReadLine(), out wybor) && wybor>=1 && wybor<=3) break;
                Console.WriteLine("Wpisz liczbę 1-3");
            }
            Console.WriteLine("\n>> "+q.Funny[wybor-1]+"\n");
            Console.WriteLine("[Enter -> dalej]"); Console.ReadLine(); Console.Clear();
        }
        Console.WriteLine("=== PODSUMOWANIE ===");
        Console.WriteLine($"Przeszedłeś {pytania.Count} pytań. Koniec!");
        Console.ReadLine();
    }
}
