using UnityEngine;
using UnityEngine.UI;

public class BaseMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _aboutAuthorsButton;
    [SerializeField] private Button _exitButton;

    [Header("Info Lable")]
    [SerializeField] private InfoLableShower _infoLableShower;

    [SerializeField] private string _aboutAuthorsText;
    [SerializeField] private Transform _aboutAuthorsPosition;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(Play);
        _aboutAuthorsButton.onClick.AddListener(ShowAboutAthorsInfo);
        _exitButton.onClick.AddListener(Exit);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(Play);
        _aboutAuthorsButton.onClick.RemoveListener(ShowAboutAthorsInfo);
        _exitButton.onClick.RemoveListener(Exit);
    }

    private void Play()
    {
        Debug.Log("Game started;");
    }

    private void ShowAboutAthorsInfo()
    {
        _infoLableShower.Show(_aboutAuthorsText, _aboutAuthorsPosition.position);
    }

    private void Exit()
    {
        Debug.Log("Exit;");
    }
}
