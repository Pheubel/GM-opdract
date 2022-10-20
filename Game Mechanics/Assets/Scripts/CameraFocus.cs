using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    [SerializeField] GameObject _target;

    private void LateUpdate()
    {
        var targetPosition = _target.transform.position;
        var originalPosition = transform.position;
        transform.position = new Vector3(targetPosition.x, targetPosition.y, originalPosition.z);
    }
}
