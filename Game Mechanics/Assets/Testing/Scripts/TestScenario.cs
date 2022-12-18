using UnityEngine;

namespace Testing
{
    public class TestScenario : MonoBehaviour
    {
        [field: SerializeField]
        public GameObject PlayerPrefab { get; private set; }

        [field: Header("locations")]
        [field: SerializeField]
        public Transform EmptyBox { get; private set; }

        [field: SerializeField]
        public Transform SlopeBottom { get; private set; }

        [field: SerializeField]
        public Transform SlopeTop { get; private set; }

        [field: SerializeField]
        public Transform SlopeDrop { get; private set; }

        [field: SerializeField]
        public Transform DestructableBlocks { get; private set; }

        [field: SerializeField]
        public Transform LeftFromBlock { get; private set; }

        [field: SerializeField]
        public Transform OnBlock { get; private set; }

        [field: SerializeField]
        public Transform RightFromBlock { get; private set; }

        [field: Header("triggers")]
        [field: SerializeField]
        public TriggerInformation TriggerLeftFromEmpty { get; private set; }

        [field: SerializeField]
        public TriggerInformation TriggerRightFromEmpty { get; private set; }

        [field: SerializeField]
        public TriggerInformation TriggerLeftFromSlope { get; private set; }

        [field: SerializeField]
        public TriggerInformation TriggerRightFromSlope { get; private set; }

        [field: SerializeField]
        public TriggerInformation TriggerAboveDestructableBlock { get; private set; }

        [field: SerializeField]
        public TriggerInformation TriggerOnDestructableBlock { get; private set; }

        [field: SerializeField]
        public TriggerInformation TriggerBelowDestructableBlock { get; private set; }

        [field: SerializeField]
        public TriggerInformation TriggerLeftFromBlock { get; private set; }

        [field: SerializeField]
        public TriggerInformation TriggerRightFromBlock { get; private set; }

        [field: SerializeField]
        public TriggerInformation TriggerOnTopOfBlock { get; private set; }
    }
}
