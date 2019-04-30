using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rB;
    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();

    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
            print("Rotating left");
        }else if(Input.GetKey(KeyCode.D)){
            transform.Rotate(-Vector3.forward);
            print("Rotating right");
        }
        if (Input.GetKey(KeyCode.Space))
        {
            rB.AddRelativeForce(Vector3.right);
            print("Sağa döndü");
        }
    }
}
