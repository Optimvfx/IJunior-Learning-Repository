using UnityEngine;

[System.Serializable]
public class GameSceneLoadArguments : MonoBehaviour
{
    [SerializeField] private LvlMenu.ReadOnlyLvlInfo _lvlInfo;

    public LvlMenu.ReadOnlyLvlInfo LvlInfo => _lvlInfo;

    public GameSceneLoadArguments(LvlMenu.ReadOnlyLvlInfo lvlInfo)
    {
        _lvlInfo = lvlInfo;
    }
}
