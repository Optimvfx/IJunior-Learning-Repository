using System.Collections;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] private UFloat _rechargeTimeInSeconds;

    private Coroutine _rechargeCorutine;

    private bool _canShot = true;

    public override bool TryShoot(Transform shootPoint, out Bullet bullet)
    {
        bullet = default(Bullet);

        if (_canShot == false)
            return false;

        if (_rechargeCorutine != null)
            StopCoroutine(_rechargeCorutine);

        _rechargeCorutine = StartCoroutine(Recharge());

        bullet = Instantiate(Bullet, shootPoint.position, Quaternion.identity);

        return true;
    }

    public IEnumerator Recharge()
    {
        _canShot = false;

        yield return new WaitForSeconds(_rechargeTimeInSeconds);

        _canShot = true;
    }
}