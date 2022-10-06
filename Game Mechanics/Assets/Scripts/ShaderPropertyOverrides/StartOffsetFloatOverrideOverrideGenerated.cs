using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Rendering
{
    [MaterialProperty("_Start_Offset")]
    struct StartOffsetFloatOverride : IComponentData
    {
        public float Value;
    }
}
