using UnityEngine.UI;
using UnityEngine;

public class ShopMenu : Menu
{
    [SerializeField] private MenuManager _menuManger;

    [SerializeField] private Button _closeButton;

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(Close);
    }


    private void Close()
    {
        _menuManger.CloseMenu(this);
    }
}
