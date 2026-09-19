# Changelog

## 0.0.4-beta.2

- Reuse native encoder raster arrays and opt in to Core's GPU Luma4 writer without changing the palette, codec ID or datagram.
- Write the decoded header prefix and payload directly to Core's combined readback target, retaining the legacy shader path.
- Require Core 0.3.0-beta.3 through UPM and >=0.3.0-beta.3 through VPM for the buffered/GPU writer APIs. Update Core first.
- In the paired Core/Luma4 Udon profile (720p, 4 KiB, 60 Hz), mean encoder time decreased from 11.27 ms to 0.457 ms. The 360p / 32 B raster helper decreased from 0.158 ms to 0.066 ms. These are paired pipeline results, not an isolated codec comparison.
- GPU encoding adds a conversion pass: the measured 720p Gamma draw median was 16.5 us versus 8.3 us for expansion alone. CPU fallback is retained.
- Validated native Gamma/Linear and compiled Udon VM pixel parity. See the matching Core release notes for hardware, methodology and limits; live VRChat, Quest and IL2CPP were not verified.

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
