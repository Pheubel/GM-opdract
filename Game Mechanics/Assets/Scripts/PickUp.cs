using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class PickUp : MonoBehaviour
{
    [SerializeField] UnityEvent OnPickUp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPickUp?.Invoke();
        }
    }

    public void Destroy() =>
        Destroy(gameObject);
}
