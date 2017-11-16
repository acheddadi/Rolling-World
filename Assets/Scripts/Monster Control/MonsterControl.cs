using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl : MonoBehaviour
{
    [SerializeField] Material _transparent;
    private bool isDead = false;
    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void Kill()
    {
        isDead = true;
        Animator animator = GetComponent<Animator>();
        animator.SetBool("isDead", true);
        _audio.pitch = Random.Range(0.8f, 1.2f);
        _audio.Play();
        StartCoroutine(FadeDeath());
    }

    public bool IsDead()
    {
        return isDead;
    }

    private IEnumerator FadeDeath()
    {
        Renderer renderer = GetComponentInChildren<Renderer>();
        renderer.material = _transparent;
        Material material = renderer.material;

        float change;
        change = 1.0f;
        while (change > 0.0f)
        {
            change -= 0.5f * Time.deltaTime;
            material.color =
                new Color(1.0f, 0.0f, 0.0f, change);
            yield return null;
        }
    }
}
