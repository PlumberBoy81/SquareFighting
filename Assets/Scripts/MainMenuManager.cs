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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            StartGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton8))
        {
            QuitGame();
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


