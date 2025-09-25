using UnityEngine;

public class PillarController : MonoBehaviour
{
    public PillarManager pillarManager;
    public int pillarNum;
    public Material onMaterial;
    public Material offMaterial;

    private Renderer pillarRenderer;

    void Awake()
    {
        pillarRenderer = GetComponent<Renderer>();
    }

    public void Interact()
    {
        pillarManager.PillarOn(pillarNum);
    }

    public void CallState(bool isOn)
    {
        if (isOn)
        {
            pillarRenderer.material = onMaterial;
        }
        else
        {
            pillarRenderer.material = offMaterial;
        }
    }
}
