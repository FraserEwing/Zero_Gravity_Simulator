# VR Zero Gravity Simulator

This project is a Unity-developed **VR zero gravity simulator** designed for deployment on **Meta Quest** devices. Built using the **XR Interaction Toolkit (XRI)**, it provides propulsion and 3D rotation mechanics in a zero gravity environment. The simulator includes **cybersickness mitigation techniques** and early-stage implementations of **navigation** and **attention-based tasks**.

## Features

- Zero resistance locomotion using **Grab Move**
- Full 3D rotation using **Grab Move**
- **ISS 3D model environment**
- **Novel Snap Turn** implementation
- **Vignetting** for cybersickness reduction
- **Novel rotation system** for X and Z axes

## Project Structure

### `Assets/`

Contains all core elements of the simulator, including:

- 3D models and sample scenes
- Custom scripts (see below)

### `Assets/Scripts/`

| Script                        | Purpose                                                                 |
|------------------------------|-------------------------------------------------------------------------|
| `HeadRotationHelper.cs`      | Provides horizontal **snap turning**                                    |
| `ZeroGravityPropulsion.cs`   | Manages **momentum-based zero gravity movement**                        |
| `ZeroGravityRotation.cs`     | Allows **environmental rotation** around the user                       |
| `ZeroGravityRotationLCS.cs`  | Implements a **Time-Based Rotation** system to reduce cybersickness     |

> `ZeroGravityPhysics.cs` is **deprecated** and should not be used or referenced.

## Requirements

- **Unity Hub**
- **Unity Editor version**: `2022.3.50f1`  
  (Later versions may work but are not guaranteed stable)
- **Android Build Support** module for Unity

> **Note:** Meta Quest does **not support macOS** deployment. A **Windows machine is required**.

## Required Unity Packages

Ensure the following packages are installed in the Unity Package Manager:

- `Input System` **1.11.0**
- `Oculus XR Plugin` **4.3.0**
- `OpenXR Plugin` **1.12.1**
- `XR Core Utilities` **2.3.0**
- `XR Interaction Toolkit` **3.0.3**
- `XR Plugin Management` **4.4.0**

## Additional Setup

You will also need:

- A **Meta Quest headset**
- A **Meta account**
- The **Meta Quest desktop app**

## Build & Deployment Instructions

1. Power on the **Meta Quest headset**.
2. Connect it to your Windows machine via **USB** or **Air Link** (Wi-Fi required).
3. Open the project in **Unity Editor 2022.3.50f1**.
4. Press the **Play** button in the Unity Editor to test the application.

To build and deploy to the Meta Quest:

- Ensure **Developer Mode** is enabled on your headset.
- Follow Meta’s official setup guides below.

## Useful Links

- [Enable Developer Mode on Quest](https://www.meta.com/en-gb/help/quest/1336626146870772/)
- [Mobile Device Setup Guide (Meta Developer)](https://developers.meta.com/horizon/documentation/native/android/mobile-device-setup/?locale=en_GB)
- [Meta USB Connectivity Help](https://www.meta.com/en-gb/help/quest/509273027107091/)

---

