using UnityEngine.UI;
using UnityEngine;

public class StateMenuOpener : MonoBehaviour
{
    [SerializeField] private MenuManager _menuManger;
    [SerializeField] private GameState _gameState;

    [SerializeField] private WinMenu _winMenu;
    [SerializeField] private LosseMenu _losseMenu;

    private void OnEnable()
    {
        _gameState.OnWin += OpenWinMenu;
        _gameState.OnLosse += OpenLosseMenu;
    }

    private void OnDisable()
    {
        _gameState.OnWin -= OpenWinMenu;
        _gameState.OnLosse -= OpenLosseMenu;
    }

    private void OpenWinMenu()
    {
       _menuManger.OpenMenu(_winMenu);
    }

    private void OpenLosseMenu()
    {
        _menuManger.OpenMenu(_losseMenu);
    }
}
