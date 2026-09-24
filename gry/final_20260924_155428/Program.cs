
using System;
using System.Collections.Generic;
class Program {
    class Q {
        public string Text;
        public string[] Options = new string[3];
        public string[] Funny = new string[3];
    }
    static void Main(){
        var pytania = new List<Q>{new Q{ Text="Gdzie najlepiej ukryć butelkę piwa w szkole?", Options=new[]{"w kieszeni marynarki","na dnie skrzyni z lekarstwami","w plecostym uroczystości"}, Funny=new[]{"Zostajesz na uczestnictwie społecznym","Dostaешь бесплатное лекарство","Twój plecak zaczyna wyglądać jak rzeźba z sztuki cyrkowej"} },
new Q{ Text="Jak najszybciej dostać się do katedry na zajęcia, nie korzystając z schodów?", Options=new[]{"przepchnąć się przez ścianę","spuścić się z dachu","zakopac się przed katedrą i wydłubać się z tyłu"}, Funny=new[]{"Zostajesz zamknięty w kościele na noc","Dostajesz ofertę pracy jako szpieg dla wojska","Twój szaniec zostaje odkryty przez profesora"} },
new Q{ Text="Jakie najwyższe piwo można wytrącić z ręki w karcianym turnieju?", Options=new[]{"piwne sosiska","pleśniewe jabłko","papierowy płachtówka"}, Funny=new[]{"Zostajesz uspokojony przez sąsiada","Twój napój zostaje zatopiony w północnej polarnie","Papierowy płachtówka trafia cię w oko i wywiera na ciebie nieoczekiwany wpływ"} }};
        int wynik=0;
        foreach(var q in pytania){
            Console.WriteLine("\n"+q.Text);
            Console.WriteLine($" 1) {q.Options[0]}");
            Console.WriteLine($" 2) {q.Options[1]}");
            Console.WriteLine($" 3) {q.Options[2]}");
            int wybor;
            while(true){
                Console.Write("Wybierz 1-3: ");
                if(int.TryParse(Console.ReadLine(), out wybor) && wybor>=1 && wybor<=3) break;
                Console.WriteLine("Wpisz 1, 2 lub 3");
            }
            Console.WriteLine("\n>> "+q.Funny[wybor-1]);
            Console.WriteLine("\n[Naciśnij Enter aby iść dalej]");
            Console.ReadLine();
            Console.Clear();
        }
        Console.WriteLine("\n=== PODSUMOWANIE ===");
        Console.WriteLine($"Przeszedłeś {pytania.Count} pytań. Dzięki za grę!");
        Console.WriteLine("Naciśnij Enter aby zakończyć.");
        Console.ReadLine();
    }
}
