using UnityEngine;

public class VitaToLisaLipSync : MonoBehaviour
{
    public Component vitaProxy;          // Drag Vita's VRMBlendShapeProxy here
    public SkinnedMeshRenderer lisaMesh; // Drag Lisa's SkinnedMeshRenderer here

    public int A = 15;
    public int I = 16;
    public int U = 17;
    public int E = 18;
    public int O = 19;

    // Cached method info to call GetValue dynamically
    System.Reflection.MethodInfo getValueMethod;
    object blendShapeKeyA, blendShapeKeyI, blendShapeKeyU, blendShapeKeyE, blendShapeKeyO;

    void Start()
    {
        if (vitaProxy != null)
        {
            var proxyType = vitaProxy.GetType();

            // Create BlendShapeKey instances dynamically
            var blendShapeKeyType = proxyType.Assembly.GetType("VRM.BlendShapeKey");
            var createFromPreset = blendShapeKeyType.GetMethod("CreateFromPreset");

            var presetType = proxyType.Assembly.GetType("VRM.BlendShapePreset");
            blendShapeKeyA = createFromPreset.Invoke(null, new object[] { System.Enum.Parse(presetType, "A") });
            blendShapeKeyI = createFromPreset.Invoke(null, new object[] { System.Enum.Parse(presetType, "I") });
            blendShapeKeyU = createFromPreset.Invoke(null, new object[] { System.Enum.Parse(presetType, "U") });
            blendShapeKeyE = createFromPreset.Invoke(null, new object[] { System.Enum.Parse(presetType, "E") });
            blendShapeKeyO = createFromPreset.Invoke(null, new object[] { System.Enum.Parse(presetType, "O") });

            // Cache GetValue method
            getValueMethod = proxyType.GetMethod("GetValue");
        }
    }

    void Update()
    {
        if (vitaProxy == null || lisaMesh == null || getValueMethod == null) return;

        // Dynamically read Vita's blendshape weights
        float aWeight = (float)getValueMethod.Invoke(vitaProxy, new object[] { blendShapeKeyA });
        float iWeight = (float)getValueMethod.Invoke(vitaProxy, new object[] { blendShapeKeyI });
        float uWeight = (float)getValueMethod.Invoke(vitaProxy, new object[] { blendShapeKeyU });
        float eWeight = (float)getValueMethod.Invoke(vitaProxy, new object[] { blendShapeKeyE });
        float oWeight = (float)getValueMethod.Invoke(vitaProxy, new object[] { blendShapeKeyO });

        // Apply to Lisa's blendshapes
        lisaMesh.SetBlendShapeWeight(A, aWeight * 100f);
        lisaMesh.SetBlendShapeWeight(I, iWeight * 100f);
        lisaMesh.SetBlendShapeWeight(U, uWeight * 100f);
        lisaMesh.SetBlendShapeWeight(E, eWeight * 100f);
        lisaMesh.SetBlendShapeWeight(O, oWeight * 100f);
    }
}
