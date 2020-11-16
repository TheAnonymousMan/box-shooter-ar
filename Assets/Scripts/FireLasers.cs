using UnityEngine;
using System.Collections;

public class FireLasers : MonoBehaviour
{

    public float FireRate = .5f;
    public float FireRange = 50f;
    public float HitForce = 100f;
    public int LaserDamage = 100;

    // Line render that will represent the Laser
    private LineRenderer LaserLine;

    // Define if laser line is showing
    private bool LaserLineEnabled;

    // Time that the Laser lines shows on screen
    private WaitForSeconds LaserDuration = new WaitForSeconds(0.05f);

    // time of the until the next fire
    private float NextFire;

    // Use this for initialization
    void Start()
    {
        // getting the Line Renderer
        LaserLine = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > NextFire)
        {
            Fire();
        }
    }

    private void Fire()
    {
        // Get ARCamera Transform
        Transform cam = Camera.main.transform;

        // Define the time of the next fire
        NextFire = Time.time + FireRate;

        // Set the origin of the RayCast
        Vector3 rayOrigin = cam.position;

        // Set the origin position of the Laser Line
        // It will always 10 units down from the ARCamera
        // We adopted this logic for simplicity
        LaserLine.SetPosition(0, transform.up * -10f);

        // Hold the Hit information
        RaycastHit hit;

        // Checks if the RayCast hit something
        if (Physics.Raycast(rayOrigin, cam.forward, out hit, FireRange))
        {

            // Set the end of the Laser Line to the object hit
            LaserLine.SetPosition(1, hit.point);

            // Get the CubeBehavior script to apply damage to target
            CubeBehaviour cubeCtr = hit.collider.GetComponent<CubeBehaviour>();
            if (cubeCtr != null)
            {
                if (hit.rigidbody != null)
                {
                    // apply force to the target
                    hit.rigidbody.AddForce(-hit.normal * HitForce);
                    // apply damage the target
                    cubeCtr.Hit(LaserDamage);
                }
            }

        }
        else
        {
            // Set the enfo of the laser line to be forward the camera
            // using the Laser range
            LaserLine.SetPosition(1, cam.forward * FireRange);
        }

        StartCoroutine(LaserFx());
    }

    private IEnumerator LaserFx()
    {
        LaserLine.enabled = true;

        // Way for a specific time to remove the LineRenderer
        yield return LaserDuration;
        LaserLine.enabled = false;
    }
}