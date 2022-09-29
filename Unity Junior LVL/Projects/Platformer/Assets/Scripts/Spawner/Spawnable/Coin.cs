using UnityEngine;

public class Coin : Spawnable
{
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        Destroy(gameObject);
    }
}
