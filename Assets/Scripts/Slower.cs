using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slower : Enemy
{
	[SerializeField] float _slowAmount = 5;

	protected override void PlayerImpact(Player player)
	{
		TankController controller = player.GetComponent<TankController>();
		controller.MoveSpeed -= _slowAmount;

		gameObject.SetActive(false);
	}
}
