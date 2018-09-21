public abstract class Controller : IMessageListener
{
    public Controller(UI_Panel panel) { }
    public virtual void Initialize() { Register(); }
    public virtual void Dispose() { UnRegister(); }

    public virtual void Register() { }

    public virtual void UnRegister() { }
}
