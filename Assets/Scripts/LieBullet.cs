using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LieBullet : MonoBehaviour
{
	// Private variables
	private Rigidbody2D _rb;
	private GameObject _enemy;

	private Transform _playerPos;

	[Header("Bullet Properties")]
	[SerializeField] private float _bulletSpeed;
	[SerializeField] private int _damage;

	private void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		_playerPos = GameObject.Find("Player").GetComponent<Transform>();
		_enemy = GameObject.FindGameObjectWithTag("Enemy");

		Vector2 distance = new Vector2(_playerPos.position.x - transform.position.x, _playerPos.position.y - transform.position.y);
		distance.Normalize();
		_rb.velocity = distance * _bulletSpeed * Time.fixedDeltaTime;
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (_enemy != null)
			{
				_enemy.GetComponent<EnemyAI>().EnemyTakeDamage(_damage);
			}
			Destroy(this.gameObject);
		}
	}
}