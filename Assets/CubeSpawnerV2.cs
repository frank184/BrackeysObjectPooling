using UnityEngine;
using UnityEngine.UI;

public class CubeSpawnerV2 : MonoBehaviour {
    [SerializeField]
    private Text cubeCountText;

    private ObjectPooler objectPooler = ObjectPooler.singleton;

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
        ObjectPooler.singleton.SpawnFromPool("Cube", transform.position, Quaternion.identity);
        cubeCountText.text = "Spawned Cubes: " + cubeCount++;
    }
}
