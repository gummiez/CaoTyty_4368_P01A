using UnityEngine;
using UnityEngine.AI;

public class Boss : Enemy
{
	[SerializeField] NavMeshAgent _agent;
	[SerializeField] Transform _player;
	[SerializeField] LayerMask whatIsGround, whatIsPlayer;
	[SerializeField] float _bossHealth = 10f;

	[SerializeField] Vector3 _walkPoint;
	bool _walkPointSet;
	[SerializeField] float _walkPointRange;

	[SerializeField] float _timeBetweenAttacks;
	bool _alreadyAttacked;
	[SerializeField] GameObject _projectile;
	[SerializeField] GameObject _projectileLarge;

	[SerializeField] float _sightRange, _attackRange;
	[SerializeField] bool _playerInSightRange, _playerInAttackRange;

	private void Awake()
	{
		_player = GameObject.Find("Tank").transform;
		_agent = GetComponent<NavMeshAgent>();
		_maxHealth = _bossHealth;
	}

	private void Update()
	{
		//Check for sight and attack range
		_playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, whatIsPlayer);
		_playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, whatIsPlayer);

		if (!_playerInSightRange && !_playerInAttackRange) Patroling();
		if (_playerInSightRange && !_playerInAttackRange) ChasePlayer();
		if (_playerInAttackRange && _playerInSightRange) AttackPlayer();
	}

	private void Patroling()
	{
		if (!_walkPointSet) SearchWalkPoint();

		if (_walkPointSet)
			_agent.SetDestination(_walkPoint);

		Vector3 distanceToWalkPoint = transform.position - _walkPoint;

		//Walkpoint reached
		if (distanceToWalkPoint.magnitude < 1f)
			_walkPointSet = false;
	}
	private void SearchWalkPoint()
	{
		//Calculate random point in range
		float randomZ = Random.Range(-_walkPointRange, _walkPointRange);
		float randomX = Random.Range(-_walkPointRange, _walkPointRange);

		_walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

		if (Physics.Raycast(_walkPoint, -transform.up, 2f, whatIsGround))
			_walkPointSet = true;
	}

	private void ChasePlayer()
	{
		_agent.SetDestination(_player.position);
	}

	private void AttackPlayer()
	{
		_agent.SetDestination(transform.position);

		transform.LookAt(_player);

		int randAttack = Random.Range(0, 4);

		if (!_alreadyAttacked && randAttack != 0)
		{
			Rigidbody rb = Instantiate(_projectile, transform.position + transform.forward *2, Quaternion.identity).GetComponent<Rigidbody>();
			rb.AddForce(transform.forward * 24f, ForceMode.Impulse);
			//rb.AddForce(transform.up * 8f, ForceMode.Impulse);

			_alreadyAttacked = true;
			Invoke(nameof(ResetAttack), _timeBetweenAttacks);
		}
		else if(!_alreadyAttacked && randAttack == 0)
		{
			print("charge");
			Rigidbody rb = Instantiate(_projectileLarge, transform.position + transform.forward * 2, Quaternion.identity).GetComponent<Rigidbody>();
			rb.AddForce(transform.forward * 4f, ForceMode.Impulse);
			rb.AddForce(transform.up * 8f, ForceMode.Impulse);

			_alreadyAttacked = true;
			Invoke(nameof(ResetAttack), _timeBetweenAttacks);
		}
	}
	private void ResetAttack()
	{
		_alreadyAttacked = false;
	}



	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, _attackRange);
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, _sightRange);
	}
}
