
using System;
using System.Collections.Generic;
class Program {
    class Q { public string Text; public string[] Options = new string[3]; public string[] Funny = new string[3]; }
    static void Main(){
        var pytania = new List<Q>{new Q{ Text="Kto pilnuje psa na plaży?", Options=new[]{"policja","lodówka","piwa"}, Funny=new[]{"Policja pilnuje psa od kradzież ewakuatora.","Lodówka schowała psa przed słońcem na piwie.","haha!"} },
new Q{ Text="Jak ukryć listy z okna?", Options=new[]{"pralka","lodówka","lampa"}, Funny=new[]{"Listy schowamy pod pralką!","W lodówce listy stałyby na twardym ledu!","Lampka oświetla nas while kradniemy listy."} },
new Q{ Text="Jak ukryć skrzynię z srebrem przed komisarzem?", Options=new[]{"szafka","plecak","safe"}, Funny=new[]{"Komisarz trzyma klucze do szafki.","Safe przyciąga uwagę komisarza w garażu.","Plecak jest lekki i łatwo ukryć pod dywanem."} }};
        Console.WriteLine("=== GRA v5.1 JANUSZ-FIX ===\n");
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
