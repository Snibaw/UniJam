using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCube : MonoBehaviour
{
    [SerializeField] private float bounceForce = 10f;

    public bool isActive = false;
    public int cubeMode;
    private GameObject player;
    public AnimationCurve rotationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f); // Courbe d'animation par défaut
    public float rotationDuration = 1f; // Durée totale de la rotation

    void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("si ça marche pas c tro");
        if (!isActive) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            print("slt la zone");
            if (cubeMode==0)
            {
                Quaternion targetRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 180f);
                Physics.gravity = new Vector3(0, 10.0F, 0);
                StartCoroutine(SmoothRotate(player.transform, targetRotation, rotationDuration));
            }
            else if (cubeMode==1)
            {
                Physics.gravity = new Vector3(0, -10.0F, 0);
                Quaternion targetRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0f);
                StartCoroutine(SmoothRotate(player.transform, targetRotation, rotationDuration));
            }
            else if (cubeMode==2)
            {
                Physics.gravity = new Vector3(10f, 0, 0);
                Quaternion targetRotation = Quaternion.Euler(0f, 0f, 90f);
                StartCoroutine(SmoothRotate(player.transform, targetRotation, rotationDuration));
            }
            else if (cubeMode==3)
            {
                Physics.gravity = new Vector3(-10f, 0, 0);
                Quaternion targetRotation = Quaternion.Euler(0f, 0f, -90f);
                StartCoroutine(SmoothRotate(player.transform, targetRotation, rotationDuration));
            }
        }
    }


    
    IEnumerator SmoothRotate(Transform targetTransform, Quaternion targetRotation, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float curveValue = rotationCurve.Evaluate(t);
            targetTransform.rotation = Quaternion.Slerp(targetTransform.rotation, targetRotation, curveValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Assure que la rotation soit exacte à la fin de la courbe
        targetTransform.rotation = targetRotation;
    }
}