#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    void Update()
    {
        // Jika block sudah keluar dari screen 
        if (transform.position.y < -6f)
        {
            Destroy(gameObject); // Hancurkan block tersebut
        }
    }
}
#endif