using UnityEngine;

public class DestroyerSelf : MonoBehaviour
{
    [SerializeField] private float _timeInSecondsBetwenDestroy;

    private void OnValidate()
    {
        _timeInSecondsBetwenDestroy = Mathf.Max(0, _timeInSecondsBetwenDestroy);
    }

    public void Destroy()
    {
        Destroy(gameObject, _timeInSecondsBetwenDestroy);
    }
}
