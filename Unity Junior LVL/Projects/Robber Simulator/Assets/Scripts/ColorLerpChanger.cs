using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorLerpChanger : MonoBehaviour
{
    [SerializeField] private Color _negativeColor;
    [SerializeField] private Color _positiveColor;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeColor(float lerpValue)
    {
        _spriteRenderer.color = Color.Lerp(_negativeColor, _positiveColor, lerpValue);
    }
}
