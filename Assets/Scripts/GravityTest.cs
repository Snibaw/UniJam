using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTest : MonoBehaviour

{
    public KeyCode gravityKey001 = KeyCode.G;
    public KeyCode gravityKey0m10 = KeyCode.H;
    public KeyCode gravityKey100 = KeyCode.J;
    public KeyCode gravityKeym100 = KeyCode.K;
    private GameObject player;
    public AnimationCurve rotationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f); // Courbe d'animation par défaut
    public float rotationDuration = 1f; // Durée totale de la rotation



    private bool isGravityInverted = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(gravityKey001))
        {
            ChangeGravity(Vector3.up * 9.81f);
            player.GetComponent<PlayerMovement>().ChangeControlDependingOnGravity();
        }
        else if (Input.GetKeyDown(gravityKey0m10))
        {
            ChangeGravity(Vector3.down * 9.81f);
            player.GetComponent<PlayerMovement>().ChangeControlDependingOnGravity();
        }
        else if (Input.GetKeyDown(gravityKey100))
        {
            ChangeGravity(Vector3.left * 9.81f);
            player.GetComponent<PlayerMovement>().ChangeControlDependingOnGravity();
        }
        else if (Input.GetKeyDown(gravityKeym100))
        {
            ChangeGravity(Vector3.right * 9.81f);
            player.GetComponent<PlayerMovement>().ChangeControlDependingOnGravity();
        }
    }
    

    void ChangeGravity(Vector3 gravityDirection)
    {
        // Inversion de la gravité globale
        Physics.gravity = gravityDirection;

        // Déclenche le changement de direction de la gravité pour le joueur
        isGravityInverted = !isGravityInverted;

        if (player != null)
        {
            // Calcule la rotation cible en fonction de la gravité actuelle
            Quaternion targetRotation = Quaternion.FromToRotation(player.transform.up, -Physics.gravity) * player.transform.rotation;

            // Lisse la transition en utilisant la courbe d'animation
            StartCoroutine(SmoothRotate(player.transform, targetRotation, rotationDuration));
        }
        else
        {
            Debug.LogError("Player object reference is not set.l");
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