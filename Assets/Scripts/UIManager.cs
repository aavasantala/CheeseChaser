using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    private Label mushroomCounter;
    private VisualElement crosshair;

    [Header("Crosshair Images")]
    public Sprite defaultCrosshair;
    public Sprite interactCrosshair;

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        mushroomCounter = root.Q<Label>("mushroomCounter");
        crosshair = root.Q<VisualElement>("crosshair");

        if (defaultCrosshair != null)
            crosshair.style.backgroundImage = new StyleBackground(defaultCrosshair);
    }

    public void UpdateMushroomCount(int count)
    {
        if (mushroomCounter != null)
            mushroomCounter.text = $"🍄 x {count}";
    }

    public void SetCanInteract(bool canInteract)
    {
        if (crosshair == null) return;

        if (canInteract)
        {
            if (interactCrosshair != null)
                crosshair.style.backgroundImage = new StyleBackground(interactCrosshair);
        }
        else
        {
            if (defaultCrosshair != null)
                crosshair.style.backgroundImage = new StyleBackground(defaultCrosshair);
        }
    }
}
