# Luma4 shader startup, 2026-11-15

The non-LUT classifier now keeps its sixteen-symbol search as a loop instead of
forcing unrolling. Affine fit parameters are prepared once per output fragment,
then shared by its eight symbols. LUT unrolling, symbol order, strict tie
comparison, byte layout and CRC behavior are unchanged. A local copy of the
shared output fragment avoids requiring a new Core API.

On Unity2022.3.22f1/D3D11/RTX4090, the original shader's first non-LUT use took
374.6s in one Native run. Other original-shader runs exceeded the6GiB preventive
working-set guard; one earlier run reached16.3GB before being stopped. The loaded
NVIDIA compiler modules and busy workers suggest driver optimization, but no
stack trace proves that cause. These are not GPU execution-time measurements.
The modified shader completed all following functional tests within the guard.

- Native Player: full DecoderSnapshot, including216 changing-source cases;
  compiled client Udon in Editor: full DecoderSnapshot.
- Both runtimes: prediction-shrink, prediction-regression and overlap-regression,
  with both the original Core Decoder and the private prefix candidate.
- Normal inputs:96 mode/layout/format/sample/orientation/length combinations
  matched bytes and CRC. Affine inputs: modes0/1 matched all64 cases; all32 mode2
  mismatches were CRC-rejected, with zero incorrect accepted payloads.

Warmed4096B GPU measurements use30 valid samples and Batch32; values below are
microseconds per decode after batch normalization.

| Mode | sample4/LUT original → modified mean | sample1 modified mean / p95 / max |
| --- | ---: | ---: |
| 0 | 29.7587 → 29.7840 | 2.4235 / 2.528 / 2.560 |
| 1 | 110.1000 → 110.0795 | 1.3427 / 1.376 / 1.376 |
| 2 | 24.3133 → 24.2963 | 0.8368 / 0.864 / 0.896 |

All measured outputs matched4096 bytes, CRC and padding. LUT changes were within
0.026µs. Original non-LUT GPU timing could not finish under the resource guard,
so its speedup/regression ratio is unavailable. The sample1 maximum is the
maximum of normalized batches, not an individual-draw maximum. The candidate-only
run stayed below the predeclared500µs bound and used at most516MiB working set.

Core1.0.0 is the declared minimum. Its codec API and both shared shader includes
are byte-identical to those used by current development sources. An SDK-free
Editor smoke test with the complete Core `v1.0.0` package (`1e8d4dcc`) passed:
prefab/material/shader references, exact payloads of4/56/1027 bytes, header bytes,
samples1/4, and both prepared calibration and missing-preparation fallback.
The private fixture only updates the historical minimum-version assertion and
allows the recorded extra optional codec packages; only Luma4 is exercised.

Evidence: `F:/Unity/TSMP/Validation-Results/prediction-followup-20261115`, especially
`shader-loop-*`, `final-combined-snapshot-*`, `gpu-tradeoff-01` and
`gpu-candidate-only-01` and `shader-candidate/mincore-smoke`. Shader SHA256 begins
`9d269e57`. These are local Editor/
Player checks, not VRChat-client, other-GPU, FPS or OBS/CDN transport results.
