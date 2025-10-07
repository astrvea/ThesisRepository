using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MinigameManager : MonoBehaviour
{
    public TextMeshProUGUI tasksCompleted;
    public List<MonoBehaviour> minigames;

    private HashSet<IMinigame> completedMinigame = new HashSet<IMinigame>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateTasks();
    }

    public void MinigameCompleted(IMinigame minigame)
    {
        if (!completedMinigame.Contains(minigame))
        {
            completedMinigame.Add(minigame);
            UpdateTasks();

            // adds minigame to the list if its not already there
        }
    }

    private void UpdateTasks()
    {
        int total = minigames.Count;
        int completed = completedMinigame.Count;
        if (tasksCompleted != null)
            tasksCompleted.text = $"Tasks Completed: {completed}/{total}";
    }
}
