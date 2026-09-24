import ollama, subprocess, pathlib, datetime, json, re, sys, random, time, copy

MODEL = "qwen2.5:7b-instruct-q4_K_M"
MODEL_GEN = MODEL
MODEL_JUDGE = MODEL
MODEL_CHILD = MODEL

POPULACJA = 4
POKOLENIA = 3
ILE_PYTAN = 3
ILE_ODP = 3

TEMPLATE = """
using System;
using System.Collections.Generic;
class Program {
    class Q { public string Text; public string[] Options = new string[__ILE_ODP__]; public string[] Funny = new string[__ILE_ODP__]; }
    static void Main(){
        var pytania = new List<Q>{__PYTANIA__};
        Console.WriteLine("=== GRA v5.2 CROSS-FIX ===\\n");
        foreach(var q in pytania){
            Console.WriteLine(q.Text);
            for(int i=0;i<q.Options.Length;i++) Console.WriteLine($" {i+1}) {q.Options[i]}");
            int wybor;
            while(true){
                Console.Write("\\nWybierz 1-__ILE_ODP__: ");
                if(int.TryParse(Console.ReadLine(), out wybor) && wybor>=1 && wybor<=__ILE_ODP__) break;
                Console.WriteLine("Wpisz 1-__ILE_ODP__");
            }
            Console.WriteLine("\\n>> "+q.Funny[wybor-1]+"\\n");
            Console.WriteLine("[Enter]"); Console.ReadLine(); Console.Clear();
        }
        Console.WriteLine("=== KONIEC ==="); Console.ReadLine();
    }
}
"""

def stream_json(prompt, model, system="Tylko JSON po polsku."):
    full=""
    try:
        client = ollama.Client(timeout=120)
        for ch in client.chat(
            model=model,
            messages=[{"role":"system","content":system},{"role":"user","content":prompt}],
            format="json", stream=True,
            options={"temperature": 0.70, "top_p": 0.90, "repeat_penalty": 1.15, "num_predict": 380}
        ):
            full+=ch['message']['content']
    except KeyboardInterrupt:
        print("\n[CTRL+C]"); raise
    except Exception as e:
        print(f"[ERR {model}] {e}"); time.sleep(1)
        return '{"q":"error","o":["a","b","c"],"f":["x","y","z"]}'
    if "</think>" in full:
        full = full.split("</think>")[-1]
    return full.strip()

def score_humor(pytania):
    if isinstance(pytania, dict): pytania=[pytania]
    prompt=f'Oceń 0-10 humor: {json.dumps(pytania, ensure_ascii=False)[:1800]} Zwróć {{"score": liczba}}'
    for _ in range(2):
        try:
            raw=stream_json(prompt, MODEL_JUDGE, 'Tylko JSON {"score":7}')
            m=re.search(r'"score"\s*:\s*(\d+)', raw)
            if m: return max(0,min(10,int(m.group(1))))
            m2=re.search(r'\b([0-9]|10)\b', raw)
            if m2: return max(0,min(10,int(m2.group(1))))
        except KeyboardInterrupt: raise
        except: continue
    return 5

def is_polish_ok(text, kind="q"):
    if not text or len(text) < 2:
        return False
    if re.search(r'[\u4e00-\u9fff\u3040-\u30ff\uac00-\ud7af\u0400-\u04FF]', text):
        return False
    if any(x in text for x in [')))','{{{','}}}','[[[',']]]','最快','蜗牛']):
        return False
    if re.search(r'[čšžřďťňľáéíóúýàèìòùäëïöüâêîôû]', text.lower()):
        return False
    if re.search(r'\b(while|if\s|for\s|safe|the|and|you|with|not\s|is\s)\b', text.lower()):
        if kind in ("o","f"):
            return False
    nonsense=['maconiaku','ogreja','piskiw','wygrodzić','wilcza kogut','wazą na przyjęciu','bez ręk','klonka','ptakosz','szynko','pleśnie','macoń','wędzel','skurcza','przedawkę','wóz za chwilę','mahunka','gajda','suita','kucja','wczesnym wyrazem','pojadąć','mogąć','kosmonauta zjadł','skorupię','trącički','szpic','medoksyénylem','lamпа','szkло','п','укр','符咒']
    if any(n in text.lower() for n in nonsense):
        return False
    letters=re.findall(r'[a-ząćęłńóśźż ]', text.lower())
    if len(letters) < len(text)*0.5:
        return False
    words=len(text.split())
    if kind=="q":
        if words < 3 or words > 12: return False
        if '?' not in text: return False
    elif kind=="o":
        if words < 1 or words > 2: return False
        if len(text) > 25: return False
    elif kind=="f":
        if words < 3 or words > 18: return False
    return True

