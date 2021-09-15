using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
	[SerializeField] protected float _maxHealth = 5f;
	[SerializeField] int _damageAmount = 1;
	[SerializeField] ParticleSystem _impactParticles;
	[SerializeField] AudioClip _impactSound;
	float _currentHealth;

	Rigidbody _rb;

    void Awake()
    {
		_rb = GetComponent<Rigidbody>();
		_currentHealth = _maxHealth;
		print(_currentHealth);
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

	public void DecreaseHealth(int amount)
	{
			_currentHealth -= amount;
			Debug.Log("Enemy's health: " + _currentHealth);
			if (_currentHealth <= 0)
			{
				Kill();
		}
	}

	public void Kill()
	{
		gameObject.SetActive(false);
		//play particles
		//play sounds
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
		Kill();
	}

	private void FixedUpdate()
	{
		Move();
	}

	public void Move()
	{

	}
}
