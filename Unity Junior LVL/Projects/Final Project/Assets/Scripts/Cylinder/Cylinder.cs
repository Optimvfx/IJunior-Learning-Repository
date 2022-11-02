using UnityEngine;
using UnityEngine.Events;

public class Cylinder : MonoBehaviour
{
    private bool _isActivated = false;

    public event UnityAction<Cylinder> OnActivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
            TryActivate(other);
    }

    private void TryActivate(Collider other)
    {
        if (_isActivated)
            return;

        OnActivate?.Invoke(this);

        _isActivated = true;
    }
}
