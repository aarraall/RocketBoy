using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rB;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();

    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rB.AddRelativeForce(Vector3.up);
            print("Rotating up");
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }else
        {
            audioSource.Stop();
        }
        
        
        if(Input.GetKey(KeyCode.A)){
            transform.Rotate(Vector3.forward);
            print("Rotating left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
            print("Rotating right");
        }
    }
}
