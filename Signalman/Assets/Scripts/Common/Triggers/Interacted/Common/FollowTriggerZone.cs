public class FollowTriggerZone : Trigger<Follower>
{
    protected override void OnEnter(Follower follower) => follower.transform.SetParent(this.gameObject.transform);
    protected override void OnExit(Follower follower) => follower.transform.SetParent(null);
}
