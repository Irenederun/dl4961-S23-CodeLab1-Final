using UnityEngine;

public class T2Grab : AlcGrab
{
    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void Grabbed()
    {
        base.Grabbed();
        Debug.Log("Bellini Grabbed");
    }
    
    public override void Release()
    {
        base.Release();
        Debug.Log("Bellini Released");
    }
    
    public override void AnimTrigger()
    {
        base.AnimTrigger();
        anim.SetTrigger("T2Break");
    }
    
    public override void ReturnKeyAndValue()
    {
        GameManager.instance.drunkKey = "Bellini";
        GameManager.instance.drunkValue = 4;
    }
}
