#if defined(TSMP_CALIBRATION_LUT)
Texture2D<float4> _CalibrationLut;
#endif

float _UseBilinear4;
#if defined(TSMP_HAS_BLOCK_COORDINATES)
float4 _MainTex_TexelSize;
SamplerState tsmp_linear_clamp_sampler;

float3 Luma4SamplePair(float2 pixel)
{
    float2 uv = (pixel + 0.5) / float2(_SourceWidth, _SourceHeight);
    uv.y = lerp(uv.y, 1.0 - uv.y, _FlipY);
    return _MainTex.SampleLevel(tsmp_linear_clamp_sampler, uv, 0).rgb;
}
#endif

float Luma4Block(float2 block)
{
#if defined(TSMP_HAS_BLOCK_COORDINATES)
    float size = _SampleSize > 0.5 ? floor(_SampleSize) : (_BlockSize >= 8 ? 4 : 3);
    if (_UseBilinear4 > 0.5 && min(size, _BlockSize) == 4 &&
        all(_MainTex_TexelSize.zw == float2(_SourceWidth, _SourceHeight)))
    {
        float2 pixel = block * _BlockSize + floor((_BlockSize - 4) * 0.5) + 0.5;
        float3 sum = Luma4SamplePair(pixel) + Luma4SamplePair(pixel + float2(2, 0))
                   + Luma4SamplePair(pixel + float2(0, 2)) + Luma4SamplePair(pixel + 2);
        return dot(sum, float3(1.0/12.0, 1.0/12.0, 1.0/12.0));
    }
#endif
    return SampleBlockLuma(block.x, block.y);
}

float CalibrationLuma(int symbol)
{
#if defined(TSMP_CALIBRATION_LUT)
    return _CalibrationLut.Load(int3(symbol, 0, 0)).r;
#else
    float blockX0 = symbol * 2;
    float blockX1 = blockX0 + 1.0;
    float a = Luma4Block(float2(blockX0, 1.0));
    float b = Luma4Block(float2(blockX1, 1.0));
    return (a + b) * 0.5;
#endif
}
