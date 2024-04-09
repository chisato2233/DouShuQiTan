using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;
    public static bool musicIsClose, sfxIsClose = false;
    public void Awake()
    {
        _musicSlider.value = AudioManager.Instance.musicSource.volume;
        _sfxSlider.value = AudioManager.Instance.sfxSource.volume;
    }
    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }
    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }
    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }
    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
    }
    public void changeMusicSprite()
    {
        if (!musicIsClose)
        {
            transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/“Ù¿÷ui/musicClose");
            musicIsClose = !musicIsClose;
        }
        else
        {
            transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/“Ù¿÷ui/musicOpen");
            musicIsClose = !musicIsClose;

        }
    }
    public void changeSFXSprite()
    {
        if (!sfxIsClose)
        {
            transform.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/“Ù¿÷ui/sfxClose");
            sfxIsClose = !sfxIsClose;
        }
        else
        {
            transform.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/“Ù¿÷ui/sfxOpen");
            sfxIsClose = !sfxIsClose;

        }
    }
}
