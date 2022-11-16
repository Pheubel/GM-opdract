using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Rendering
{
#if UNITY_ENABLE_ECS
    [MaterialProperty("_Columns")]
    struct ColumnsFloatOverride : IComponentData
    {
        public float Value;
    }
#endif
}
