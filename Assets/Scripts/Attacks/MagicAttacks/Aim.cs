using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public Rigidbody2D theRB;

    private Camera theCam;

   public GameObject main;
    private SpriteRenderer m_SpriteRenderer;
    public Transform firePoint;
    public GameObject bullet;
    private bool direction=false;
    [SerializeField] private int apcost;
    
    void Start()
    {
        theCam = Camera.main;
        m_SpriteRenderer=main.GetComponent<SpriteRenderer>();
        apcost = 5 ;
    }

   
    void Update()
    {
        Vector3 mouse = Input.mousePosition;

        Vector3 screenPoint =theCam.WorldToScreenPoint(main.transform.localPosition);

        Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        if (offset.x<0 && direction==false)
        {
            direction = true;
            m_SpriteRenderer.flipX = true;
        }
        else if(offset.x>=0 && direction == true)
        {
            direction = false;
            m_SpriteRenderer.flipX = false;
        }
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (Input.GetMouseButtonDown(0)&&GridManager.Instance.mouseTile!=null)
        {
            
                Instantiate(bullet, firePoint.position, transform.rotation);
                UnitManager.Instance.SelectedHero.setSteps(apcost);
                UnitManager.Instance.SelectedHero.changeHeroState(Herostate.Idle);
            
        }    
    }
}
