using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFloorAttack : MonoBehaviour
{
	Rigidbody _rb;
	Renderer _ground;

	private void Awake()
	{
		_ground = GetComponent<Renderer>();
		_rb = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		int randVal = Random.Range(5, 60);
		StartCoroutine(timer(randVal));
	}

	IEnumerator timer(int amount)
	{
		int counter = amount;

		while (counter > 0)
		{
			yield return new WaitForSeconds(1);
			counter--;
			if(counter <= 2)
			{
				_ground.material.color = Color.red;
			}
		}
		_rb.useGravity = true;
	}
}
