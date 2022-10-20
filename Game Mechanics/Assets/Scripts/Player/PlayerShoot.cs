using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Bullet _jumpBullet;
    [SerializeField] Transform _jumpBulletOrigin;
    [SerializeField, Min(0)] float _jumpBulletSpeed = 1;

    public void JumpShoot()
    {
        Debug.Log("Shot!");
        var jb = Instantiate(_jumpBullet, _jumpBulletOrigin.position, Quaternion.identity);

        jb.Damage = 3;
        jb.Velocity = Vector2.down * _jumpBulletSpeed;
        jb.Parent = this.gameObject;
    }
}
