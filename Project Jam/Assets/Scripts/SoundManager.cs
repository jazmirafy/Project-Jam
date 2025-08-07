using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SoundType {
    Test,
    PerfectNote,
    HitNote,
    MissNote,
    CancelSFX,
    ConfirmSFX
 }
[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    [Header("Background Music Clips")]
    public AudioClip mainMenuMusic;
    public AudioClip levelMusic;
    public AudioClip pauseMusic;
    public AudioClip winScreenMusic;
    public AudioClip loseScreenMusic;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private SoundList[] soundList;
    [SerializeField] private AudioSource audioSource;
    public static SoundManager instance;

    private void Awake()
    {
        instance = this;
    }

    //basically we get a random sound of the type of sound we want (like if we want a miss sound to play it will choose a random index from the miss sound array to play)
    //the it will play the random clip at the set volume
    public static void PlaySound(SoundType sound, float volume = 1)
    {
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        if (clips.Length == 0)
        {
            Debug.LogWarning($"SoundManager: No audio clips set for {sound}");
            return;
        }
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.audioSource.PlayOneShot(randomClip, volume);
    }
    //basically this plays music and if i want the music looped it will loop it (like menu background music)
    //but if its something we dont want looped (like the actual level song) we can play that and make sure it only gets played one
    public static void PlayBackgroundMusic(AudioClip clip, bool shouldLoop = true)
    {
        //if (instance.musicSource.clip == clip) return; // Avoid restarting same clip

        instance.musicSource.Stop();
        instance.musicSource.clip = clip;
        instance.musicSource.loop = shouldLoop;
        instance.musicSource.Play();
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);
        for (int i = 0; i < soundList.Length; i++) {
            soundList[i].name = names[i];
        }
    }
#endif
}
[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds { get => sounds; }
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sounds;
}
