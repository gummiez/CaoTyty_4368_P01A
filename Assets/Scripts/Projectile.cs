using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
	[SerializeField] protected float _travelSpeed = 5f;
	[SerializeField] protected float _angleSpeed = 1f;
	[SerializeField] float _projectileDuration = 2f;

	[SerializeField] ParticleSystem _impactParticles;
	[SerializeField] AudioClip _impactSound;
	[SerializeField] float _projectileDamage = 1f;
	protected Rigidbody _rb;

	protected float TravelSpeed
	{
		get { return _travelSpeed; }
		set { _travelSpeed = value; }
	}

	protected void Awake()
	{
		_rb = GetComponent<Rigidbody>();
		_rb.useGravity = false;

	}


	private void FixedUpdate()
	{
		Move();
	}

	protected virtual void Move()
	{

	}

	private void OnCollisionEnter(Collision other)
	{
		Enemy enemy = other.gameObject.GetComponent<Enemy>();
		if (enemy != null)
		{
			enemy.DecreaseHealth((int)_projectileDamage);
			ImpactFeedback();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		Enemy enemy = other.gameObject.GetComponent<Enemy>();
		if (enemy != null)
		{
			gameObject.SetActive(false);
		}
	}

	IEnumerator timer(int amount)
	{
		int counter = amount;

		while (counter > 0)
		{
			yield return new WaitForSeconds(1);
			counter--;
			Debug.Log("timer: " + counter);
		}
		gameObject.SetActive(false);
	}

	protected virtual void StartTimer()
	{
		StartCoroutine(timer((int)_projectileDuration));
	}

	private void ImpactFeedback()
	{
		// particles
		if (_impactParticles != null)
		{
			_impactParticles = Instantiate(_impactParticles, transform.position, Quaternion.identity);
		}
		//audio, TODO - consider object pooling
		if (_impactSound != null)
		{
			AudioHelper.PlayClip2D(_impactSound, 1f);
		}
	}
}
