using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    //Main thrust of rocket
    [SerializeField] float mainThrust = 10.0f;
    [SerializeField] float rotationThrust = 10.0f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;

    AudioSource audioSource;
    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    public void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
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
        rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBoosterParticles.isPlaying)
        {
            mainBoosterParticles.Play();
        }
    }

    private void StopThrusting()
    {
        mainBoosterParticles.Stop();
        audioSource.Stop();
    }

    

    public void ProcessRotation()
    {
        //Rotate the rocket left
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        //Or Rotate the rocket right
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    private void RotateRocket(Vector3 rotateDirection)
    {
        //We are freezing rotation so we can manually rotate
        this.rigidbody.freezeRotation = true;
        this.transform.Rotate(rotateDirection * rotationThrust * Time.deltaTime);
        //We are unfreezing rotation so the physics system can take over manually rotate
        this.rigidbody.freezeRotation = false;
    }

    private void RotateRight()
    {
        RotateRocket(Vector3.back);
        if (!leftBoosterParticles.isPlaying)
        {
            leftBoosterParticles.Play();
        }
    }

    private void RotateLeft()
    {
        RotateRocket(Vector3.forward);
        if (!rightBoosterParticles.isPlaying)
        {
            rightBoosterParticles.Play();
        }
    }

    private void StopRotating()
    {
        leftBoosterParticles.Stop();
        rightBoosterParticles.Stop();
    }


}
