using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    public float explosionDuration = 2f;
    public AudioSource blast;

    private void Start()
    {
        StartCoroutine(StopExplosion());
        blast.Play();
    }

    private IEnumerator StopExplosion()
    {
        yield return new WaitForSeconds(explosionDuration);


        Destroy(gameObject); 
    }
}
