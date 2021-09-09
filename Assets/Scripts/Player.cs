using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TankController))]
public class Player : MonoBehaviour
{
	[SerializeField] int _maxHealth = 3;
	[SerializeField] TextMeshProUGUI treasureUI;
	[SerializeField] MeshRenderer body;
	[SerializeField] MeshRenderer turret;
	int _currentHealth;
	int _treasureCount;
	bool _isInvincible = false;

	TankController _tankController;

	private void Awake()
	{
		_tankController = GetComponent<TankController>();
	}

	void Start()
    {
		_currentHealth = _maxHealth;
    }

	public void IncreaseHealth(int amount)
	{
		_currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
		Debug.Log("Player's health: " + _currentHealth);
	}

	public void DecreaseHealth(int amount)
	{
		if (_isInvincible == false)
		{
			_currentHealth -= amount;
			Debug.Log("Player's health: " + _currentHealth);
			if (_currentHealth <= 0)
			{
				Kill();
			}
		}
	}

	public void IncreaseTreasure(int amount)
	{
		_treasureCount += amount;
		Debug.Log("Player's score: " + _treasureCount);
		UpdateUI(_treasureCount);
	}

	public void UpdateUI(int amount)
	{
		treasureUI.text = "Treasure: " + amount;
	}

	public void SetInvicibility()
	{
		_isInvincible = !_isInvincible;
		Debug.Log(_isInvincible);
	}

	public void MaterialSwap(Material mat)
	{
		body.material = mat;
		turret.material = mat;
	}

	public void Kill()
	{
		gameObject.SetActive(false);
		//play particles
		//play sounds
	}
}
