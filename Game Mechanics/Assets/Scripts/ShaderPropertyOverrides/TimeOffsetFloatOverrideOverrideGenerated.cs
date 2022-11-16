using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Rendering
{
#if UNITY_ENABLE_ECS
    [MaterialProperty("_Time_Offset")]
    struct TimeOffsetFloatOverride : IComponentData
    {
        public float Value;
    }
#endif
}
