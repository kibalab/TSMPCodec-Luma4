Shader "Hidden/TSMP/Decode Luma4 Bytes"
{
    Properties
    {
        [HideInInspector] _TSMPHeaderTex ("Decoded Header", 2D) = "black" {}
        [HideInInspector] _TSMPHeaderPixels ("Header Pixels", Float) = 0
        [HideInInspector] _CalibrationLut ("Calibration LUT", 2D) = "black" {}
        _RecoveryMode ("Recovery Mode", Float) = 0
        _MainTex ("TSMP Source", 2D) = "black" {}
        _BlockSize ("Block Size", Float) = 8
        _SampleSize ("Sample Size", Float) = 0
        _StartBlock ("Start Block", Float) = 0
        _ByteCount ("Byte Count", Float) = 0
        _ActiveWidthBlocks ("Active Width Blocks", Float) = 80
        _SourceWidth ("Source Width", Float) = 640
        _SourceHeight ("Source Height", Float) = 360
        _OutputWidth ("Output Width", Float) = 14
        _OutputHeight ("Output Height", Float) = 1
        _FlipY ("Flip Y", Float) = 1
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "Queue" = "Overlay"
        }

        Cull Off
        ZWrite Off
        ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma target 3.5
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_local _ TSMP_CALIBRATION_LUT
            #include "Packages/com.kibalab.tsmp.core/Runtime/Codecs/Common/Shaders/cgincs/TSMPDecodeCommon.cginc"

            #include "Luma4Recovery.cginc"

            float2 PrepareDecodeFit()
            {
#if !defined(TSMP_CALIBRATION_LUT)
                if (_RecoveryMode > 0.5 && _RecoveryMode < 1.5)
                    return RecoveryFitParameters();
#endif
                return float2(1.0, 0.0);
            }

            int ClassifySymbol(float luma, float2 fit)
            {
                int bestSymbol = 0;
                float bestDistance = 999.0;

#if defined(TSMP_CALIBRATION_LUT)
                [unroll]
#else
                [loop]
#endif
                for (int i = 0; i < 16; i++)
                {
                    float c = RecoveryCalibrationLuma(i, fit);
                    float d = abs(luma - c);

                    if (d < bestDistance)
                    {
                        bestDistance = d;
                        bestSymbol = i;
                    }
                }

                return bestSymbol;
            }

            int DecodeSymbolAtBlockIndex(float blockIndex, float2 fit)
            {
                float luma = SampleLumaBlockByIndex(blockIndex);
                return ClassifySymbol(luma, fit);
            }

            int DecodeByte(int byteIndex, float2 fit)
            {
                if (byteIndex < 0 || byteIndex >= (int)_ByteCount)
                    return 0;

                float blockIndex = _StartBlock + byteIndex * 2;
                int low = DecodeSymbolAtBlockIndex(blockIndex, fit);
                int high = DecodeSymbolAtBlockIndex(blockIndex + 1.0, fit);
                return (high << 4) | low;
            }

            // Keep the shared output layout while preparing this decoder's fit once.
            float4 frag(v2f i) : SV_Target
            {
#if defined(TSMP_COMBINED_BYTE_OUTPUT)
                int baseByte;
                float4 prefix;
                if (TSMPReadOutputPrefix(i.uv, baseByte, prefix))
                    return prefix;
#else
                float2 pixel = floor(i.uv * float2(_OutputWidth, _OutputHeight));
                pixel = clamp(pixel, 0.0, float2(_OutputWidth - 1.0, _OutputHeight - 1.0));
                int baseByte = ((int)pixel.y * (int)_OutputWidth + (int)pixel.x) * 4;
#endif

                if (baseByte >= (int)_ByteCount)
                    return 0.0;

                float2 decodeFit = PrepareDecodeFit();
                int b0 = DecodeByte(baseByte + 0, decodeFit);
                int b1 = DecodeByte(baseByte + 1, decodeFit);
                int b2 = DecodeByte(baseByte + 2, decodeFit);
                int b3 = DecodeByte(baseByte + 3, decodeFit);

                return float4(b0 / 255.0, b1 / 255.0, b2 / 255.0, b3 / 255.0);
            }
            ENDCG
        }
    }

    Fallback Off
}
