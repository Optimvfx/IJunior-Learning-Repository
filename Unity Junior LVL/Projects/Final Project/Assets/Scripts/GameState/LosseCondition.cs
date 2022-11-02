using UnityEngine;
using UnityEngine.Events;

public class LosseCondition : MonoBehaviour
{
    public event UnityAction OnLosse;

    protected void Losse()
    {
        OnLosse?.Invoke();
    }
}
