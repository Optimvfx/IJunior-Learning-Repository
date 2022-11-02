using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(0,100)]
    [SerializeField] private float _speed;
    [SerializeField] private ParticleSystem _dieParticle;

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);    
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(_dieParticle, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
