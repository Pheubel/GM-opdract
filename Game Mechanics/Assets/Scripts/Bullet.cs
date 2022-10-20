using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    public GameObject Parent { get; set; }
    public float Damage { get; set; }
    public Vector2 Velocity { get; set; }

    [SerializeField] UnityEvent OnHit;
    [SerializeField] LayerMask _staticCollissionMask = 55;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        _transform.position += (Vector3)Velocity * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((_staticCollissionMask & (1 << other.gameObject.layer)) != 0)
        {
            OnHit?.Invoke();
        }
        else
        {
            if (other.TryGetComponent<BulletTarget>(out var target))
            {
                if (Parent != target.gameObject)
                {
                    target.HandleHit(Damage);
                    OnHit?.Invoke();
                }
            }
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
