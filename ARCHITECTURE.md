# WCRCorder Architecture

## Project Goal

WCRCorder is a portable background webcam recorder for Windows.

The application is designed to run unattended for long periods while providing reliable video recording with minimal user interaction.

The graphical interface is intended only for configuration and diagnostics.

---

## Design Principles

1. Reliability is more important than feature count.
2. One responsibility per service.
3. MainForm contains no business logic.
4. Application state is managed from a single place.
5. All services have a predictable lifecycle.
6. The application must remain portable.

---

## Layers

```
MainForm
    │
    ▼
ApplicationController
    │
    ▼
ApplicationService
    │
 ┌──┴───────────────┐
 ▼                  ▼
Infrastructure   Functional Services
```

Infrastructure:

- ConfigService
- LogService
- ApplicationStateService

Functional services:

- RecorderService
- DeviceService
- FFmpegService
- TrayManager

---

## Application State

The application operates as a state machine.

Current planned states:

- Starting
- Ready
- Recording
- WaitingForCamera
- WaitingForMicrophone
- DiskFull
- Error
- Closing

Only ApplicationStateService is allowed to change the current state.

---

## Folder Structure

```
WCRCorder
│
├── WCRCorder.exe
├── ffmpeg.exe
│
└── Data
    ├── config.json
    ├── Logs
    ├── Video
    └── Temp
```

During Debug builds, the Data folder is created in the project directory.

During Release builds, the Data folder is created next to the executable.

---

## Development Rules

- Every completed subsystem ends with a Git commit.
- No duplicated services.
- No business logic inside UI.
- Keep classes focused on a single responsibility.
- Stability has higher priority than adding new features.