using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
	[SerializeField] protected float _travelSpeed = 5f;
	[SerializeField] protected float _angleSpeed = 1f;
	[SerializeField] float _projectileDuration = 2f;

	[SerializeField] ParticleSystem _impactParticles = null;
	[SerializeField] AudioClip _impactSound = null;
	[SerializeField] AudioClip _spawnSound = null;
	[SerializeField] float _projectileDamage = 1f;
	protected Rigidbody _rb;
	protected Renderer _r;

	protected float TravelSpeed
	{
		get { return _travelSpeed; }
		set { _travelSpeed = value; }
	}

	protected void Awake()
	{
		_rb = GetComponent<Rigidbody>();
		_r = GetComponent<Renderer>();
		_rb.useGravity = false;
		StartTimer();
		gameObject.SetActive(true);
		SpawnFeedback();
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
			enemy.TakeDamage((int)_projectileDamage);
			ImpactFeedback();
			_r.enabled = false;
			StartTimer();
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
		Destroy(gameObject);
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

	private void SpawnFeedback()
	{
		if (_spawnSound != null)
		{
			AudioHelper.PlayClip2D(_spawnSound, 1f);
		}
	}

}
