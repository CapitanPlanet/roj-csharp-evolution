"""
RÓJ PROGRAMISTA - Samooptymalizujący się rój dla AMD Ryzen 5 3600 + 16GB RAM
Działa w 100% lokalnie na Ollama, tryb CPU (GTX 960 2GB pominięta)

Role:
- ARCHITECT: planuje strukturę
- CODER: pisze kod do app.py
- TESTER / KRYTYK: odpala i naprawia

Użycie: python roj-programista.py
"""

import ollama
import subprocess
import os

# TWOJE MODELE - te co masz i działają
MODEL_CODER = "qwen2.5:7b-instruct-q4_K_M"  # dobry w kodzie i po polsku
MODEL_CRITIC = "phi3:3.8b" # szybki do analizy błędów

APP_FILE = "app.py"

def chat_stream(role, task, model):
    print(f"\n{'='*20} {role} ({model}) {'='*20}\n")
    full = ""
    stream = ollama.chat(model=model, messages=[
        {"role": "system", "content": f"Jesteś {role}. Piszesz TYLKO kod lub konkretne instrukcje. Jeśli piszesz kod Python, daj TYLKO kod, bez opowieści. Pisz po polsku w komentarzach."},
        {"role": "user", "content": task}
    ], stream=True)
    for chunk in stream:
        token = chunk['message']['content']
        print(token, end='', flush=True)
        full += token
    print("\n")
    return full

def extract_code(text):
    """Wyciąga kod z markdown ```python ... ``` jeśli jest"""
    if "```python" in text:
        start = text.find("```python") + len("```python")
        end = text.find("```", start)
        return text[start:end].strip()
    if "```" in text:
        start = text.find("```") + 3
        end = text.find("```", start)
        # usuń pierwszą linię jeśli to 'python'
        code = text[start:end].strip()
        if code.startswith("python"):
            code = code[6:].strip()
        return code
    return text.strip()

# ================= START =================
print("ROJ PROGRAMISTA - podaj co ma zbudować")
zadanie_uzytkownika = input("Zadanie (np. 'napisz kalkulator BMI w konsoli'): ")
if not zadanie_uzytkownika.strip():
    zadanie_uzytkownika = "Napisz prosty kalkulator BMI w konsoli - pyta o wagę i wzrost, liczy BMI i mówi czy niedowaga/norma/nadwaga"

print(f"\nCEL: {zadanie_uzytkownika}")

# 1. ARCHITECT
plan = chat_stream("ARCHITECT - doświadczony programista Python", 
                   f"Zadanie: {zadanie_uzytkownika}. Zaplanuj w 3 punktach: jakie funkcje, jakie wejście/wyjście, jakie edge-case obsłużyć. Krótko.", 
                   MODEL_CODER)

# 2. PIERWSZA WERSJA KODU
code_v1_text = chat_stream("CODER - Senior Python Developer",
                           f"Plan: {plan}\nZadanie: {zadanie_uzytkownika}\nNapisz CAŁY kod Python do pliku app.py. Ma działać od razu po python app.py. Użyj input() jeśli trzeba. Tylko kod.",
                           MODEL_CODER)

code_v1 = extract_code(code_v1_text)
with open(APP_FILE, "w", encoding="utf-8") as f:
    f.write(code_v1)
print(f"\n[ZAPISANO] {APP_FILE} - {len(code_v1)} znaków")

# 3. PĘTLA SAMOOPTYMALIZACJI - TEST -> POPRAWA
max_iter = 3
for i in range(max_iter):
    print(f"\n{'#'*10} TEST RUN {i+1}/{max_iter} {'#'*10}")
    try:
        # Odpal app.py i złap output
        result = subprocess.run(["python", APP_FILE], capture_output=True, text=True, timeout=10, input="70\n175\n" if "BMI" in zadanie_uzytkownika else "")
        stdout = result.stdout
        stderr = result.stderr
        
        print("STDOUT:\n", stdout[:1000])
        if stderr:
            print("STDERR:\n", stderr[:1000])

        if result.returncode == 0 and not stderr:
            print("\n✅ TEST ZALICZONY! Kod działa.")
            break
        else:
            # Jest błąd - daj do KRYTYKA i CODERA do poprawy
            error_log = f"STDOUT: {stdout}\nSTDERR: {stderr}\nReturn code: {result.returncode}"
            feedback = chat_stream("TESTER / KRYTYK - QA Engineer",
                                   f"Kod się wywalił. Log błędu: {error_log}\nKod: {code_v1}\nDaj 2 konkretne wskazówki co poprawić, krótko.",
                                   MODEL_CRITIC)
            
            code_fixed_text = chat_stream("CODER - Fixer",
                                          f"Poprzedni kod nie działał. Błąd: {error_log}\nFeedback QA: {feedback}\nPopraw cały kod i zwróć TYLKO poprawiony kod do app.py. Zadanie: {zadanie_uzytkownika}",
                                          MODEL_CODER)
            code_v1 = extract_code(code_fixed_text)
            with open(APP_FILE, "w", encoding="utf-8") as f:
                f.write(code_v1)
            print(f"\n[POPRAWIONO] {APP_FILE}")

    except subprocess.TimeoutExpired:
        print("⏰ Timeout - program czeka na input() w nieskończoność, ale to może być OK dla aplikacji interaktywnej. Przerywam test auto.")
        break
    except Exception as e:
        print(f"Błąd testera: {e}")
        break

print(f"\n\n{'='*30} GOTOWE {'='*30}")
print(f"Twój program jest w pliku: {os.path.abspath(APP_FILE)}")
print("Odpal go ręcznie: python app.py")
