using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : Projectile
{
	private float _acceleration = .2f;
	protected override void Move()
	{
		TravelSpeed += _acceleration;
		base.Move();
	}
}
