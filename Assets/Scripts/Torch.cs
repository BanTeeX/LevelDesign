using UnityEngine;

public class Torch : MonoBehaviour, IInteractive
{
	public void Action(PlayerMovement player)
	{
		player.Collect(transform);
	}

	public string GetMessage() => "Press Action to collect";
}
