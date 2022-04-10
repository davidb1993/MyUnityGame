using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public GameObject parent;
    // Start is called before the first frame update

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
   public void SetHealth(int health)
    {
        slider.value = health;
    }

    private void Start()
    {
        parent = transform.parent.gameObject;   
    }
    private void Update()
    {
        transform.rotation = parent.transform.rotation;
    }
}
