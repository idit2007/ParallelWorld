using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour
{
    private Animator anim;
    public GameObject[] BigMap;
    // Use this for initialization
    void Start()
    {
        anim = GameObject.Find("MapFeild").GetComponent<Animator>();
        BigMap[0].SetActive(false);
    }


    public void MapButtonBig()
    {
        anim.SetTrigger("Big");
        BigMap[0].SetActive(true);
        Debug.Log("a");
    }

    public void MapButtonSmall()
    {
        anim.SetTrigger("Small");
        BigMap[0].SetActive(false);
    }
}
