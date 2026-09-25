# Rój - Ewolucyjny Generator Gier C# z Ollama

Ewolucyjny roj który generuje absurdalne quizy C# za pomocą lokalnych modeli LLM (qwen2.5:7b). Projekt Michała - optymalizowany pod słabszy sprzęt.

[Python](https://img.shields.io/badge/Python-3.10%2B-blue) [C#](https://img.shields.io/badge/C%23-.NET%208-green) [Ollama](https://img.shields.io/badge/Ollama-qwen2.5%3A7b-orange)

## Co to robi?

- Generuje 4x3 populacji pytań typu "Gdzie ukryć zegarek przed złodziejem?"
- Filtruje bełkot: `mahunka`, `gajda`, chińskie `符咒`, ruskie `ло`, czeskie `čšž`, angielskie `while/safe`
- Ewolucja: krzyżowanie + mutacja, ocena humoru 0-10 przez LLM
- Buduje gotową grę C# `dotnet run`

Ostatni wynik v5.2:
```json
{
  "q": "Gdzie ukryć zegarek przed złodziaju?",
  "o": ["szafka", "regal", "plecak"],
  "f": ["Zegarek schowany za szybką w szafce!", "Na regale zegarek wygląda jak książka!"]
}
```

## Wymagania

- Python 3.10+
- .NET 8 SDK
- Ollama: https://ollama.com

```powershell
ollama pull qwen2.5:7b-instruct-q4_K_M
```

## Instalacja

```powershell
git clone https://github.com/TWOJ_NICK/roj-csharp-evolution.git
cd roj-csharp-evolution
pip install -r requirements.txt
```

## Użycie

```powershell
ollama ps
# jeśli działa model to stop żeby nie żarł RAMu
ollama stop qwen2.5:7b-instruct-q4_K_M
python roj-prog-csharp-gen.py
```

Gra ląduje w `gry/elastyczny_YYYYMMDD_HHMMSS/BEST/`:

```powershell
dotnet run --project gry/elastyczny_*/BEST
```

## v5.2 CROSS-FIX - co naprawia?

- `is_polish_ok(kind="q/o/f")` - osobne limity słów dla pytań/opcji/funny
- Blokada angielskiego, czeskiego, ruskiego, chińskiego
- Temperatura 0.70 zamiast 0.85 = mniej `trącički`
- Krzyżówka naprawiona - prompt bez zbędnego gadania

## Hardware

Testowane na słabszym sprzęcie. Q4_K_M ~4.5GB RAM. 14B niepotrzebne, 7B wystarcza z filtrami.

## Licencja

MIT - rób co chcesz, bądź twardy a nie miękki ;)

---
Generowane lokalnie, bez chmury. Python + C# = serce boli ale działa.
