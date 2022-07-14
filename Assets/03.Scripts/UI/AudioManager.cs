using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoSingleton<AudioManager>
{

    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider EffectSlider;
    private void Start()
    {
        Sound temp = DataManager.LoadJsonFile<Sound>(Application.dataPath, "sound");
        MasterSlider.value = temp.Master;
        SetAudioVolume("Master", temp.Master);
        MusicSlider.value = temp.Music;
        SetAudioVolume("Music", temp.Music);
        EffectSlider.value = temp.Effect;
        SetAudioVolume("Effect", temp.Effect);
    }

    public AudioMixer _mixer = null;
    public void SetAudioVolume(string key, float value)
    {
        Sound sound = null;
        _mixer.SetFloat(key, Mathf.Log10(value) * 20);
        Sound temp = DataManager.LoadJsonFile<Sound>(Application.dataPath, "sound");

        switch (key)
        {
            case "Master":
                sound = new Sound(value, temp.Music, temp.Effect);
                break;
            case "Music":
                sound = new Sound(temp.Master, value, temp.Effect);
                break;
            case "Effect":
                sound = new Sound(temp.Master, temp.Music, value);
                break;
        }
        DataManager.CreateJsonFile(Application.dataPath, "sound", DataManager.ObjectToJson(sound));
    }
}
