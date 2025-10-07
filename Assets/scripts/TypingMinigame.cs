using UnityEngine;
using TMPro;

public class TypingMinigame : MonoBehaviour, IMinigame
{
    [Header("UI")]
    public GameObject typingCanvas;
    public GameObject typingMinigameInteract;
    public GameObject interactPrompt;
    public TextMeshProUGUI typingText;
    public TextMeshProUGUI escapeText;

    [Header("Typing Variables")]
    [TextArea]
    public string paragraph = "type your text here!";
    public float lettersTyped = 1f; // how many letters are typed when a key is pressed

    private int currentCharCount;
    private bool isTyping;

    public MinigameManager manager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        typingCanvas.SetActive(false);
        typingText.enabled = false;
        escapeText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTyping) 
            return;

        // any key types paragraph
        if (Input.anyKeyDown)
        {
            currentCharCount += Mathf.RoundToInt(lettersTyped);
            currentCharCount = Mathf.Clamp(currentCharCount, 0, paragraph.Length);
            typingText.text = paragraph.Substring(0, currentCharCount);

            // checks if completed
            if (currentCharCount >= paragraph.Length)
            {
                TypingFinished();
            }
        }
    }

    public void StartGame()
    {
        typingMinigameInteract.SetActive(false);

        typingCanvas.SetActive(true);
        interactPrompt.SetActive(false);
        typingText.enabled = true;
        typingText.text = "";
        currentCharCount = 0;
        isTyping = true;

        // stops player movement
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    void TypingFinished()
    {
        escapeText.enabled = true;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndTypingGame();
        }
    }

    void EndTypingGame()
    {
        // notify minigame manager
        manager?.MinigameCompleted(this);

        isTyping = false;
        typingCanvas.SetActive(false);
        typingText.enabled = false;
        escapeText.enabled = false;

        // resumes player movement
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;

        Debug.Log("you win the typing game!");
    }
}
