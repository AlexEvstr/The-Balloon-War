using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    public AudioClip btnSound;
    public GameObject AudioOn;
    public GameObject AudioOff;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        AudioListener.volume = PlayerPrefs.GetFloat("AudioVolume", 1);
        if (AudioListener.volume > 0) EnableAudio();
        else DisableAudio();
    }

    public void DisableAudio()
    {
        AudioOn.SetActive(false);
        AudioOff.SetActive(true);
        AudioListener.volume = 0;
        PlayerPrefs.SetFloat("AudioVolume", 0);
    }

    public void EnableAudio()
    {
        AudioOff.SetActive(false);
        AudioOn.SetActive(true);
        AudioListener.volume = 1;
        PlayerPrefs.SetFloat("AudioVolume", 1);
    }

    public void PlayBtnSound()
    {
        _audioSource.PlayOneShot(btnSound);
    }
}