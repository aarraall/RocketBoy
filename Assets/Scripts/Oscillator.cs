
using UnityEngine;
[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(0,10f,0);
    [Range(0, 1)] [SerializeField] float movementFactor;
    [SerializeField] float period = 2f;

    Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //TODO protect against period is zero
        if (period >= Mathf.Epsilon) {

            float cycles = Time.time / period; // grows continually from zero

            const float tau = Mathf.PI * 2; // around 6.28 
            float rawSinWave = Mathf.Sin(cycles * tau);
            movementFactor = rawSinWave / 2f + 0.5f;
            Vector3 offset = movementVector * movementFactor;
            transform.position = startingPos + offset;
        }
        
    }
}
