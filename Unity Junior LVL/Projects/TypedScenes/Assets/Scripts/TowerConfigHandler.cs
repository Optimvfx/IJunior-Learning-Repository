using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class TowerConfigHandler : MonoBehaviour, ISceneLoadHandler<TowerConfig>
{
    public void OnSceneLoaded(TowerConfig argument)
    {
        Debug.Log(argument.ToString());
    }
}
