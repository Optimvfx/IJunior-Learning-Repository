using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Cylinder))]
[RequireComponent(typeof(MeshRenderer))]
public class CylinderColorChanger : MonoBehaviour
{
    [SerializeField] private Material _activatedMaterial;

    [SerializeField] private UnityEvent _onColorChange;

    private Cylinder _cylinder;
    private MeshRenderer _renderer;

    private void Awake()
    {
        _cylinder = GetComponent<Cylinder>();
        _renderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        _cylinder.OnActivate += SetActivateColor;   
    }

    private void OnDisable()
    {
        _cylinder.OnActivate -= SetActivateColor;
    }

    private void SetActivateColor(Cylinder cylinder)
    {
        _renderer.material = _activatedMaterial;

        _onColorChange?.Invoke();
    }
}
