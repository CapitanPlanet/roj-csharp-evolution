
using System;
using System.Collections.Generic;
class Program {
    class Q { public string Text; public string[] Options = new string[3]; public string[] Funny = new string[3]; }
    static void Main(){
        var pytania = new List<Q>{new Q{ Text="Jak na])))最快的蜗牛}}说爱？", Options=new[]{"na szaleniak z kwiatkiem","wysłać mu szybkę z notatką","zrobić mu spektakularny ślub"}, Funny=new[]{"Zanim się ocknie, już daleko!","Szybkościami miłego nie zdobędziesz!","Spektakularne? To na to potrzeba wolniej poruszającego się zwierzątka!"} },
new Q{ Text="Jak zatrzymać piłkę w powietrzu bez rąk?", Options=new[]{"wykorzystać magnes","wyciągnąć język","nabrać głęboko tchu"}, Funny=new[]{"Magnesowe efekty!","Leng ping!","Trzyma się za język!"} },
new Q{ Text="Jak nakarmić rybę bez użycia rąk?", Options=new[]{"wyrzucić z.portioną do wody","wziąć długopis i narysować jedzenia na akwarium","zaprojektować podwodny robot do dostarczania jedzenia"}, Funny=new[]{"Pchnij wodę!","Magia rysunków!","Podwodne serwisowanie!"} }};
        Console.WriteLine("=== GRA TEKSTOWA v4.2 FINAL ===\n");
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
