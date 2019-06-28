using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class oyunKontrol : MonoBehaviour
{
    public float gokyuzuHiz;
    public GameObject gokyuzu1;
    public GameObject gokyuzu2;
    Rigidbody2D gokyuzu1Fizik;
    Rigidbody2D gokyuzu2Fizik;
    float gokyuzuUzunluk;
    public GameObject engel;
    public int engelSayisi = 5;
    GameObject[] engeller;

    float degisimZaman = 0;
    int engellerIndis = 0;
    bool oyunDevamEdiyor = true;
    
    // Start is called before the first frame update
    void Start()
    {
        if (gokyuzuHiz == 0.0f)
            gokyuzuHiz = 1.5f;
        gokyuzu1Fizik = gokyuzu1.GetComponent<Rigidbody2D>();
        gokyuzu2Fizik = gokyuzu2.GetComponent<Rigidbody2D>();
        gokyuzu1Fizik.velocity = new Vector2(-gokyuzuHiz, 0);
        gokyuzu2Fizik.velocity = new Vector2(-gokyuzuHiz, 0);

        gokyuzuUzunluk = gokyuzu1Fizik.GetComponent<BoxCollider2D>().size.x;
        engeller = new GameObject[engelSayisi];
        for(int i=0; i<engeller.Length; i++)
        {
            engeller[i] = Instantiate(engel, new Vector2(-20*i,-20*i), Quaternion.identity);
            Rigidbody2D engelFizik = engeller[i].AddComponent<Rigidbody2D>();
            engelFizik.gravityScale = 0;
            engelFizik.velocity = new Vector2(-gokyuzuHiz, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (oyunDevamEdiyor)
        {
            if (gokyuzu1.transform.position.x <= -gokyuzuUzunluk)
            {
                gokyuzu1.transform.position = new Vector3(gokyuzu2.transform.position.x + gokyuzuUzunluk, gokyuzu1.transform.position.y);
            }
            if (gokyuzu2.transform.position.x <= -gokyuzuUzunluk)
            {
                gokyuzu2.transform.position = new Vector3(gokyuzu1.transform.position.x + gokyuzuUzunluk, gokyuzu2.transform.position.y);
            }


            degisimZaman += Time.deltaTime;
            if (degisimZaman > 2.0f)
            {
                degisimZaman = 0.0f;
                float y = Random.Range(-0.50f, 1.10f);
                engeller[engellerIndis].transform.position = new Vector3(18.0f, y);
                engellerIndis++;
                if (engellerIndis == engeller.Length - 1)
                {
                    engellerIndis = 0;
                }
            }
        }

    }

    public void oyunBitti()
    {
        oyunDevamEdiyor = false;
        foreach(GameObject item in engeller)
        {
            item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gokyuzu1Fizik.velocity = Vector2.zero;
            gokyuzu2Fizik.velocity = Vector2.zero;
        }
        Invoke("ana_menuya_don", 2);//2 saniye sonra tırnak içindeki metod çalışıyo
    }

    void ana_menuya_don()
    {
        SceneManager.LoadScene("Ana Menu");
    }
}