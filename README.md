# Spanish Citizenship — Maze Board Game

A turn-based **maze board game** for up to 4 players, written in **C# (.NET 8)** as a console application. An object-oriented design exercise wrapped in a playful theme.

## The game

Each player starts in a corner of the board and races through a maze of Cuban bureaucracy to obtain the **Spanish passport**. To win you must first clear two intermediate goals — *paperwork accreditation at MINREX* and the *embassy appointment* — before reaching the final goal.

Players choose a **token**, each with its own stats and special ability:

- **Millonario** — pays to skip ahead (speed 4, cooldown 5; ability: advance +10 squares).
- **El Socio** — has connections in the system (speed 5, cooldown 4; ability: shortcut the process).
- *(…and more — see the code.)*

Traps and setbacks (`Trampa`) are scattered across the maze (`Laberinto`).

## Design

Object-oriented model with clear responsibilities:

| Class | Role |
|---|---|
| `Juego` / `JuegoFino` | Game loop and turn orchestration |
| `Jugador` | Player state and progress |
| `Ficha` | Token type, stats, and ability |
| `Laberinto` | Board / maze generation and layout |
| `Celda` | Individual board cell |
| `Trampa` | Traps and setback logic |

## Run it

```bash
dotnet run
```

Requires the .NET 8 SDK. Follow the on-screen prompts to pick tokens and play.

---

*University of Havana — Computer Science. Object-oriented programming project.*
