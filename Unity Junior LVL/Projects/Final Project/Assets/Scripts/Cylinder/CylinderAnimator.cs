using UnityEngine;

public class CylinderAnimator : MonoBehaviour
{
    private readonly UFloat _fullCircle = 360;

    [SerializeField] private UFloat _rotationModiffier;
    [SerializeField] private UFloat _rotationSpeed;

    private uint _conectedSawCount = 0;

    private Quaternion _standartRotation;

    private Quaternion _targetRotation;

    private void Awake()
    {
        _standartRotation = transform.rotation;
        _targetRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerSaw saw))
            RotateFromContact(other.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerSaw saw))
        {
            _conectedSawCount--;

            if (_conectedSawCount <= 0)
            {
                BackStandartRotation();
            }
        }
    }

    private void Update()
    {
        Rotate();
    }

    private void RotateFromContact(Vector3 conectionPoint)
    {
        _conectedSawCount++;

        var conectedPointOffset = -(conectionPoint - transform.position);
        var normalizedOffset = conectedPointOffset.normalized;

        var rotationFromConectPoint = normalizedOffset * _fullCircle;

        _targetRotation = Quaternion.Lerp(_standartRotation, Quaternion.Euler(rotationFromConectPoint), _rotationModiffier);
    }


    private void BackStandartRotation()
    {
        _targetRotation = _standartRotation;
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, _rotationSpeed);
    }
}
