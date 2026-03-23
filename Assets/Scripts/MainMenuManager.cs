using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip menuMusic;
    private AudioSource audioSource;

    void Start()
    {
        if (menuMusic != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = menuMusic;
            audioSource.loop = true;
            audioSource.volume = 0.5f; 
            audioSource.Play();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game..."); 
        Application.Quit(); 
    }
}


