using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class GloabalInstaller : MonoInstaller
{
    [SerializeField] private AudioMixer _audioMixer;

    [SerializeField] private bool _isPhysicalMovement;

    public override void InstallBindings()
    {
        BindSignals();
        BindServices();
        BindInput();
        BindLoadingOperation();
        BindAudio();


    }

    private void BindServices()
    {
        Container.Bind<Level>().AsSingle();
        Container.BindInterfacesAndSelfTo<PauseHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<Timer>().FromInstance(new Timer(this)).AsSingle();
    }

    private void BindInput()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop) // Если ПК ...
            Container.BindInterfacesAndSelfTo<DesktopInput>().AsSingle();
        else
            Container.BindInterfacesAndSelfTo<DesktopInput>().AsSingle();
    }

    private void BindLoadingOperation()
    {
        Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
        Container.BindInterfacesAndSelfTo<LoadingScreenProvider>().AsSingle();
    }

    private void BindAudio()
    {
        AudioManager audioManager = new(_audioMixer);
        Container.Bind<AudioManager>().FromInstance(audioManager).AsSingle().NonLazy();
        Container.Bind<AudioUtility>().FromInstance(new AudioUtility(audioManager)).AsSingle();
    }

    private void BindSignals()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<GameLoseSignal>().OptionalSubscriber();
        Container.DeclareSignal<PauseSignal>().OptionalSubscriber();
    }
}
