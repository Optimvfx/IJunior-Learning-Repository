using UnityEngine;

public class GameStateMenuOpener : MonoBehaviour
{
    [SerializeField] private MenuManager _menuManager;

    [SerializeField] private WinMenu _winMenu;
    [SerializeField] private LosseMenu _losseMenu;

    [SerializeField] private GameState _gameState;

    private void OnEnable()
    {
        _gameState.OnWin += OnWin;
        _gameState.OnLosse += OnLosse;
    }

    private void OnDisable()
    {
        _gameState.OnWin -= OnWin;
        _gameState.OnLosse -= OnLosse;
    }

    public void OnWin()
    {
       _menuManager.OpenMenu(_winMenu);
    }

    public void OnLosse()
    {
        _menuManager.OpenMenu(_losseMenu);
    }
}
