using UnityEngine;

namespace K13A.TSMP
{
    [AddComponentMenu("TSMP/Codecs/Luma4 Codec")]
    public sealed class TSMPCodecLuma4 : TSMPCodec
    {
        public Material byteDecodeMaterial;
        [Tooltip("Average a native-resolution 4x4 sample with four bilinear reads. Other footprints keep the standard sampler.")]
        public bool useBilinear4;
        [Tooltip("Prepare the calibration LUT even for single-sample decoding. Leave disabled to avoid the extra pass.")]
        public bool preferCalibrationLut;

#if !COMPILER_UDONSHARP
        public override bool SupportsGpuLuma4Encoding => true;

        public override int SymbolMode => (int)K13A.TSMP.SymbolMode.Luma4;
        public override int GetPayloadStartRow(int width, int blockSize) => Luma4Raster.PayloadStartRow;
        public override int GetPayloadCapacityBytes(int width, int height, int blockSize) => Luma4Raster.GetPayloadCapacityBytes(width, height, blockSize);
        public override int GetPayloadBlocksForBytes(int byteCount) => byteCount * 2;

        public override bool TryWriteFrame(Texture2D texture, int blockSize, byte[] headerBytes, byte[] payloadBytes, out string error)
        {
            return Luma4Raster.TryWriteFrame(texture, blockSize, headerBytes, payloadBytes, out error);
        }

        public override bool TryWriteFrameBuffered(Texture2D texture, int blockSize, byte[] headerBytes, byte[] payloadBytes, ref Color32[] pixels, out string error)
        {
            return Luma4Raster.TryWriteFrameBuffered(texture, blockSize, headerBytes, payloadBytes, ref pixels, out error);
        }

        public override byte[] GetCodecOptionBytes() => null;
        public override int DecodeMaterialCount => byteDecodeMaterial != null ? 1 : 0;
        public override Material GetDecodeMaterial(int index) => index == 0 ? byteDecodeMaterial : null;
#endif

        public override void PrepareDecode(Texture source, Material material)
        {
            base.PrepareDecode(source, material);
            if (material == null)
                return;
            material.SetFloat("_UseBilinear4", useBilinear4 ? 1f : 0f);
            if (!preferCalibrationLut && GetDecodeSampleSize(material) <= 1)
                return;
            PrepareCalibrationLut(source, material, 16);
        }

        public override void ApplyDecodeOptions()
        {
            selectedDecodeMaterial = byteDecodeMaterial;
            payloadStartRow = 5;
            payloadBlockCount = byteCount * 2;
        }
    }
}
