using UnityEngine;

/// <summary>
/// UI基类，所有UI均应继承此类
/// </summary>
public class UI_Base : MonoBehaviour
{
    private RectTransform _rectTransform;
    protected RectTransform rectTransform
    {
        get
        {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();
            return _rectTransform;
        }
    }

    public float Width
    {
        get { return rectTransform.sizeDelta.x; }
        set
        {
            Vector2 size = rectTransform.sizeDelta;
            size.x = value;
            rectTransform.sizeDelta = size;
        }
    }

    public float Height
    {
        get { return rectTransform.sizeDelta.y; }
        set
        {
            Vector2 size = rectTransform.sizeDelta;
            size.y = value;
            rectTransform.sizeDelta = size;
        }
    }
}
