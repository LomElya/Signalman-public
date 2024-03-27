using Cysharp.Threading.Tasks;
using DG.Tweening;

public class FollowPathLooping : FollowPath
{

    private void Awake()
    {
        _pointInPath = _path.GetNextPoint();
    }

    public override UniTask StartMove()
    {
        _taskCompletion = new UniTaskCompletionSource();

        _sequence = DOTween.Sequence();

        foreach (var point in _path.Paths)
        {
            _pointInPath.MoveNext();
            _sequence.Append(transform.DOMove(_pointInPath.Current.position, _duration)).OnComplete(Repeat);
            _sequence.AppendInterval(_delay);
        }

        return _taskCompletion.Task;
    }

    private void Repeat() => StartMove();

    private void OnDisable() => EndMove();
}
