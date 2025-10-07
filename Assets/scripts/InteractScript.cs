using UnityEngine;

public class InteractScript : MonoBehaviour
{
    private bool isPlayerInRange;
    public GameObject interactPrompt;
    public MonoBehaviour minigameStart;

    private IMinigame minigame;

    private void Start()
    {
        if (minigameStart != null && minigameStart is IMinigame)
            minigame = minigameStart as IMinigame;

        // check the minigame's validity
    }

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
            minigame?.StartGame();
        }
    }
}
