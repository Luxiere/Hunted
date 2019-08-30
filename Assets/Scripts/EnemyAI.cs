using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	private Rigidbody2D _rb;

	// private variables
	private float _shootTimer = 0f;
	private float _changePos = 0f;
	private float _randX;
	private float _randY;

	private Vector2 _movement;

	private PlayerMovement _playerPos;
	private GameObject projectileParent;
    private GameManager gm;

	[Header("General Enemy Values")]
	[SerializeField] private int _health;

	[Header("Enemy Movement Releated Values")]
	[SerializeField] private float _moveSpeed;
	[SerializeField] private float _maxVelocity;
	[SerializeField] private float _directionChangeTime;

	[Header("Shooting Releated Values")]
	[SerializeField] private float _minShootDelay;
	[SerializeField] private float _maxShootDelay;
	[SerializeField] private float _bulletSpeed;
	[SerializeField] private float _destroyBulletTime;
	[SerializeField] private GameObject _bulletPrefab;

	private void Start()
	{
        gm = FindObjectOfType<GameManager>();
        projectileParent = GameObject.Find("Enemy Projectile");
		_rb = GetComponent<Rigidbody2D>();
        _playerPos = FindObjectOfType<PlayerMovement>();
		if (_playerPos != null)
		{
			_playerPos.GetComponent<Transform>();
		}

		_shootTimer = Random.Range(_minShootDelay, _maxShootDelay);

		_randX = Random.Range(-4.5f, 4.5f);
		_randY = Random.Range(-2.0f, 2.0f);

		//_randX = Random.Range(-8.3f, 8.3f);
		//_randY = Random.Range(-4.5f, 4.5f);
	}

	private void Update()
	{
		ChangeDirection();

		_shootTimer -= Time.deltaTime;
	}

	public void EnemyTakeDamage(int amount)
	{
		_health -= amount;

		if (_health <= 0)
		{
			Destroy(this.gameObject);
            gm.EnemyDeath();
		}
	}

	private void Shoot()
	{
		if (_shootTimer <= 0f)
		{
			Vector2 playerDir = new Vector2(_playerPos.transform.position.x - transform.position.x, _playerPos.transform.position.y - transform.position.y);
			playerDir.Normalize();
			var bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation) as GameObject;
			bullet.transform.rotation = Quaternion.Euler(playerDir);
            bullet.transform.parent = projectileParent.transform;
			bullet.GetComponent<Rigidbody2D>().velocity = playerDir * _bulletSpeed * Time.fixedDeltaTime;
			Destroy(bullet, _destroyBulletTime);
			_shootTimer = Random.Range(_minShootDelay, _maxShootDelay);
		}
	}

	private void FixedUpdate()
	{
		Vector2 _movement = new Vector2(_randX, _randY);
		_movement.Normalize();

		Shoot();

		//_rb.AddForce(_movement * _moveSpeed * Time.fixedDeltaTime);

		if (_rb.velocity.magnitude < _maxVelocity)
		{
			_rb.velocity = _movement * _moveSpeed * Time.fixedDeltaTime;
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

	private void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.CompareTag("Wall"))
		{
			_moveSpeed *= -1;
		}
        else if (other.gameObject.CompareTag("Enemy"))
        {
            _moveSpeed *= -1f;
        }
    }
}