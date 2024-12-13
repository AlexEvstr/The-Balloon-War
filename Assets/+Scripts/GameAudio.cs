using UnityEngine;

public class GameAudio : MonoBehaviour
{
    public AudioClip btnSound;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip shootSound;
    public AudioClip boomSound;
    public AudioClip minusHeartSound;
    public AudioClip coinsSound;
    public AudioClip upgradeSound;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        AudioListener.volume = PlayerPrefs.GetFloat("AudioVolume", 1);
    }

    public void PlayBtnSound()
    {
        _audioSource.PlayOneShot(btnSound);
    }

    public void PlayWinSound()
    {
        _audioSource.PlayOneShot(winSound);
    }

    public void PlayLoseSound()
    {
        _audioSource.PlayOneShot(loseSound);
    }

    public void PlayShootSound()
    {
        _audioSource.PlayOneShot(shootSound);
    }

    public void PlayBoomSound()
    {
        _audioSource.PlayOneShot(boomSound);
    }

    public void PlayMinusHeartSound()
    {
        _audioSource.PlayOneShot(minusHeartSound);
    }

    public void PlayCoinsSound()
    {
        _audioSource.PlayOneShot(coinsSound);
    }

    public void PlayUpgradeSound()
    {
        _audioSource.PlayOneShot(upgradeSound);
    }

    public void StopAllSounds()
    {
        _audioSource.Stop();
    }
}