using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public Button butL;
    public Button butR;
    public GameObject locked;
    public GameObject unlocked;
    public GameObject leftlocked;
    public GameObject rightlocked;
    bool open;
    public int dex;
    
    // Start is called before the first frame update
    void Start()
    {
        open = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!open){
            locked.SetActive(false);
            unlocked.SetActive(false);
            leftlocked.SetActive(false);
            rightlocked.SetActive(false);
            if(!butL.on&&!butR.on){
                locked.SetActive(true);
            }
            else if(!butR.on){
                rightlocked.SetActive(true);
            }
            else if(!butL.on){
                leftlocked.SetActive(true);
            }
            else{
                unlocked.SetActive(true);
                open = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        if(open){
            SceneManager.LoadScene(dex);
        }
    }
}
