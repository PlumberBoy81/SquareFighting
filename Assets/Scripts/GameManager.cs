using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    [Header("Players")]
    public GameObject player1;
    public GameObject player2;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public Text winnerText;

    // --- NEW: PAUSE MENU UI ---
    [Header("Pause Menu UI")]
    public GameObject pauseMenuPanel;
    private bool isPaused = false;

    [Header("Audio")]
    public AudioClip backgroundMusic;
    private AudioSource audioSource;

    private bool isGameOver = false;

    void Start()
    {
        // Ensure UI panels are hidden when the match starts
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);

        // Set up the Background Music
        if (backgroundMusic != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;       
            audioSource.volume = 0.4f;     
            audioSource.Play();
        }
        
        // Ensure time is running normally at the start of the scene
        Time.timeScale = 1f;
    }

    void Update()
    {
        // If the game isn't over, play normally and check for pauses
        if (!isGameOver)
        {
            CheckForWinner();

         
           // --- NEW: PAUSE INPUT (Keyboard Space or Controller Start) ---
    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton7))
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

        }
        else
        {
            // If the game IS over, Space acts as the restart button
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RestartGame();
            }
        }
    }

    void CheckForWinner()
    {
        if (player1 != null && !player1.activeInHierarchy) DeclareWinner("PLAYER 2 WINS!");
        else if (player2 != null && !player2.activeInHierarchy) DeclareWinner("PLAYER 1 WINS!");
    }

    void DeclareWinner(string winMessage)
    {
        isGameOver = true;
        
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        
        if (winnerText != null)
        {
            winnerText.text = winMessage;
            if (winMessage.Contains("1")) winnerText.color = Color.red; 
            else winnerText.color = Color.blue; 
        }

        if (audioSource != null) audioSource.pitch = 0.5f; 
    }

    // --- NEW: PAUSE MENU FUNCTIONS ---

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // This freezes all physics and movement in Unity!
        
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);
        if (audioSource != null) audioSource.Pause(); // Pauses the music
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Unfreezes the game
        
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        if (audioSource != null) audioSource.UnPause(); // Resumes the music
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // CRITICAL: Unfreeze time before reloading!
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; // CRITICAL: Unfreeze time before loading the menu!
        SceneManager.LoadScene(0); // Index 0 is your Main Menu in Build Settings
    }
}