using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class GameStateManager
{
    private IEnumerator _executeCommand;

    private MonoBehaviour _context;

    private DiContainer _container;

    private UniTaskCompletionSource _taskCompletion;

    public GameStateManager(MonoBehaviour context) => _context = context;

    [Inject]
    private void Construct(DiContainer container) => _container = container;


    public async UniTask ExecuteCommands(Command command)
    {
        var commands = new List<Command>()
          {
              command
          };

        await ExecuteCommands(commands);
    }

    public UniTask ExecuteCommands(List<Command> commands)
    {
        _taskCompletion = new UniTaskCompletionSource();

        _executeCommand = ExecuteCommandsCoroutine(commands);

        _context.StartCoroutine(_executeCommand);

        return _taskCompletion.Task;
    }

    private IEnumerator ExecuteCommandsCoroutine(List<Command> commands)
    {
        foreach (var command in commands)
        {
            bool commandCompleted = false;

            command.Execute(() =>
             {
                 commandCompleted = true;
             });

            yield return new WaitUntil(() => commandCompleted == true);
        }

        _taskCompletion.TrySetResult();
    }

    public Command CreateSetGameStateCommand(GameStateType gameStateType, string description = "")
    {
        var command = new SetGameStateCommand(new SetGameStateData(gameStateType, description));
        _container.Inject(command);
        return command;
    }

    public Command CreateStartGameCommand(LevelData levelData)
    {
        var command = new StartGameCommand(new StartGameData(levelData));
        _container.Inject(command);
        return command;
    }

    public Command CreateSetPausedGameCommand(bool isPaused)
    {
        var command = new SetPausGameCommand(new SetPausGameData(isPaused));
        _container.Inject(command);
        return command;
    }

    public Command CreateQuitGameCommand()
    {
        var command = new QuitGameCommand(new QuitGameData());
        _container.Inject(command);
        return command;
    }

    public Command CreateUnFollowCameraComand()
    {
        var command = new UnFollowCameraComand(new UnFollowCameraData());
        _container.Inject(command);
        return command;
    }

    public Command CreateLoadSceneCommand(string sceneName)
    {
        var command = new LoadSceneCommand(new LoadSceneData(sceneName));
        _container.Inject(command);
        return command;
    }
}
