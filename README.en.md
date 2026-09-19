[한국어](README.ko.md) | **English** | [日本語](README.md)

# TSMP Codec Luma4

Luma4 is the default TSMP codec. It writes TSMP data through luminance-oriented symbols instead of relying on rich color information, making it the baseline codec for simple setup and stable decoding.

Use Luma4 first when installing TSMP or diagnosing a stream path.

## Characteristics

- Default TSMP codec
- Low color dependency and simple decode path
- Practical baseline pattern for VRChat camera capture, OBS, Spout, and similar video routes
- Automatically discovered in the `TSMPSetup` Codec tab

## Requirements

- TSMP Core: https://github.com/kibalab/TSMP-Core
- Unity 2022.3
- `com.kibalab.tsmp.core` 1.0.0 or newer (UPM dependency: 1.0.0)
- VRChat Worlds SDK 3.9.0 or newer only when used in VRChat worlds

## Installation

Add the VPM repository in VRChat Creator Companion.

```text
https://vpm.kiba.red/
```

Then install `TSMP Core` and `TSMP Codec Luma4`.

For ordinary Unity, install Core 1.0.0 and this package through UPM's **Add package from disk**. VRCSDK/UdonSharp is not required. Both environments use the same Controller prefab and automatic setup preparation.

## Usage

1. Add `Packages/com.kibalab.tsmp.core/Samples/TSMPController.prefab` from the Core package to your scene.
2. Open the Codec tab in `TSMPSetup` and click `Refresh Codecs`.
3. Select `Luma4`.
4. Confirm the input/output settings. Setup prepares components and bindings automatically; `Apply Setup` can also refresh them manually.

## Release Status

Luma4 1.0.0 is a stable release consolidating all beta changes since 0.0.3. Install Core 1.0.0 first. VCC does not require prerelease packages for these versions.

## License

MIT License. Copyright (c) 2026 KIBA_Labs.

## Preparation API compatibility

This release requires Core 1.0.0 and its codec preparation/output APIs. Update Core before installing this codec. Missing preparation materials can select the legacy shader path, but cannot compensate for an incompatible Core API.

UPM uses Core 1.0.0; VPM accepts Core >=1.0.0. Local/disk or Git installs must supply Core directly in the project's dependencies; package metadata does not tell UPM to fetch Core from GitHub. VRChat Worlds SDK remains a VPM-only dependency.
