using Unity.VisualScripting;
using UnityEngine;

public class T1Grab : AlcGrab
{
    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void Grabbed()
    {
        base.Grabbed();
        Debug.Log("Manhattan Grabbed");
    }

    public override void Release()
    {
        base.Release();
        Debug.Log("Manhattan Released");
    }

    public override void AnimTrigger()
    {
        base.AnimTrigger();
        anim.SetTrigger("T1Break");
    }

    public override void ReturnKeyAndValue()
    {
        GameManager.instance.drunkKey = "Manhattan";
        GameManager.instance.drunkValue = 15;
    }
}
