# SuperHex — Hex Merge Puzzle Game

A Unity puzzle game where players place and merge numbered hex tiles on a configurable grid to maximize their score.

---

## Gameplay Overview

Players receive a piece containing two randomly generated numbers. The goal is to place them on the hexagonal grid — either on empty tiles or on tiles sharing the same number — and trigger merges to build up the highest possible score before the game ends.

### Core Concepts

| Element | Description |
|---|---|
| **Empty Hex** | An unoccupied tile where a number can be placed |
| **Filled Hex** | An occupied tile displaying a number, each with its own unique color |
| **Number Piece** | The pair of numbers the player places each turn |
| **Rotate** | Tapping the piece rotates it clockwise; 6 unique orientations are available |

---

## Game Flow

1. **Start** — The board initializes as a configurable hexagonal grid. Two random numbers are generated for the first piece, drawn from the initial pool: `{1, 2, 3}`.
2. **Rotate** — The player taps the number piece to cycle through its 6 orientations.
3. **Drag & Place** — The player drags the piece toward a valid target. The piece floats above the finger to avoid visual obstruction.
4. **Hover Feedback** — When hovering over valid tiles, those tiles change color to signal a legal placement. A placement is valid only when **both** numbers in the piece have a valid destination (an empty hex or a tile with the same number).
5. **Invalid Placement** — Releasing the piece over an invalid position snaps it back to the spawn point.
6. **Merge** — When a number is placed on a tile with the same value, they combine: `2 + 2 = 4`, `4 + 4 = 8`, and so on. The resulting number appears in its predefined color and is added to the pick pool.
7. **Pool Growth** — Every newly discovered number is added to the random selection pool, making higher-value pieces increasingly possible.
8. **Game End** — After **6 number pieces** are placed, the game ends. The final score is the sum of all values on the board (e.g., `1 + 4 + 1 + 3 + 3 + 2 = 14`). A **Restart** button resets and replays the game.

---

## Functionalities

- Drag-and-drop interaction with nearest-cell detection.
- Snap piece to grid on valid placement.
- Return piece to spawn point on invalid placement.

---

## Built With

- **Engine:** Unity

## Where can I find the game?
[Super Hex](https://marizitoss.itch.io/superhex)


