using System;
using UnityEngine;
using UnityEngine.UIElements;

public class IngameUI : MonoBehaviour
{
    [Header("Crosshair Images")]
    [SerializeField] private  Sprite defaultCrosshair;
    [SerializeField] private Sprite interactCrosshair;
    
    public PanelSettings panelSettings;

    private Label _mushroomCounter;
    private VisualElement _crosshair;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _mushroomCounter = root.Q<Label>("mushroomCounter");
        _crosshair = root.Q<VisualElement>("crosshair");

        if (defaultCrosshair != null)
        {
            _crosshair.style.backgroundImage = new StyleBackground(defaultCrosshair);
        }
    }

    private void Start()
    {
        LevelManager.Instance.onMushroomNumberChange += UpdateMushroomCount;
    }

    private void OnDestroy()
    {
        LevelManager.Instance.onMushroomNumberChange -= UpdateMushroomCount;
    }

    private void UpdateMushroomCount(int count)
    {
        if (_mushroomCounter != null) _mushroomCounter.text = $"🍄 x {count}";
    }

    public void SetCanInteract(bool canInteract)
    {
        if (_crosshair == null) return;

        if (canInteract)
        {
            if (interactCrosshair != null)
            {
                _crosshair.style.backgroundImage = new StyleBackground(interactCrosshair);
            }
        }
        else
        {
            if (defaultCrosshair != null)
            {
                _crosshair.style.backgroundImage = new StyleBackground(defaultCrosshair);
            }
        }
    }
}
