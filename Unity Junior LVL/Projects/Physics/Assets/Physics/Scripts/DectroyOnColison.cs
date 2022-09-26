using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DectroyOnColison : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
