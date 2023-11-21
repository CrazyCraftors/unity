using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip menuMusic;
    public AudioClip levelMusic;
    public AudioClip pauseSound;
    public AudioClip unpauseSound;
    public AudioClip smallMarioHitSound;
    public AudioClip bigMarioHitSound;
    public AudioClip breakBlockSound;
    public AudioClip hitBlockSound;
    public AudioClip luckyBoxCoinSound;
    public AudioClip luckyBoxMushroomSound;
    public AudioClip powerUpSound;
    public AudioClip gameOverSound;
    public AudioClip winSound;
    public AudioClip jumpSound;

    public AudioSource menuMusicSource;
    public AudioSource levelMusicSource;
    public AudioSource soundEffectSource;

    private void Start()
    {
        menuMusicSource = gameObject.AddComponent<AudioSource>();
        levelMusicSource = gameObject.AddComponent<AudioSource>();
        soundEffectSource = gameObject.AddComponent<AudioSource>();

        menuMusicSource.clip = menuMusic;
        menuMusicSource.loop = true;
        menuMusicSource.Play();

        levelMusicSource.clip = levelMusic;
        levelMusicSource.loop = true;
    }

    public void PauseLevelMusic()
    {
        levelMusicSource.Pause();
    }

    public void UnpauseLevelMusic()
    {
        levelMusicSource.UnPause();
    }

    public void PlayPauseSound()
    {
        soundEffectSource.PlayOneShot(pauseSound);
    }

    public void PlayUnpauseSound()
    {
        soundEffectSource.PlayOneShot(unpauseSound);
    }

    public void PlaySmallMarioHitSound()
    {
        soundEffectSource.PlayOneShot(smallMarioHitSound);
    }

    public void PlayBigMarioHitSound()
    {
        soundEffectSource.PlayOneShot(bigMarioHitSound);
    }

    public void PlayBreakBlockSound()
    {
        soundEffectSource.PlayOneShot(breakBlockSound);
    }

    public void PlayHitBlockSound()
    {
        soundEffectSource.PlayOneShot(hitBlockSound);
    }

    public void PlayLuckyBoxCoinSound()
    {
        soundEffectSource.PlayOneShot(luckyBoxCoinSound);
    }

    public void PlayLuckyBoxMushroomSound()
    {
        soundEffectSource.PlayOneShot(luckyBoxMushroomSound);
    }

    public void PlayPowerUpSound()
    {
        soundEffectSource.PlayOneShot(powerUpSound);
    }

    public void PlayGameOverSound()
    {
        soundEffectSource.PlayOneShot(gameOverSound);
    }

    public void PlayJumpSound()
    {
        soundEffectSource.PlayOneShot(jumpSound);
    }

    public void PlayWinSound()
    {
        soundEffectSource.PlayOneShot(winSound);
    }
}
