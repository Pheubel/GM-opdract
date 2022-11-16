#if UNITY_ENABLE_ECS
using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Rendering
{
    [MaterialProperty("_Time_Offset")]
    struct TimeOffsetFloatOverride : IComponentData
    {
        public float Value;
    }
}

#endif