using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainmenu, guidemenu, settingsmenu, selectmapmenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("music") / 100f;
    }

    public void openguide()
    {
        mainmenu.SetActive(false);
        guidemenu.SetActive(true);
    }
    public void Goback()
    {
        guidemenu.SetActive(false);
        settingsmenu.SetActive(false);
        selectmapmenu.SetActive(false);
        mainmenu.SetActive(true);
    }
    public void openSettings()
    {
        settingsmenu.SetActive(true);
        mainmenu.SetActive(false);
    }
    public void ChangeSound(float sound)
    {
        sound = settingsmenu.transform.GetChild(1).GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("music", sound);
        print(sound);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ChooseMap()
    {
        mainmenu.SetActive(false);
        selectmapmenu.SetActive(true);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
