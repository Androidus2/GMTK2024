using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{

    [SerializeField]
    private SoundClip[] soundClips;

    private static SoundManager instance;

    private List<SoundClip> currentSceneClips;

    private float volumeMultiplier = 1f;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            for(int i = 0; i < soundClips.Length; i++)
            {
                SoundClip soundClip = soundClips[i];
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = soundClip.clip;
                audioSource.loop = soundClip.loop;
                audioSource.volume = soundClip.volume;
                audioSource.pitch = soundClip.pitch;
                soundClip.source = audioSource;
            }

            PlaySound("BackgroundMusic");

            currentSceneClips = new List<SoundClip>();

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static SoundManager GetInstance()
    {
        return instance;
    }

    public AudioClip GetClip(string name)
    {
        foreach(SoundClip soundClip in soundClips)
        {
            if(soundClip.name == name)
            {
                return soundClip.clip;
            }
        }
        return null;
    }

    public void PlaySound(string name)
    {
        for(int i=0; i<soundClips.Length; i++)
        {
            SoundClip soundClip = soundClips[i];
            if(soundClip.name == name)
            {
                soundClip.source.Play();
                return;
            }
        }
    }

    public bool GetClipIsPlaying(string name)
    {
        for(int i=0; i<soundClips.Length; i++)
        {
            SoundClip soundClip = soundClips[i];
            if(soundClip.name == name)
            {
                return soundClip.source.isPlaying;
            }
        }
        return false;
    }

    public void StopSound(string name)
    {
        for(int i=0; i<soundClips.Length; i++)
        {
            SoundClip soundClip = soundClips[i];
            if(soundClip.name == name)
            {
                soundClip.source.Stop();
                return;
            }
        }
    }

    public void SetGlobalVolume(float volume)
    {
        volumeMultiplier = volume;
        for(int i=0; i<soundClips.Length; i++)
        {
            SoundClip soundClip = soundClips[i];
            soundClip.source.volume = soundClip.volume * volume;
        }
        for(int i=0; i<currentSceneClips.Count; i++)
        {
            SoundClip soundClip = currentSceneClips[i];
            soundClip.source.volume = soundClip.volume * volume;
        }
    }

    public float GetGlobalVolume()
    {
        return volumeMultiplier;
    }

    public void SetGlobalPitch(float pitch)
    {
        for(int i=0; i<soundClips.Length; i++)
        {
            SoundClip soundClip = soundClips[i];
            soundClip.source.pitch = soundClip.pitch * pitch;
        }
        for(int i=0; i<currentSceneClips.Count; i++)
        {
            SoundClip soundClip = currentSceneClips[i];
            soundClip.source.pitch = soundClip.pitch * pitch;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);

        float pitchMultiplier = soundClips[0].source.pitch / soundClips[0].pitch;

        // Find every AudioSource in the scene and add it to the list of currentSceneClips
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach(AudioSource audioSource in audioSources)
        {
            SoundClip soundClip = new SoundClip("TMP", audioSource.clip, audioSource.loop);
            soundClip.source = audioSource;
            soundClip.volume = audioSource.volume;
            soundClip.pitch = audioSource.pitch;
            currentSceneClips.Add(soundClip);

            audioSource.pitch *= pitchMultiplier;
            audioSource.volume *= volumeMultiplier;
        }
    }

    private void OnSceneUnloaded(Scene scene)
    {
        Debug.Log("Scene unloaded: " + scene.name);
        currentSceneClips.Clear();
    }
}
