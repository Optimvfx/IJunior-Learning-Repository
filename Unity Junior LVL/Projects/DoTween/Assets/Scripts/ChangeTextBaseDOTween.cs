using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChangeTextBaseDOTween : MonoBehaviour
{
    [SerializeField] private float _textChangeSpeedInSeconds;

    [SerializeField] private Text _changingText;
    [SerializeField] private string _changingTextTarget;

    [SerializeField] private Text _changingRelativeText;
    [SerializeField] private string _changingRelativeTextTarget;

    [SerializeField] private Text _hackingText;
    [SerializeField] private string _hackingTextTarget;
    [SerializeField] private Color _hackedColor;

    private void OnValidate()
    {
        _textChangeSpeedInSeconds = Mathf.Max(_textChangeSpeedInSeconds, 0);
    }

    private void Start()
    {
        _changingText.DOText(_changingTextTarget, _textChangeSpeedInSeconds);

        _changingRelativeText.DOText(_changingRelativeTextTarget, _textChangeSpeedInSeconds).SetRelative();

        _hackingText.DOText(_hackingTextTarget, _textChangeSpeedInSeconds, true, ScrambleMode.All);
        _hackingText.DOColor(_hackedColor, _textChangeSpeedInSeconds);
    }
}
