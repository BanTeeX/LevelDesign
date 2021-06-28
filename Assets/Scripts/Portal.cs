using UnityEngine;

public class Portal : MonoBehaviour
{
	[SerializeField] private GameObject _portalInside;

    public void Activate() => _portalInside.SetActive(true);

	private void OnTriggerEnter(Collider other)
	{
		if (null != other.GetComponent<PlayerMovement>())
		{
			Application.Quit();
		}
	}
}
