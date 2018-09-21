using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理UI_Panel
/// </summary>
public class UIManager : ManagerBase
{
    private LinkedList<UI_Panel> loadedPanels = new LinkedList<UI_Panel>();
    private UI_Panel currentPanel
    {
        get
        {
            return loadedPanels.Count > 0 ? loadedPanels.Last.Value : null;
        }
    }

    public BoolYieldInstruction ShowPanel<T>() where T : UI_Panel
    {
        UI_Panel panel = GetPanel<T>();
        if (panel == null)
        {
            StartCoroutine(CreatePanel(typeof(T).Name));
            return new BoolYieldInstruction(() => GetPanel<T>() != null);
        }
        else
        {
            loadedPanels.Remove(panel);
            loadedPanels.AddLast(panel);
            ShowPanel(panel);
            return null;
        }
    }

    public void ClosePanel(UI_Panel panel)
    {
        Destroy(panel.gameObject);
    }

    private IEnumerator CreatePanel(string panelName)
    {
        var req = Resources.LoadAsync(string.Format("{0}/{1}", PathConfig.UI_PREFAB_PATH, panelName));
        yield return req;
        GameObject go = req.asset as GameObject;
        UI_Panel panel = go.GetComponent<UI_Panel>();
        panel.OnCreate();
        ShowPanel(panel);
        loadedPanels.AddLast(panel);
    }

    private UI_Panel GetPanel<T>() where T : UI_Panel
    {
        LinkedListNode<UI_Panel> node = loadedPanels.First;
        while (node != null)
        {
            if (node.Value is T)
                return node.Value as T;
            node = node.Next;
        }
        return null;
    }

    private void ShowPanel(UI_Panel panel)
    {
        LinkedListNode<UI_Panel> node = loadedPanels.First;
        switch (panel.ShowType)
        {
            case EPanelShowType.Lapped:
                break;
            case EPanelShowType.FreezeOthers:
                while (node != null)
                {
                    node.Value.Freeze();
                    node = node.Next;
                }
                break;
            case EPanelShowType.CloseOthers:
                while (node != null)
                {
                    ClosePanel(node.Value);
                    node = node.Next;
                }
                break;
        }
        panel.Show();
    }
}
