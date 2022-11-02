using UnityEditor;
using UnityEngine;

public static class GameSaveLoader 
{
    private const string _saveKey = "LvlMenu";

    private static SaveLoadHandler<LvlMenu.ReadOnlyLvlList> _lvlListSaveLoadHandler;

    static GameSaveLoader()
    {
        _lvlListSaveLoadHandler = new SaveLoadHandler<LvlMenu.ReadOnlyLvlList>();
    }

    public static void Save(LvlMenu.ReadOnlyLvlList savable)
    {
        _lvlListSaveLoadHandler.Save(savable, _saveKey);
    }

    public static LvlMenu.ReadOnlyLvlList Load()
    {
        if (_lvlListSaveLoadHandler.TryLoad(_saveKey, out LvlMenu.ReadOnlyLvlList lvlList))
            return lvlList;

        return null;
    }

    [MenuItem("SaveLoad/GameInfo/Clear")]
    public static void ClearSave()
    {
        _lvlListSaveLoadHandler.Clear(_saveKey);
    }
}
