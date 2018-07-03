using UnityEngine;
using UnityEngine.UI;

public class CubeSpawnerV1 : MonoBehaviour {
    [SerializeField]
    private GameObject cubePrefab;

    [SerializeField]
    private Text cubeCountText;

    private int cubeCount = 0;

    //void Start () {
    //    InvokeRepeating("SpawnCube", 0f, 0.1f);
    //}

    private void FixedUpdate()
    {
        SpawnCube();
    }

    void SpawnCube()
    {
        Instantiate(cubePrefab, transform.position, Quaternion.identity);
        cubeCountText.text = "Instantiated Cubes: " + cubeCount++;
    }
}
