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
