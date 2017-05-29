public class HealthPickupSmall : HealthPickup {
	protected override void Start () {
		base.Start ();
		healthGain = 5;
	}
}