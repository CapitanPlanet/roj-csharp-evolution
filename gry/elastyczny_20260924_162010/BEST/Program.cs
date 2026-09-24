
using System;
using System.Collections.Generic;
class Program {
    class Q {
        public string Text;
        public string[] Options = new string[3];
        public string[] Funny = new string[3];
    }
    static void Main(){
        var pytania = new List<Q>{new Q{ Text="Czy ciasto może być wariatem?", Options=new[]{"Tak, ma problemy z masą.","Nie, jest słodyczą.","Zaraz wyjdzie na wolność."}, Funny=new[]{"Przestało piec i zaczęło psikuszać.","Wyobraziło sobie ucieczki do Kanału Gdańskiego","Połapało się ze szmalcownikiem i postanowiło podstępnie zaatakować inne ciasta."} },
new Q{ Text="Czy kawa ma prawo jazdy?", Options=new[]{"Tak, jest przestronna.","Nie, za słodka.","Absurdalnie, może samodzielnie kierować się do łazienki."}, Funny=new[]{"Zaczęła prowadzić pociągiem i zderzyła się z wysokim drzewem.","Rzuciła kawą konduktora, ale od razu przyszedł pomocnik.","Wyskoczyła z auta i zaczęła robić mocny napitki."} },
new Q{ Text="Czy szklanka ma na imię?", Options=new[]{"Tak, to jest jej nazwisko.","Nie, ona jest bezimienistym obiektem.","Absurdalnie, jest to Imieniem Naocznym."}, Funny=new[]{"Zrobiła kawałek papieru i napisała na nim 'Szklanka' a potem się rozbiła.","Przyszedł jej do głowy pomysł nazywania się Szyba, ale nie zdobyła się na to.","Przejechała przez nią samolot i ocaliła trudny moment."} }};
        Console.WriteLine("=== GRA TEKSTOWA vELASTYCZNA ===\n");
        foreach(var q in pytania){
            Console.WriteLine(q.Text);
            for(int i=0;i<q.Options.Length;i++) Console.WriteLine($" {i+1}) {q.Options[i]}");
            int wybor;
            while(true){
                Console.Write("\nWybierz 1-3: ");
                var s=Console.ReadLine();
                if(int.TryParse(s, out wybor) && wybor>=1 && wybor<=3) break;
                Console.WriteLine("Wpisz liczbę 1-3");
            }
            Console.WriteLine("\n>> "+q.Funny[wybor-1]+"\n");
            Console.WriteLine("[Enter -> dalej]");
            Console.ReadLine();
            Console.Clear();
        }
        Console.WriteLine("=== PODSUMOWANIE ===");
        Console.WriteLine($"Zaliczyłeś {pytania.Count} pytań. Dzięki za grę!");
        Console.ReadLine();
    }
}
