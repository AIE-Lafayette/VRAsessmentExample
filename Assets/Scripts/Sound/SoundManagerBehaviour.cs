
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerBehaviour : MonoBehaviour
{
    private static SoundManagerBehaviour _instance;
    [SerializeField] private AudioSource _soundEffectSource;
    [SerializeField] private AudioSource _musicSource;
    private AudioClip _lastClip;

    /// <summary>
    /// Gets the static instance of the sound manager. Creates one if none exists
    /// </summary>
    public static SoundManagerBehaviour Instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType(typeof(SoundManagerBehaviour)) as SoundManagerBehaviour;

            if (!_instance)
            {
                GameObject blackBoard = new GameObject("SoundManager");
                _instance = blackBoard.AddComponent<SoundManagerBehaviour>();

                _instance._musicSource = new GameObject("MusicSource").AddComponent<AudioSource>();
                _instance._musicSource.transform.SetParent(_instance.transform);

                _instance._soundEffectSource = new GameObject("SoundEffectSource").AddComponent<AudioSource>();
                _instance._soundEffectSource.transform.SetParent(_instance.transform);
            }

            return _instance;
        }
    }

    public void TogglePauseMusic()
    {
        if (_musicSource.isPlaying)
            _musicSource.Pause();
        else
            _musicSource.UnPause();
    }

    public void StopSound(AudioClip clip)
    {
        if (!_soundEffectSource.isPlaying || clip != _lastClip)
            return;

        _soundEffectSource.Stop();
    }

    public void PlaySound(AudioClip clip, float volumeScale)
    {
        if (!clip)
            return;

        _lastClip = clip;
        _soundEffectSource.clip = clip;
        _soundEffectSource.PlayOneShot(clip, volumeScale);

    }
        
    public void PlaySound(AudioClip clip)
    {
        if (!clip)
            return;

        _lastClip = clip;
        _soundEffectSource.PlayOneShot(clip);
    }
    public void SetMusic(AudioClip music)
    {
        if (!music)
            return;

        _musicSource.Stop();    

        _musicSource.clip = music;
        _musicSource.Play();
    }

}
