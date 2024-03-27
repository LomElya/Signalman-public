using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public interface IGameModeCleaner
{
    IEnumerable<GameObjectFactory> Factories { get; }
    string SceneName { get; }
    UniTask Cleanup();
}