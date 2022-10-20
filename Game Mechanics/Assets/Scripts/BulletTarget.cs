using UnityEngine;
using UnityEngine.Events;

public class BulletTarget : MonoBehaviour
{
    [SerializeField] UnityEvent<float> OnHit;

    public void HandleHit(float damage)
    {
        OnHit?.Invoke(damage);
    }
}
