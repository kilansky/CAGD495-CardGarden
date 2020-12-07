using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public Transform p0;
    public Transform p1;
    public Transform p2;

    private LineRenderer lineRenderer;
    private Vector3 hitTilePos;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        //p0.localPosition = new Vector3(0.96f, 0.25f, 4.76f);

    }

    void Update()
    {
        //Perform a raycast and draw the bezier curve if a card has been selected and the game is NOT paused
        if (CardSelector.cardSelected && CardSelector.cardSelected.GetComponent<CardSelector>().canPlay && !MenuUI.Instance.isPaused)
        {
            MouseRaycast();
            lineRenderer.enabled = true;
            DrawQuadraticBezierCurve(p0.position, p1.position, p2.position);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    private void MouseRaycast()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        //Check if the mouse is above the Hand Zone & Shoot a raycast at the mouse position
        if (Physics.Raycast(mouseRay, out hitInfo))
        {
            //Check if the raycast hit a tile
            if (hitInfo.transform.gameObject && hitInfo.transform.gameObject.GetComponent<Tile>())
            {
                hitTilePos = hitInfo.transform.parent.transform.position;
                p2.position = hitTilePos;
            }
        }
    }

    void DrawQuadraticBezierCurve(Vector3 point0, Vector3 point1, Vector3 point2)
    {
        lineRenderer.positionCount = 200;
        float t = 0f;
        Vector3 B = new Vector3(0, 0, 0);
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            B = (1 - t) * (1 - t) * point0 + 2 * (1 - t) * t * point1 + t * t * point2;
            lineRenderer.SetPosition(i, B);
            t += (1 / (float)lineRenderer.positionCount);
        }
    }
}
