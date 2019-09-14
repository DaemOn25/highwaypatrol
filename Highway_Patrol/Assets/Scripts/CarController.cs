using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using UnityEditor;

public class CarController : MonoBehaviour
{
    public float carSpeed = 175f;
    public float maxpos_h = 2.15f;
    public float maxpos_v = 4.00f;
    public UiManager ui;
    public AudioManager am;

    Light lt;

    Color colorb = Color.blue;
    Color colorr = Color.red;
    private float durationl = 0.2f;

    /*
        [SerializeField] private Color colorb = Color.blue;        
        [SerializeField] private Color colorr = Color.red; 
    */

    Vector3 position;

    bool platformdroid = false;
     
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lt = GetComponent<Light>();

#if UNITY_ANDROID
        platformdroid = true;
#else
        platformdroid = false;
#endif
    }


    // Start is called before the first frame update
    void Start()
    {
        if(platformdroid == true)
        {
            Debug.Log("Android");
        }
        else
        {
            Debug.Log("Windows");
        }

        position = transform.position;    //storing the current position of car in temp var.
        am.sound.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (platformdroid == true)  //When the platform will be android this code will be executed
        {

            position = transform.position;
            position.x = Mathf.Clamp(position.x, -maxpos_h, maxpos_h);  //to restrict the positions
            transform.position = position;      //updating the current position of the car 

           // TouchMove();

        }

        else
        {
            position.x += Input.GetAxis("Horizontal") * carSpeed * Time.deltaTime;    //updating the position every frame horizontally
            position.y += Input.GetAxis("Vertical") * carSpeed * Time.deltaTime;      //updating the position every frame vertically

            
            position.y = Mathf.Clamp(position.y, -maxpos_v, maxpos_v);

            transform.position = position;      //updating the current position of the car 
        }

        // set light color
        //color loop between two colors 
        float t = Mathf.PingPong(Time.time, durationl) / durationl;
        lt.color = Color.Lerp(colorb, colorr, t);


        /* ++tchanger;

         if (tchanger % 5 == 0)
         {

                 //colorBlue();
         }
         if(tchanger % 7 == 0)
         {
            // colorRed();
         }
       */

    }

/*
    void  colorBlue()
    {
        SerializedObject halo = new SerializedObject(GetComponent("Halo"));
        halo.FindProperty("m_Color").colorValue = colorb ;
        halo.ApplyModifiedProperties();
    }

    void colorRed()
    {
        SerializedObject halo = new SerializedObject(GetComponent("Halo"));
        halo.FindProperty("m_Color").colorValue = colorr;
        halo.ApplyModifiedProperties();
    }
*/

    void OnCollisionEnter2D(Collision2D col)
    {
        if( col.gameObject.tag == "EnemyCar")
        {
            //Destroy(gameObject);            //to destroy the object   
            gameObject.SetActive(false);      // to deactivate object
            ui.GameOver();
            am.sound.Stop();
        }
    }

    void TouchMove()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            float middle = Screen.width / 2;

            if (touch.position.x < middle && touch.phase == TouchPhase.Began)
            {
                MoveLeft();
            }

            else if(touch.position.x > middle && touch.phase == TouchPhase.Began)
            {
                MoveRight();
            }

            else
            {
                SetVelocityZero();
            }

        }
    }

    public void MoveLeft()
    {
        rb.velocity = new Vector2(-carSpeed * Time.deltaTime , 0);
    }

    public void MoveRight()
    {
        rb.velocity = new Vector2(carSpeed * Time.deltaTime , 0);
    }

    public void SetVelocityZero()
    {
        rb.velocity = Vector2.zero;
    }
}
