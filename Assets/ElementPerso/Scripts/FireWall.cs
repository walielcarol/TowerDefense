using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefense.Towers;
using TowerDefense.Towers.Projectiles;
using System;
using Core.Utilities;


using ActionGameFramework.Health;
using Core.Health;

[RequireComponent(typeof(Damager))]
public class FireWall : MonoBehaviour
{
    BoxCollider fireWall;
    Transform partner;
    Transform self;
    public float height = 3f;

    public float delay;

    protected Targetable m_Enemy;

    protected Damager m_Damager;
    ParticleSystem collisionParticles;




    private void Start()
    {
        m_Damager = GetComponent<Damager>();
        fireWall = GetComponent<BoxCollider>();
        self = transform.parent.parent.transform;
        collisionParticles = m_Damager.collisionParticles;



    }

    private void Update()
    {
        ExtendToPartner();
    }

    void ExtendToPartner()
    {


        if(self.GetComponent<MoveableTower>()!=null)
        {
            if(self.GetComponent<MoveableTower>().partner != null)
            {
                partner = self.GetComponent<MoveableTower>().partner.transform;
                if (partner != null && self != partner)
                {
                    StartCoroutine(CreateCollider(self));
                }
            }

        }


    }

    IEnumerator CreateCollider(Transform self)
    {

        transform.LookAt(partner);
        float dist = Vector3.Distance(self.position, partner.position);

        transform.localScale = new Vector3(1, height, 2*dist);


        yield return new WaitForSeconds(2f);
    }

    private void OnTriggerEnter(Collider other)
    {

       
        if(other.gameObject.tag == "Enemy")
        {

            m_Enemy = other.gameObject.GetComponent<Targetable>();
            m_Enemy.TakeDamage(m_Damager.damage, m_Enemy.position, m_Damager.alignmentProvider);



            var pfx = Poolable.TryGetPoolable<ParticleSystem>(collisionParticles.gameObject);

            pfx.transform.position = other.transform.position;
            pfx.Play();
        }
       
       
       
    }

}
