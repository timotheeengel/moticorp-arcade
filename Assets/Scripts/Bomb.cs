using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Trap {

    [SerializeField] Texture bombBlinkT;
    [SerializeField] Material bombM;
    [SerializeField] Texture bombOriginalT;
    bool originalT = true;
    [SerializeField] float blinkSpeed = 0.3f;

    [SerializeField] float blastForce = 12.0f;
    [SerializeField] float explosionRadius = 1.0f;
    [SerializeField] float timer = 3.0f;
    [SerializeField] AudioClip boomSFX;
    bool hasGoneBoom = false;
    AudioSource audioSource;

    ParticleSystem explosionParticles;
    Rigidbody rb;

    private void Start()
    {
        explosionParticles = GetComponentInChildren<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();


        StartCoroutine(TickTock());
    }

    // Update is called once per frame
    void Update () {

	}

    IEnumerator TickTock()
    {
        while(!rb.useGravity)
        {
            yield return null;
        }

        float initTimer = timer;
        float blinkCounter = 0;
        float blinkTimer = 0;
        while (timer > 0)
        {
            blinkTimer -= Time.deltaTime;
            timer -= Time.deltaTime;
            if (blinkTimer < 0)
            {
                blinkTimer += blinkCounter;
                Blink();
            }

            blinkCounter = blinkSpeed * (timer / initTimer + (0.1f * (1 - timer/initTimer)));

            yield return null;
        }
        Boom();
        
        while(explosionParticles.IsAlive())
        {
            yield return null;
        }
        Destroy(gameObject);
    }

    void Blink ()
    {
        if(originalT == true)
        {
            bombM.mainTexture = bombBlinkT;
        } else
        {
            bombM.mainTexture = bombOriginalT;
        }
        originalT = !originalT;
    }

    void Boom()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(boomSFX);
        explosionParticles.Play();
        GetComponent<MeshRenderer>().enabled = false;

        // https://docs.unity3d.com/ScriptReference/Rigidbody.AddExplosionForce.html
        Vector3 explosionPos = GetComponent<Transform>().position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            // TODO: Add check for Food since pans have to not be affected by the explosion.
            if (rb != null)
                rb.AddExplosionForce(blastForce, explosionPos, explosionRadius, 3.0F, ForceMode.VelocityChange);
        }
        GetComponent<Collider>().enabled = false;
        hasGoneBoom = true;
        return;
    }
}
