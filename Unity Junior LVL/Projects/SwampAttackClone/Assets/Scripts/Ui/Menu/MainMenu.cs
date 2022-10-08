using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Menu
{
    [SerializeField] private MenuManager _menuManger;

    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _closeButton;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(Exit);
        _closeButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(Exit);
        _closeButton.onClick.RemoveListener(Close);
    }


    private void Close()
    {
        _menuManger.CloseMenu(this);    
    }

    private void Exit()
    {
        Application.Quit();
    }
}
