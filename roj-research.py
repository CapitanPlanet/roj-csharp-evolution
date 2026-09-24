"""
RÓJ RESEARCHER - Rój do researchu i raportów
Działa lokalnie na Ryzen 5 3600 + 16GB RAM

Role:
- SEARCHER: (symulowany research - na razie bazuje na wiedzy modelu, później podłączymy DuckDuckGo)
- ANALYST: wyciąga wnioski
- WRITER + CRITIC: pętla samooptymalizacji raportu

Użycie: python roj-researcher.py
"""

import ollama
import os

MODEL_SMART = "qwen2.5:7b-instruct-q4_K_M"
MODEL_FAST = "phi3:3.8b"

def stream(role, prompt, model):
    print(f"\n--- {role} ({model}) ---\n")
    full = ""
    for chunk in ollama.chat(model=model, messages=[
        {"role": "system", "content": f"Jesteś {role}. Pisz po polsku, konkretnie, z przykładami."},
        {"role": "user", "content": prompt}
    ], stream=True):
        t = chunk['message']['content']
        print(t, end='', flush=True)
        full += t
    print("\n")
    return full

topic = input("Temat do zbadania (np. 'czy warto kupić PS5 w 2026'): ")
if not topic.strip():
    topic = "3 zastosowania małych lokalnych modeli LLM w małej firmie w Polsce"

print(f"\nTEMAT: {topic}\n")

# 1. RESEARCH
research = stream("RESEARCHER", 
                  f"Temat: {topic}. Podaj 5 kluczowych faktów, trendów, liczb. Jeśli nie wiesz, zaznacz że to estymacja. Krótko, w punktach.",
                  MODEL_SMART)

# 2. ANALYST
analysis = stream("ANALYST",
                  f"Na bazie faktów: {research}\nTemat: {topic}\nWyciągnij 3 wnioski, 2 szanse, 2 zagrożenia.",
                  MODEL_SMART)

# 3. WRITER
draft = stream("WRITER",
               f"Fakty: {research}\nAnaliza: {analysis}\nNapisz raport 200 słów, struktura: Wstęp, Fakty, Wnioski, Rekomendacja.",
               MODEL_SMART)

# 4. CRITIC + FINAL
critic = stream("KRYTYK - Redaktor", 
                f"Oceń raport 1-10, daj 2 rady jak go poprawić żeby był bardziej praktyczny dla Kowalskiego. Raport: {draft}",
                MODEL_FAST)

final = stream("WRITER v2",
               f"Popraw raport wg uwag krytyka: {critic}\nStary raport: {draft}\nFakty: {research}",
               MODEL_SMART)

# ZAPIS
with open("raport.md", "w", encoding="utf-8") as f:
    f.write(f"# Raport: {topic}\n\n## Research\n{research}\n\n## Analiza\n{analysis}\n\n## FINAL\n{final}\n")

print(f"\n✅ Zapisano raport do {os.path.abspath('raport.md')}")
