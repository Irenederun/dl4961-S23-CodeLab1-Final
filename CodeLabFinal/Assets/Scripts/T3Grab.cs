using UnityEngine;

public class T3Grab : AlcGrab
{
    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void Grabbed()
    {
        base.Grabbed();
        Debug.Log("Margaritta Grabbed");
    }
    
    public override void Release()
    {
        base.Release();
        Debug.Log("Margaritta Released");
    }
    
    public override void AnimTrigger()
    {
        base.AnimTrigger();
        anim.SetTrigger("T3Break");
    }
    
    public override void ReturnKeyAndValue()
    {
        GameManager.instance.drunkKey = "Margaritta";
        GameManager.instance.drunkValue = 10;
    }
}
