using UnityEngine;
using UnityEngine.UIElements;

public class InGameUI : MonoBehaviour
{


    void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Label mushLabel = root.Q<Label>("labelMush");
        MushroomScript mushroomScript = new MushroomScript();
        mushLabel.text=mushroomScript.mushroomCount.ToString();
        Debug.Log(mushLabel.text);
        Debug.Log(mushroomScript.mushroomCount);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
