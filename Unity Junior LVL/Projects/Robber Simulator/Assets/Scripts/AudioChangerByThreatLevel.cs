using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioChangerByThreatLevel : MonoBehaviour
{
    [SerializeField] private ThreatController _threatController;
    [SerializeField] private AudioSource _audioSource;

    public void OnEnable()
    {
        _threatController.ThreatLevelChanged += ChangeAudioVolume;
    }

    public void OnDisable()
    {
        _threatController.ThreatLevelChanged += ChangeAudioVolume;
    }

    private void ChangeAudioVolume(float threatLevel)
    {
        var audioVolume = (threatLevel - ThreatController.MinimalThreatLevel) / (ThreatController.MaximalThreatLevel - ThreatController.MinimalThreatLevel);

        _audioSource.volume = audioVolume;
    }
}
