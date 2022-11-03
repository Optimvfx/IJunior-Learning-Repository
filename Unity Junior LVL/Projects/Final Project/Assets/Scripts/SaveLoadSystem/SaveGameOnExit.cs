using UnityEngine;

public class SaveGameOnExit : MonoBehaviour
{
    [SerializeField] private LvlMenu _savable;

    private void OnEnable()
    {
        _savable.OnGameLoading += OnGameLoading; 
    }

    private void OnDisable()
    {
        _savable.OnGameLoading -= OnGameLoading;
    }

    private void OnApplicationQuit()
    {
        OnGameLoading();
    }

    public void OnGameLoading()
    {
        GameSaveLoader.Save(_savable.Lvls);
    }
}
