using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSetting : AbstractPopup
{
    public GameObject soundOn;
    public GameObject soundOff;

    public GameObject musicOn;
    public GameObject musicOff;

    private void OnEnable()
    {
        ShowSoundIcon(AudioSystem.Instance.IsOnSound);
        ShowMusicIcon(AudioSystem.Instance.IsOnMusic);
    }

    public void OnClickBtnBack()
    {
        Dismiss();
    }

    public void OnClickItemSound()
    {
        AudioSystem.Instance.IsOnSound = !AudioSystem.Instance.IsOnSound;
        ShowSoundIcon(AudioSystem.Instance.IsOnSound);
    }

    private void ShowSoundIcon(bool val)
    {
        soundOn.SetActive(val);
        soundOff.SetActive(!val);
    }

    public void OnClickItemMusic()
    {
        AudioSystem.Instance.IsOnMusic = !AudioSystem.Instance.IsOnMusic;
        ShowMusicIcon(AudioSystem.Instance.IsOnMusic);
    }

    private void ShowMusicIcon(bool val)
    {
        musicOn.SetActive(val);
        musicOff.SetActive(!val);
    }
}
