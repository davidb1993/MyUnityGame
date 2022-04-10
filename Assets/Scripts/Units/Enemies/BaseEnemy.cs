using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseUnit
{
    SpriteRenderer m_SpriteRenderer;
    private void Awake()
    {
        numberofSteps = 3; 
        maxHealth = 50;
        currentHealth = maxHealth;
        Healthbar.SetMaxHealth(maxHealth);
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    public void attack()
    {
        GridManager.Instance.FindPath(this.OccupiedTile,UnitManager.Instance.SelectedHero.OccupiedTile);
        if (numberofSteps >= GridManager.Instance.path.Count - 2 && GridManager.Instance.path.Count - 2 >= 0)
        {
            GridManager.Instance.path[GridManager.Instance.path.Count - 2].SetUnit(this);
            UnitManager.Instance.SelectedHero.takeDamage(20);
            
        }
        else if (numberofSteps < GridManager.Instance.path.Count - 2) GridManager.Instance.path[numberofSteps].SetUnit(this);
        else
        {
            UnitManager.Instance.SelectedHero.takeDamage(20);
            
        }
        transform.rotation = Quaternion.identity;
        
        Vector3 worldPosition =  transform.position - UnitManager.Instance.SelectedHero.transform.position;
        if (worldPosition.y >= - worldPosition.x) {
            if (worldPosition.y>=worldPosition.x) {
                m_SpriteRenderer.flipX = false;
                transform.Rotate(0, 0, 90.0f);
            }
            else
            {
               m_SpriteRenderer.flipX = false;
            }
        }
        else
        {
            if (worldPosition.y >= worldPosition.x)
            { 
                m_SpriteRenderer.flipX = true;
            }
            else
            {
                m_SpriteRenderer.flipX = false;
                transform.Rotate(0, 0, -90.0f);
            }
        }
       

    }
    private void OnMouseDown()
    {
        GridManager.Instance.mouseTile = this.OccupiedTile;
    }
    private void OnMouseExit()
    {
        GridManager.Instance.mouseTile = null;
    }
}
