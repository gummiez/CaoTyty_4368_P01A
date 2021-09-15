using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
	[SerializeField] Transform _target;

	private void Start()
	{
		FindBoss();
	}

	private void FindBoss()
	{
		if (_target != null)
		{
			_target = GameObject.Find("Boss").transform;
		}
		else
		{
			_target = null;
		}
	}

	protected override void Move()
	{
		if (_target != null)
		{
			Vector3 moveOffset = transform.forward * _travelSpeed * Time.fixedDeltaTime;
			_rb.MovePosition(_rb.position + moveOffset);
			var rotation = Quaternion.LookRotation(_target.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _angleSpeed);
		}
		else if (_target == null)
		{
			Vector3 moveOffset = transform.forward * _travelSpeed * Time.fixedDeltaTime;
			_rb.MovePosition(_rb.position + moveOffset);
		}
	}
}
