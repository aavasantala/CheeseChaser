using UnityEngine;

public class Helper1 : MonoBehaviour
{
    public HelpText helpText;
    public MouseController mouseController;


    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && mouseController.isSmall)
        {
            helpText.CanText();
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
