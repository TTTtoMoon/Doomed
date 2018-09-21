using UnityEngine;

/// <summary>
/// UI界面
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public abstract class UI_Panel : UI_Base
{
    public string Name { get { return GetType().Name; } }
    public EPanelShowType ShowType { get; private set; }

    private UI_Element[] elements;
    protected UI_Element[] Elements
    {
        get
        {
            if (elements == null)
                elements = GetComponentsInChildren<UI_Element>(true);
            return elements;
        }
    }

    public abstract Controller GetController();

    public virtual void OnCreate()
    {
        for (int i = 0; i < Elements.Length; i++)
            Elements[i].Initialize();
        Initialize();
        GetController().Initialize();
    }

    protected virtual void OnDestroy()
    {
        GetController().Dispose();
        Dispose();
        for (int i = 0; i < Elements.Length; i++)
            Elements[i].Dispose();
    }

    protected virtual void Initialize() { }

    protected virtual void Dispose() { }

    private CanvasGroup _canvasGroup;
    protected CanvasGroup canvasGroup
    {
        get
        {
            if (_canvasGroup == null)
                _canvasGroup = GetComponent<CanvasGroup>();
            return _canvasGroup;
        }
    }

    public void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        OnShow();
    }
    protected void OnShow() { }

    public void Freeze()
    {
        canvasGroup.alpha = 0.75f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    public void Hide()
    {
        OnHide();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
    protected void OnHide() { }
}

