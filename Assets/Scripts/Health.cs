using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Health
{
	void TrackHealth(float currentHealth, float maxHealth);
	void Kill(float currentHealth);
}