def gen_one():
    prompt="""Jeden absurd. TYLKO JSON {"q":"pytanie?","o":["1","2","3"],"f":["r1","r2","r3"]}
ZASADY ŻELAZNE:
1. q=śmieszne, poprawne gramatycznie, 6-9 słów, kończy się?
2. o=3 różne, krótkie, ISTNIEJĄCE polskie rzeczowniki, 1-2 słowa, np lodówka, czapka, policja. ZAKAZ: wymyślonych słów typu mahunka, gajda, suita, kucja
3. f=3 różne zdania, KAŻDE musi zawierać słowo z o. Np jak o to lodówka to f to "W lodówce kot zamarzł!"
4. Nie kopiuj przykładów 1:1, pisz tylko po polsku

Przykłady DOBRE:
{"q":"Gdzie schować kota przed teściową?","o":["lodówka","pralka","czapka"],"f":["W lodówce kot robi lody!","Pralka zrobiła z kota sweter!","Teściowa myślała że to czapka i założyła!"]}
{"q":"Jak zaparkować UFO w Biedronce?","o":["nabiał","kasy","toaleta"],"f":["UFO kupuje mleko na nabiale!","UFO stoi 3h w kolejce do kasy!","Kosmici sikają w toalecie Biedronki!"]}

Jeden JSON:"""

    for _ in range(10):
        try:
            raw=stream_json(prompt, MODEL_GEN)
            dec=json.JSONDecoder()
            for i,ch in enumerate(raw):
                if ch=='{':
                    try:
                        obj,_=dec.raw_decode(raw[i:])
                        if all(k in obj for k in ('q','o','f')):
                            q_raw=str(obj['q']).strip()
                            o_raw=list(obj['o'])[:ILE_ODP]
                            f_raw=list(obj['f'])[:ILE_ODP]
                            if not is_polish_ok(q_raw, kind="q"):
                                print(f" [filtr Q] {q_raw[:70]}..."); break
                            if not all(is_polish_ok(str(x), kind="o") for x in o_raw):
                                print(f" [filtr O] {o_raw}"); break
                            if not all(is_polish_ok(str(x), kind="f") for x in f_raw):
                                print(f" [filtr F] {f_raw}"); break
                            q=q_raw.replace('"','\\"')[:110]
                            o=[str(x).replace('"','\\"')[:30] for x in o_raw]
                            f=[str(x).replace('"','\\"')[:110] for x in f_raw]
                            while len(o)<ILE_ODP: o.append("nie wiem")
                            while len(f)<ILE_ODP: f.append("haha!")
                            if len(q)>=10:
                                return {"q":q,"o":o,"f":f}
                    except: continue
        except KeyboardInterrupt: raise
        except: continue
    return None

def gen_osobnik(idx=0, is_mutant=False):
    if is_mutant:
        print(f" >> MUTACJA:")
    else:
        print(f" Gen osobnik {idx}:")
    pytania=[]
    for _ in range(ILE_PYTAN*8):
        if len(pytania)>=ILE_PYTAN: break
        try:
            one=gen_one()
        except KeyboardInterrupt: raise
        if one and one['q'] not in [p['q'] for p in pytania]:
            pytania.append(one)
            print(f" + {one['q'][:70]}...")
    while len(pytania)<ILE_PYTAN:
        pytania.append({"q":"Gdzie schować skarpetę przed pralką?","o":["lodówka","czapka","poduszka"],"f":["Zamarzła w lodówce!","Czapka zjadła skarpetę!","Poduszka się dusi!"]})
    return pytania[:ILE_PYTAN]

def build_cs(pytania):
    if isinstance(pytania, dict): pytania=[pytania]
    cs=[]
    for p in pytania:
        o=",".join([f'"{x}"' for x in p['o']])
        f=",".join([f'"{x}"' for x in p['f']])
        cs.append(f'new Q{{ Text="{p["q"]}", Options=new[]{{{o}}}, Funny=new[]{{{f}}} }}')
    return TEMPLATE.replace("__PYTANIA__", ",\n".join(cs)).replace("__ILE_ODP__", str(ILE_ODP))

