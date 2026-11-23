using System;
using System.Linq;
using K13A.TSMP;
using UnityEditor;
using UnityEngine;

public static class Luma4ProductionValidation
{
    public static void Run()
    {
        const int blocks = 81;
        const int block = 8;
        var source = new Texture2D(blocks * block, 128, TextureFormat.RGBA32, false, true);
        var bytes = new byte[251];
        new System.Random(574).NextBytes(bytes);
        var decode = new Material(Shader.Find("Hidden/TSMP/Decode Luma4 Bytes"));
        var calibration = new Material(Shader.Find("Hidden/TSMP/Prepare Luma4 Calibration"));
        var go = new GameObject("Luma4 receive validation");
        var codec = go.AddComponent<TSMPCodecLuma4>();
        var output = RenderTexture.GetTemporary(63, 1, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
        var pixels = new Texture2D(63, 1, TextureFormat.RGBA32, false, true);
        var previous = RenderTexture.active;
        try
        {
            source.filterMode = FilterMode.Point;
            if (!Luma4Raster.TryWriteFrame(source, block, new byte[56], bytes, out var error))
                throw new InvalidOperationException(error);
            decode.SetFloat("_BlockSize", block);
            decode.SetFloat("_StartBlock", blocks * Luma4Raster.PayloadStartRow);
            decode.SetFloat("_ActiveWidthBlocks", blocks);
            decode.SetFloat("_ByteCount", bytes.Length);
            decode.SetFloat("_SourceWidth", source.width);
            decode.SetFloat("_SourceHeight", source.height);
            decode.SetFloat("_OutputWidth", output.width);
            decode.SetFloat("_OutputHeight", 1);
            decode.SetFloat("_FlipY", 1);
            foreach (bool fast in new[] { false, true })
            foreach (bool lut in new[] { false, true })
            foreach (int samples in new[] { 1, 3, 4, 8 })
            {
                codec.useBilinear4 = fast;
                codec.preferCalibrationLut = lut;
                codec.calibrationMaterial = lut ? calibration : null;
                decode.SetFloat("_SampleSize", samples);
                codec.PrepareDecode(source, decode);
                if (decode.IsKeywordEnabled("TSMP_CALIBRATION_LUT") != lut)
                    throw new InvalidOperationException("Unexpected LUT policy");
                Graphics.Blit(source, output, decode);
                RenderTexture.active = output;
                pixels.ReadPixels(new Rect(0, 0, output.width, 1), 0, 0);
                if (!pixels.GetRawTextureData<byte>().ToArray().Take(bytes.Length).SequenceEqual(bytes))
                    throw new InvalidOperationException($"Byte mismatch: fast={fast}, LUT={lut}, samples={samples}");
            }
            if (ShaderUtil.ShaderHasError(decode.shader) || ShaderUtil.ShaderHasError(calibration.shader))
                throw new InvalidOperationException("Luma4 shader compilation failed");
            Debug.Log($"Luma4 receive PASS: 16 GPU cases on {SystemInfo.graphicsDeviceName}");
        }
        finally
        {
            RenderTexture.active = previous;
            UnityEngine.Object.DestroyImmediate(go);
            RenderTexture.ReleaseTemporary(output);
            UnityEngine.Object.DestroyImmediate(source);
            UnityEngine.Object.DestroyImmediate(pixels);
            UnityEngine.Object.DestroyImmediate(decode);
            UnityEngine.Object.DestroyImmediate(calibration);
        }
    }
}
