using UnityEngine;

public class PillarsSystem : MonoBehaviour
{
	[SerializeField] private Portal _portal;
	private int _amountOfActivatedPillars;

	public void PillarActivated()
	{
		_amountOfActivatedPillars++;
		if (_amountOfActivatedPillars == 4)
		{
			_portal.Activate();
		}
	}
}
