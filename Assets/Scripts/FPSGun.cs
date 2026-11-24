using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSGun : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;


    public Camera fpsCam;
    public ParticleSystem muzzelFlash;

    public GameObject impatEffect;
    public float ImpactForce = 30f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();

        }

        void Shoot()
        {
            muzzelFlash.Play();

            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);

               Enemy target = hit.transform.GetComponent<Enemy>();

                if (target != null)
                {
                    target.takeDamage(damage);
                }
                if(hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * ImpactForce);
                }
               
               GameObject impactGo = Instantiate(impatEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGo, 2f);
            }
        }
    }
}
