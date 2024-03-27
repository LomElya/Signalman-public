using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;
using System.Collections.Generic;

public class MiniGameMenu : UIState
{
    [SerializeField] private TMP_Text _textResult;
    [SerializeField] private MainButton _buttonExit;
    [SerializeField] private ButtonManager _buttonManager;

    private List<AnswerGenerator> _answers = new();

    private string _result;
    private int _currentID = 0;

    public bool IsExit { get; private set; }

    public static UniTask<Disposable<MiniGameMenu>> Load()
    {
        var assetLoader = new LocalAssetLoader();
        return assetLoader.LoadDisposable<MiniGameMenu>(AssetsConstants.MiniGameMenu);
    }

    protected override void OnShow()
    {
        base.OnShow();

        GenerateAnswer();
    }

    protected override void Unload()
    {

        _canvas.enabled = false;
    }

    private void GenerateAnswer()
    {
        int randomRange = Random.Range(4, 5);

        for (int i = 0; i <= randomRange; i++)
        {
            _answers.Add(new AnswerGenerator());
            _result += _answers[i].RandomNumber;
        }

        _textResult.text = _result;
    }

    private void OnClickButton(MainButton button)
    {
        int id = button.ID;

        CheckAnswer(id);

        _currentID++;

        CheckFinish();
    }

    private void OnClickButtonExit(MainButton button)
    {
        IsExit = true;
        _taskCompletion.TrySetResult(false);
    }

    private async void CheckAnswer(int answer)
    {
        if (answer == _answers[_currentID].Answer)
            return;

        _textResult.color = Color.red;

        await UniTask.WaitForSeconds(0.5f);

        IsExit = false;
        _taskCompletion.TrySetResult(false);

    }

    private async void CheckFinish()
    {
        if (_currentID != _answers.Count)
            return;

        _textResult.color = Color.green;

        await UniTask.WaitForSeconds(0.5f);

        IsExit = false;
        _taskCompletion.TrySetResult(true);
    }

    protected override void Subscribe()
    {
        base.Subscribe();

        _buttonExit.Click += OnClickButtonExit;
        _buttonManager.Click += OnClickButton;
    }

    protected override void Unsubscribe()
    {
        base.Unsubscribe();

        _buttonExit.Click -= OnClickButtonExit;
        _buttonManager.Click -= OnClickButton;
    }
}