def krzyzuj(a,b):
    if isinstance(a,dict): a=[a]
    if isinstance(b,dict): b=[b]
    if random.random()<0.30:
        print(" [MUTACJA] losowy zamiast krzyżowania")
        return gen_osobnik(0, is_mutant=True)
    wariant=random.choice(["kot i pralka","UFO w Biedronce","smok i Żabka","lodówka ucieka","proboszcz i kosmici"])
    prompt=f"""Stwórz {ILE_PYTAN} NOWYCH pytań w stylu A i B, temat: {wariant}.
Tylko JSON tablica: [{{"q":"...?","o":["a","b","c"],"f":["x","y","z"]}}]
Bez wyjaśnień, tylko tablica.
A:{json.dumps(a, ensure_ascii=False)[:600]}
B:{json.dumps(b, ensure_ascii=False)[:600]}"""
    for _ in range(5):
        try:
            raw=stream_json(prompt, MODEL_CHILD, f"Tylko JSON tablica {ILE_PYTAN} pytań.")
            dec=json.JSONDecoder()
            obj=None
            for i,ch in enumerate(raw):
                if ch in '[{':
                    try: obj,_=dec.raw_decode(raw[i:]); break
                    except: continue
            if obj is None: continue
            if isinstance(obj,dict) and "questions" in obj: obj=obj["questions"]
            if isinstance(obj,dict): obj=[obj]
            if not isinstance(obj,list) or len(obj)<ILE_PYTAN: continue
            out=[]
            for d in obj[:ILE_PYTAN]:
                if not isinstance(d,dict): continue
                if not all(k in d for k in ('q','o','f')): continue
                if not is_polish_ok(str(d.get('q','')), kind="q"): continue
                if '?' not in str(d.get('q','')): continue
                if any(d.get('q','')[:22]==p.get('q','')[:22] for p in a+b): continue
                q=str(d['q']).replace('"','\\"')[:110]
                o=[str(x).replace('"','\\"')[:30] for x in list(d['o'])[:ILE_ODP]]
                f=[str(x).replace('"','\\"')[:110] for x in list(d['f'])[:ILE_ODP]]
                while len(o)<ILE_ODP: o.append("nie wiem")
                while len(f)<ILE_ODP: f.append("haha!")
                if not all(is_polish_ok(x, kind="o") for x in o): continue
                if not all(is_polish_ok(x, kind="f") for x in f): continue
                out.append({"q":q,"o":o,"f":f})
            if len(out)==ILE_PYTAN:
                print(f" [KRZYŻÓWKA {wariant}] OK")
                return out
        except KeyboardInterrupt: raise
        except Exception as e:
            print(f"[KRZYZ RETRY] {e}"); continue
    print(" [KRZYŻÓWKA FAIL -> MUTACJA AWARYJNA]")
    return gen_osobnik(0, is_mutant=True)

try:
    base=pathlib.Path(f"gry/elastyczny_{datetime.datetime.now().strftime('%Y%m%d_%H%M%S')}")
    base.mkdir(parents=True, exist_ok=True)
    print(f"ROJ v5.2 CROSS-FIX w {base} | {POPULACJA}x{POKOLENIA} | model={MODEL} | Ctrl+C zapisuje")
    populacja=[gen_osobnik(i) for i in range(POPULACJA)]
    for gen in range(POKOLENIA):
        print(f"\n===== POKOLENIE {gen} =====")
        populacja=[copy.deepcopy(p) if isinstance(p,list) else [copy.deepcopy(p)] for p in populacja]
        oceny=[]
        for os in populacja:
            s=score_humor(os); oceny.append((s,os))
        for i,(s,os) in enumerate(oceny):
            print(f" osobnik{i}: {s}/10 | {os[0]['q'][:65]}...")
        oceny.sort(key=lambda x:x[0], reverse=True)
        print(f">> Najlepszy: {oceny[0][0]}/10 | {oceny[0][1][0]['q'][:60]}")
        if gen==POKOLENIA-1: break
        best=copy.deepcopy(oceny[0][1])
        second=copy.deepcopy(oceny[1][1]) if len(oceny)>1 else copy.deepcopy(best)
        nowa=[best]
        for _ in range(POPULACJA-1):
            child=krzyzuj(best, second)
            nowa.append(copy.deepcopy(child))
        populacja=nowa
    best_q=max([(score_humor(os), os) for os in populacja], key=lambda x:x[0])[1]
    if isinstance(best_q,dict): best_q=[best_q]
    final=base/"BEST"
    final.mkdir(exist_ok=True)
    subprocess.run(["dotnet","new","console","-n","gra","-o",str(final),"--force"], capture_output=True)
    (final/"Program.cs").write_text(build_cs(best_q), encoding="utf-8")
    r=subprocess.run(["dotnet","build"], capture_output=True, text=True, encoding="utf-8", errors="replace", cwd=str(final))
    if r.returncode==0:
        print(f"\n✅ GOTOWE! {final}\nOdpal: dotnet run --project {final}\n{json.dumps(best_q, ensure_ascii=False, indent=2)}")
    else:
        print(r.stdout[-2000:]); print(r.stderr[-2000:])
except KeyboardInterrupt:
    print("\n\n🛑 Ctrl+C - zapisuję...")
    try:
        if 'populacja' in locals() and len(populacja)>0 and 'base' in locals():
            best_q=populacja[0]
            if isinstance(best_q,dict): best_q=[best_q]
            best_q=[p for p in best_q if is_polish_ok(p.get('q',''), kind="q")]
            if not best_q: best_q=[{"q":"Gdzie schować kota?","o":["lodówka","czapka","pralka"],"f":["Zamarzł!","Awans!","Wiruje!"]}]
            final=base/"BEST-PRZERWANE"
            final.mkdir(exist_ok=True)
            subprocess.run(["dotnet","new","console","-n","gra","-o",str(final),"--force"], capture_output=True)
            (final/"Program.cs").write_text(build_cs(best_q), encoding="utf-8")
            subprocess.run(["dotnet","build"], capture_output=True, cwd=str(final))
            print(f"Zapisano: {final}")
    except Exception as e:
        print(f"Błąd: {e}")
    sys.exit(0)