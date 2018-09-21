using UnityEngine;

/// <summary>
/// 各类管理器基类
/// </summary>
public class ManagerBase : MonoBehaviour
{
    public static ManagerBase Instance { get; private set; }

    public void OnCreate() { Instance = this; Initialize(); }

    protected virtual void OnDestroy() { Dispose(); }

    protected virtual void Initialize() { Instance = this; }

    protected virtual void Dispose() { Destroy(gameObject); }
}
