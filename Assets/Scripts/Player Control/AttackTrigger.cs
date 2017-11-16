using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _sparksPrefab;
    private Animator _animator;
    private AudioSource _audio;

	private void Start()
	{
        _animator = GetComponentInParent<Animator>();
        _audio = GetComponent<AudioSource>();
	}

    private void OnTriggerStay(Collider other)
    {
        MonsterControl monster = other.GetComponent<MonsterControl>();

        if (_animator.GetBool("impact") && (monster != null))
        {
            if (!monster.IsDead())
            {
                _audio.pitch = Random.Range(0.8f, 1.2f);
                _audio.Play();
                monster.Kill();
                GameObject spark = Instantiate(_sparksPrefab, transform.position, Quaternion.identity);
                spark.transform.position = transform.position;
                spark.transform.SetParent(transform);
            }
        }
    }
}
