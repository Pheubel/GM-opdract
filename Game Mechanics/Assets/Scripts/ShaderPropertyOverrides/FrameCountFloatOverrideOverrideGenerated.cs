using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Rendering
{
#if UNITY_ENABLE_ECS
    [MaterialProperty("_Frame_Count")]
    struct FrameCountFloatOverride : IComponentData
    {
        public float Value;
    }
#endif
}
