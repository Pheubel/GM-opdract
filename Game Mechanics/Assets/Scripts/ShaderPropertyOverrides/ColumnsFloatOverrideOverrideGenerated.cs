using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Rendering
{
    [MaterialProperty("_Columns")]
    struct ColumnsFloatOverride : IComponentData
    {
        public float Value;
    }
}
