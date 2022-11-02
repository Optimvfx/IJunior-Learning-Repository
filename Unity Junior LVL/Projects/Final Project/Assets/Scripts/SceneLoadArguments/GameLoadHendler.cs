using IJunior.TypedScenes;
using UnityEngine;

public class GameLoadHendler : MonoBehaviour, ISceneLoadHandler<GameSceneLoadArguments>
{
    [SerializeField] private CylinderFieldGeneratorByImage _generatorByImage;
    [SerializeField] private PlayerMover _playerMover;

    public LvlMenu.ReadOnlyLvlInfo LoadedLvlInfo { get; private set; }

    public void OnSceneLoaded(GameSceneLoadArguments argument)
    {
        LoadedLvlInfo = argument.LvlInfo;

        _playerMover.transform.position = new Vector3(LoadedLvlInfo.PlayerPosition.x, 0, LoadedLvlInfo.PlayerPosition.y);

        _generatorByImage.TryGenerate(LoadedLvlInfo.GameMap);
    }
}
