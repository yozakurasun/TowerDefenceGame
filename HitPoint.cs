using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    public int Hp;
    public bool IsDie = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Damage(int damage)
    {
        Hp -= damage;

        if(Hp <= 0)
        {
            IsDie = true;
            Destroy(this.gameObject);
        }
    }
}
