using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCheck : MonoBehaviour
{
    public Material testMat;

    private Material objectMat;
    private GameObject hitObject;

    private void Update()
    {
        if(hitObject != null)
        {
            hitObject.GetComponent<Renderer>().material = objectMat;
            hitObject = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(ray, out hitInfo))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitInfo.distance, Color.yellow);
            Debug.Log("Did Hit");

            if(hitInfo.transform.gameObject.GetComponent<Renderer>().material)
            {
                hitObject = hitInfo.transform.gameObject;
                objectMat = hitObject.GetComponent<Renderer>().material;

                hitObject.GetComponent<Renderer>().material = testMat;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
