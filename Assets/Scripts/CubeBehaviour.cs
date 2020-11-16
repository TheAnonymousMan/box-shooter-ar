using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    // Cube's Max/Min scale
    public float ScaleMax = 2f;
    public float ScaleMin = 0.5f;

    // Orbit max Speed
    public float OrbitMaxSpeed = 30f;

    // Orbit speed
    private float OrbitSpeed;

    // Anchor point for the Cube to rotate around
    private Transform OrbitAnchor;

    // Orbit direction
    private Vector3 OrbitDirection;

    // Max Cube Scale
    private Vector3 CubeMaxScale;

    // Growing Speed
    public float GrowingSpeed = 10f;
    private bool IsCubeScaled = false;

    public GameObject CenterofWorld;

    public int CubeHealth = 100;

    // Define if the Cube is Alive
    private bool IsAlive = true;

    void Start()
    {
        CubeSettings();
    }

    // Update is called once per frame
    void Update()
    {
        // makes the cube orbit and rotate
        RotateCube();

        // scale cube if needed
        if (!IsCubeScaled)
            ScaleObj();
    }

    // Set initial cube settings
    private void CubeSettings()
    {
        // defining the anchor point as the main camera
        OrbitAnchor = CenterofWorld.transform;

        // defining the orbit direction
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        OrbitDirection = new Vector3(x, y, z);

        // defining speed
        OrbitSpeed = Random.Range(5f, OrbitMaxSpeed);

        // defining scale
        float scale = Random.Range(ScaleMin, ScaleMax);
        CubeMaxScale = new Vector3(scale, scale, scale);

        // set cube scale to 0, to grow it lates
        transform.localScale = Vector3.zero;
    }

    private void RotateCube()
    {
        // rotate cube around camera
        transform.RotateAround(
            OrbitAnchor.position, OrbitDirection, OrbitSpeed * Time.deltaTime);

        // rotating around its axis
        transform.Rotate(OrbitDirection * 30 * Time.deltaTime);
    }

    // Scale object from 0 to 1
    private void ScaleObj()
    {

        // growing obj
        if (transform.localScale != CubeMaxScale)
            transform.localScale = Vector3.Lerp(transform.localScale, CubeMaxScale, Time.deltaTime * GrowingSpeed);
        else
            IsCubeScaled = true;
    }

   

    // Cube got Hit
    // return 'false' when cube was destroyed
    public bool Hit(int hitDamage)
    {
        CubeHealth -= hitDamage;
        if (CubeHealth >= 0 && IsAlive)
        {
            StartCoroutine(DestroyCube());
            return true;
        }
        return false;
    }

    // Destroy Cube
    private IEnumerator DestroyCube()
    {
        IsAlive = false;

        // Make the cube desappear
        GetComponent<Renderer>().enabled = false;

        // we'll wait some time before destroying the element
        // this is usefull when using some kind of effect
        // like a explosion sound effect.
        // in that case we could use the sound lenght as waiting time
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
