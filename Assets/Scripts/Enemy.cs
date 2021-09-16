using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour, IDamageable, Health
{
	[SerializeField] protected float _maxHealth = 1f;
	[SerializeField] int _damageAmount = 1;
	[SerializeField] ParticleSystem _impactParticles;
	[SerializeField] AudioClip _impactSound;
	float _currentHealth;

	Rigidbody _rb;

    void Awake()
    {
		_rb = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		_currentHealth = _maxHealth;
		Debug.Log("current enemy health 2 " + _maxHealth);
	}

	public void TrackHealth(float currentHealth, float maxHealth)
	{
		Kill(currentHealth);
	}

	public void Kill(float currentHealth)
	{
		currentHealth = _currentHealth;
		if(currentHealth <= 0)
		{
			gameObject.SetActive(false);
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		Player player = other.gameObject.GetComponent<Player>();
		if(player != null)
		{
			PlayerImpact(player);
			ImpactFeedback();
		}
	}

	protected virtual void PlayerImpact(Player player)
	{
		player.DecreaseHealth(_damageAmount);
	}

	public void TakeDamage(int amount)
	{
		_currentHealth -= amount;
		Debug.Log("Enemy's health: " + _currentHealth);
		TrackHealth(_currentHealth, _maxHealth);
	}

	protected virtual void StartTimer()
	{
		StartCoroutine(timer((int)1.5f));
	}

	IEnumerator timer(int amount)
	{
		int counter = amount;

		while (counter > 0)
		{
			yield return new WaitForSeconds(2);
			counter--;
			Debug.Log("timer: " + counter);
		}
		gameObject.SetActive(false);
	}


	private void ImpactFeedback()
	{
		// particles
		if (_impactParticles != null)
		{
			_impactParticles = Instantiate(_impactParticles, transform.position, Quaternion.identity);
		}
		//audio, TODO - consider object pooling
		if(_impactSound != null)
		{
			AudioHelper.PlayClip2D(_impactSound, 1f);
		}
		TakeDamage(1);
	}

	private void FixedUpdate()
	{
		Move();
	}

	public void Move()
	{

	}
}
