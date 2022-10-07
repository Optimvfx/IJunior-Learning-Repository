using UnityEngine;
using IJunior.TypedScenes;

public class GameLoader : MonoBehaviour
{
    [SerializeField] TowerConfig _towerConfig;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameScene.Load(_towerConfig);
        }
    }
}
