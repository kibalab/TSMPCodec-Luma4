# Changelog

## Unreleased

- Reuse native encoder raster arrays and opt in to Core's GPU Luma4 writer without changing the palette, codec ID or datagram.
- This source requires the corresponding unreleased Core buffered/GPU writer APIs. Publish matching Core and Luma4 dependency versions together; the current beta Core does not provide these APIs.

## 0.0.4-beta.1

- Require Core 0.3.0-beta.2 in UPM and >=0.3.0-beta.2 in VPM because the codec now calls the preparation API. Core 0.2.0 and 0.3.0-beta.1 do not provide that API.
- Use a 16-entry Float32 calibration LUT for effective sample sizes greater than one; retain the original single-sample path.
- Include the preparation shader/material on the codec prefab. Missing preparation resources retain ordinary decoding with a compatible Core.
- Preserve codec IDs, packet layout and existing script/material/prefab GUIDs. Keep VRChat SDK requirements in VPM only.
- Update Core before installing this codec. Enable prerelease packages in VCC to select the matching beta versions.

## 0.0.3

- Promote Luma4 out of beta with the shader include fixes and SDK-neutral prefab support introduced in 0.0.3-beta.2 and 0.0.3-beta.3.
- Target stable Core 0.2.0 through UPM and Core 0.2.0 or newer through VPM.
- Regenerate the bundled Udon program and field metadata for Core 0.2.0's compact encoder query response while preserving asset references.
- Update English, Korean and Japanese setup and requirements documentation for ordinary Unity and VRChat.
- Keep codec ID, encoding/decoding logic, shaders, wire format and existing asset GUIDs unchanged from 0.0.3-beta.3.

## 0.0.3-beta.3

- Provide an SDK-neutral codec prefab for Core's automatic Controller preparation in ordinary Unity and VRChat.
- Detect installed Worlds packages through assembly version defines without requiring VRCSDK through UPM.
- Require Core 0.2.0-beta.1 or newer through VPM; use the exact matching Core version for UPM.
- Preserve Luma4 codec ID, script and material references, shader decoding logic, and wire format.

## 0.0.3-beta.2

- Fix TSMP Core shader includes for local `file:` packages by resolving them through `Packages/com.kibalab.tsmp.core/` (PR #1, contributed by AYANO-TFT).
- Keep shader decoding logic and the encoded data format unchanged.

## 0.0.3-beta.1

- Beta release metadata for VPM distribution.
- Includes the default Luma4 codec runtime, shaders, materials, prefab, and sample.
