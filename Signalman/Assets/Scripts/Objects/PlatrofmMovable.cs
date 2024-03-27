using UnityEngine;

public class PlatrofmMovable : MonoBehaviour
{
   [SerializeField] private FollowPath _path;

   private void Start() => _path.StartMove();
}
