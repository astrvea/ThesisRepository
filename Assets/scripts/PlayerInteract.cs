using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    private PillarController currentPillar;
    public GameObject interactUI;

    // Update is called once per frame
    void Update()
    {
        if(currentPillar !=null && Input.GetKeyDown(KeyCode.E))
        {
            currentPillar.Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PillarController pillar = other.GetComponent <PillarController>();

        if(pillar != null)
        {
            currentPillar = pillar;
            Debug.Log("near pillar: " + pillar.pillarNum);

            if(interactUI != null && !pillar.pillarManager.puzzleSolved)
            {
                interactUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PillarController>() == currentPillar)
        {
            currentPillar = null;
            Debug.Log("goodbye pillar D:");

            if (interactUI != null)
            {
                interactUI.SetActive(false);
            }
        }
    }
}
