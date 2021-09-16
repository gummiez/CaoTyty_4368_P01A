using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{
	[SerializeField] int powerupDuration = 5;
	MeshRenderer _mR;
	BoxCollider _bC;

	protected abstract void PowerUp(Player player);
	protected abstract void PowerDown(Player player);

	private void Awake()
	{
		_mR = GetComponent<MeshRenderer>();
		_bC = GetComponent<BoxCollider>();
	}

	private void OnTriggerEnter(Collider other)
	{
		Player player = other.gameObject.GetComponent<Player>();
		if(player != null)
		{
			Debug.Log("powerup");
			StartCoroutine(timer(powerupDuration, player));
		}
	}

	IEnumerator timer(int amount, Player player)
	{
		PowerUp(player);
		int counter = amount;
		DisableObject();

		while(counter > 0)
		{
			yield return new WaitForSeconds(1);
			counter--;
			Debug.Log("timer: " + counter);
		}
		gameObject.SetActive(false);

		PowerDown(player);
	}

	private void DisableObject()
	{
		_mR.enabled = false;
		_bC.enabled = false;
	}
}
