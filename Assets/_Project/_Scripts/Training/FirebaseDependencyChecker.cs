using Firebase;
using System.Threading.Tasks;

public static class FirebaseDependencyChecker
{
    private static Task<DependencyStatus> s_checkTask;

    public static Task<DependencyStatus> EnsureDependenciesAsync()
    {
        if (s_checkTask == null)
        {
            s_checkTask = FirebaseApp.CheckAndFixDependenciesAsync();
        }

        return s_checkTask;
    }
}
