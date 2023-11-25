using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTest : MonoBehaviour

{
    public KeyCode gravityKey001 = KeyCode.G;
    public KeyCode gravityKey0m10 = KeyCode.H;
    public KeyCode gravityKey100 = KeyCode.J;
    public KeyCode gravityKeym100 = KeyCode.K;
    [SerializeField] private GameObject player;
    public AnimationCurve rotationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f); // Courbe d'animation par défaut
    public float rotationDuration = 1f; // Durée totale de la rotation



    private bool isGravityInverted = false;

    void Update()
    {
        if (Input.GetKeyDown(gravityKey001))
        {
            Gravity180();
        }
        else if (Input.GetKeyDown(gravityKey0m10))
        {
            Gravity0m10();
        }
        else if (Input.GetKeyDown(gravityKey100))
        {
            Gravity100();
        }
        else if (Input.GetKeyDown(gravityKeym100))
        {
            Gravitym100();
        }
    }

    void InvertGravity()
    {
        // Inversion de la gravité globale
        Physics.gravity = -Physics.gravity;

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

    void Gravity180()
    {
        // Inversion de la gravité globale
        Physics.gravity = new Vector3(0, 10.0F, 0);

        // Déclenche le changement de direction de la gravité pour le joueur
        isGravityInverted = !isGravityInverted;

        if (player != null)
        {
            // Calcule la rotation cible en fonction de la gravité actuelle
            //Quaternion targetRotation = Quaternion.FromToRotation(player.transform.up, -Physics.gravity) * player.transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, 180f);

            // Lisse la transition en utilisant la courbe d'animation
            StartCoroutine(SmoothRotate(player.transform, targetRotation, rotationDuration));
        }
        else
        {
            Debug.LogError("Player object reference is not set.l");
        }
    }

    void Gravity0m10()
    {
        // Inversion de la gravité globale
        Physics.gravity = new Vector3(0, -10.0F, 0);

        // Déclenche le changement de direction de la gravité pour le joueur
        isGravityInverted = !isGravityInverted;

        if (player != null)
        {
            // Calcule la rotation cible en fonction de la gravité actuelle
            //Quaternion targetRotation = Quaternion.FromToRotation(player.transform.up, -Physics.gravity) * player.transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f);

            // Lisse la transition en utilisant la courbe d'animation
            StartCoroutine(SmoothRotate(player.transform, targetRotation, rotationDuration));
        }
        else
        {
            Debug.LogError("Player object reference is not set.l");
        }
    }

    void Gravity100()
    {
        // Inversion de la gravité globale
        Physics.gravity = new Vector3(10f, 0, 0);

        // Déclenche le changement de direction de la gravité pour le joueur
        isGravityInverted = !isGravityInverted;

        if (player != null)
        {
            // Calcule la rotation cible en fonction de la gravité actuelle
            //Quaternion targetRotation = Quaternion.FromToRotation(player.transform.up, -Physics.gravity) * player.transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, 90f);

            // Lisse la transition en utilisant la courbe d'animation
            StartCoroutine(SmoothRotate(player.transform, targetRotation, rotationDuration));
        }
        else
        {
            Debug.LogError("Player object reference is not set.l");
        }
    }

    void Gravitym100()
    {
        // Inversion de la gravité globale
        Physics.gravity = new Vector3(-10f, 0, 0);

        // Déclenche le changement de direction de la gravité pour le joueur
        isGravityInverted = !isGravityInverted;

        if (player != null)
        {
            // Calcule la rotation cible en fonction de la gravité actuelle
            //Quaternion targetRotation = Quaternion.FromToRotation(player.transform.up, -Physics.gravity) * player.transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, -90f);

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