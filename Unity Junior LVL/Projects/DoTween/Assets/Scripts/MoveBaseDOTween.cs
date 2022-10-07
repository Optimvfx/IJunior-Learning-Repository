using DG.Tweening;
using UnityEngine;

public class MoveBaseDOTween : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _durectionInSeconds;

    [SerializeField] private float _delayInSeconds;

    [SerializeField] private int _loops;

    private void OnValidate()
    {
        _delayInSeconds = Mathf.Max(_delayInSeconds, 0);
        _durectionInSeconds = Mathf.Max(_durectionInSeconds, 0);
    
        _loops = Mathf.Max(_loops, 1);
    }

    void Start()
    {
        var moveTread = transform.DOMove(_target.position, _durectionInSeconds).From(transform.position);
        moveTread.SetDelay(_delayInSeconds);
        moveTread.SetLoops(_loops, LoopType.Yoyo);
    }
}
