using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : MonoBehaviour
{
    //public ParticleSystem lightning;
    public GameObject lightningLight;
    public float lightFlashTime;

    // Start is called before the first frame update
    void Start()
    {
        lightningLight.SetActive(true);
        StartCoroutine(lightningFlash());
        StartCoroutine(lightningDestroy());
    }

    private IEnumerator lightningFlash()
    {
        yield return new WaitForSeconds(lightFlashTime);
        lightningLight.SetActive(false);
    }

    private IEnumerator lightningDestroy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

}
