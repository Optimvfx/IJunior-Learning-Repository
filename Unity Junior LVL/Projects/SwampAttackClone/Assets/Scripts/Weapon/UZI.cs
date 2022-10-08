using UnityEngine;

public class UZI : Weapon
{
    [SerializeField] private UFloat _rotationZRange;

    public override bool TryShoot(Transform shootPoint, out Bullet bullet)
    {
        bullet = Instantiate(Bullet,  shootPoint.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(-_rotationZRange, _rotationZRange))));

        return true;
    }
}
