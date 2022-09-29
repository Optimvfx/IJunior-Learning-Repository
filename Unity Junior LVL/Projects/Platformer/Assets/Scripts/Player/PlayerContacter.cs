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

    private void OnTriggerEnter(Collider other)
    {
        OnContactEnter(other.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnContactEnter(collision.gameObject);
    }

    private void OnContactEnter(GameObject otherGameObject)
    {
        if (otherGameObject.TryGetComponent(out Coin coin))
        {
            _wallet.AddMoney();

            coin.Destroy();
        }
        if (otherGameObject.TryGetComponent(out Enemy enemy))
        {
            _health.TakeDamage();
        }
    }
}
