using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Trap {

    [SerializeField] float blastForce = 100.0f;
    [SerializeField] float explosionRadius = 10.0f;
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
    }

    // Update is called once per frame
    void Update () {
        if (rb.useGravity == true)
        {
            TickTock();
        }
	}

    void TickTock()
    {
        // TODO: Add function that sets hasTimerStarted to true after it appears on the playing area.
        timer -= Time.deltaTime;
        if(timer <= Mathf.Epsilon && hasGoneBoom == false)
        {
            Boom();
        }
        else if (hasGoneBoom == true && explosionParticles.IsAlive() == false)
        // TODO: Check particle effect in use. If children effects, then withChildren needs to be added to check no particles are alive
        {
            Destroy(gameObject);
        }
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

        hasGoneBoom = true;
        return;
    }
}
