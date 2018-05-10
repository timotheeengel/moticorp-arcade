using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    [SerializeField] int blastForce = 100;
    [SerializeField] float explosionRadius = 10.0f;
    [SerializeField] float timer = 3.0f;
    bool hasTimerStarted = false;
    bool hasGoneBoom = false;

    ParticleSystem explosionParticles;

    private void Start()
    {
        explosionParticles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update () {
        
        if (hasTimerStarted == true){
            TickTock();
        }
	}

    void TickTock()
    {
        // TODO: Add function that sets hasTimerStarted to true after it appears on the playing area.
        timer -= Time.deltaTime;
        if(timer <= Mathf.Epsilon)
        {
            Boom();
        }
    }

    void Boom()
    {
        if (hasGoneBoom == false)
        {
            explosionParticles.Play();
            GetComponent<MeshRenderer>().enabled = false;

            // https://docs.unity3d.com/ScriptReference/Rigidbody.AddExplosionForce.html
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                // TODO: Add check for Food since pans have to not be affected by the explosion.
                if (rb != null)
                    rb.AddExplosionForce(blastForce, explosionPos, explosionRadius, 3.0F);
            }

            hasGoneBoom = true;
            return;
        } else if (explosionParticles.IsAlive() == false)
            // TODO: Check particle effect in use. If children effects, then withChildren needs to be added to check no particles are alive
        {
            Destroy(gameObject);
        }
    }

    public void StartTime()
    {
        hasTimerStarted = true;
    }
}
