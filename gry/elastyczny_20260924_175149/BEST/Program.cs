
using System;
using System.Collections.Generic;
class Program {
    class Q { public string Text; public string[] Options = new string[3]; public string[] Funny = new string[3]; }
    static void Main(){
        var pytania = new List<Q>{new Q{ Text="Jak sprawdzić, czy twoja psina jest w ciemnej pokoju?", Options=new[]{"pozycjonować senną słuchawkę","podpuścić myszy","spywać"}, Funny=new[]{"Śluchawka akustyczna!","Mysze zawsze wiedzą!","Spieszenie!"} },
new Q{ Text="Jak wyrównać różnicę zepsutym grzebieniem i nozdrzem ptaka?", Options=new[]{"użyć levulenu","wziąć notatnik do zapisów","przerwać relację"}, Funny=new[]{"Pucharowy sukces!","Notatnik do refleksji!","Przerwa na kawę!"} },
new Q{ Text="Jak wstawić orczyk do lodówki, aby trzymał się zawsze pusty?", Options=new[]{"na szufladzie","w szafce","na stole"}, Funny=new[]{"Puste powietrze!","Lód na zamówienie!","Orczykowski robotnik!"} }};
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
