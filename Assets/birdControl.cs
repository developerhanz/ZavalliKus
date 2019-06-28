using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class birdControl : MonoBehaviour
{
    public Sprite[] birdSprites;
    SpriteRenderer spriteRenderer;
    int kusSayac = 0;
    float kusAnimasyonZaman = 0.0f;
    Rigidbody2D fizik;
    int puan = 0;
    public Text puanText;
    bool oyunDevamEdiyor = true;
    oyunKontrol oyunKontrolItem;
    AudioSource[] sesler;
    int reklamSayaci;
    //public AudioClip carpmaSesi;
    //public AudioClip puanSesi;
    //public AudioClip kanatSesi;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        oyunKontrolItem = GameObject.FindGameObjectWithTag("oyunKontrolTag").GetComponent<oyunKontrol>();
        sesler = GetComponents<AudioSource>();

        reklamSayaci = PlayerPrefs.GetInt("reklam_sayaci", 0);
        reklamSayaci++;
        reklamSayaci %= 5;
        PlayerPrefs.SetInt("reklam_sayaci", reklamSayaci);
    }

    // Update is called once per frame
    void Update()
    {
        animasyon();

        if (oyunDevamEdiyor)
        {
            /*int parmakSayisi = Input.touchCount;
            for (int i = 0; i < parmakSayisi; i++)
            {
                Touch parmak = Input.GetTouch(i);

                if (parmak.phase == TouchPhase.Began)
                {
                    fizik.velocity = new Vector2(0, 0);
                    fizik.AddForce(new Vector2(0, 200));
                    sesler[0].Play();
                    //ses.clip = kanatSesi;
                    //ses.Play();
                }
            }*/
            if (Input.GetButtonDown("Fire1"))
            {
                fizik.velocity = new Vector2(0, 0);
                fizik.AddForce(new Vector2(0, 200));
                sesler[0].Play();
                //ses.clip = kanatSesi;
                //ses.Play();
            }

        }

        if (fizik.velocity.y > 0)
        {
            transform.eulerAngles = new Vector3(0,0,30);
        }
        else if(fizik.velocity.y<0)
        {
            transform.eulerAngles=new Vector3(0, 0, -30);
        }
        else
        {
            transform.eulerAngles=new Vector3(0, 0, 0);
        }
    }

    void animasyon()
    {
        kusAnimasyonZaman += Time.deltaTime;
        if (kusAnimasyonZaman > 0.5)
        {
            kusAnimasyonZaman = 0;
            spriteRenderer.sprite = birdSprites[kusSayac];
            kusSayac++;
            if (kusSayac == birdSprites.Length)
                kusSayac = 0;
            /*kusAnimasyonZaman = 0;
            if (ileriGericontrol)
            {
                if (kusSayac == 0)
                    kusSayac++;
                spriteRenderer.sprite = birdSprites[kusSayac];
                kusSayac++;
                ileriGericontrol &= kusSayac != birdSprites.Length;
            }
            else
            {
                if (kusSayac == birdSprites.Length)
                    kusSayac--;
                kusSayac--;
                spriteRenderer.sprite = birdSprites[kusSayac];
                ileriGericontrol |= kusSayac == 0;
            }*/
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "puan")
        {
            //ses.clip = puanSesi;
            //ses.Play();
            sesler[1].Play();
            puan++;
            puanText.text = "SKOR: "+puan;
        } else if(other.gameObject.tag == "engel")
        {
            //ses.clip = carpmaSesi;
            //ses.Play();
            sesler[2].Play();
            oyunDevamEdiyor = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            oyunKontrolItem.oyunBitti();

            if (puan > PlayerPrefs.GetInt("eski_rekor", 0))
                PlayerPrefs.SetInt("eski_rekor", puan);

            PlayerPrefs.SetInt("puan", puan);
        }
    }
}