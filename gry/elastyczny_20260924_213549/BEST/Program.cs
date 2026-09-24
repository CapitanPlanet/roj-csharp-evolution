
using System;
using System.Collections.Generic;
class Program {
    class Q { public string Text; public string[] Options = new string[3]; public string[] Funny = new string[3]; }
    static void Main(){
        var pytania = new List<Q>{new Q{ Text="Gdzie ukryć zegarek przed złodziaju?", Options=new[]{"szafka","regal","plecak"}, Funny=new[]{"Zegarek schowany za szybką w szafce!","Na regale zegarek wygląda jak książka!","Plecak to bezpieczne miejsce dla zegarka!"} },
new Q{ Text="Jak ukryć graczka przed synem?", Options=new[]{"pralka","plecak","szafka"}, Funny=new[]{"Pralka ma tajne schronienie!","Graczek w plecaku spał całą noc.","Syn szukał szafki ale znalazł tylko ubrania."} },
new Q{ Text="Jak ukryć buty od marudzącej siostry?", Options=new[]{"szafka","kufel","plecak"}, Funny=new[]{"Buty schowane w szafce marżą o wyjściu.","Kufel z butami zamiast piwa przyciągnie siostrę do pokoju.","Plecak z butami idealnie pasuje pod krzesłem."} }};
        Console.WriteLine("=== GRA v5.2 CROSS-FIX ===\n");
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
        Console.WriteLine("=== KONIEC ==="); Console.ReadLine();
    }
}
