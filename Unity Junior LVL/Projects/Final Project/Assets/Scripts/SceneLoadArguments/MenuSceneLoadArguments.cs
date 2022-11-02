using UnityEngine;

[System.Serializable]
public class MenuSceneLoadArguments
{
    [SerializeField] private LvlMenu.ReadOnlyLvlInfo _passedLvl;
    [SerializeField] private bool _isWin;

    public LvlMenu.ReadOnlyLvlInfo PassedLvl => _passedLvl;
    public bool IsWin => _isWin;

    public MenuSceneLoadArguments(LvlMenu.ReadOnlyLvlInfo passedLvl, bool isWin)
    {
        if (passedLvl == null)
            throw new System.ArgumentNullException();

        _passedLvl = passedLvl;
        _isWin = isWin;
    }
}
