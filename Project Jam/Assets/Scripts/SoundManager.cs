using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType {
    LevelMusic,
    HitNote,
    MissNote,
    MainMenu,
    ScreenSFX,
 }
  [RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private AudioSource audioSource;
    private static SoundManager instance;

    private void Awake()
    {
        instance = this;
    }
    public static void PlaySound(SoundType sound, float volume = 1)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
