using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSaw : MonoBehaviour
{
    private List<Cylinder> _connections = new List<Cylinder>();

    public bool IsConected => _connections.Count > 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cylinder cylinder))
            _connections.Add(cylinder);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Cylinder cylinder))
            _connections.Remove(cylinder);
    }
}
