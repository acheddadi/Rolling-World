using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    private Animator _animator;
    private bool _invincible = false;
    private Renderer[] _objectsToFlicker;
    private Material[][] _transparentArray;
    [SerializeField] private Material[] _transparentMaterials;

    public float invincibilityDelay = 1.5f;

    private void Start()
    {
        _animator = GetComponentInParent<Animator>();
        GameObject parent = transform.parent.gameObject;
        _objectsToFlicker = parent.GetComponentsInChildren<Renderer>();

        // Create Multi-Dimensional array contaniing transparent materials in the right order
        _transparentArray = new Material[_objectsToFlicker.Length][]; int k = 0;
        for (int i = 0; i < _transparentArray.Length; i++)
        {
            _transparentArray[i] = new Material[_objectsToFlicker[i].materials.Length];

            for (int j = 0; j < _transparentArray[i].Length; j++, k++)
            {
                _transparentArray[i][j] = _transparentMaterials[k];
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        MonsterControl monster = other.GetComponent<MonsterControl>();

        if (!_animator.GetBool("impact") && (monster != null) && !_invincible)
        {
            if (!monster.IsDead())
            {
                _animator.SetBool("isHit", true);
                StartCoroutine(Invincibility());
            }
        }
    }

    private IEnumerator Invincibility()
    {
        _invincible = true;
        float startTimer = Time.time;
        yield return new WaitForSeconds(0.25f);

        // Save original materials array
        Material[][] oldMaterials = new Material[_objectsToFlicker.Length][];
        for (int i = 0; i < oldMaterials.Length; i++) oldMaterials[i] = _objectsToFlicker[i].materials;

        // Apply transparent materials array
        for (int i = 0; i < _objectsToFlicker.Length; i++) _objectsToFlicker[i].materials = _transparentArray[i];

        // Flicker renderer
        while (Time.time < startTimer + invincibilityDelay)
        {
            for (int i = 0; i < _objectsToFlicker.Length; i++) _objectsToFlicker[i].enabled = false;
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < _objectsToFlicker.Length; i++) _objectsToFlicker[i].enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        // Apply original materials array
        for (int i = 0; i < _objectsToFlicker.Length; i++) _objectsToFlicker[i].materials = oldMaterials[i];
        _invincible = false;
    }
}
