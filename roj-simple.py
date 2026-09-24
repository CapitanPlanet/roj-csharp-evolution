import ollama
MODEL = "qwen2.5:7b-instruct-q4_K_M"

def agent_stream(rola, zadanie):
    print(f"\n--- {rola} ---\n")
    full = ""
    for chunk in ollama.chat(model=MODEL, messages=[
        {"role": "system", "content": f"Jesteś {rola}. Piszesz TYLKO po polsku, krótko i logicznie. Zero angielskiego."},
        {"role": "user", "content": zadanie}
    ], stream=True):
        token = chunk['message']['content']
        print(token, end='', flush=True)
        full += token
    print("\n")
    return full

cel = "3 praktyczne pomysły na użycie autonomicznych agentów AI dla zwykłej osoby"
r = agent_stream("RESEARCHER", cel)
d = agent_stream("WRITER", f"Na bazie {r} napisz raport 120 słów")
c = agent_stream("KRYTYK", f"Oceń raport i daj 2 rady do poprawy: {d}")
f = agent_stream("WRITER v2", f"Popraw wg krytyki: {c}. Raport: {d}")
print("\n===== FINAL =====\n", f)