using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class DialogueLine
{
    public string characterName;
    [TextArea(2, 5)]
    public string line;
    public Sprite portrait;
}

public class Dialogue : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image portraitImage;

    [Header("Dialogue Settings")]
    public List<DialogueLine> lines = new List<DialogueLine>();
    public float textSpeed = 0.05f;

    private int index = 0;
    private bool isTyping = false;
    private bool isActive = false;

    void Start()
    {
        // hide UI
        dialogueText.text = string.Empty;
        nameText.text = string.Empty;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isActive) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = lines[index].line;
                isTyping = false;
            }
            else
            {
                NextLine();
                // tracks dialogue lines
            }
        }
    }

    public void StartDialogue()
    {
        gameObject.SetActive(true);
        isActive = true;
        index = 0;
        DisplayLine();
    }

    void DisplayLine()
    {
        DialogueLine currentLine = lines[index];

        nameText.text = currentLine.characterName;
        portraitImage.sprite = currentLine.portrait;

        dialogueText.text = string.Empty;
        StartCoroutine(TypeLine(currentLine.line));
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isTyping = false;
    }

    void NextLine()
    {
        if (index < lines.Count - 1)
        {
            index++;
            DisplayLine();
        }
        else
        {
            EndDialogue();
        }
    }

    public bool IsActive()
    {
        return isActive;
    }

    void EndDialogue()
    {
        isActive = false;
        gameObject.SetActive(false);
    }
}
