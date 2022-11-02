using UnityEngine;

public class LoadGameOnEnter : MonoBehaviour
{
    [SerializeField] private LvlMenu _lvlMenu;

    private void Awake()
    {
        _lvlMenu.Init(GameSaveLoader.Load());
    }
}
