using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowerMinigame : MonoBehaviour, IMinigame
{
    [Header("UI")]
    public RectTransform bar;
    public RectTransform indicator;
    public RectTransform warmZone;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI escapeText;
    public MinigameManager manager;

    public GameObject showerMinigameInteract;
    public GameObject interactPrompt;
    public GameObject showerMinigameCanvas;

    [Header("Game Logic")]
    public float moveSpeed = 300f;
    public int hitsToWin = 5;
    public float minZoneWidth = 60f;
    public float maxZoneWidth = 120f;

    private bool movingRight = true;
    private int successfulHits = 0;
    private bool gameActive = false;
    private bool showerDone = false;

    public void StartGame()
    {
        showerMinigameCanvas.SetActive(true);
        interactPrompt.SetActive(false);

        gameActive = true;
        successfulHits = 0;
        movingRight = true;
        indicator.anchoredPosition = new Vector2(-bar.rect.width / 2, indicator.anchoredPosition.y);
        
        ZoneRandomization();

        if (feedbackText) 
            feedbackText.text = "";

        // stops player movement
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (!gameActive) 
            return;

        MoveIndicator();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (InWarmZone())
            {
                successfulHits++;
                if (feedbackText) 
                    feedbackText.text = $"Ahhh warm water :D ({successfulHits}/{hitsToWin})";

                if (successfulHits >= hitsToWin)
                {
                    ShowerFinished();
                }
                else
                {
                    ZoneRandomization();
                }
            }
            else
            {
                if (feedbackText) 
                    feedbackText.text = "Nooo so cold D:";
            }
        }

        if (showerDone == true && Input.GetKeyDown(KeyCode.Escape))
        {
            EndShowerGame();
        }
    }

    void MoveIndicator()
    {
        float move = moveSpeed * Time.unscaledDeltaTime * (movingRight ? 1 : -1);
        indicator.anchoredPosition += new Vector2(move, 0);

        float halfWidth = bar.rect.width / 2;
        if (Mathf.Abs(indicator.anchoredPosition.x) > halfWidth)
        {
            movingRight = !movingRight;
            indicator.anchoredPosition = new Vector2(Mathf.Sign(indicator.anchoredPosition.x) * halfWidth, indicator.anchoredPosition.y);
        }
    }

    bool InWarmZone()
    {
        float x = indicator.anchoredPosition.x;
        float left = warmZone.anchoredPosition.x - warmZone.rect.width / 2;
        float right = warmZone.anchoredPosition.x + warmZone.rect.width / 2;
        return x >= left && x <= right;
    }

    void ZoneRandomization()
    {
        float halfBarWidth = bar.rect.width / 2;
        float randomWidth = Random.Range(minZoneWidth, maxZoneWidth);
        warmZone.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, randomWidth);
        float maxX = halfBarWidth - randomWidth / 2;
        float randomX = Random.Range(-maxX, maxX);
        warmZone.anchoredPosition = new Vector2(randomX, warmZone.anchoredPosition.y);
    }

    void ShowerFinished()
    {
        escapeText.enabled = true;
        showerDone = true;
        moveSpeed = 0;

        if (feedbackText)
            feedbackText.text = "Nice shower ;)";
    }

    void EndShowerGame()
    {
        gameActive = false;
        showerMinigameCanvas.SetActive(false);
        showerMinigameInteract.SetActive(false);
        escapeText.enabled = false;
      
        manager?.MinigameCompleted(this);

        // resumes player movement
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
}
