using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class ClockWipeRing : VisualElement
{
    [UxmlAttribute] public float progress { get; set; } = 1f;

    // Match these to your image's ring dimensions
    [UxmlAttribute] public float ringRadius { get; set; } = 0.72f;   // as fraction of element size
    [UxmlAttribute] public float ringThickness { get; set; } = 0.1f; // as fraction of element size

    static readonly Color RingColor = new Color(0.969f, 0.933f, 0.769f); // #F7EEC4

    public ClockWipeRing()
    {
        generateVisualContent += OnGenerateVisualContent;
        usageHints = UsageHints.DynamicColor;
    }

    public void SetProgress(float t)
    {
        progress = Mathf.Clamp01(t);
        MarkDirtyRepaint();
    }

    void OnGenerateVisualContent(MeshGenerationContext ctx)
    {
        if (progress <= 0f) return;

        var rect = contentRect;
        var center = rect.center;
        float size = Mathf.Min(rect.width, rect.height);
        float radius = size * ringRadius;
        float thickness = size * ringThickness;

        float startAngle = -90f;                        // 12 o'clock
        float endAngle = startAngle + (progress * 360f);

        var p = ctx.painter2D;
        p.strokeColor = RingColor;
        p.lineWidth = thickness;
        p.lineCap = LineCap.Butt;

        p.BeginPath();
        p.Arc(center, radius, startAngle, endAngle, ArcDirection.Clockwise);
        p.Stroke();
    }
}