using UnityEngine;

public class WinMenuOpener : MonoBehaviour
{
    [SerializeField] private MenuManager _menuManager;

    [SerializeField] private Spawner _spawner;

    [SerializeField] private WinMenu _winMenu;

    private void OnEnable()
    {
        _spawner.AllWavesEnded += ActivateWinMenu;
    }

    private void OnDisable()
    {
        _spawner.AllWavesEnded -= ActivateWinMenu;
    }

    private void ActivateWinMenu()
    {
       _menuManager.OpenMenu(_winMenu);
    }
}
