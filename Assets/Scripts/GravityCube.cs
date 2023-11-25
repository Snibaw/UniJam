using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCube : MonoBehaviour
{
    public bool isActive = false;
    public int cubeMode;
    private GameObject player;
    public AnimationCurve rotationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f); // Courbe d'animation par défaut
    public float rotationDuration = 10f; // Durée totale de la rotation

    private void OnCollisionEnter(Collision collision)
    {
        if (!isActive) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.transform.position.y > transform.position.y)
            {
                cubeMode = 0;
            }
            else if (collision.gameObject.transform.position.y < transform.position.y)
            {
                cubeMode = 1;
            }
            
            
            if (cubeMode==0)
            {
                Quaternion targetRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 180f);
                Physics.gravity = new Vector3(0, 10.0F, 0);
                StartCoroutine(SmoothRotate(collision.gameObject.GetComponent<PlayerMovement>().camera.transform, targetRotation, rotationDuration));
            }
            else if (cubeMode==1)
            {
                Physics.gravity = new Vector3(0, -10.0F, 0);
                Quaternion targetRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0f);
                StartCoroutine(SmoothRotate(collision.gameObject.GetComponent<PlayerMovement>().camera.transform, targetRotation, rotationDuration));
            }
            else if (cubeMode==2)
            {
                Physics.gravity = new Vector3(10f, 0, 0);
                Quaternion targetRotation = Quaternion.Euler(0f, 0f, 90f);
                StartCoroutine(SmoothRotate(collision.gameObject.GetComponent<PlayerMovement>().camera.transform, targetRotation, rotationDuration));
            }
            else if (cubeMode==3)
            {
                Physics.gravity = new Vector3(-10f, 0, 0);
                Quaternion targetRotation = Quaternion.Euler(0f, 0f, -90f);
                StartCoroutine(SmoothRotate(collision.gameObject.GetComponent<PlayerMovement>().camera.transform, targetRotation, rotationDuration));
            }
        }
    }


    
    IEnumerator SmoothRotate(Transform targetTransform, Quaternion targetRotation, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float curveValue = rotationCurve.Evaluate(t)/10;
            targetTransform.rotation = Quaternion.Slerp(targetTransform.rotation, targetRotation, curveValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Assure que la rotation soit exacte à la fin de la courbe
        targetTransform.rotation = targetRotation;
    }
}