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
        _gameState.OnWin += OnWin;
        _gameState.OnLosse += OnLosse;
    }

    private void OnDisable()
    {
        _gameState.OnWin -= OnWin;
        _gameState.OnLosse -= OnLosse;
    }

    private void OnWin()
    {
       _menuManger.OpenMenu(_winMenu);
    }

    private void OnLosse()
    {
        _menuManger.OpenMenu(_losseMenu);
    }
}
