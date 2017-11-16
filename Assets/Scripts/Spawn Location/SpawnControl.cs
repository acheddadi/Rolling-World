using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
    public float spawnBounds = 6.0f;
    public float outerSpawnDelay = 1.0f;
    public float enemySpawnDelay = 1.0f;

    [SerializeField] private Transform _ground;
    [SerializeField] private Transform _enemyGround;
    [SerializeField] private GameObject[] _outerObjects;
    [SerializeField] private GameObject[] _enemyObjects;
    private float _currentTime0;
    private float _currentTime1;

    private void Start()
	{
        _currentTime0 = Time.time;
        _currentTime1 = _currentTime0;
    }
	
	private void Update()
	{
		if ((Time.time - _currentTime0 > outerSpawnDelay / 4) && (_outerObjects.Length > 0))
        {
            float range = 0;
            switch (Random.Range(0, 2))
            {
                case 0:
                    range = Random.Range(-spawnBounds / 2, -spawnBounds * 2 / 3);
                    break;
                case 1:
                    range = Random.Range(spawnBounds * 2 / 3, spawnBounds / 2);
                    break;
            }
            GameObject obj = Instantiate(_outerObjects[Random.Range(0, _outerObjects.Length)], transform.position, Quaternion.identity);
            obj.transform.position = transform.position + new Vector3(range, 0, 0);
            obj.transform.Rotate(-45, 0, 0);
            obj.transform.localScale /= Random.Range(2,3);
            obj.transform.SetParent(_ground);
            obj.transform.Rotate(0, Random.Range(0, 360), 0);
            Rigidbody rb = obj.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            _currentTime0 = Time.time;
        }

        if ((Time.time - _currentTime1 > enemySpawnDelay) && (_enemyObjects.Length > 0))
        {
            float range = Random.Range(-spawnBounds * 1 / 4, spawnBounds * 1 / 4);
            GameObject obj = Instantiate(_enemyObjects[Random.Range(0, _enemyObjects.Length)], transform.position, Quaternion.identity);
            obj.transform.position = transform.position + new Vector3(range, 0, -0.225f);
            obj.transform.Rotate(-45, 0, 0);
            obj.transform.SetParent(_enemyGround);
            Rigidbody rb = obj.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            _currentTime1 = Time.time;
        }
    }
}
