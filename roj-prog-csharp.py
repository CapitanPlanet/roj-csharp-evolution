import ollama, subprocess, os, pathlib, random, datetime

MODEL = "deepseek-r1:8b"
MODEL_FAST = "qwen2.5:7b-instruct-q4_K_M"

def stream(role, prompt, model):
    print(f"\n--- {role} ({model}) ---\n")
    full = ""
    for chunk in ollama.chat(model=model, messages=[
        {"role": "system", "content": f"Jesteś {role}. Znasz C# .NET 8. Piszesz TYLKO kod C#."},
        {"role": "user", "content": prompt}
    ], stream=True):
        t = chunk['message']['content']
        print(t, end='', flush=True)
        full += t
    print("\n")
    if "</think>" in full:
        after = full.split("</think>")[-1].strip()
        # jeśli po </think> jest pusto, to znaczy że cała odpowiedź była w think - weź całość
        if len(after) < 20:
            print("[WARN] Po </think> pusto, biorę całość i czyszczę tagi...")
            after = full.replace("<think>", "").replace("</think>", "")
        full = after
    return full.strip()

def extract_code(text):
    # usuń tagi think które mogły zostać
    text = text.replace("<think>", "").replace("</think>", "")
    if "```csharp" in text:
        s = text.find("```csharp") + 9
        e = text.find("```", s)
        return text[s:e].strip() if e != -1 else text[s:].strip()
    if "```cs" in text:
        s = text.find("```cs") + 5
        e = text.find("```", s)
        return text[s:e].strip() if e != -1 else text[s:].strip()
    if "```" in text:
        s = text.find("```") + 3
        e = text.find("```", s)
        c = text[s:e].strip() if e != -1 else text[s:].strip()
        if c.lower().startswith("csharp"): c = c[6:].strip()
        elif c.lower().startswith("cs"): c = c[2:].strip()
        return c
    return text.strip()

zadanie = input("Co ma być za gra C#? (np. zgadywanie liczby): ")
if not zadanie.strip():
    zadanie = "Prosta gra tekstowa w C# - zgadywanie liczby od 1 do 100, konsola, pętla, licznik prób, TryParse"

timestamp = datetime.datetime.now().strftime("%Y%m%d_%H%M%S")
los = random.randint(100, 999)
folder_gry = pathlib.Path(f"gry/gra_{timestamp}_{los}")
folder_gry.mkdir(parents=True, exist_ok=True)

FILE = folder_gry / "Program.cs"

print(f"\nCEL: {zadanie}")
print(f"FOLDER: {folder_gry.resolve()}")

# FIX 1: encoding=utf-8 żeby nie wywalało się na ąę„”
if not (folder_gry / "gra.csproj").exists():
    print(f"\n[INIT] Tworzę projekt dotnet w {folder_gry}...")
    subprocess.run(["dotnet", "new", "console", "-n", "gra", "-o", str(folder_gry), "--force"], capture_output=True, encoding="utf-8", errors="replace")

plan = stream("ARCHITECT C#", f"{zadanie}. Zaplanuj w 3 punktach: klasa, pętla gry, obsługa input.", MODEL)

code_text = stream("CODER C#", f"Plan: {plan}\nZadanie: {zadanie}\nNapisz CAŁY Program.cs dla .NET 8. Ma zawierać static void Main. Ma działać po dotnet run. TYLKO KOD w bloku ```csharp.", MODEL)
code = extract_code(code_text)

# FIX 2: sprawdź czy kod nie jest pusty
if "static" not in code or "Main" not in code:
    print("\n[WARN] Kod bez Main - model dał pusty wynik, próbuję jeszcze raz z mocniejszym promptem...")
    code_text = stream("CODER C# RETRY", f"Napisz POPRAWNY i KOMPLETNY plik Program.cs w C# .NET 8. Musi mieć class Program {{ static void Main }}. Zadanie: {zadanie}. TYLKO KOD.", MODEL)
    code = extract_code(code_text)

FILE.write_text(code, encoding="utf-8")

for i in range(3):
    print(f"\n### TEST {i+1}/3 - dotnet run w {folder_gry} ###")
    try:
        r = subprocess.run(["dotnet", "run"], capture_output=True, text=True, encoding="utf-8", errors="replace", timeout=15, input="50\n75\n90\n", cwd=str(folder_gry))
        print(r.stdout[:2000])
        if r.stderr:
            print(r.stderr[:2000])
        if r.returncode == 0 and "CS5001" not in r.stderr and "error" not in r.stderr.lower():
            print(f"\n✅ DZIAŁA! Gra w {folder_gry}")
            break
        else:
            feedback = stream("QA", f"Błąd:\n{r.stderr}\nKod:\n{code}\nCo poprawić? Krótko.", MODEL_FAST)
            code_text = stream("CODER FIX", f"Błąd: {r.stderr}\nFeedback: {feedback}\nPopraw cały Program.cs. Musi mieć Main. Zadanie: {zadanie}", MODEL)
            code = extract_code(code_text)
            FILE.write_text(code, encoding="utf-8")
    except Exception as e:
        print(f"Error: {e}")
        import traceback; traceback.print_exc()
        break

print(f"\nGOTOWE: {FILE.resolve()}\nOdpal: dotnet run --project {folder_gry}")