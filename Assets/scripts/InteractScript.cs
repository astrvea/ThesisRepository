using UnityEngine;

public class InteractScript : MonoBehaviour
{
    private bool isPlayerInRange;
    public GameObject interactPrompt;
    public TypingMinigame typingMinigame;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            interactPrompt.SetActive(true);
            isPlayerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            interactPrompt.SetActive(false);
            isPlayerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            typingMinigame.StartTypingGame();
        }
    }
}
