using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveRemaining : MonoBehaviour
{
    private Image Jauge;
    private bool fill;
     
    // Start is called before the first frame update
    void Start()
    {
        Jauge = GetComponent<Image>();    
    }
    
    public void listener(bool add)
    {
        if ( add == false )
        {
            fill = true;
        } else if (add == true )
        {
            fill = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ( fill )
        {
            Jauge.fillAmount -= (float)Time.deltaTime * 0.1f ;  
        }
        else {
            Jauge.fillAmount += (float)Time.deltaTime * 0.1f;
        }
    }
}
