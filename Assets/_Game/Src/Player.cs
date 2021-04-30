using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int health = 100;
    [SerializeField] Rigidbody bullet;
    [SerializeField] Transform bulletSpawnPos;
    [SerializeField] float shootForce = 500f;

    Rigidbody currentBullet;

    Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        RequestNewBullet();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    public void ReceiveDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, 100);
        Debug.Log($"Received damage: {damage} currentHealth: {health}");
        if (health <= 0) Die();
    }

    private void Die()
    {
        Debug.Log("Player DEAD");
    }

    void Fire()
    {
        if (currentBullet != null)
        {
            currentBullet.isKinematic = false;
            currentBullet.transform.parent = null;
            currentBullet.AddForce(_camera.transform.forward * shootForce, ForceMode.Impulse);
            Destroy(currentBullet.gameObject, 3f);
            currentBullet = null;
            RequestNewBullet();
        }
    }

    void SpawnBullet()
    {
        currentBullet = Instantiate(bullet, bulletSpawnPos.position, bulletSpawnPos.rotation);
        currentBullet.transform.parent = bulletSpawnPos;
        currentBullet.isKinematic = true;
    }

    void RequestNewBullet()
    {
        StartCoroutine(RequestNewBulletRoutine());
    }

    IEnumerator RequestNewBulletRoutine()
    {
        yield return new WaitForSeconds(2f);
        SpawnBullet();
    }
}
