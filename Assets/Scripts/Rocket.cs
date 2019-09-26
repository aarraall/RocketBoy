using System;
using UnityEngine;
using UnityEngine.SceneManagement;

#pragma warning disable 1696 // #pragma yönergesinden sonra tek satırlı açıklama veya satır sonu beklenir
#pragma warning disable 0649  

public class Rocket : MonoBehaviour
#pragma warning restore CS1696 // #pragma yönergesinden sonra tek satırlı açıklama veya satır sonu beklenir
{
    
    Rigidbody rB;
    AudioSource audioSource;
    private int currentIndex;
    int finalChapter; 
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;   
    [SerializeField] float levelLoadDelay;
    //[SerializeField] float brakeThrust = 100f;

    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] AudioClip OnSuccessSFX;
    [SerializeField] AudioClip OnDeathSFX;

    [SerializeField] ParticleSystem mainEngineGFX;
    [SerializeField] ParticleSystem OnSuccessGFX;
    [SerializeField] ParticleSystem OnDeathGFX;

    [SerializeField] bool collisionsDisabled = false;

    enum State { Alive, Dying, Transcending };
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        currentIndex = SceneManager.GetActiveScene().buildIndex;
        finalChapter = SceneManager.sceneCountInBuildSettings - 1;

        ShowLevel();
    }
    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            if (Debug.isDebugBuild)
            {
                RespondToDebugKeys();
            }
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadLevel();
        }else if(Input.GetKeyDown(KeyCode.C)){
            collisionsDisabled = !collisionsDisabled;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || collisionsDisabled)
        {
            return;
        }      
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    break;
                case "Finish":
                    SuccessSequence();
                    break;
                default:
                    DeathSequence();
                    break;
            }          
        
    }
    
    private void ShowLevel()
    {
        int curLevel = currentIndex + 1;
        print(curLevel + ". Level");
    }

    private void SuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(OnSuccessSFX);
        if (mainEngineGFX.isPlaying)
        {
            mainEngineGFX.Stop();
        }
        OnSuccessGFX.Play();
        Invoke("LoadLevel", levelLoadDelay);
    }

    private void DeathSequence()
    {
        print("Spaceship blown up!");
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(OnDeathSFX);
        if (mainEngineGFX.isPlaying)
        {
            mainEngineGFX.Stop();
        }
        OnDeathGFX.Play();
        Invoke("LoadLevel", levelLoadDelay);
    }

    private void LoadLevel()
    {
        if (state == State.Dying)
        {
            if (currentIndex == 0) { FirstLevel();}
            else { SceneManager.LoadScene(currentIndex - 1); }
        }
        else {
            if (currentIndex == finalChapter) { SceneManager.LoadScene(currentIndex-currentIndex); }
            else { SceneManager.LoadScene(currentIndex + 1); }

        }
    }

    private void FirstLevel()
    {
        SceneManager.LoadScene(currentIndex-currentIndex);
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            mainEngineGFX.Stop();
        }
    }

    private void ApplyThrust()
    {
        rB.AddRelativeForce(Vector3.up * mainThrust);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineSFX);
        }
        mainEngineGFX.Play();
    }

    /*private void RespondToBrake()
    {
        if (Input.GetKey(KeyCode.S))
        {
            ApplyBrake();
        }
    }*/

    /*private void ApplyBrake()
    {
        rB.AddRelativeForce(Vector3.down * brakeThrust);
    }*/

    private void RespondToRotateInput()
    {
        ApplyRotation();
    }

    private void ApplyRotation()
    {
        float rotateThisFrame = rcsThrust * Time.deltaTime;

        rB.freezeRotation = true;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotateThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotateThisFrame);
        }
        rB.freezeRotation = false;
    }
}
