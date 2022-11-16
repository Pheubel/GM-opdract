#if UNITY_ENABLE_ECS
using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Rendering
{
    [MaterialProperty("_Rows")]
    struct RowsFloatOverride : IComponentData
    {
        public float Value;
    }
}

#endif