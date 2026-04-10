using UnityEngine;

public class MushText : MonoBehaviour
{
    public HelpText helpText;
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            helpText.MushroomText();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            helpText.TextOut();
        }
    }
}
