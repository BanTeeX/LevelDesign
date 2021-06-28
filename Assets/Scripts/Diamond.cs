using UnityEngine;

public class Diamond : MonoBehaviour, IInteractive
{
	[HideInInspector] public bool isActive = true;

	public void Action(PlayerMovement player)
	{
		if (isActive)
		{
			player.Collect(transform);
			return;
		}
	}

	public string GetMessage()
	{
		if (isActive)
		{
			return "Press Action to collect";
		}

		return string.Empty;
	}
}
