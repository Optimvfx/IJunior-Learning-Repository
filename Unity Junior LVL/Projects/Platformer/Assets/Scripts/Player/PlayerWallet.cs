using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField] private int _money;

    private void OnValidate()
    {
        _money = Mathf.Max(_money, 0);
    }

    public void AddMoney()
    {
        _money++;
    }
}
