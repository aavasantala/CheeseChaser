using UnityEngine;
using UnityEngine.UIElements;

public class HelpText : MonoBehaviour
{
    public Label helpText;
    

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
        helpText.text = "Hmm... If I was bigger, I could move this bucket...";
    }

    public void MushroomText()
    {
        helpText.text = "A mushroom! My favorite powerup from Mega Italian Plumber Bros!";
    }

    public void TextOut()
    {
        helpText.text = "";
    }
}
