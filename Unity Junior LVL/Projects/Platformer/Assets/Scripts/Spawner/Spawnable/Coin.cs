using UnityEngine;

public class Coin : Spawnable
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
