using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtTarget : MonoBehaviour
{
    public Transform target;
    public GameObject projectile;
    public AudioClip shootSFX;

    public float power = 10.0f;
    public float rateOfFire = 1.0f;

    private float nextShot;
    private GameObject newProjectile;
    private float turnSpeed = 1.0f;

    // Use this for initialization
    private void Start()
    {
        nextShot = Time.time + 1 / rateOfFire;
    }

    // Update is called once per frame
    void Update()
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = target.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = turnSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);

        if (projectile && Time.time >= nextShot)
        {
            newProjectile = Instantiate(projectile, transform.position + transform.forward, transform.rotation) as GameObject; ;

            if (!newProjectile.GetComponent<Rigidbody>())
            {
                newProjectile.AddComponent<Rigidbody>();
            }

            newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * power, ForceMode.VelocityChange);

            if (shootSFX)
            {
                AudioSource.PlayClipAtPoint(shootSFX, transform.position);
            }

            nextShot = Time.time + 1 / rateOfFire;
        }
    }
}
