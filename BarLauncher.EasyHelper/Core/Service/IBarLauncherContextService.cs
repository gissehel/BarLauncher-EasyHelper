namespace BarLauncher.EasyHelper.Core.Service
{
    public interface IBarLauncherContextService
    {
        void ChangeQuery(string query);

        string ActionKeyword { get; }

        string Seperater { get; }

        string IconPath { get; }
    }
}