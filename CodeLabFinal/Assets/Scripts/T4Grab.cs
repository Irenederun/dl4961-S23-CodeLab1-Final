using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4Grab : AlcGrab
{
    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void Grabbed()
    {
        base.Grabbed();
        Debug.Log("TequilaSunrise Grabbed");
    }
    
    public override void Release()
    {
        base.Release();
        Debug.Log("TequilaSunrise Released");
    }
    
    public override void AnimTrigger()
    {
        base.AnimTrigger();
        anim.SetTrigger("T4Break");
    }
    
    public override void ReturnKeyAndValue()
    {
        GameManager.instance.drunkKey = "Tequila Sunrise";
        GameManager.instance.drunkValue = 12;
    }
}
