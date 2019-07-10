using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalForHeart : MonoBehaviour
{
    //粒子特效的list
    public GameObject[] Prtiacals;
    //调用0号粒子特效
    public void Partical0()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
  }
  public void Partical1()
  {
      transform.GetChild(0).gameObject.SetActive(false);
      transform.GetChild(1).gameObject.SetActive(true);
      transform.GetChild(2).gameObject.SetActive(false);
  }
  public void Partical2()
  {
      transform.GetChild(0).gameObject.SetActive(false);
      transform.GetChild(1).gameObject.SetActive(false);
      transform.GetChild(2).gameObject.SetActive(true);
    }

  
    
}
