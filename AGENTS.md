# AGENTS.md - Joker MMO Project Guide

## Project Overview

This is a Unity-based MMO game project using **HybridCLR** for hot updates and **Unity Netcode** for multiplayer networking.

### Technology Stack

| Technology | Purpose |
|------------|---------|
| Unity Netcode | Multiplayer networking framework |
| Unity Transport (UTP) | Network transport layer |
| HybridCLR | C# hot update solution |
| Addressables | Resource management system |
| JKFrame | Unity development framework (Singleton, Events, UI, Resources) |
| MongoDB (Bson) | Database persistence |
| Cinemachine | Camera control |

---

## Build & Development Commands

### Unity Editor
- Open the project in Unity Hub or via `Joker_MMO.sln`
- Build: `File > Build Settings`
- Play in Editor: Press Play button (supports both client and server modes)

### Running Single Tests
This is a Unity project - tests are run via Unity's test runner:
```
Window > General > Test Runner
```
- Run all tests: Click "Run All" in Test Runner
- Run single test: Right-click a specific test > "Run Selected"

### Build Commands (via command line)
```bash
# Windows - Build Unity project
Unity.exe -buildTarget Windows -projectPath "D:/Unity/Projects/Joker_MMO" -quit

# Or use Unity Batchmode
Unity.exe -batchmode -projectPath "D:/Unity/Projects/Joker_MMO" -executeMethod BuildScript.Build -quit
```

---

## Code Structure

```
Assets/Scripts/
├── AOT/                    # Native compiled code (non-hot-update)
│   ├── HotUpdate/          # Hot update system entry
│   ├── LoadingWindow/      # Loading window
│   └── SettingAndConfig/   # Basic configuration
├── Common/                 # Shared code (client + server)
│   ├── Manager/            # Network/Scene managers
│   ├── Netcode/            # Network message protocols
│   ├── Item/               # Item system
│   ├── Player/             # Player controllers
│   └── Event/              # Common event definitions
├── HotUpdate/              # Hot update code
│   ├── UI/                 # Full UI system
│   ├── Map/                # Client map management
│   └── Login/              # Login module
└── Server/                 # Server-only code
    ├── AOI/                # Area of Interest management
    ├── ClientManager/      # Client management
    └── Data/               # Data persistence (MongoDB)
```

### Key Classes

| Class | Location | Responsibility |
|-------|----------|----------------|
| `ClientLaunch` | AOT/ClientLaunch.cs | Client startup entry |
| `ClientGlobal` | HotUpdate/ClientGlobal.cs | Client global singleton |
| `ServerLaunch` | Server/ServerLaunch.cs | Server startup entry |
| `ServerGlobal` | Server/ServerGlobal.cs | Server global singleton |
| `HotUpdateSystem` | AOT/HotUpdate/HotUpdateSystem.cs | Hot update core |
| `NetManager` | Common/Manager/NetManager.cs | Network manager |
| `GameSceneManager` | Common/Manager/GameSceneManager.cs | Scene management |

---

## Code Style Guidelines

### Naming Conventions

- **Classes**: PascalCase (`GameSceneManager`, `NetManager`)
- **Methods**: PascalCase (`Init()`, `StartHotUpdate()`)
- **Variables**: PascalCase or camelCase (`playerController`, `maxPlayers`)
- **Private fields**: camelCase with optional underscore prefix (`_instance`, `player`)
- **Constants**: PascalCase (`MaxPlayers`, `DefaultPort`)
- **Structs**: PascalCase (`BulletSpawnEvent`, `PlayerSpawnEvent`)
- **Enums**: PascalCase with values in PascalCase

### File Organization

- One class per file
- File name matches class name
- Use regions for organizing code:
```csharp
#region Fields
#endregion

#region Properties
#endregion

#region Lifecycle
#endregion

#region Public Methods
#endregion

#region Private Methods
#endregion
```

### Using Statements

- Place `using` statements at the top
- Sort alphabetically or group by namespace
- Common namespaces used:
```csharp
using JKFrame;
using UnityEngine;
using UnityEngine.Scripting;
using Unity.Netcode;
```

### Attributes

- Use `[Preserve]` for AOT compatibility on classes/methods
- Use `[SerializeField]` for private serialized fields
- Use `[Range]` for numeric fields in inspector

### Unity-Specific Patterns

- Use `MonoBehaviour` for components
- Use `SingletonMono<T>` from JKFrame for singletons
- Use `Coroutine` for async operations
- Use `NetworkBehaviour` for networked objects

---

## Error Handling

- Check for null before using objects: `if (obj == null) return;`
- Use early returns to avoid nesting
- Log errors appropriately: `Debug.LogError()`, `Debug.LogWarning()`
- Wrap network operations in null checks:
```csharp
if (networkObject == null || !networkObject.IsSpawned) return;
```

---

## Important Rules

### Modification Rules (from AITool/SKILL.md)

**CRITICAL**: Before modifying any code or files, you MUST:
1. Declare what will be changed
2. Wait for user approval
3. Only then proceed with modifications

### Hot Update Guidelines

- **AOT Code**: Cannot be hot updated - keep only essential startup logic here
- **HotUpdate Code**: Business logic goes here
- Use `[Preserve]` attribute on classes/methods needed by AOT

### Network Guidelines

- Use `INetworkSerializable` for custom network messages
- Always check `IsServer` / `IsClient` before server/client-specific logic
- Use `NetworkObject.SpawnWithOwnership()` for spawning

---

## Git Workflow

- Commit changes regularly
- Write meaningful commit messages
- Do not commit: `Library/`, `Temp/`, `Builds/`, `*.csproj.user`
- The `AGENTS.md` file should be committed to help AI assistants

---

## Testing Guidelines

- Tests go in `Assets/Test/` folder
- Use Unity's Play Mode or Edit Mode tests
- Tag tests with `[Test]` attribute
- Use `[UnityTest]` for tests that need Play mode

---

## UI Development

- UI system based on JKFrame.UISystem
- UI windows in `Assets/Scripts/HotUpdate/UI/`
- Window prefabs managed via Addressables
- Use localization keys for text content
