using UnityEngine;
using TMPro;

public class MiniGameSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private int _answer;

    public void Init(AnswerGenerator generator)
    {
        _text.color = Color.white;
        _text.text = generator.RandomNumber.ToString();

        _answer = generator.Answer;
    }

    public bool IsCorrectAnswer(int value)
    {
        if (_answer == value)
        {
            SetColor(Color.green);
            return true;
        }

        SetColor(Color.red);
        return false;
    }

    public void SetColor(Color color) => _text.color = color;
}
