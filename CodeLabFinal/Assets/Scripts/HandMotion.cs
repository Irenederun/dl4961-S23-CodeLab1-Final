using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class HandMotion : MonoBehaviour
{
    public GameObject glassOfAlc;
    public float speed = 1;
    public bool handFull;
    private AlcGrab glassOfAlcScript;
    private Vector3 originalPos;
    public AudioSource sipSound;
    public float drunkAmount;
    private float offSetX;
    private float offSetY;
        
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        handFull = false;
        glassOfAlc = null;
        glassOfAlcScript = null;
        GameManager.instance.handScript = this;
        drunkAmount = 0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        }
        
        ResetOffSet();
        transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Contains("Destroy") && handFull)
        {
            gameObject.SetActive(false);
            if (!sipSound.isPlaying && !glassOfAlcScript.isDiallogue)
            {
                sipSound.Play(0);
            }

            if (!glassOfAlcScript.isDiallogue)
            {
                Invoke("Release", 1.5f);
                Invoke("Drop", 3f);
            }

            if (glassOfAlcScript.isDiallogue)
            {
                Release();
                Drop();
            }
            return;
        }
        
        if (col.gameObject.CompareTag("Alc"))
        {
            if (!handFull)
            {
                glassOfAlc = col.gameObject;
                glassOfAlcScript = glassOfAlc.GetComponent<AlcGrab>();
                glassOfAlcScript.hand = gameObject;
                glassOfAlcScript.Grabbed();
                glassOfAlcScript.ReturnKeyAndValue();
                handFull = true;
            }
        }
    }

    void Release()
    {
        GameManager.instance.NumberOfDrinks++;
        sipSound.Stop();
        glassOfAlcScript.Release();
    }
    
    void Drop()
    {
        handFull = false;
        gameObject.SetActive(true);
        transform.position = originalPos;
    }

    public void UpdateNumber(float amount)
    {
        drunkAmount = amount/500;
        ResetOffSet();
    }

    public void ResetOffSet()
    {
        offSetX = Random.Range(-drunkAmount, drunkAmount);
        offSetY = Random.Range(-drunkAmount, drunkAmount);
        transform.position = new Vector3(transform.position.x + offSetX, transform.position.y - offSetY, 0);

        if (transform.position.x < -7.5f)
        {
            transform.position = new Vector3(-7f, transform.position.y, 0);
        }
        if (transform.position.x > 7.5f)
        {
            transform.position = new Vector3(7f, transform.position.y, 0);
        }
        if (transform.position.y > 16f)
        {
            transform.position = new Vector3(transform.position.x, 16, 0);
        }
        if (transform.position.y < 0f)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
    }
}

//喝到带多了可以把兄弟的对话框拿起来砸了 然后game over
//对话框变成问号
//捏出来的时候有一个兄弟
