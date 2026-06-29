# Mechanics Playground

A modular Unity project for experimenting with game mechanics.  
Built as a clean-architecture sandbox and portfolio piece.

[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](https://www.gnu.org/licenses/gpl-3.0)

---

## 📖 Overview

Mechanics Playground is not a game — it's a **living toolkit** where each game mechanic is a self-contained module that can be activated, swapped, and deactivated at runtime.  
The core of the project is a **camera-agnostic system** that lets you switch between different camera behaviours (free 3D, orthographic 2D, and soon more) with smooth cinematic blends — including seamless perspective-to-orthographic transitions.

All modules are built around modern Unity patterns: dependency injection with VContainer, reactive data flows with R3, and async operations with UniTask.

---

## 🌐 Try It Online

👉 **[Launch WebGL Demo](https://pavelkasapov.github.io/)** – updated regularly with the latest features.

---

## ✨ Current Features

### 🎥 Modular Camera System
- **Two fully playable cameras** – Free 3D (flycam) and Orthographic 2D, each with its own movement and controls  
- **Runtime switching** via UI – one click, instant deactivation of the old module and activation of the new one  
- **Custom Cinemachine blending** – a specially written `PerspectiveToOrthoCustomBlender` that eliminates the jarring “projection pop” during perspective ↔ ortho transitions  
- **Snapshot duplicator camera** – ensures smooth blending without leaving dead modules running  
- **Unified camera access** – `CameraHandler` + `CameraFacade` provide a single public entry point to the active camera, without exposing internal module logic  
- **Blend interruption protection** – camera switching is blocked while a blend is in progress, guaranteeing visual stability  

### 🧩 True Modular Architecture
- Every feature (camera, future player, etc.) lives in its own folder under `Features/` with its own `LifetimeScope`, scripts, and prefabs  
- **Zero cross-feature dependencies** – modules only depend on the `Core` layer  
- **`FeatureManager`** handles module lifecycle (activate/deactivate) and coordinates with camera blending  
- Modules are registered via `ModuleDefinition` ScriptableObjects (automatic discovery from `Resources`)

### 🕹️ Reactive Input & Settings UI
- **InputAdapter** wraps Unity’s Input System into clean `Observable<T>` streams (move, look, zoom, etc.)  
- **Dynamic settings panel** – when a module is activated, its settings automatically appear in the UI; when deactivated, they are removed  
- Object pooling for settings controls, driven by reactive collections (`ObservableCollections`)  
- Settings are applied instantly — no “Apply” buttons needed  

### 🧠 Core Architecture Highlights
- **DI Containers** – every module is a `LifetimeScope`, dependencies are injected by VContainer  
- **Reactive state** – R3 powers UI updates, module registry, and input streams  
- **Async-first** – `UniTask` used for blend waiting, deferred deactivation, and future async loading  
- **Custom blend engine** – `PerspectiveToOrthoCustomBlender` works without requiring a LookAt target and maintains visual fidelity across any camera orientation  

---

## 🎮 Controls

### Free 3D Camera
| Key         | Action                |
|-------------|------------------------|
| `WASD`      | Move camera            |
| `Shift`     | Sprint                 |
| `Space`     | Move up                |
| `Ctrl`      | Move down              |
| `Z`         | Zoom (smooth)          |
| `Mouse`     | Look around            |
| `Tab`       | Toggle cursor / freeze camera rotation |

### Orthographic 2D Camera
| Key              | Action                          |
|------------------|---------------------------------|
| `WASD`           | Move camera                     |
| `Shift`          | Sprint (faster movement)        |
| `Mouse Scroll`   | Zoom in/out                     |
| `Mouse Cursor`   | Move to screen edge → camera pans in that direction |

*Note: In the Ortho 2D camera, moving the mouse cursor to the edges of the screen automatically scrolls the view. This works alongside WASD movement.*

---

## 🧰 Tech Stack

- **Unity 6 (6000.0.x)**  
- **VContainer** – dependency injection  
- **R3** – reactive programming  
- **UniTask** – async/await for Unity  
- **ObservableCollections** – reactive collections  
- **Cinemachine** – virtual cameras and custom blending  
- **Input System** – unified input handling  

---

## 📁 Project Structure

- `Assets/`
  - `Core/` – shared code (interfaces, base classes, registry, managers, custom blenders)
  - `Features/` – self-contained modules
    - `FreeCamera3D/` – free-flight 3D camera (WASD + mouse)
    - `Ortho2DCamera/` – orthographic 2D camera (WASD + edge scrolling + zoom)
    - `... (future modules)`
  - `GlobalArt/` – shared visual assets
  - `Scenes/` – demo scenes

Each feature folder contains its own `Scripts/`, `Prefabs/`, `Input/`, and a root `LifetimeScope`. No module knows about another.

---

## 🗺️ Roadmap & Next Steps

I maintain a public **GitHub Project** board where you can see all tasks, priorities, and my development progress:

👉 [View the Project Board](https://github.com/users/PavelKasapov/projects/2)

The board is more up-to-date than this README — check it to see which features are ready, which are in progress, and what’s coming next.

Immediate focus:
- 🚶 **Player module** – a simple controllable capsule with basic movement  
- 🎥 **Third-person camera** – follow cam with smooth look, integrated into the blending system  
- 🔄 **Smart camera spawning** – when switching to a free camera or top-down view, the camera starts from a position relative to the player  
- 🔀 **Blend stress-testing** – ensure smooth transitions between all three camera types (3D follow, free 3D, ortho 2D)  
- 🧪 **Photo mode / free look** – when a free camera is active, player input is temporarily disabled  

Further down the line: more player mechanics (jumping, obstacles), simple AI with 2D vision cones, or even a racing module. The modular design makes it easy to drop in any new idea.

---

## 🚀 Getting Started

1. Clone the repo  
   `git clone https://github.com/PavelKasapov/mechanics-playground.git`
2. Open the project in Unity 6 (6000.0.x).
3. Open `MainScene`.
4. Press Play – use UI buttons to switch between cameras and experiment.

---

## 📄 License

Licensed under **GNU General Public License v3.0**.  
See [LICENSE](LICENSE) for details.

---

## ✍️ About

This project is part of my portfolio. It demonstrates clean architecture, modular design, custom Cinemachine blending, and reactive UI patterns — all built with modern Unity tools.  
Questions or ideas? [Open an issue](https://github.com/PavelKasapov/mechanics-playground/issues) or reach out.
