using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    [SerializeField]
    int levelToLoad, target = 0;
    [SerializeField]
    bool autoIndex = true;
    [SerializeField]
    Sprite valid;

    private bool next;
    UIScript uIScript;
    public ParticleSystem particle;
    EffectManager effectManager;

    private void Awake()
    {
        particle.Stop();
        particle.enableEmission = false;
        uIScript = GameObject.Find("Canvas").GetComponent<UIScript>();
        effectManager = GameObject.Find("LevelTransition").GetComponent<EffectManager>();
    }

    void Start()
    {
        next = false;
        if (autoIndex)
        {
            levelToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        }
    }
    
    void Update()
    {
        if (uIScript.coinNb >= target && next == false)
        {
            StartCoroutine(valide());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && next == true)
        {
            if (effectManager)
            {
                effectManager.LoadNext(levelToLoad);
            }
        }
    }

    private IEnumerator valide(){
        next = true;
        particle.Play();
        particle.enableEmission = true;
        yield return new WaitForSeconds(.5f);
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = valid;
        particle.Stop();
        particle.enableEmission = false;
    }
}
