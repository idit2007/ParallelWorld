using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverAndWin : MonoBehaviour {
    private Slider hpSlider;
    private Slider virusSlider;
    public GameObject win;
    public GameObject lose;
    public static bool StopGame;
    // Use this for initialization
    void Start () {
        hpSlider = GameObject.Find("HPSlider").GetComponent<Slider>();
        virusSlider = GameObject.Find("VirusSlider").GetComponent<Slider>();
        lose.SetActive(false);
        win.SetActive(false);
        StopGame = false;
    }
	
	// Update is called once per frame
	void Update () {
                 if(virusSlider.value == 0 || hpSlider.value == 0)
            {
                lose.SetActive(true);
                StopGame = true;
            }
                if (Unit.win)
            {
                win.SetActive(true);
                StopGame = true;
            }   
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void blacktomenu()
    {
        Application.LoadLevel("Menu2");
    }

    public void NextState()
    {
        
    }
}
