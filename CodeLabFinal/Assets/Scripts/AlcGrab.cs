using Unity.Mathematics;
using UnityEngine;

public class AlcGrab : MonoBehaviour
{
    public bool grabbed;
    public bool released;
    public Vector3 offSet;
    public GameObject hand;
    public Animator anim;
    public Rigidbody2D rb2d;
    public GameObject prefab;
    public bool isDiallogue = false;
    public bool brokenSoundPlayed = false;

    private Vector3 originalPos;
    
    // Start is called before the first frame update
    void Start()
    {
        grabbed = false;
        released = false;
        gameObject.GetComponent<Collider2D>().isTrigger = true;
        originalPos = transform.position;
    }

    // Update is called once per frame
    public void Update()
    {
        if (grabbed)
        {
            hand.GetComponent<HandMotion>().handFull = true;
            transform.position = hand.transform.position + offSet;
            
        }
        
        if (released)
        {
            transform.rotation = Quaternion.Euler(0,0, -33.97f);
        }
    }

    public virtual void Grabbed()
    {
        if (!hand.GetComponent<HandMotion>().handFull)
        {
            grabbed = true;
        }
    }

    public virtual void Release()
    {
        grabbed = false;
        released = true;
        rb2d = gameObject.AddComponent<Rigidbody2D>();
        gameObject.GetComponent<Collider2D>().isTrigger = false;
    }
    
    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Contains("AlcDestroy"))
        {
            Instantiate(prefab, originalPos, Quaternion.identity);
            hand.GetComponent<HandMotion>().handFull = false;
            if (!brokenSoundPlayed)
            {
                GameManager.instance.PlayAudio();
                brokenSoundPlayed = true;
            }
            Destroy(gameObject);
            return;
        }
        
        if (col.gameObject.name.Contains("Table") && released)
        {
            released = false;
            rb2d.mass = 0.01f;
            transform.rotation = Quaternion.Euler(0,  0, -90f);
            if (!brokenSoundPlayed)
            {
                GameManager.instance.PlayAudio();
                brokenSoundPlayed = true;
            }
            AnimTrigger();
        }
    }

    public virtual void AnimTrigger()
    {
        anim = GetComponent<Animator>();
    }

    public virtual void ReturnKeyAndValue()
    {
        
    }
}
