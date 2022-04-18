using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingBoard : MonoBehaviour
{
    [SerializeField] public Texture2D Texture;
    private Vector2 textureSize = new Vector2(512, 512);
    public float TextureSizeX => textureSize.x;
    public float TextureSizeY => textureSize.y;
    // Start is called before the first frame update
    void Start()
    {
        var r = GetComponent<Renderer>();
        Texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        r.material.mainTexture = Texture;
    }   
}
