using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavesManager : MonoBehaviour
{
    public GameObject[] Enemies;
    int wave;
    public int totalEnemies;
    public Text WavesText;
    Animator wavesani;
    bool spawning = false;
    bool paused = false;
    public GameObject pause;
    // Start is called before the first frame update
    void Start()
    {
        wave = 0;
        wavesani = WavesText.gameObject.GetComponent<Animator>();
        StartCoroutine(Wave1To3());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("music") / 100f;
        if (totalEnemies == 0 && !spawning)
        {
            if (wave <= 2)
                StartCoroutine(Wave1To3());
            else if (wave <= 9)
                StartCoroutine(Wave4To10());
            else if (wave <= 19)
                StartCoroutine(Wave11To20());
            else if (wave <= 50)
                StartCoroutine(Wave21To51());
            else
                Application.Quit();
        }
    }

    public void Pause()
    {
        if (!paused)
        {
            pause.SetActive(true);
            paused = true;
            Time.timeScale = 0f;
        }
        else
        {
            pause.SetActive(false);
            paused = false;
            Time.timeScale = 1;
        }
    }

    public void continueOrExit(int code)
    {
        if (code == 1)
        {
            Pause();
        }
        else
        {
            Application.Quit();
        }
    }

    void NewWave()
    {
        wave++;
        WavesText.gameObject.SetActive(true);
        WavesText.gameObject.GetComponent<AudioSource>().Play();
        wavesani.Play(0);
        WavesText.text = "Wave: " + wave;
    }

    void Spawn(int EnemyID)
    {
        totalEnemies++;
        Instantiate(Enemies[EnemyID]);
    }

    IEnumerator Wave1To3()
    {
        spawning = true;
        yield return new WaitForSeconds(5);
        NewWave();
        if (wave < 3)
        {
            for (int i = 0; i < (wave * 2); i++)
            {
                Spawn(0);
                yield return new WaitForSeconds(1);
            }
        }
        else
        {
            for (int i = 0; i < wave + 2; i++)
            {
                Spawn(0);
                yield return new WaitForSeconds(1);
            }
            yield return new WaitForSeconds(1);
            Spawn(1);
        }
        spawning = false;
    }

    IEnumerator Wave4To10()
    {
        spawning = true;
        yield return new WaitForSeconds(5);
        NewWave();
        if (wave < 5)
        {
            Spawn(4);
            yield return new WaitForSeconds(1);
            Spawn(4);
            Spawn(1);
            yield return new WaitForSeconds(1);
            Spawn(1);
        }
        else if (wave < 7)
        {
            Spawn(5);
            yield return new WaitForSeconds(1);
            for (int i = 0; i < wave - 3; i++)
            {
                Spawn(4);
                yield return new WaitForSeconds(1);
            }
        }
        else if (wave < 10)
        {
            Spawn(6);
            yield return new WaitForSeconds(1);
            for (int i = 0; i < wave - 3; i++)
            {
                Spawn(1);
                yield return new WaitForSeconds(1);
            }
        }
        else
        {
            Spawn(2);
        }
        spawning = false;
    }

    IEnumerator Wave11To20()
    {
        spawning = true;
        yield return new WaitForSeconds(5);
        NewWave();
        if (wave < 15)
        {
            for (int i = 0; i < wave - 9; i++)
            {
                Spawn(10);
                yield return new WaitForSeconds(1);
            }
            Spawn(5);
            yield return new WaitForSeconds(1);
            Spawn(5);
            yield return new WaitForSeconds(1);
            Spawn(5);
            yield return new WaitForSeconds(1);
        }
        else if (wave <= 20)
        {
            Spawn(10);
            yield return new WaitForSeconds(1);
            Spawn(10);
            yield return new WaitForSeconds(1);
            Spawn(12);
            yield return new WaitForSeconds(1);
            Spawn(2);
            yield return new WaitForSeconds(1);
        }
        spawning = false;
    }

    IEnumerator Wave21To51()
    {
        spawning = true;
        yield return new WaitForSeconds(5);
        NewWave();
        if (wave < 26)
        {
            for (int i = 0; i < wave - 16; i++)
            {
                Spawn(12);
                yield return new WaitForSeconds(1);
            }
            Spawn(13);
        }
        else if (wave < 28)
        {
            Spawn(2);
            yield return new WaitForSeconds(1);
            Spawn(2);
            yield return new WaitForSeconds(1);
        }
        else if (wave < 31)
        {
            for (int i = 0; i < wave - 20; i++)
            {
                Spawn(5);
                yield return new WaitForSeconds(1);
            }
            for (int i = 0; i < wave - 25; i++)
            {
                Spawn(12);
                yield return new WaitForSeconds(1);
            }
            Spawn(13);
        }
        else if (wave < 34)
        {
            for (int i = 0; i < (wave * 2) - 55; i++)
            {
                Spawn(1);
                yield return new WaitForSeconds(1);
            }
            Spawn(3);
        }
        else if (wave < 35)
        {
            Spawn(8);
        }
        else if (wave < 37)
        {
            Spawn(11);
        }
        else if (wave < 40)
        {
            Spawn(7);
            yield return new WaitForSeconds(1);
            Spawn(7);
            yield return new WaitForSeconds(1);
            Spawn(9);
        }
        else if (wave < 41)
        {
            Spawn(14);
        }
        else if (wave < 50)
        {
            for (int i = 0; i < (wave * 3) - 110; i++)
            {
                Spawn(5);
                yield return new WaitForSeconds(0.5f);
            }
            for (int i = 0; i < wave - 35; i++)
            {
                Spawn(10);
                yield return new WaitForSeconds(1);
            }
            Spawn(11);
        }
        else if (wave < 51)
        {
            for (int i = 0; i < wave - 57; i++)
            {
                Spawn(13);
                yield return new WaitForSeconds(1);
            }
            Spawn(15);
        }
        else
        {
            Spawn(3);
            yield return new WaitForSeconds(3);
            Spawn(9);
            yield return new WaitForSeconds(3);
            Spawn(11);
            yield return new WaitForSeconds(3);
            Spawn(15);
            yield return new WaitForSeconds(3);
            Spawn(15);
        }
        spawning = false;
    }
}
