 using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
  [SerializeField] InputAction thrust;
  [SerializeField] InputAction rotation;
  [SerializeField] float thrustStrength = 10f;
  [SerializeField] float rotationStrength =100f;
  [SerializeField] AudioClip mainEngineSFX; 
  [SerializeField] ParticleSystem mainEngineParticles;
  [SerializeField] ParticleSystem rightThrustParticles;
  [SerializeField] ParticleSystem LeftThurstParticles;
  
   
  Rigidbody rb;
  AudioSource audioSource;

  private void Start() 
  {
   rb = GetComponent<Rigidbody>();  
   audioSource = GetComponent<AudioSource>();
  }
  private void OnEnable() 
  {
      thrust.Enable();  
      rotation.Enable();
  }

  private void FixedUpdate()
    {
        ProccesThurst();
        ProccesRotation();
    }

    private void ProccesThurst()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();

        }
        else
        {
            StopThrusting();
        }
    }
     private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineSFX);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

   

    private void ProccesRotation()
{
   float rotationInput = rotation.ReadValue<float>();
   
   if(rotationInput < 0)
        {
            RotateRight();
        }
        else if(rotationInput > 0)
        {
            RotateLeft();
        }
        else
        {
            StopRotating();
        }
    }

  

 private void RotateRight()
    {
        ApplyRotation(+rotationStrength);
        if (!rightThrustParticles.isPlaying)
        {
            LeftThurstParticles.Stop();
            rightThrustParticles.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(-rotationStrength);
        if (!LeftThurstParticles.isPlaying)
        {
            rightThrustParticles.Stop();
            LeftThurstParticles.Play();
        }
    }

     private void StopRotating()
    {
        LeftThurstParticles.Stop();
        rightThrustParticles.Stop();
    }


    private void ApplyRotation(float rotationThisFrame)
{
    rb.freezeRotation = true;
    transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
    rb.freezeRotation = false;
}


}
