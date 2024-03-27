using System.Collections.Generic;
using UnityEngine;

public class TileMover : MonoBehaviour
{
    [SerializeField] private List<TileScroller> _images = new List<TileScroller>();

    [SerializeField] private float _speedMove = 1f;

    private void Update() => Move();

    public void Move()
    {
        foreach (var image in _images)
        {
            image.Scrolling(_speedMove);
        }
    }
}
