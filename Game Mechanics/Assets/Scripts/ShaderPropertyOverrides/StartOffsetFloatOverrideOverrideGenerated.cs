using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Rendering
{
#if UNITY_ENABLE_ECS
    [MaterialProperty("_Start_Offset")]
    struct StartOffsetFloatOverride : IComponentData
    {
        public float Value;
    }
#endif
}
