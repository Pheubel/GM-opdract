using Cinemachine;
using Unity.Assertions;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CameraZone : MonoBehaviour
{
    CinemachineVirtualCamera _camera;

#if UNITY_ASSERTIONS
    private void Awake()
    {
        _camera = GetComponentInChildren<CinemachineVirtualCamera>();
        Assert.IsNotNull(_camera, "No virtual camera child game object to control.");
    }
#endif

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            _camera.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            _camera.enabled = false;
    }
}
