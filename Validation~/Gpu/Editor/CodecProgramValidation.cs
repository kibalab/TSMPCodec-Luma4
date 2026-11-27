#if UDONSHARP
using System;
using UdonSharp;
using UnityEditor;
using UnityEngine;
using VRC.Udon.ProgramSources;

public static class CodecProgramValidation
{
    public static void Run()
    {
        foreach (string name in new[] { "Luma4", "RGB16", "RGB20", "Color256" })
        {
            string root = "Packages/com.kibalab.tsmp.codec." + name.ToLowerInvariant() + "/Runtime/Scripts/";
            var asset = AssetDatabase.LoadAssetAtPath<UdonSharpProgramAsset>(root + "TSMPCodec" + name + ".asset");
            var reference = new SerializedObject(asset).FindProperty("serializedUdonProgramAsset");
            var compiled = reference.objectReferenceValue as SerializedUdonProgramAsset;
            if (!compiled || !AssetDatabase.GetAssetPath(compiled).StartsWith(root, StringComparison.Ordinal))
                throw new InvalidOperationException(name + ": compiled Udon program is not in its own package");
            var program = compiled.RetrieveProgram();
            if (program == null || program.ByteCode.Length == 0 ||
                !program.SymbolTable.HasAddressForSymbol("useBilinear4") ||
                (name != "Color256" && !program.SymbolTable.HasAddressForSymbol("preferCalibrationLut")))
                throw new InvalidOperationException(name + ": missing codec bytecode or receive options");
            Debug.Log("PASS packaged codec program: " + AssetDatabase.GetAssetPath(compiled));
        }
    }
}
#endif
