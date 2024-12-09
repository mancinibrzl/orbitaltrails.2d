using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public ParticleSystem particleSystem;
    public float timeToHide = 3;
    public GameObject graphicItem;
    public Collider2D[] colliders;

    [Header("Sounds")]
    public AudioSource audioSource;



    private void Awake()
    {
        //if (particleSystem != null) particleSystem.transform.SetParent(null);
        colliders = GetComponentsInChildren<Collider2D>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }
    protected virtual void Collect() 
    {
        if(graphicItem != null) graphicItem.SetActive(false);
        if(colliders != null) 
        {
             foreach(var collider in colliders) 
            
           {
                collider.enabled = false;
           }
        Invoke("HideObject", timeToHide);
        OnCollect();


    }

    }

    private void HideObject()
    {
        gameObject.SetActive(false);

    }

    protected virtual void OnCollect() 
    {
        if (particleSystem != null) particleSystem.Play();
        if (audioSource != null) audioSource.Play();
    }




}