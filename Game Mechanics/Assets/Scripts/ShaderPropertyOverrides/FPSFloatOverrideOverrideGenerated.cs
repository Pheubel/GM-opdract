using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Rendering
{
#if UNITY_ENABLE_ECS
    [MaterialProperty("_FPS")]
    struct FPSFloatOverride : IComponentData
    {
        public float Value;
    }
#endif
}
