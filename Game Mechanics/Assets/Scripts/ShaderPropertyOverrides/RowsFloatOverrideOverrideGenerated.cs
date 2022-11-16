using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Rendering
{
#if UNITY_ENABLE_ECS
    [MaterialProperty("_Rows")]
    struct RowsFloatOverride : IComponentData
    {
        public float Value;
    }
#endif
}
