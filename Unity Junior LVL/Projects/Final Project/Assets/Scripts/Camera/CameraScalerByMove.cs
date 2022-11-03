using UnityEngine;

[RequireComponent(typeof(MoveObserver))]
[RequireComponent(typeof(Camera))]
public class CameraScalerByMove : MonoBehaviour
{
    [SerializeField] private float _moveFieldOfViewModiffier;
    [SerializeField] private float _stopFieldOfViewModiffier;
    [Header("Range")]
    [SerializeField] private UFloat _maxFieldOfView;
    [SerializeField] private UFloat _minFieldOfView;

    private MoveObserver _moveObserver;
    private Camera _camera;

    private void OnValidate()
    {
        _maxFieldOfView = Mathf.Max(_minFieldOfView, _maxFieldOfView);
    }

    private void Awake()
    {
        _moveObserver = GetComponent<MoveObserver>();
        _camera = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        _moveObserver.OnStop += OnStop;
        _moveObserver.OnMove += OnMove;
    }

    private void OnDisable()
    {
        _moveObserver.OnStop -= OnStop;
        _moveObserver.OnMove -= OnMove;
    }

    private void OnStop()
    {
        AddFildOfViewModiffer(_stopFieldOfViewModiffier);
    }

    private void OnMove(Vector3 offset)
    {
        AddFildOfViewModiffer(_moveFieldOfViewModiffier);
    }

    private void AddFildOfViewModiffer(float modiffier)
    {
       var newFieldOfView = _camera.fieldOfView + (modiffier * Time.deltaTime);

        _camera.fieldOfView = Mathf.Clamp(newFieldOfView, _minFieldOfView, _maxFieldOfView);
    }
}
