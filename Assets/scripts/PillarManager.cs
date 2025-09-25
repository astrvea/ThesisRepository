using UnityEngine;

public class PillarManager : MonoBehaviour
{
    // each pillar in scene
    public PillarController[] pillars;

    // state of each pillar
    private bool[] pillarState;

    public bool puzzleSolved = false;

    public GameObject deactivateObj;

    void Awake()
    {
        // sets initial state of all pillars
        pillarState = new bool[pillars.Length];

        pillarState[2] = true;
        pillarState[4] = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        InitializePillars();
        CheckWin();
    }

    private void InitializePillars()
    {
        for (int i = 0; i < pillars.Length; i++)
        {
            pillars[i].CallState(pillarState[i]);
        }
    }

    private void FlipPillar(int pillarNum)
    {
        pillarState[pillarNum] = !pillarState[pillarNum];

    }

    public void PillarOn(int pillarNum)
    {
        if (puzzleSolved) return; 

        // pillar being called
        int numPillars = pillars.Length;

        // flips state
        FlipPillar(pillarNum);

        // toggles clockwise pillars
        int clockPillar = (pillarNum - 1 + numPillars) % numPillars;
        FlipPillar(clockPillar);

        // toggles counterclockwise pillars
        int counterPillar = (pillarNum + 1) % numPillars;
        FlipPillar(counterPillar);
        Debug.Log("changed" + pillarNum);

        // checking for win
        CheckWin();
    }

    private void CheckWin()
    {
        for (int i = 0; i < pillars.Length; i++)
        {
            pillars[i].CallState(pillarState[i]);
        }

        for (int i = 0; i < pillarState.Length; i++)
        {
            if (!pillarState[i])
            {
                return;
            }
        }
        Debug.Log("You Win");

        puzzleSolved = true;

        deactivateObj.SetActive(false);
    }
}
