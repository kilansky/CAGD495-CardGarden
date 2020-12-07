using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandController : SingletonPattern<CardHandController>
{
    private Vector3 curveStart = new Vector3(2f, -0.7f, 0), curveEnd = new Vector3(-2f, -0.7f, 0);
    private Vector2 handOffset = new Vector2(0, -0.3f), handSize = new Vector2(9, 1.7f);
    private Rect handBounds;
    private Vector3 a, b, c; // Used for shaping hand into curve
    public float cardMoveSpeed = 10f;

    private void Start()
    {
        UpdateCardIndexes();
    }

    public void UpdateCardIndexes()
    {
        //Set index and sorting order of each card in the hand zone
        for (int i = 0; i < transform.childCount; i++)
        {        
            transform.GetChild(i).GetComponent<CardSelector>().cardInHandIndex = i;
            transform.GetChild(i).GetComponent<Canvas>().sortingOrder = i;
        }
    }

    //Update the size of the Hand's curve based on the number of cards in the Hand
    public void Update()
    {
        int cardsInHand = transform.childCount;
        curveStart.x = 1 + cardsInHand * 0.7f;
        curveEnd.x = -1 - cardsInHand * 0.7f;
        curveStart.y = 0f - cardsInHand * 0.15f;
        curveEnd.y = 0f - cardsInHand * 0.15f;

        a = transform.TransformPoint(curveStart);
        c = transform.TransformPoint(curveEnd);
    }

    public void InitializeHandCurve()
    {
        a = transform.TransformPoint(curveStart);
        b = transform.position;
        c = transform.TransformPoint(curveEnd);
        handBounds = new Rect((handOffset - handSize / 2), handSize);
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.blue;

        Gizmos.DrawSphere(curveStart, 0.03f);
        Gizmos.DrawSphere(curveEnd, 0.03f);

        Vector3 p1 = curveStart;
        for (int i = 0; i < 20; i++)
        {
            float t = (i + 1) / 20f;
            Vector3 p2 = GetGizmoCurvePoint(curveStart, Vector3.zero, curveEnd, t);
            Gizmos.DrawLine(p1, p2);
            p1 = p2;
        }

        Gizmos.DrawWireCube(handOffset, handSize);
    }

    // Obtains a point along a curve based on 3 points. Equal to Lerp(Lerp(a, b, t), Lerp(b, c, t), t).
    public Vector3 GetCurvePoint(float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return (oneMinusT * oneMinusT * a) + (2f * oneMinusT * t * b) + (t * t * c);
    }

    // Obtains a point along a curve based on 3 points. Equal to Lerp(Lerp(a, b, t), Lerp(b, c, t), t).
    public Vector3 GetGizmoCurvePoint(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return (oneMinusT * oneMinusT * a) + (2f * oneMinusT * t * b) + (t * t * c);
    }

    // Obtains the derivative of the curve (tangent)
    public Vector3 GetCurveTangent(float t)
    {
        return 2f * (1f - t) * (b - a) + 2f * t * (c - b);
    }

    // Obtains a direction perpendicular to the tangent of the curve
    public Vector3 GetCurveNormal(float t)
    {
        Vector3 tangent = GetCurveTangent(t);
        return Vector3.Cross(tangent, Vector3.forward);
    }
}
