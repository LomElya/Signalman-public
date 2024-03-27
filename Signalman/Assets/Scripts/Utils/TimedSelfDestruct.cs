using UnityEngine;

public class TimedSelfDestruct : MonoBehaviour
{
    private float _lifeTime = 1f;
    private float _spawnTime;
    private Timer _timer;

    public TimedSelfDestruct() => _timer = new Timer(this);

    public void SetLifeTime(float lifeTime)
    {
        _lifeTime = lifeTime;

        _timer.TimeFinished += OnTimeFinished;

        _timer.SetTime(lifeTime);
        _timer.Start();
    }

    //private void Awake() => _timer = new Timer(this);


    //  _spawnTime = Time.time;

    private void OnTimeFinished() => Destroy(gameObject);

    /*   private void Update()
      {
          if (Time.time <= _spawnTime + _lifeTime)
              return;

          Destroy(gameObject);
      } */
}
