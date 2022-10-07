using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerConfig 
{
    [SerializeField] private int _length;

    public override string ToString()
    {
        return _length.ToString();
    }
}
