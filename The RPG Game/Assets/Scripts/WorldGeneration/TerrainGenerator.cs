using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int depth = 20;

    public int width = 256;
    public int height = 256;

    public float scale = 20f;

    public float offsetX = 100f;
    public float offsetY = 100f;

    Terrain terrain;
    NavMeshBaker baker;
    ForestGenerator forestGenerator;

    public void Start() {
        terrain = GetComponent<Terrain>();
        baker = GetComponent<NavMeshBaker>();
        forestGenerator = GameObject.Find("Forest").GetComponent<ForestGenerator>();
        width = forestGenerator.forestSize;
        height = forestGenerator.forestSize;

        offsetX = Random.Range(0, 99999f);
        offsetY = Random.Range(0, 99999f);

        terrain.terrainData = GererateTerrain(terrain.terrainData);
        baker.Bake();
        
    }


    public TerrainData GererateTerrain(TerrainData data) {
        data.heightmapResolution = width + 1;

        data.size = new Vector3(width, depth, height);

        data.SetHeights(0, 0, GenerateHeights());
        return data;
    }

    private float[,] GenerateHeights() {
        float[,] heights = new float[width, height];
        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                heights[x, y] = CalculateHeight(x,y);
            }
        }

        return heights;
    }

    private float CalculateHeight(int x, int y) {
        float xCoord = (float)x / width * 20 + offsetX;
        float yCoord = (float)y / height * 20 + offsetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
