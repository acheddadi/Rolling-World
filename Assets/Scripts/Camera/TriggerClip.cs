using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerClip : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(FadeClipping(other));
    }

    private IEnumerator FadeClipping(Collider other)
    {
        Renderer renderer = other.GetComponent<Renderer>();
        Material[] materials = renderer.materials;
        float change;
        change = 1.0f;
        while (change > 0.0f)
        {
            change -= 3.0f * Time.deltaTime;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].color =
                new Color(materials[i].color.r, materials[i].color.g, materials[i].color.b, change);
            }
            yield return null;
        }
    }
}
