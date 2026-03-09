using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Players")]
    public GameObject player1;
    public GameObject player2;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI winnerText;

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
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);

        if (backgroundMusic != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;       
            audioSource.volume = 0.4f;     
            audioSource.Play();
        }
        
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (!isGameOver)
        {
            CheckForWinner();

         
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

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; 
        
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);
        if (audioSource != null) audioSource.Pause(); 
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        if (audioSource != null) audioSource.UnPause(); 
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(0); 
    }
}