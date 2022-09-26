using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioChangerByThreatLevel : MonoBehaviour
{
    [SerializeField] private AlarmSystem _threatController;
    [SerializeField] private AudioSource _audioSource;

    private void Update()
    {
        ChangeAudioVolume();
    }

    private void ChangeAudioVolume()
    {
        _audioSource.volume = _threatController.NormolizedThreatLevel;
    }
}
