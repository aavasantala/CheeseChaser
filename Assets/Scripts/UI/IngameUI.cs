using System;
using UnityEngine;
using UnityEngine.UIElements;

public class IngameUI : MonoBehaviour
{
    [Header("Crosshair Images")]
    [SerializeField] private  Sprite defaultCrosshair;
    [SerializeField] private Sprite interactCrosshair;
    
    public PanelSettings panelSettings;
    public MouseController mouseController;
    public ClockWipeController clockWipe;
    public Boolean timerOn = false;
    public float timer=0f;

    private Label _mushroomCounter;
    private VisualElement _crosshair;
    private VisualElement _bigMouse;

    private void OnEnable()
    {

        var root = GetComponent<UIDocument>().rootVisualElement;
        
        _crosshair = root.Q<VisualElement>("crosshair");
        _bigMouse = root.Q<VisualElement>("bigMouse");

        _bigMouse.style.opacity = 0;
        _crosshair.style.opacity = 0;
        Debug.Log("Helloooo!");
        if (defaultCrosshair != null)
        {
            _crosshair.style.backgroundImage = new StyleBackground(defaultCrosshair);
        }
    }

    private void Start()
    {
        LevelManager.Instance.onMushroomNumberChange += UpdateMushroomCount;
        
        _crosshair.style.opacity = 1;
    }

    private void Update()
    {
        if (!timerOn) return;
        Timer();
        
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

    public void TimerUpdate()
    {
        timerOn = true;
        _bigMouse.style.opacity = 1;
        timer = 15f;
        
    }

    public void TimerStop()
    {
        timer = 0;
    }

    public void Timer()
    {
        timer-=Time.deltaTime;
        clockWipe.SetTime(timer, 15f);
        if (timer <= 0)
        {
            _bigMouse.style.opacity = 0;
            timerOn = false;
        }
    }
}
