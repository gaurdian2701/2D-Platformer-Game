using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    public SoundType[] SoundsList;

    public AudioSource soundEffect;
    public AudioSource musicEffect;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
    }

    private void Start()
    {
        PlayMusic(Sounds.Music);
    }

    public void PlayMusic(Sounds sound)
    {
        SoundType type = GetSoundClip(sound);

        if (type == null)
            return;

        musicEffect.clip = type.soundClip;
        musicEffect.Play();
    }

    public void PlaySound(Sounds sound)
    {
        SoundType type = GetSoundClip(sound);

        if (type == null)
            return;

        soundEffect.clip = type.soundClip;

        switch (type.playBackType)
        {
            case PlayBackType.Instant:
                soundEffect.PlayOneShot(soundEffect.clip); 
                break;

            case PlayBackType.Continuous:
                if (!soundEffect.isPlaying)
                    soundEffect.Play();
                break;

            default:
                break;
        }
    }

    private SoundType GetSoundClip(Sounds sound)
    {
        SoundType item = Array.Find(SoundsList, i => sound == i.soundType);

        if (item != null)
            return item;

        return null;
    }

}

public enum Sounds
{
    ButtonClick,
    NextLevel,
    PlayerMove,
    PlayerHurt,
    PlayerJump,
    Music,
    DeathMusic
};

public enum PlayBackType
{
    Instant,
    Continuous
};

[Serializable]
public class SoundType
{
    public Sounds soundType;
    public AudioClip soundClip;
    public PlayBackType playBackType;
}
