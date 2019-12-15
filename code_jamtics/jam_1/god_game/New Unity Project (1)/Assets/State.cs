public abstract class State<T>
{
    public virtual void Push(T source)
    {
    }
    public virtual void Pop(T source)
    {
    }
    public virtual void Enter(T source)
    {
    }
    public virtual void Exit(T source)
    {
    }
    public abstract void Update(T source);
}