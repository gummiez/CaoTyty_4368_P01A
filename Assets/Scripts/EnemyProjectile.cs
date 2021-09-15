using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Enemy
{

	private void Start()
	{
		Enemy enemy = GetComponent<Enemy>();
		StartTimer();
	}

	protected override void StartTimer()
	{
		print("timer");
		base.StartTimer();
	}

	protected override void PlayerImpact(Player player)
	{
		base.PlayerImpact(player);
		gameObject.SetActive(false);
	}
}
