using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource levelMusic;
    public AudioClip pauseSound;
    public AudioClip resumeSound;
    public AudioClip startLevelSound;
    public AudioClip cubeHitSmallSound;
    public AudioClip cubeHitBigSound;
    public AudioClip luckyBoxCoinSound;
    public AudioClip luckyBoxMushroomSound;
    public AudioClip powerUpSound;
    public AudioClip hitByEnemySound;
    public AudioClip deathSound;
    public AudioClip gameOverSound;

    private bool isPaused = false;

    void Start()
    {
        // Reproduce la música del nivel en bucle al inicio.
        PlayLevelMusic(startLevelSound);
    }

    void Update()
    {
        // Ejemplo: Pausa y reanuda la música del nivel con la tecla P.
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            // Resumen del juego
            ResumeGame();
        }
        else
        {
            // Pausa el juego
            PauseGame();
        }
    }

    void PauseGame()
    {
        isPaused = true;
        levelMusic.Pause();
        // Reproduce el sonido de pausa.
        PlaySound(pauseSound);
    }

    void ResumeGame()
    {
        isPaused = false;
        levelMusic.UnPause();
        // Reproduce el sonido de reanudación.
        PlaySound(resumeSound);
    }

    public void PlayLevelMusic(AudioClip clip)
    {
        levelMusic.clip = clip;
        levelMusic.loop = true;
        levelMusic.Play();
    }

    // Otros métodos para reproducir sonidos específicos, por ejemplo:
    public void PlayCubeHitSmallSound()
    {
        PlaySound(cubeHitSmallSound);
    }

    public void PlayCubeHitBigSound()
    {
        PlaySound(cubeHitBigSound);
    }

    // Agrega métodos similares para los demás sonidos.

    void PlaySound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    public void PlayGameOverSound()
    {
        // Pausa cualquier sonido antes de reproducir el sonido de Game Over.
        levelMusic.Pause();
        // Reproduce el sonido de Game Over.
        PlaySound(gameOverSound);
    }
}
