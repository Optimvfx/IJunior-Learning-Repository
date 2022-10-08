using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TimeScaleEffector _effector;

    public void OpenMenu(Menu menu)
    {
        _effector.AddTimeStopEffect(menu.gameObject);

        menu.gameObject.SetActive(true);
    }

    public void CloseMenu(Menu menu)
    {
        _effector.RemoveTimeStopEffect(menu.gameObject);

        menu.gameObject.SetActive(false);
    }
}