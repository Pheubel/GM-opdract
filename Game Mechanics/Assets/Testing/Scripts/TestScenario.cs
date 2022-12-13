using UnityEngine;

namespace Testing
{
    public class TestScenario : MonoBehaviour
    {
        [field: SerializeField] 
        public GameObject PlayerPrefab { get; private set; }

        [field:Header("locations")]
        [field:SerializeField]
        public Transform EmptyBox { get; private set; }
        [field:SerializeField]
        public Transform SlopeBottom { get; private set; }
        [field:SerializeField]
        public Transform SlopeTop { get; private set;}
        [field:SerializeField]
        public Transform SlopeDrop { get; private set;}
        [field:SerializeField]
        public Transform DestructableBlocks { get; private set;}
        [field:SerializeField]
        public Transform LeftFromBlock { get; private set;}
        [field:SerializeField]
        public Transform OnBlock { get; private set;}
        [field: SerializeField]
        public Transform RightFromBlock { get; private set;}
    }
}
