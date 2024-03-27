using Cinemachine;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;


[RequireComponent(typeof(PlayableDirector))]
public class IntroAnimation : MonoBehaviour
{
    [SerializeField] private CinemachineTrack _track;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private Camera _camera2;

    private PlayableDirector _director;
    private UniTaskCompletionSource<bool> _playAwater;

    private void Awake() => _director = GetComponent<PlayableDirector>();

    public async UniTask Play()
    {
        /*  foreach (var s in _settings)
         {
             s.Object.SetActive(s.Type == result);
         } */

        _playAwater = new UniTaskCompletionSource<bool>();

        _director.stopped -= OnTimelineFinished;
        _director.stopped += OnTimelineFinished;

        var a = _director.GetGenericBinding(_camera2);

        Debug.Log(a);

        _director.Play();

        await _playAwater.Task;
    }

    private void OnTimelineFinished(PlayableDirector _) => _playAwater.TrySetResult(true);
}
