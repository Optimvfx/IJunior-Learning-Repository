using UnityEngine;
using TMPro;

public class InfoLable : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void Show(string text)
    {
        _text.text = text;
    }
}
