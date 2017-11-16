using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Rotator _world;
    [SerializeField] private Transform _slashLocation;
    [SerializeField] private GameObject _slashPrefab;
    private float _lastAttack = 0;
    private Animator _animator;
    private AudioSource _audio;
    private bool _isDead = false;
    private float _originalAngle, _targetAngle, _originalPos, _currentVelocity = 0.0f, _smoothTime = 0.1f;

    public float speed = 3.0f, maxRotation = 10.0f, bounds = 2.0f, attackDelay = 0.1f;

	private void Start()
	{
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        _originalAngle = transform.localEulerAngles.y;
        _originalPos = transform.position.x;
	}
	
	private void Update()
	{
        // Movement
        float moveX = Input.GetAxis("Horizontal") * speed * _world.multiplier * Time.deltaTime;
        if (
            (transform.position.x + moveX < _originalPos - bounds / 2) || 
            (transform.position.x + moveX > _originalPos + bounds / 2)
            ) moveX = 0;

        Vector3 move = new Vector3(moveX, 0, 0);
        transform.position += move;

        _targetAngle = _originalAngle - (Input.GetAxis("Horizontal") * maxRotation);
        float rotate = Mathf.SmoothDampAngle(transform.localEulerAngles.y, _targetAngle, ref _currentVelocity, _smoothTime);
        transform.localEulerAngles = new Vector3(0, rotate, 0);

        // Attack prefabs
        if (Input.GetMouseButtonDown(0) && !_animator.GetBool("isHit") && (Time.time > _lastAttack + attackDelay))
        {
            GameObject slash = Instantiate(_slashPrefab, transform.position, Quaternion.identity);
            slash.transform.position = _slashLocation.position;
            slash.transform.localEulerAngles = new Vector3(90, -180, 270);
            slash.transform.SetParent(_slashLocation);
            _animator.SetTrigger("attack");
            _audio.pitch = Random.Range(0.8f, 1.2f);
            _audio.Play();
            _lastAttack = Time.time;
        }
        
        // Death animation
        _animator.SetBool("isDead", _isDead);

        // Walk speed
        _animator.SetFloat("blendThreshold", _world.multiplier);
        if (_world.multiplier > 1) _animator.SetFloat("walkSpeed", _world.multiplier);
        else _animator.SetFloat("walkSpeed", 1.0f);
    }
}
