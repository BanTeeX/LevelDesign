using UnityEngine;

public class DeathArea : MonoBehaviour
{
	[SerializeField] private Transform _respawnPoint;

	private void OnTriggerEnter(Collider other)
	{
		PlayerMovement player = other.GetComponent<PlayerMovement>();
		if (player != null)
		{
			other.transform.position = _respawnPoint.position;
		}
	}
}
