using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound {

    public string name;
    public AudioClip clip;

    private AudioSource source;

    [Range(0f,1f)]
    public float volume;

    [Range(0.5f, 1.5f)]
    public float pitch;


    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
    }


}


public class AudioManager : MonoBehaviour {

    public static AudioManager instance;


    [SerializeField]
    Sound[] sounds;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one AudioManager in the scene");
        }
        else
        {
            instance = this;
        }   
    }


    void Start()
    {
        for(int i = 0; i< sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }  

    }

    public void PlaySound (string _name)
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            if(sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }

        // no sound with _name
        Debug.LogWarning("AudioManager: Sound not found in list: " + _name);
    }
}
