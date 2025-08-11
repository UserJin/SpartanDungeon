using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public AudioClip[] footStepClips;
    private AudioSource audioSource;
    private Rigidbody rb;
    public float footStepThrehold;
    public float footStepRate;
    private float footStepTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Mathf.Abs(rb.velocity.y ) < 0.1f)
        {
            if(rb.velocity.magnitude > footStepThrehold)
            {
                if(Time.time - footStepTime > footStepRate)
                {
                    footStepTime = Time.time;
                    audioSource.PlayOneShot(footStepClips[Random.Range(0, footStepClips.Length)]);
                }
            }
        }
    }
}
