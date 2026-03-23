# Mechanics Playground

A collection of interactive game mechanics for Unity.  
Built as a modular toolkit and portfolio showcase.

[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](https://www.gnu.org/licenses/gpl-3.0)

---

## 📖 Overview

This project is a sandbox for implementing and demonstrating various game mechanics in Unity.  
Each mechanic is designed as a self‑contained module, making it easy to reuse, test, and extend.

The goal is to show clean architecture, modern Unity patterns, and the ability to build scalable, maintainable systems.

---

## ✨ Features (current)

- **Free 3D Camera**  
  – First‑person style movement (WASD, Shift, Space/Ctrl)  
  – Mouse look with configurable sensitivity and smoothing  
  – Zoom (Z key) with smooth transitions  
  – Cursor lock on Tab (camera rotation stops, UI becomes interactable)

- **Dynamic Settings UI**  
  – Automatically displays controls for every active module  
  – Built with object pooling and reactive data flow (R3)  
  – Fully modular: new modules register their own settings

- **Modular Architecture**  
  – Each feature (camera, pathfinding, etc.) lives in its own folder with its own `LifetimeScope`  
  – Dependency injection with **VContainer**  
  – Reactive programming with **R3**  
  – Asynchronous operations with **UniTask**

- **Centralized Settings Registry**  
  – Reactive collections (`ObservableCollections`)  
  – Modules can register/unregister at runtime  
  – UI updates automatically when modules appear or disappear

---

## 🎮 Controls (Free Camera)

| Key         | Action                |
|-------------|------------------------|
| `WASD`      | Move camera            |
| `Shift`     | Sprint (speed boost)   |
| `Space`     | Move up                |
| `Ctrl`      | Move down              |
| `Z`         | Zoom                   |
| `Mouse`     | Look around            |
| `Tab`       | Show cursor / pause camera rotation |

*Note: When `Tab` is held, the cursor becomes visible and you can interact with UI elements.*

---

## 🧰 Tech Stack

- **Unity 6 (6000.0.x)**  
- **VContainer** – Dependency injection  
- **R3** – Reactive Extensions  
- **UniTask** – Async operations  
- **ObservableCollections** – Reactive collections  
- **Cinemachine** (planned for advanced camera blending)

---

## 📁 Project Structure

- `Assets/`
  - `Core/` – shared code (interfaces, base classes, registry)
  - `Features/` – self‑contained modules
    - `FreeCamera3D/` – example feature
      - `Scripts/`
      - `Prefabs/`
      - `Settings/`
      - `Input/`
  - `GlobalArt/` – shared visual assets
  - `Scenes/` – demo scenes

Each feature folder contains its own `Scripts`, `Prefabs`, `Input` assets, and a `LifetimeScope` for dependency injection.

---

## 🗺️ Current Status & Roadmap

I maintain a public **GitHub Project** board where you can see all tasks, priorities, and my development progress:

👉 [View the Project Board](https://github.com/users/PavelKasapov/projects/2)

Planned mechanics include:
- 2D cameras (top‑down, side‑scroller, isometric)
- 3D third‑person camera
- Pathfinding (A*)
- Raycasting and grid interaction
- Gamepad support
- Mobile‑friendly UI
- And many more…

---

## 🚀 Getting Started

1. Clone the repository  
   `git clone https://github.com/PavelKasapov/mechanics-playground.git`
2. Open the project in Unity 6 (6000.0.x).
3. Load the `MainScene` scene (or any demo scene).
4. Press Play and start exploring.

---

## 📄 License

This project is licensed under the **GNU General Public License v3.0**.  
See the [LICENSE](LICENSE) file for details.

---

## ✍️ About

This project is part of my portfolio. It demonstrates my ability to design clean, scalable architectures and to work with modern Unity tooling.  
If you have any questions or suggestions, feel free to [open an issue](https://github.com/PavelKasapov/mechanics-playground/issues) or contact me.
