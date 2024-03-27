using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class GameStateInProgress : GameState, IDisposable
{


    private GameStateManager _gameStateManager;

    private Timer _timer;
    private IInput _input;
    private Level _level;

    private bool _isPaused = false;

    private Disposable<PauseMenu> _pauseMenu;

    private string _description = "";

    private SignalBus _signalBus;

    public GameStateInProgress(GameStateType gameStateType) : base(gameStateType)
    {
    }

    public override bool CanSwitchToState(GameStateType gameStateType) => true;

    [Inject]
    private void Construct(GameStateManager gameStateManager, Timer timer, IInput input, Level level, SignalBus signalBus )
    {
        _gameStateManager = gameStateManager;
        _timer = timer;
        _input = input;
        _level = level;

        _signalBus = signalBus;
    }

    public override async UniTask OnEnter(string description = "")
    {
        Debug.Log("Процесс запущен");

        Subscribe();
    }

    private async void OnLevelPassed(int id)
    {
        Unsubscribe();

        _description = $"Уровень пройден за {_timer.PassedSeconds} секунд";
        await WinGame();
    }

    private async void OnPlayerLose()
    {
        _description = "Выведен из строя!";
        await LoseGame();
    }

    private async void OnPausePlayer(PauseSignal signal)
    {
        bool isPause = signal.IsPause;
        await PauseGame(isPause);
    }

    private async void OnTimeOver()
    {
        _description = "Время вышло, сработала тревога!";
        await LoseGame();
    }

    private async void OnPauseGame()
    {
        _isPaused = !_isPaused;

        await PauseGame(_isPaused);

        if (_isPaused == true)
        {
            _pauseMenu = await PauseMenu.Load();

            var isContinue = await _pauseMenu.Value.Show();

            if (isContinue)
            {
                _isPaused = false;
                await PauseGame(_isPaused);
            }
            else
            {
                _pauseMenu.Dispose();
                await QuitGame();
            }

            _pauseMenu.Dispose();
            _pauseMenu = null;
        }
        else
        {
            if (_pauseMenu != null)
            {
                _pauseMenu.Dispose();
                _pauseMenu = null;
            }
        }
    }

    private async UniTask PauseGame(bool isPaused)
    {
        var command = _gameStateManager.CreateSetPausedGameCommand(isPaused);

        await _gameStateManager.ExecuteCommands(command);
    }

    private async UniTask WinGame()
    {
        //  Unsubscribe();

        var command = _gameStateManager.CreateSetGameStateCommand(GameStateType.Win, _description);

        await _gameStateManager.ExecuteCommands(command);
    }

    private async UniTask LoseGame()
    {
        Unsubscribe();

        var command = _gameStateManager.CreateSetGameStateCommand(GameStateType.Lose, _description);

        await _gameStateManager.ExecuteCommands(command);
    }

    private async UniTask QuitGame()
    {
        Unsubscribe();

        var command1 = _gameStateManager.CreateQuitGameCommand();

        await _gameStateManager.ExecuteCommands(command1);
    }

    private void Subscribe()
    {
        _level.LevelChange += OnLevelPassed;
        _input.ClickPauseButton += OnPauseGame;
        _timer.TimeFinished += OnTimeOver;

        _signalBus.Subscribe<GameLoseSignal>(OnPlayerLose);
        _signalBus.Subscribe<PauseSignal>(OnPausePlayer);
    }

    private void Unsubscribe()
    {
        _level.LevelChange -= OnLevelPassed;
        _timer.TimeFinished -= OnTimeOver;
        _input.ClickPauseButton -= OnPauseGame;

        _signalBus.Unsubscribe<GameLoseSignal>(OnPlayerLose);
        _signalBus.Unsubscribe<PauseSignal>(OnPausePlayer);
    }

    public void Dispose() => Unsubscribe();
}
