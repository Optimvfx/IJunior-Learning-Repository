using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    [SerializeField] private uint _damage;
    [SerializeField] private UFloat _speed;

    public UnityAction<Bullet> OnDestroy;

    private void Update()
    {
        Debug.Log(transform.forward);
        transform.Translate(Vector2.left * _speed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ReadOnlyEnemy enemy))
        {
            enemy.TakeDamage((int)_damage);
        }

        OnDestroy?.Invoke(this);

        Destroy(gameObject);
    }
}