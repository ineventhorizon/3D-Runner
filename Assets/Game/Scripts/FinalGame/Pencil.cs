using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pencil : MonoBehaviour
{
    [SerializeReference] private Camera mainCamReference;
    [SerializeField] private int penSize;
    [SerializeField] private DrawingBoard currentDrawingBoard;

    private Color[] colors;
    private Color[] currentColors;
    private Vector2 touchPos;
    private bool winTrigger = false;
    int count = 0;
    int percentage = 0;
    void Start()
    {
        colors = Enumerable.Repeat(Color.red, penSize * penSize).ToArray();
        currentColors = Enumerable.Repeat(Color.white, penSize * penSize).ToArray();
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameState.FINAL) return;
        Draw();
    }
    private void Draw()
    {
        var ray = mainCamReference.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(mainCamReference.transform.position , ray.direction*30f, Color.green, 0f);
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
                currentColors =  currentDrawingBoard.Texture.GetPixels(x, y, drawSizeX, drawSizeY);
                count += currentColors.Count(o => o != Color.red);
                percentage = (int)((count * 100) / (currentDrawingBoard.TextureSizeX * currentDrawingBoard.TextureSizeX));
                UIManager.Instance.FinalPanel.UpdatePaintProgress(percentage);
                currentDrawingBoard.Texture.SetPixels(x, y, drawSizeX, drawSizeY, colors);
                currentDrawingBoard.Texture.Apply();

                if (percentage >= 70 && !winTrigger)
                {
                    UIManager.Instance.WinPanel.EnablePanel();
                    winTrigger = true;
                }

                //Debug.Log((count*100)/(currentDrawingBoard.TextureSizeX*currentDrawingBoard.TextureSizeX));
            }
            else
            {
                currentDrawingBoard = null;
            }

        }

        
    }
}
