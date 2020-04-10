using UnityEngine;

public class GrowTimeUI : MonoBehaviour
{
    public RectTransform barTransform;

    public void SetBarLength(float percentageToShrink)
    {
        float leftAnchor = percentageToShrink / 2;
        barTransform.anchorMin = new Vector2(leftAnchor, 0.0f);
        barTransform.anchorMax = new Vector2(1.0f - leftAnchor, 1.0f);
    }
}
