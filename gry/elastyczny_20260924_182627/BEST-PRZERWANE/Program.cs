
using System;
using System.Collections.Generic;
class Program {
    class Q { public string Text; public string[] Options = new string[3]; public string[] Funny = new string[3]; }
    static void Main(){
        var pytania = new List<Q>{new Q{ Text="Jak wygrodzić ogon zmysłowiemu końcowi?", Options=new[]{"laska","plecak","kij"}, Funny=new[]{"Zmyśliwszy mu nowy kierunek!","Każdy sam widzi co tam ma!","haha!"} },
new Q{ Text="Jak zadać testowi IQ psa?", Options=new[]{"szmałowym pędzlem","lazarette do masłego","nie wiem"}, Funny=new[]{"Pies twierdzi że to grypse!","Test okazał się mało inteligentny.","haha!"} },
new Q{ Text="Jak zlikwidować psa bez śladu?", Options=new[]{"woda mineralna","śnieg","pleśniowe jabłko"}, Funny=new[]{"Psa przeobraziło się w płytkę!","Śnieg go zamroził i zmaznął!","Jabłko spowodowało alergię - brak psa!"} }};
        Console.WriteLine("=== GRA TEKSTOWA v4.5 MIX ===\n");
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
        Console.WriteLine("=== KONIEC - POKONAŁEŚ RÓJ ===");
        Console.ReadLine();
    }
}
