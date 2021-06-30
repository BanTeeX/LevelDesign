using UnityEngine;

public class Pillar : MonoBehaviour, IInteractive
{
	[SerializeField] private Transform _diamondPlace;
	private bool _isActivated;

	public void Action(PlayerMovement player)
	{
		if (_isActivated)
		{
			return;
		}

		if (player.leftHandObject != null)
		{
			Diamond diamond = player.leftHandObject.GetComponentInParent<Diamond>();
			if (diamond != null)
			{
				player.leftHandObject.position = _diamondPlace.position;
				player.leftHandObject = null;
				player.isLeftHandBusy = false;
				_isActivated = true;
				diamond.isActive = false;
				GetComponentInParent<PillarsSystem>().PillarActivated();
				return;
			}
		}

		if (player.rightHandObject != null)
		{
			Diamond diamond = player.rightHandObject.GetComponentInParent<Diamond>();
			if (diamond != null)
			{
				player.rightHandObject.position = _diamondPlace.position;
				player.rightHandObject = null;
				player.isRightHandBusy = false;
				_isActivated = true;
				diamond.isActive = false;
				GetComponentInParent<PillarsSystem>().PillarActivated();
				return;
			}
		}
	}

	public string GetMessage()
	{
		if (_isActivated)
		{
			return string.Empty;
		}
		return "Press E to put diamond";
	}
}
