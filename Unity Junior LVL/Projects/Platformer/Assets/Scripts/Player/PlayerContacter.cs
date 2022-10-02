using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(PlayerWallet))]
[RequireComponent(typeof(PlayerHealth))]
public class PlayerContacter : MonoBehaviour
{
    private PlayerWallet _wallet;
    private PlayerHealth _health;

    private void Awake()
    {
        _wallet = GetComponent<PlayerWallet>();
        _health = GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            _wallet.AddMoney();

            coin.Destroy();
        }
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            _health.TakeDamage();
        }
    }

}
