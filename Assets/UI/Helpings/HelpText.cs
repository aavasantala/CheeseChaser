using System;
using UnityEngine;
using UnityEngine.UIElements;

public class HelpText : MonoBehaviour
{
    public Label helpText;
    public MouseController mouseController;

    public Boolean canMoved;

    int done1 = 0;
    int done2 = 0;
    int done3 = 0;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument component missing!");
            return;
        }

        var root = uiDocument.rootVisualElement;


        helpText = root.Q<Label>("helpText");
        
    }
    public void BucketText()
    {
        if (done1 <= 2)
        {
            helpText.text = "Hmm... If I was bigger, I could move this bucket...";
            done1++;
        }
        
    }

    public void MushroomText()
    {
        helpText.text = "A mushroom! My favorite powerup from Mega Italian Plumber Bros!";
    }

    public void CanText()
    {
        
        if (done2 <= 1)
        {
            helpText.text = "I bet I can knock this over!";
            done2++;
        }
    }

    public void CrackText()
    {
        if (done3 <= 1)
        {
            helpText.text = "This can-sized crack is so annoying.. Can't get over it like this.";
            done3++;
        }
        
    }

    public void TextOut()
    {
        helpText.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && mouseController.isSmall && canMoved == false)
        {
            CrackText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && mouseController.isSmall)
        {
            TextOut();
        }
    }
}
