Shader "Hidden/TSMP/Decode Luma4 Bytes"
{
    Properties
    {
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
            #include "Packages/com.kibalab.tsmp.core/Runtime/Codecs/Common/Shaders/cgincs/TSMPDecodeCommon.cginc"

            float CalibrationLuma(int symbol)
            {
                float blockX0 = symbol * 2;
                float blockX1 = blockX0 + 1.0;
                float a = SampleBlockLuma(blockX0, 1.0);
                float b = SampleBlockLuma(blockX1, 1.0);
                return (a + b) * 0.5;
            }

            int ClassifySymbol(float luma)
            {
                int bestSymbol = 0;
                float bestDistance = 999.0;

                [unroll]
                for (int i = 0; i < 16; i++)
                {
                    float c = CalibrationLuma(i);
                    float d = abs(luma - c);

                    if (d < bestDistance)
                    {
                        bestDistance = d;
                        bestSymbol = i;
                    }
                }

                return bestSymbol;
            }

            int DecodeSymbolAtBlockIndex(float blockIndex)
            {
                float luma = SampleLumaBlockByIndex(blockIndex);
                return ClassifySymbol(luma);
            }

            int DecodeByte(int byteIndex)
            {
                if (byteIndex < 0 || byteIndex >= (int)_ByteCount)
                    return 0;

                float blockIndex = _StartBlock + byteIndex * 2;
                int low = DecodeSymbolAtBlockIndex(blockIndex);
                int high = DecodeSymbolAtBlockIndex(blockIndex + 1.0);
                return (high << 4) | low;
            }

            #include "Packages/com.kibalab.tsmp.core/Runtime/Codecs/Common/Shaders/cgincs/TSMPDecodeByteOutput.cginc"
            ENDCG
        }
    }

    Fallback Off
}
