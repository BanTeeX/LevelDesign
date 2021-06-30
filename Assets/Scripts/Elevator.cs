using System.Collections;
using UnityEngine;

public class Elevator : MonoBehaviour
{
	[SerializeField] private Transform _block = null;

	private void OnTriggerEnter(Collider other)
	{
		PlayerMovement player = other.GetComponent<PlayerMovement>();
		if (player != null)
		{
			player.isLocked = true;
			StartCoroutine(MoveCoroutine(player));
			_block.Translate(Vector3.forward * 5f);
			GetComponent<BoxCollider>().enabled = false;
		}
	}

	private IEnumerator MoveCoroutine(PlayerMovement player)
	{
		Vector3 translation = Vector3.up * 7f * 0.01f;
		for (int i = 0; i < 100; i++)
		{
			yield return new WaitForSeconds(0.01f);
			transform.Translate(translation);
		}
		player.isLocked = false;
	}
}
