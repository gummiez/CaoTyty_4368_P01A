using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
	[SerializeField] private float _travelSpeed = 5f;
	[SerializeField] float _angleSpeed = 1f;
	[SerializeField] Transform _target;
	private Rigidbody _rb;

	protected float TravelSpeed
	{
		get { return _travelSpeed; }
		set { _travelSpeed = value; }
	}

	protected void Awake()
	{
		_rb = GetComponent<Rigidbody>();
		_rb.useGravity = false;
		_target = GameObject.Find("Boss").transform;
	}

	private void FixedUpdate()
	{
		Move();
	}

	protected virtual void Move()
	{
		Vector3 moveOffset = transform.forward * _travelSpeed * Time.fixedDeltaTime;
		_rb.MovePosition(_rb.position + moveOffset);
		var rotation = Quaternion.LookRotation(_target.position - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _angleSpeed);
	}
}
