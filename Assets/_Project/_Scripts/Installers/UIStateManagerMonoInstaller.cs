using Zenject;

public class UIStateManagerMonoInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<UIStateManager<RegistrationUIState>>()
                 .AsSingle()
                 .NonLazy();
    }
}