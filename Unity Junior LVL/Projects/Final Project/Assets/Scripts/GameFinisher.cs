using IJunior.TypedScenes;
using System.Collections;
using UnityEngine;

public class GameFinisher : MonoBehaviour
{
    [SerializeField] private GameLoadHendler _gameLoadHendler;
    [SerializeField] private UFloat _finishDellayInSecond;

    public void WinGame()
    {
        StartCoroutine(FinishGame(true));
    }

    public void LosseGame()
    {
        StartCoroutine(FinishGame(false));
    }

    private IEnumerator FinishGame(bool isPlayerWin)
    {
        yield return new WaitForSecondsRealtime(_finishDellayInSecond);

        if (_gameLoadHendler.LoadedLvlInfo != null)
           MenuScene.LoadAsync(new MenuSceneLoadArguments(_gameLoadHendler.LoadedLvlInfo, isPlayerWin));
    }
}
