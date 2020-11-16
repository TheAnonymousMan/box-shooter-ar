using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubes : MonoBehaviour
{
    public GameObject CubeObj;

    // Qtd of Cubes to be Spawned
    public int TotalCubes = 10;

    // Time to spawn the Cubes
    public float TimeToSpawn = 1f;

    // hold all cubes on stage
    private GameObject[] Cubes;

    // define if position was set
    private bool PositionSet;

    // Start is called before the first frame update
    void Start()
    {
        Cubes = new GameObject[TotalCubes];

        StartCoroutine(SpawnLoop());
    }

    private bool SetPosition()
    {
        // get the camera position
        Transform loc = transform.parent.transform;

        // set the position 10 units forward from the camera position
        transform.position = loc.forward * 10;
        return true;
    }

    private IEnumerator SpawnLoop()
    {
        StartCoroutine(ChangePosition());

        yield return new WaitForSeconds(0.2f);

        // Spawning the elements
        int i = 0;
        while (i <= (TotalCubes - 1))
        {
            Cubes[i] = SpawnElement();
            i++;
            yield return new WaitForSeconds(Random.Range(TimeToSpawn, TimeToSpawn * 3));
        }
    }

    private IEnumerator ChangePosition()
    {
        yield return new WaitForSeconds(0.2f);
        // Define the Spawn position only once
        if (!PositionSet)
        {
            SetPosition();
        }
    }

    // Spawn a cube
    private GameObject SpawnElement()
    {
        // spawn the element on a random position, inside a imaginary sphere
        GameObject cube = Instantiate(CubeObj, (Random.insideUnitSphere * 4) + transform.position, transform.rotation) as GameObject;
        // define a random scale for the cube
        float scale = Random.Range(0.5f, 2f);
        // change the cube scale
        cube.transform.localScale = new Vector3(scale, scale, scale);
        return cube;
    }
}
