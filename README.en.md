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
- `com.kibalab.tsmp.core` 0.3.0-beta.2 or newer (UPM dependency: 0.3.0-beta.2)
- VRChat Worlds SDK 3.9.0 or newer only when used in VRChat worlds

## Installation

Add the VPM repository in VRChat Creator Companion.

```text
https://vpm.kiba.red/
```

Then install `TSMP Core` and `TSMP Codec Luma4`.

For ordinary Unity, install Core 0.3.0-beta.2 and this package through UPM's **Add package from disk**. VRCSDK/UdonSharp is not required. Both environments use the same Controller prefab and automatic setup preparation.

## Usage

1. Add `Packages/com.kibalab.tsmp.core/Samples/TSMPController.prefab` from the Core package to your scene.
2. Open the Codec tab in `TSMPSetup` and click `Refresh Codecs`.
3. Select `Luma4`.
4. Confirm the input/output settings. Setup prepares components and bindings automatically; `Apply Setup` can also refresh them manually.

## Release Status

Luma4 0.0.4-beta.1 is a release candidate for TSMP Core 0.3.0-beta.2. Publish Core first; this candidate is not yet available through VPM.

## License

MIT License. Copyright (c) 2026 KIBA_Labs.

## Preparation API compatibility

This source requires Core 0.3.0-beta.2, currently a release candidate. Core 0.2.0 and 0.3.0-beta.1 lack `PrepareDecode` and cannot compile this codec, even when the calibration material is unassigned. Publish and install the matching Core before this codec. A missing preparation material only selects the original shader path after compilation.

UPM uses a version string, while VPM uses a version range. Local/disk or Git installs must supply a compatible Core directly in the project's dependencies; package metadata does not tell UPM to fetch Core from GitHub. For VPM betas, enable pre-release packages and select the matching versions after publication.
