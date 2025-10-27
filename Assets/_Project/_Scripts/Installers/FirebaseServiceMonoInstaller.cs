using Zenject;

public class FirebaseServiceMonoInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IFirebaseService>()
            .To<FirebaseService>()
            .AsSingle()
            .NonLazy();
    }
}