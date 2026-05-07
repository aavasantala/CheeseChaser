using UnityEngine;
using UnityEngine.UIElements;

public class ClockWipeController : MonoBehaviour
{
    public UIDocument uiDocument;

    ClockWipeRing _ring;

    void OnEnable()
    {
        _ring = uiDocument.rootVisualElement.Q<ClockWipeRing>("clock-ring");
        _ring.SetProgress(1f);
    }

    // Call this every frame from IngameUI, passing remaining time and total time
    public void SetTime(float remaining, float total)
    {
        _ring.SetProgress(remaining / total);
    }
}