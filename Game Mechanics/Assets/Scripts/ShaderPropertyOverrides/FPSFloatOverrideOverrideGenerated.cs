using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Rendering
{
    [MaterialProperty("_FPS")]
    struct FPSFloatOverride : IComponentData
    {
        public float Value;
    }
}
