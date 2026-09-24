
using System;
using System.Collections.Generic;
class Program {
    class Q { public string Text; public string[] Options = new string[3]; public string[] Funny = new string[3]; }
    static void Main(){
        var pytania = new List<Q>{new Q{ Text="Jak uniknąć zająca w lesie?", Options=new[]{"bawełna","sosny","wiadrami"}, Funny=new[]{"Zając kupił bawełnę!","Sosne trąciłeś?","Wiadro wywala wodę!"} },
new Q{ Text="Jak zabić kaczki bez ręk?", Options=new[]{"lazienka","telefon","nie wiem"}, Funny=new[]{"Kaczka wyszła się umyć!","Dzwonic będzie?!","haha!"} },
new Q{ Text="Jak uniknąć zwierząt w lesie?", Options=new[]{"szlak","śnieg","puchary"}, Funny=new[]{"Zwierzaki spały!","Ścieżka ma paliwę!","Puchary dla psów!"} }};
        Console.WriteLine("=== GRA TEKSTOWA v4.6 CLEAN ===\n");
        foreach(var q in pytania){
            Console.WriteLine(q.Text);
            for(int i=0;i<q.Options.Length;i++) Console.WriteLine($" {i+1}) {q.Options[i]}");
            int wybor;
            while(true){
                Console.Write("\nWybierz 1-3: ");
                if(int.TryParse(Console.ReadLine(), out wybor) && wybor>=1 && wybor<=3) break;
                Console.WriteLine("Wpisz 1-3");
            }
            Console.WriteLine("\n>> "+q.Funny[wybor-1]+"\n");
            Console.WriteLine("[Enter]"); Console.ReadLine(); Console.Clear();
        }
        Console.WriteLine("=== KONIEC - POKONALES ROJ ==="); Console.ReadLine();
    }
}
