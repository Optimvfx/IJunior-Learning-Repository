using UnityEngine;

public class InfoLableShower : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private InfoLable _prefab;

    public void Show(string text, Vector2 postion)
    {
        InfoLable newInfoLable = Instantiate(_prefab, postion, Quaternion.identity, _container);

        newInfoLable.Show(text);
    }
}
