using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)]float movementFactor;

    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) return;
        float cycles = Time.time / period; //Constantly growing over time
        const float tau = 2 * Mathf.PI; //Constant value of 6.283
        float rawSineWave = Mathf.Sin(cycles * tau); //going from -1 to 1

        movementFactor = (rawSineWave + 1f) / 2; //Recalucate from 0 to 1 so its clearer

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
