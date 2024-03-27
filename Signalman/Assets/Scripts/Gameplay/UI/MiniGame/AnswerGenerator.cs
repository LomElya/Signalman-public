using UnityEngine;

public class AnswerGenerator
{
    public int Answer { get; private set; }
    public int RandomNumber { get; private set; }

    public AnswerGenerator()
    {
        Generate();
    }

    public void Generate()
    {
        RandomNumber = 0;
        RandomNumber = Random.Range(1, 10);

        while (RandomNumber + Answer <= 9)
            Answer++;
    }
}
