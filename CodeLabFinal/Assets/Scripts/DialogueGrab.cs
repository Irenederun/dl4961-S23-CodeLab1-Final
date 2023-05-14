using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueGrab : AlcGrab
{
    public GameObject fadeout;
    private void Awake()
    {
        prefab = null;
    }

    void Update()
    {
        if (grabbed)
        {
            isDiallogue = true;
            transform.position = hand.transform.position + offSet;
        }
        
        if (released)
        {
            transform.rotation = Quaternion.Euler(0,0, -33.97f);
        }
    }

    public override void Grabbed()
    {
        base.Grabbed();
        Debug.Log("Dialogue Box Grabbed");
    }
    
    public override void Release()
    {
        base.Release();
        Debug.Log("Dialogue Box Released");
    }

    public override void OnCollisionEnter2D(Collision2D col)
    {
       if (released)
       {
           released = false;
           rb2d.mass = 0.01f;
           transform.rotation = Quaternion.Euler(0,  0, 90f);
           if (!brokenSoundPlayed)
           {
               GameManager.instance.PlayAudio();
               brokenSoundPlayed = true;
           }
           AnimTrigger();
       }
    }
    
    public override void AnimTrigger()
    {
        base.AnimTrigger();
        anim.SetTrigger("DiaBreak");
        Invoke("FadeOut", 3f);
    }

    public override void ReturnKeyAndValue()
    {
        GameManager.instance.drunkKey = "Dialogue";
        GameManager.instance.drunkValue = 0;
    }

    void FadeOut()
    {
        Instantiate(fadeout);
    }
}
