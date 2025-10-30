using UnityEngine;
using TMPro;

public class NPCInteract : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject interactPrompt;
    private bool playerInRange = false;

    void Start()
    {
        // hide UI
        if (interactPrompt != null)
            interactPrompt.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F) && !dialogue.IsActive())
        {
            if (interactPrompt != null)
                interactPrompt.gameObject.SetActive(false);

            dialogue.StartDialogue();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            // show UI
            if (interactPrompt != null)
                interactPrompt.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (interactPrompt != null)
                interactPrompt.gameObject.SetActive(false);

            // hides dialogue UI when out of trigger range
            if (dialogue != null && dialogue.gameObject.activeSelf)
                dialogue.gameObject.SetActive(false);
        }
    }
}