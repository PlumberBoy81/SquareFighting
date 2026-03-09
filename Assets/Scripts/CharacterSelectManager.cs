using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterSelectManager : MonoBehaviour
{
    [Header("Cursors")]
    public RectTransform p1Cursor;
    public RectTransform p2Cursor;

    [Header("Character Icons")]
    public RectTransform redIcon;
    public RectTransform blueIcon;

    [Header("UI Elements")]
    public TextMeshProUGUI readyText;

    private int p1HoverIndex = 0; 
    private int p2HoverIndex = 1;

    private bool p1LockedIn = false;
    private bool p2LockedIn = false;

    private Image p1CursorImage;
    private Image p2CursorImage;

    void Start()
    {
        if (readyText != null) readyText.gameObject.SetActive(false);
        if (p1Cursor != null) p1CursorImage = p1Cursor.GetComponent<Image>();
        if (p2Cursor != null) p2CursorImage = p2Cursor.GetComponent<Image>();
    }

    void Update()
    {
        HandleInputs();
        UpdateCursorPositions();
        UpdateCursorVisuals();
        CheckReadyState();
    }

    void HandleInputs()
    {
        if (!p1LockedIn)
        {
            if (Input.GetKeyDown(KeyCode.A)) p1HoverIndex = 0;
            if (Input.GetKeyDown(KeyCode.D)) p1HoverIndex = 1;
            if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Joystick1Button0)) p1LockedIn = true; 
        }
        else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Joystick1Button1)) // E or "B" Button 
        {
            p1LockedIn = false;
        }

        if (!p2LockedIn)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) p2HoverIndex = 0;
            if (Input.GetKeyDown(KeyCode.RightArrow)) p2HoverIndex = 1;
            if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.Joystick2Button0)) p2LockedIn = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.Joystick2Button1))
        {
            p2LockedIn = false;
        }
    }

    void UpdateCursorPositions()
    {
        Vector3 p1Offset = new Vector3(0, 50, 0);
        if (p1HoverIndex == 0) p1Cursor.position = redIcon.position + p1Offset;
        else p1Cursor.position = blueIcon.position + p1Offset;

        Vector3 p2Offset = new Vector3(0, -50, 0);
        if (p2HoverIndex == 0) p2Cursor.position = redIcon.position + p2Offset;
        else p2Cursor.position = blueIcon.position + p2Offset;
    }

    void UpdateCursorVisuals()
    {
        // Darken the cursor if they are locked in, keep it white/normal if they are still deciding
        if (p1CursorImage != null) p1CursorImage.color = p1LockedIn ? Color.gray : Color.white;
        if (p2CursorImage != null) p2CursorImage.color = p2LockedIn ? Color.gray : Color.white;
    }

    void CheckReadyState()
    {
        if (p1LockedIn && p2LockedIn)
        {
            if (readyText != null) readyText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton7))
            {
                PlayerPrefs.SetInt("P1Color", p1HoverIndex);
                PlayerPrefs.SetInt("P2Color", p2HoverIndex);
                PlayerPrefs.Save();
                
                SceneManager.LoadScene(2);
            }
        }
        else
        {
            if (readyText != null) readyText.gameObject.SetActive(false);
        }
    }
}