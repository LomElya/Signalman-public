using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Canvas))]
public class MainMenu : MonoBehaviour
{
    [SerializeField] private RecordMenu _recordMenu;

    [Header("Buttons")]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _autorButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _recordButton;

    private AssetProvider _assetProvider;
    private LoadingScreenProvider _loadingProvider;

    private Level _level;

    private Canvas _canvas;

    private void OnValidate() => _canvas ??= GetComponent<Canvas>();

    [Inject]
    private void Construct(AssetProvider assetProvider, LoadingScreenProvider loadingProvider, Level level)
    {
        _assetProvider = assetProvider;
        _loadingProvider = loadingProvider;

        _level = level;
    }

    private void Start()
    {
        _playButton.onClick.AddListener(OnPlayButtonClick);
        _autorButton.onClick.AddListener(OnAutorButtonClick);
        _recordButton.onClick.AddListener(OnRecordButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);

        Show();
    }

    private async void OnPlayButtonClick()
    {
        _level.ChangeLevel(0);

        await _loadingProvider.LoadAndDestroy(new GameLoadingOperation(_assetProvider, _level));
    }

    private async void OnAutorButtonClick()
    {
        var autorsMenu = await AutorsMenu.Load();

        await autorsMenu.Value.Show();

        autorsMenu.Dispose();
    }

    private async void OnRecordButtonClick()
    {
        await _recordMenu.Show();
    }

    private async void OnExitButtonClick()
    {
        var alertPopup = await PopupAlert.Load();
        bool isConfirmed = await alertPopup.Value.SetDescription("Выйти из игры?");

        if (isConfirmed)
        {
            Application.Quit();
            Debug.Log("выход из игры");
        }

        alertPopup.Dispose();
    }

    private void Show() => _canvas.enabled = true;

    private void Hide() => _canvas.enabled = false;

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlayButtonClick);
        _autorButton.onClick.RemoveListener(OnAutorButtonClick);
        _recordButton.onClick.RemoveListener(OnRecordButtonClick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
    }
}
