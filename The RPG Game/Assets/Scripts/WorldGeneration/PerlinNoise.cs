using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    public int width = 256;
    public int height = 256;

    public float scale = 20f;

    public float offsetX = 100f;
    public float offsetY = 100f;

    private Renderer rend;

    public void Start() {
        rend = GetComponent<Renderer>();

        offsetX = Random.Range(0, 99999f);
        offsetY = Random.Range(0, 99999f);
    }

    public void Update() {
        rend.material.mainTexture = GenerateTexture();
    }

    private Texture2D GenerateTexture() {
        Texture2D texture = new Texture2D(width, height);

        //Generate a perslin noise map for texture

        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                Color color = CalculateColor(x,y);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }

    private Color CalculateColor(int x, int y) {

        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);

        Color color = new Color(sample, sample, sample);
        return color;
    }
}
