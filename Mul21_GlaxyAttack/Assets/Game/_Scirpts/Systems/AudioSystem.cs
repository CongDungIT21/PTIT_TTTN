using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public enum SOUND_NAME
{
    None,
    ShipShoot,
    Clamp
}

[System.Serializable]
public class Sound
{
    public SOUND_NAME name;
    public AudioSource source;
} 

public class AudioSystem : Singleton<AudioSystem>
{
    public Sound[] sounds;
    public AudioSource music;

    private Dictionary<SOUND_NAME, AudioSource> soundSource = new Dictionary<SOUND_NAME, AudioSource>();
    private bool _isOnMusic { get; set; } = true;
    private bool _isOnSound { get; set; } = true;

    public bool IsOnMusic
    {
        get
        {
            return _isOnMusic;
        }
        set
        {
            _isOnMusic = value;
            if (_isOnMusic) { PlayMusic(); } else StopMusic();
        }
    }

    public bool IsOnSound
    {
        get
        {
            return _isOnSound;
        }
        set
        {
            _isOnSound = value;
        }
    }

    private void Start()
    {
        PreAllSound();
        PreMusic();

        PlayMusic();
    }

    private void PreAllSound()
    {
        foreach (var sound in sounds)
        {
            if(!soundSource.ContainsKey(sound.name))
                soundSource.Add(sound.name, sound.source);
            sound.source.Stop();
            sound.source.playOnAwake = false;
            sound.source.loop = false;
        }
    }

    private void PreMusic()
    {
        StopMusic();
        music.playOnAwake = false;
        music.loop = true;
    }

    public void PlayMusic()
    {
        music.Play();
    }

    public void StopMusic()
    {
        music.Stop();
    }

    public void PlaySoundByName(SOUND_NAME soundName)
    {
        if (!_isOnSound) return;
        if(soundSource.ContainsKey(soundName))
        {
            soundSource[soundName].Play();
        }
    }

    public void StopSoundByName(SOUND_NAME soundName)
    {
        if (soundSource.ContainsKey(soundName))
        {
            soundSource[soundName].Stop();
        }
    }

    public void StopAllSound()
    {
        foreach (var sound in soundSource)
        {
            sound.Value.Stop();
        }
    }
}
