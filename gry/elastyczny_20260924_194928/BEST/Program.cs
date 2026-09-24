
using System;
using System.Collections.Generic;
class Program {
    class Q { public string Text; public string[] Options = new string[3]; public string[] Funny = new string[3]; }
    static void Main(){
        var pytania = new List<Q>{new Q{ Text="Co się dzieje gdy kot zjadł mak?", Options=new[]{"mahunka","gajda","woda"}, Funny=new[]{"Mrożony kot!","Wiruje 1200!","Sąsiad myśli że czapka!"} },
new Q{ Text="Co zrobić gdy pralka zjadła skarpetę?", Options=new[]{"policja","zjeść pralkę","negocjacje"}, Funny=new[]{"Policja szuka!","Pralka smaczniejsza!","Skarpeta chce okupu!"} },
new Q{ Text="Co pozwoli nam odwiedzić koszykówkę na zim?", Options=new[]{"suita","szansa","czas"}, Funny=new[]{"Płaszczyk chciał grać!","Nie mam do czynienia!","Bajt poza prawem."} }};
        Console.WriteLine("=== GRA v4.9 FIX-FILTR ===\n");
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
