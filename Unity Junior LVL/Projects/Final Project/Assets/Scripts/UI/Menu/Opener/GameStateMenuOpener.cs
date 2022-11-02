using UnityEngine;

public class GameStateMenuOpener : MonoBehaviour
{
    [SerializeField] private MenuManager _menuManager;

    [SerializeField] private WinMenu _winMenu;
    [SerializeField] private LosseMenu _losseMenu;

    [SerializeField] private GameState _gameState;

    private void OnEnable()
    {
        _gameState.OnWin += ActivateWinMenu;
        _gameState.OnLosse += ActivateLosseMenu;
    }

    private void OnDisable()
    {
        _gameState.OnWin -= ActivateWinMenu;
        _gameState.OnLosse -= ActivateLosseMenu;
    }

    public void ActivateWinMenu()
    {
       _menuManager.OpenMenu(_winMenu);
    }

    public void ActivateLosseMenu()
    {
        _menuManager.OpenMenu(_losseMenu);
    }
}
