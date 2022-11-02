using UnityEngine;
using UnityEngine.Events;

public class WinCondition : MonoBehaviour
{
    public event UnityAction OnWin;

    protected void Win()
    {
        OnWin?.Invoke();
    }
}
