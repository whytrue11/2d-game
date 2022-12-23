using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour
{
    [SerializeField] private Slider optionsSlider;
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GameObject.FindObjectOfType<AudioSource>();
        audioSource.volume = DataHolder.musicVolume;
        optionsSlider.value = DataHolder.musicVolume;
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        DataHolder.musicVolume = volume;
    }
}
