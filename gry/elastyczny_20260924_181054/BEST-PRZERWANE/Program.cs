
using System;
using System.Collections.Generic;
class Program {
    class Q { public string Text; public string[] Options = new string[3]; public string[] Funny = new string[3]; }
    static void Main(){
        var pytania = new List<Q>{new Q{ Text="Gdzie schować kot przed teściową?", Options=new[]{"w skrzyni z cukrem","na stołku kuchennym","w butach"}, Funny=new[]{"Cukier się pożarł!","Kot na stole, teściowa na podłodze!","Teściowa myje stopy!"} },
new Q{ Text="Gdzie ukryć teściową przed wampirami?", Options=new[]{"pod domem","w sieni","w lodówce"}, Funny=new[]{"Pod domem pali się!","W sieni brzmi jak zakończenie filmu!","W lodówce zjada jadłodajnicę!"} },
new Q{ Text="Jak zaproponować teściowi podwyżkę?", Options=new[]{"papierem z listem zadań","pleśniącym się biurem","wiosłem"}, Funny=new[]{"Ptesy! Masz teraz wózek do przewracania biur!","Twoja praca jest na tyle mokra, że dostałaś podwyżkę!","Wiosło to kwestia zasięgu - jak daleko możesz pojechać?"} }};
        Console.WriteLine("=== GRA TEKSTOWA v4.4 ROJ ===\n");
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
        Console.WriteLine("=== KONIEC - WYGRAŁEŚ Z ROJEM ===");
        Console.ReadLine();
    }
}
