namespace Scal.Bootstrapping
{
    public enum TaskPriority
    {
        Earliest,
        Earlier,
        Later,
        Latest
    }

    public interface IStartupTask
    {
        void Run();
        TaskPriority Priority { get; }
    }
}