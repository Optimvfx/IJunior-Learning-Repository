using UnityEngine;

[RequireComponent(typeof(PlayerShoter))]
public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _shotSound;

    private PlayerShoter _shoter;

    private void Awake()
    {
        _shoter = GetComponent<PlayerShoter>();
    }

    private void OnEnable()
    {
        _shoter.OnShot += PlayShotSound;
    }

    private void OnDisable()
    {
        _shoter.OnShot += PlayShotSound;
    }

    private void PlayShotSound()
    {
        _shotSound.Play();
    }
}
