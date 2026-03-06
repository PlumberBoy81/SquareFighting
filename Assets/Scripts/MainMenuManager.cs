using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip menuMusic;
    private AudioSource audioSource;

    void Start()
    {
        // Set up and play the Main Menu music
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
        // --- NEW: Controller/Keyboard Support ---
        // Press Space or the Start button on a controller to jump right in!
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            StartGame();
        }
    }

    // This function will be called by our Play Button
    public void StartGame()
    {
        // Loads the next scene in the Build Settings (which will be your Character Select)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // --- NEW: Quit Function ---
    // This function will be called by a Quit Button
    public void QuitGame()
    {
        Debug.Log("Quitting Game..."); // You will see this in the Unity Console when testing
        Application.Quit(); // This is the command that actually closes the built .exe
    }
}


