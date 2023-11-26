using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
[System.Serializable]

public class Sound : MonoBehaviour
{
    public new string name;
    public AudioClip clip;
    [SerializeField] private Slider volumeSlider;

    public bool loop;

    [Range(0f,1f)]
    public float volume;
    

    [HideInInspector]
    public AudioSource source;

    private void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();

        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
