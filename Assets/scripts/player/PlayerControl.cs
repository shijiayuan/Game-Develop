using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {
   
    public float SpeedX = 0f;
    public float SpeedY = 0f;
    public float SpeedZ = 6f;
    public float bulletRate = 0;
    public Transform m_transform;
    public Transform m_pbullet;
    public int m_health=100;
    public int m_energy=200;
    public float energyRetriveRate = 0;

    public Slider healthSlider;
    public Slider energySlider;

    public bool leftswag = false;
    public bool rightswag = false;

    public float speedUpRate = 0;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.CompareTo("levelOneEnv")==0)
        {
            m_transform.Rotate(0, 180, 0);
            m_health -= 50;
        }
    }


    // Use this for initialization
    void Start () {
        m_transform = this.transform;
        this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        m_health=100;
        m_energy = 200;
        healthSlider.value = m_health;
        energySlider.value = m_energy;
      
    }

   
    // Update is called once per frame
    void Update () { 
       //每隔5s 加20的能量值  
        if (m_energy < 200)
        {
            energyRetriveRate -= Time.deltaTime;
            if (energyRetriveRate<=0)
            {
                energyRetriveRate = 5;
                m_energy += 20;
            }
        }
        speedUpRate -= Time.deltaTime;
        control();
        healthSlider.value = m_health;
        energySlider.value = m_energy;
        if(Application.platform==RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Home) || Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }
	}
   public void control()
    {
        float dy = 0;
        float dz = 0;
        float RotateY = 0;
        if (Input.GetKey(KeyCode.W))
        {
           
            dy += 6 * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.S))
        {
           
            dy -= 6 * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.A))
        {
            RotateY -= 30 * Time.deltaTime;
            if (leftswag == false)
            {
                m_transform.Rotate(0, 0, 20);
                leftswag = true;
            }
        }
        else if(leftswag==true)
        {
            m_transform.Rotate(0, 0, -20);
            leftswag = false;
        }
        if(Input.GetKey(KeyCode.D))
        {
            RotateY += 30 * Time.deltaTime;
            if(rightswag==false)
            {
                m_transform.Rotate(0,0,-20);
                rightswag = true;
            }
        }
        else if(rightswag==true)
        {
            m_transform.Rotate(0, 0, 20);
            rightswag = false;
        }
        if(Input.GetKey(KeyCode.Space))
        {
            /*  if(m_energy>=20)
              {
                  if(speedUpRate<=0)
                  {
                      speedUpRate = 0.1f;
                      SpeedZ += 5;
                      m_energy -= 20;
                  }


              }
              else
              {
                  SpeedZ = 6;
              }     */
            dz += 6*Time.deltaTime;

        }
        bulletRate -= Time.deltaTime;
        if(bulletRate <= 0)
        {
            bulletRate = 0.1f;
            if (Input.GetKey(KeyCode.C))
            {
                if(m_energy>=5)
                {
                    Instantiate(m_pbullet,
                   new Vector3(m_transform.position.x, m_transform.position.y, m_transform.position.z),
                   m_transform.rotation);
                    m_energy -= 20;
                }
               
            }
        }
        
        m_transform.Rotate(0, RotateY, 0,Space.World);
        m_transform.Translate(0, dy, dz);
      //  m_ch.Move(m_transform.position);
    }

}
