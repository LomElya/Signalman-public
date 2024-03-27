using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    [SerializeField] protected MovementPath _path;
    [SerializeField, Range(2, 10)] protected float _duration;
    [SerializeField, Range(0, 5)] protected float _delay;

    protected IEnumerator<Transform> _pointInPath;

    protected Sequence _sequence;

    protected UniTaskCompletionSource _taskCompletion;

    private void Awake() => _pointInPath = _path.GetNextPoint();

    public virtual UniTask StartMove()
    {
        _taskCompletion = new UniTaskCompletionSource();

        _sequence = DOTween.Sequence();

        foreach (var point in _path.Paths)
        {
            _pointInPath.MoveNext();
            _sequence.Append(transform.DOMove(_pointInPath.Current.position, _duration)).OnComplete(NextPoint);
            _sequence.AppendInterval(_delay);
        }

        return _taskCompletion.Task;
    }

    private void NextPoint()
    {
        if (_path.IsCameBack)
            EndMove();
    }

    protected void EndMove() => _taskCompletion.TrySetResult();

    private void OnDestroy() => _sequence.Kill();
}
