using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
	private Rigidbody2D _rb;
	private Animator _anim;

	// private variables
	private float _shootTimer = 0f;
	private float _changePos = 0f;
	private float _randX;
	private float _randY;
	private float _currentSpeed;

	private bool _isDead = false;

	private Vector2 _movement;

	private GameObject _playerPos;
	private GameObject projectileParent;

	[Header("General Enemy Values")]
	[SerializeField] private int _health;
    [SerializeField] private AudioClip[] audioDeath;

	[Header("Enemy Movement Releated Values")]
	[SerializeField] private float _moveSpeed;
	[SerializeField] private float _maxVelocity;
	[SerializeField] [Tooltip("The time until they start moving when hit an obstacle")]private float _startMovingTime;
	[SerializeField] private float _directionChangeTime;

	[Header("Shooting Releated Values")]
	[SerializeField] private float _timeUntilStartShooting;
	[SerializeField] private float _minShootDelay;
	[SerializeField] private float _maxShootDelay;
	[SerializeField] private float _bulletSpeed;
	[SerializeField] private float _destroyBulletTime;
	[SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private AudioClip shootingSFX;


    private void Start()
	{
		_currentSpeed = _moveSpeed;

        projectileParent = GameObject.Find("Enemy Projectile");
		_rb = GetComponent<Rigidbody2D>();
		_anim = GetComponent<Animator>();
		_playerPos = GameObject.Find("Player");
		if (_playerPos != null)
		{
			_playerPos.GetComponent<Transform>();
		}

		_shootTimer = Random.Range(_minShootDelay, _maxShootDelay);

		_randX = Random.Range(-4.5f, 4.5f);
		_randY = Random.Range(-2.0f, 2.0f);
	}

	private void Update()
	{
		if (!_isDead)
			ChangeDirection();

		_shootTimer -= Time.deltaTime;
		_timeUntilStartShooting -= Time.deltaTime;
	}

	public void EnemyTakeDamage(int amount)
	{
		_health -= amount;

		if (_health <= 0)
		{
			_rb.velocity = Vector2.zero;
            AudioSource.PlayClipAtPoint(audioDeath[Random.Range(0, audioDeath.Length)], Camera.main.transform.position);
			_anim.SetTrigger("dead");
			GetComponent<Collider2D>().enabled = false;
			_isDead = true;
		}
	}

	private void Shoot()
	{
		if (_timeUntilStartShooting <= 0)
		{
			if (_shootTimer <= 0f)
			{
				Vector2 playerDir = new Vector2(_playerPos.transform.position.x - transform.position.x, _playerPos.transform.position.y - transform.position.y);
				playerDir.Normalize();
				var bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation) as GameObject;
                AudioSource.PlayClipAtPoint(shootingSFX, Camera.main.transform.position);
				bullet.transform.rotation = Quaternion.Euler(playerDir);
				bullet.transform.parent = projectileParent.transform;
				bullet.GetComponent<Rigidbody2D>().velocity = playerDir * _bulletSpeed * Time.fixedDeltaTime;
				Destroy(bullet, _destroyBulletTime);
				_shootTimer = Random.Range(_minShootDelay, _maxShootDelay);
			}

			_timeUntilStartShooting = Random.Range(_minShootDelay, _maxShootDelay);
		}
	}

	private void EnemyAnimations()
	{
		if (_rb.velocity.x == 0f && _rb.velocity.y == 0f)
		{
			_anim.SetBool("isRunning", false);
			return;
		}
		else if (_rb.velocity.x != 0f && _rb.velocity.y != 0)
		{
			_anim.SetBool("isRunning", true);
		}

		if (_rb.velocity.y == 0f)
		{
			if (_rb.velocity.x > 0f)
			{
				_anim.SetTrigger("runningEast");
			}
			else if (_rb.velocity.x < 0f)
			{
				_anim.SetTrigger("runningWest");
			}
		}
		else if(_rb.velocity.x == 0f)
		{
			if (_rb.velocity.y > 0f)
			{
				_anim.SetTrigger("runningNorth");
			}
			else if (_rb.velocity.y < 0f)
			{
				_anim.SetTrigger("runningSouth");
			}
		}
	}

	private void FixedUpdate()
	{
		if (!_isDead)
			EnemyAnimations();

		if (!_isDead)
			Shoot();

		if (!_isDead)
		{
			Vector2 _movement = new Vector2(_randX, _randY);
			_movement.Normalize();

			if (_rb.velocity.magnitude < _maxVelocity)
			{
				_rb.velocity = _movement * _currentSpeed * Time.fixedDeltaTime;
			}
		}
	}

	private void ChangeDirection()
	{
		_changePos -= Time.deltaTime;
		if (_changePos <= 0f)
		{
			_randX = Random.Range(-4.5f, 4.5f);
			_randY = Random.Range(-2.0f, 2.0f);
			_changePos = _directionChangeTime;
		}
	}

	private IEnumerator HitObstacle()
	{
		_currentSpeed = 0;
		Shoot();
		yield return new WaitForSeconds(_startMovingTime);
		_currentSpeed = _moveSpeed *= -1;
	}

	private void OnCollisionEnter2D (Collision2D other)
	{
		switch (other.gameObject.tag)
		{
			case "Wall":
				_currentSpeed *= -1;
				break;
			case "Enemy":
				_currentSpeed *= -1;
				break;
			case "Obstacle":
				StartCoroutine(HitObstacle());
				break;
		}
	}
}