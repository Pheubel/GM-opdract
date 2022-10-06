using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Rendering
{
    [MaterialProperty("_Frame_Count")]
    struct FrameCountFloatOverride : IComponentData
    {
        public float Value;
    }
}
