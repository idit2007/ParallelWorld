using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour
{
    private Animator anim;
    public GameObject BigMap;
    // Use this for initialization
    void Start()
    {
        anim = GameObject.Find("MapFeild").GetComponent<Animator>();
        BigMap.SetActive(false);
    }


    public void MapButtonBig()
    {
        anim.SetTrigger("Big");
        BigMap.SetActive(true);
    }

    public void MapButtonSmall()
    {
        anim.SetTrigger("Small");
        BigMap.SetActive(false);
    }
}
