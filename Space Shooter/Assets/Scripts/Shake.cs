using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public static Shake instance;
    public float duration = 1f;
    public AnimationCurve curve; 
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void Shaking()
    {
        StartCoroutine(ShakingRoutine());
    }

    IEnumerator ShakingRoutine()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + (Random.insideUnitSphere * strength);
            yield return null;
        }
        transform.position = startPosition;
    }
}
