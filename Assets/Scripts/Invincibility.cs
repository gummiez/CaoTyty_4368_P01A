using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : PowerUpBase
{
	[SerializeField] Material _currentMat;
	[SerializeField] Material _powerMat;
	[SerializeField] ParticleSystem _collectParticles;
	[SerializeField] AudioClip _collectSound;
	Player _player;

	protected override void PowerUp(Player player)
	{
		player.MaterialSwap(_powerMat);
		player.SetInvicibility();
	}

	protected override void PowerDown(Player player)
	{
		player.MaterialSwap(_currentMat);
		player.SetInvicibility();
	}

	private void Protection(Player player)
	{
		player.SetInvicibility();
	}

	private void Feedback()
	{
		// particles
		if (_collectParticles != null)
		{
			_collectParticles = Instantiate(_collectParticles, transform.position, Quaternion.identity);
		}
		if (_collectSound != null)
		{
			AudioHelper.PlayClip2D(_collectSound, 1f);
		}
	}
}
