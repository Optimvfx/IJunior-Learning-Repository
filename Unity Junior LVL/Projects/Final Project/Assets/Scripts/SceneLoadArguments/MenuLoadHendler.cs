using IJunior.TypedScenes;
using UnityEngine;

public class MenuLoadHendler : MonoBehaviour, ISceneLoadHandler<MenuSceneLoadArguments>
{
    [SerializeField] private LvlMenu _lvlMenu;

    public void OnSceneLoaded(MenuSceneLoadArguments argument)
    {
        Time.timeScale = TimeScaleEffector.StandartTimeScale;

        if (argument.IsWin)
            _lvlMenu.AddFinishedLvl(argument.PassedLvl);
    }
}
