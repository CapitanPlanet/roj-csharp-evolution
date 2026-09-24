
using System;
using System.Collections.Generic;
class Program {
    class Q { public string Text; public string[] Options = new string[3]; public string[] Funny = new string[3]; }
    static void Main(){
        var pytania = new List<Q>{new Q{ Text="Jak uniknąć deszczu w otwartym boksie?", Options=new[]{"parasol","kapcie","skrzyni"}, Funny=new[]{"Deszcz cię lubi!","Pęknie po trzyminutowej chwili!","Skrzynia zimniejsza!"} },
new Q{ Text="Jak uniknąć spotkania ze wspaniałym obcym statusem?", Options=new[]{"paszport","targiewka","wiadomość SMS"}, Funny=new[]{"Obcy zasięgnie twojego paszportu!","Targiewka nie sprawdza się do takich celów!","Wiadomość SMS przeczyta twoja żona!"} },
new Q{ Text="Jak uniknąć deszczu na spacerze?", Options=new[]{"parasol","buldazier","krzesło"}, Funny=new[]{"Buldazier wodny!","Deszcz lepi się do parasolu!","Sąsiedzi myślą że schody!"} }};
        Console.WriteLine("=== GRA v4.7 ANTYKOLON ===\n");
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
