using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : Menu
{
    [SerializeField] private Button _playButton;
    [SerializeField] private MenuManager _menuManager;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(Play);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(Play);
    }

    private void Play()
    {
        _menuManager.CloseMenu(this);
    }
}
