using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MelodyMechanic : MonoBehaviour
{
    [System.Serializable]
    public class MelodyNote
    {
        public KeyCode key;
        public AudioClip clip;
        public Image fadeImage;
    }

    [Header("Resonance Setup")]
    public List<MelodyNote> notes = new List<MelodyNote>();
    public string nextSceneName = "DreamWorld";

    [Header("UI")]
    public CanvasGroup resonatePrompt; // ui prompt

    private AudioSource audioSource;
    private HashSet<KeyCode> playedNotes = new HashSet<KeyCode>();
    private bool transitioning = false;
    private bool isActive = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        // fade images are inactive
        foreach (var n in notes)
        {
            if (n.fadeImage != null)
                n.fadeImage.color = new Color(0, 0, 0, 0);
        }

        // hide UI prompt
        if (resonatePrompt != null)
        {
            resonatePrompt.alpha = 0f;
            resonatePrompt.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!isActive || transitioning) return;

        foreach (var note in notes)
        {
            if (Input.GetKeyDown(note.key))
            {
                PlayNoteAndFade(note);
            }
        }

        // remove prompt when player starts resonating
        if (isActive && resonatePrompt != null && resonatePrompt.gameObject.activeSelf && Input.anyKeyDown)
        {
            resonatePrompt.gameObject.SetActive(false);
        }

        // after all notes are played, switch scene
        if (playedNotes.Count == notes.Count && !transitioning)
        {
            StartCoroutine(FinalLoad());
        }
    }

    void PlayNoteAndFade(MelodyNote note)
    {
        // plays note
        if (note.clip != null)
            audioSource.PlayOneShot(note.clip);

        // note is marked as played
        playedNotes.Add(note.key);

        // UI image
        if (note.fadeImage != null)
            StartCoroutine(ImageFragment(note.fadeImage, 1f, 0.5f));
    }

    IEnumerator ImageFragment(Image img, float targetAlpha, float duration)
    {
        float startAlpha = img.color.a;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            float a = Mathf.Lerp(startAlpha, targetAlpha, t);
            img.color = new Color(0, 0, 0, a);
            yield return null;
        }
    }

    IEnumerator FinalLoad()
    {
        transitioning = true;
        yield return new WaitForSeconds(1f);

        // wait before scene load
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(nextSceneName);
    }

    // after dialogue ends, start resonance
    public void StartResonance()
    {
        StartCoroutine(ShowResonatePrompt());
    }

    IEnumerator ShowResonatePrompt()
    {
        if (resonatePrompt != null)
        {
            resonatePrompt.alpha = 0f;
            resonatePrompt.gameObject.SetActive(true);

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime;
                resonatePrompt.alpha = Mathf.Lerp(0f, 1f, t);
                yield return null;
            }

            yield return new WaitForSeconds(1.5f);

            t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime;
                resonatePrompt.alpha = Mathf.Lerp(1f, 0f, t);
                yield return null;
            }

            resonatePrompt.gameObject.SetActive(false);
        }

        // activate resonance
        isActive = true;
    }
}