using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class anaMenu : MonoBehaviour
{
    public Text rekorText;
    // Start is called before the first frame update
    void Start()
    {
        rekorText.text = "REKOR: "+PlayerPrefs.GetInt("eski_rekor", 0);

        //if (puan > 2)
        //{
        //    GameObject.FindGameObjectWithTag("reklamlarTag").GetComponent<reklam>().gecisReklamGoster();
        //}

        if(PlayerPrefs.GetInt("reklam_sayaci", 0)>3 && PlayerPrefs.GetInt("puan", 0) > 3)
        {
            GameObject.FindGameObjectWithTag("reklamlarTag").GetComponent<reklam>().gecisReklamGoster();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void oyunaBasla()
    {
        SceneManager.LoadScene("level");
    }

    public void oyundanCik()
    {
        Application.Quit();
    }
}