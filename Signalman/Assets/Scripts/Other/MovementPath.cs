using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    [SerializeField] private PathType _pathType;
    [SerializeField] private List<Transform> _paths;

    private MovementDirection _movementDirection;

    private int _nextIndex = 1;

    public List<Transform> Paths => _paths;

    public bool IsCameBack { get; private set; }

    public IEnumerator<Transform> GetNextPoint()
    {
        if (_paths.Count < 1)
            yield break;

        while (true)
        {

            yield return _paths[_nextIndex];

            if (_paths.Count == 1)
                continue;

            LinearPath();

            int oldIndex = _nextIndex;

            _nextIndex += (int)_movementDirection;

            ChecBack(oldIndex);

            LoopPath();
        }
    }

    private void LinearPath()
    {
        if (_pathType != PathType.Linear)
            return;

        if (_nextIndex <= 0)
            _movementDirection = MovementDirection.Forward;

        if (_nextIndex >= _paths.Count - 1)
            _movementDirection = MovementDirection.Back;
    }

    private void LoopPath()
    {
        if (_pathType != PathType.Loop)
            return;

        if (_nextIndex >= _paths.Count)
            _nextIndex = 0;

        if (_nextIndex < 0)
            _nextIndex = _paths.Count - 1;
    }

    private void ChecBack(int oldIndex )
    {
        /*   if (_pathType != PathType.Linear)
              return; */

        IsCameBack = false;

        if (oldIndex == 1 && _nextIndex == 0)
            IsCameBack = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (_paths.Count < 2)
            return;

        Gizmos.color = Color.red;

        for (int i = 1; i < _paths.Count; i++)
            Gizmos.DrawLine(_paths[i - 1].position, _paths[i].position);

        if (_pathType == PathType.Loop)
            Gizmos.DrawLine(_paths[0].position, _paths[_paths.Count - 1].position);
    }
}

public enum PathType
{
    Linear = 0,
    Loop,
}

public enum MovementDirection : int
{
    Back = -1,
    Forward = 1,
}
