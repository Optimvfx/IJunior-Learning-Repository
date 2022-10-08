using UnityEngine;

[System.Serializable]
public class UFloat
{
    [Range(0f, float.MaxValue / 2)]
    [SerializeField] private float _value;

    public float Value => _value;

    public UFloat(float value)
    {
        _value = Mathf.Clamp(value, 0, float.MaxValue / 2);
    }

    public static implicit operator float(UFloat uFloat) => uFloat.Value;
    public static implicit operator UFloat(float @float) => new UFloat(@float);
}
