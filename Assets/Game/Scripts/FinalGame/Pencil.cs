using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pencil : MonoBehaviour
{
    [SerializeField] private Camera mainCamReference;
    [SerializeField] private int penSize;
    [SerializeField] private DrawingBoard currentDrawingBoard;

    private Color[] colors;
    private Vector2 touchPos;
    void Start()
    {
        colors = Enumerable.Repeat(Color.red, penSize * penSize).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        Draw();
    }

    private void Draw()
    {
        var ray = mainCamReference.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity))
        {
            if (raycastHit.transform.CompareTag("DrawingBoard"))
            {
                currentDrawingBoard = currentDrawingBoard != null ? currentDrawingBoard : raycastHit.transform.GetComponent<DrawingBoard>();

                touchPos = new Vector2(raycastHit.textureCoord.x, raycastHit.textureCoord.y);

                var x = (int)(touchPos.x * currentDrawingBoard.TextureSizeX - (penSize/2));
                var y = (int)(touchPos.y * currentDrawingBoard.TextureSizeY - (penSize/2));

                int drawSizeX = penSize;
                int drawSizeY = penSize;

                if((currentDrawingBoard.TextureSizeX - x) - penSize <= 0)
                {
                    drawSizeX = (int)(currentDrawingBoard.TextureSizeX - x);
                }
                if ((currentDrawingBoard.TextureSizeY - y) - penSize <= 0)
                {
                    drawSizeY = (int)(currentDrawingBoard.TextureSizeY - y);
                }
                if( x < 0)
                {
                    drawSizeX += x;
                    x += -x;
                }
                if (y < 0)
                {
                    drawSizeY += y;
                    y += -y;
                }

                if (x < 0 || x > currentDrawingBoard.TextureSizeX || y < 0 || y > currentDrawingBoard.TextureSizeY) return;
                currentDrawingBoard.Texture.SetPixels(x, y, drawSizeX, drawSizeY, colors);

                currentDrawingBoard.Texture.Apply();
            }
            else
            {
                currentDrawingBoard = null;
            }

        }

        
    }
}
