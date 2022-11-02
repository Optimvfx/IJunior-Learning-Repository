using UnityEngine;

public class SaveGameOnExit : MonoBehaviour
{
    [SerializeField] private LvlMenu _savable;

    private void OnEnable()
    {
        _savable.OnGameLoading += Save; 
    }

    private void OnDisable()
    {
        _savable.OnGameLoading -= Save;
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    public void Save()
    {
        GameSaveLoader.Save(_savable.Lvls);
    }
}
