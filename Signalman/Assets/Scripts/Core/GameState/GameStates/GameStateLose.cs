using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Zenject;

public class GameStateLose : GameState
{

    private const string KeyLose = "Lose";

    private GameStateManager _gameStateManager;

    private LevelFactory _levelFactory;
    private Level _level;

    public GameStateLose(GameStateType gameStateType) : base(gameStateType)
    {
    }

    [Inject]
    private void Construct(GameStateManager gameStateManager, LevelFactory levelFactory, Level level)
    {
        _gameStateManager = gameStateManager;

        _levelFactory = levelFactory;
        _level = level;
    }

    public override bool CanSwitchToState(GameStateType gameStateType) => true;

    public override async UniTask OnEnter(string description = "")
    {
        SaveStatus();

        var command1 = _gameStateManager.CreateSetPausedGameCommand(true);
        var command2 = _gameStateManager.CreateUnFollowCameraComand();

        var commands = new List<Command>()
        {
            command1,
            command2,
        };

        await _gameStateManager.ExecuteCommands(commands);

        var resultMenu = await GameResultMenu.Load();

        await resultMenu.Value.Show(GameResultType.Lose, OnRestartLevel, OnQuit, description);

        resultMenu.Dispose();
    }

    private void SaveStatus()
    {
        LevelData data = _levelFactory.LevelDatas[_level.CurrentLevelID].Config.Data;
        int value;
        PlayerExtensions.Load(data.NAME_SCENE + KeyLose, out value);
        PlayerExtensions.Save(data.NAME_SCENE + KeyLose, value + 1);

    }

    private async void OnRestartLevel()
    {
        LevelData levelData = _levelFactory.GetData(_level.CurrentLevelID);
        string nameScene = levelData.NAME_SCENE;

        var command = _gameStateManager.CreateLoadSceneCommand(nameScene);
        var command1 = _gameStateManager.CreateSetGameStateCommand(GameStateType.GameStart);

        var commands = new List<Command>()
        {
            command,
            command1,
        };

        await _gameStateManager.ExecuteCommands(commands);
    }

    private async void OnQuit()
    {
        var command1 = _gameStateManager.CreateQuitGameCommand();

        await _gameStateManager.ExecuteCommands(command1);
    }
}
