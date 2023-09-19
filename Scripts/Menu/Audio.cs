using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class Audio : MonoBehaviour
{
    public AudioMixer am;

    public void AudioVolume(float sliderValue)
    {

        if (sliderValue <= -49f)
        {
            am.SetFloat("MusicVol", -80f);
            SteamAchievements.Ach("ACH_IN_SILENCE");
        }
        else
            am.SetFloat("MusicVol", sliderValue);
    }
    public AudioMixer sm;
    public void SoundVolume(float sliderValue)
    {
        if (sliderValue <= -49f)
        {
            am.SetFloat("SoundVol", -80f);
            SteamAchievements.Ach("ACH_TOO_MUCH_FEAR");
        }
        else
            sm.SetFloat("SoundVol", sliderValue);
    }
    public GameObject SliderMusic;
    public GameObject SliderSound;
    void Start()
    {
        if (SliderMusic.GetComponent<Slider>().value==-50f)
            am.SetFloat("MusicVol",-80f);
        else
            am.SetFloat("MusicVol", SliderMusic.GetComponent<Slider>().value);
        
        if (SliderSound.GetComponent<Slider>().value==-50f)
            am.SetFloat("MusicVol",-80f);
        else
            sm.SetFloat("SoundVol", SliderSound.GetComponent<Slider>().value);
    }
}
