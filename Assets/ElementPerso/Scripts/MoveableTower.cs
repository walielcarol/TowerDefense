using System;
using System.Collections.Generic;
using ActionGameFramework.Health;
using Core.Health;
using UnityEngine;
using TowerDefense.Targetting;
using TowerDefense.Towers.Placement;

using Core.Utilities;
using TowerDefense.Level;
using TowerDefense.Affectors;


namespace TowerDefense.Towers {
    public class MoveableTower : Tower
    {
        public enum State
        {
            /// <summary>
            /// search for the very first approaching enemy
            /// </summary>
            Idle,
            Flying,
            Attacking,
            /// <summary>
            /// when two flying towers are attached, they could no more move but construct an obstacle
            /// </summary>
            Attached
        }

        public State towerState;

        public MoveableTowersManager manager;
        public ParticleSystem fireParticleSystem;
        public MoveableTower partner;
        public float speed = 1f;
        AttackAffector attackAffector;



        private void Start()
        {
            towerState = State.Flying;
            attackAffector =transform.GetChild(1).GetChild(0).GetComponent<AttackAffector>();
            attackAffector.enabled = true;
            fireParticleSystem.gameObject.SetActive(false);
            fireParticleSystem.transform.position = new Vector3(0, 0, 0);
            manager = FindObjectOfType<MoveableTowersManager>();


        }

        private void Update()
        {

            if(towerState == State.Attached)
            {
                if (this == manager.previousHit && this != manager.currentHit)
                {
                    partner = manager.currentHit;

                    fireParticleSystem.gameObject.SetActive(true);
                    attackAffector.enabled = false;
                    partner.transform.GetChild(1).GetChild(0).GetComponent<AttackAffector>().enabled = false;

                    StartCoroutine(MoveParticle(fireParticleSystem.transform, partner.gameObject.transform));

                }
            }
        }



        IEnumerator<MoveableTower> MoveParticle(Transform self, Transform target)
        {

            Vector3 targetPosition = target.position;
            float dist = Vector3.Distance(self.position, targetPosition);

            self.LookAt(targetPosition, Vector3.up);
            self.position += self.forward * speed * Time.deltaTime;



            if (dist < 1f)
            {
              

                self.gameObject.SetActive(false);
                self.position = this.transform.position;
                self.gameObject.SetActive(true);

            }

            yield return null;
        }

        public void AttachedToFlying()
        {
            towerState = State.Flying;
            partner.towerState = State.Flying;
            fireParticleSystem.gameObject.SetActive(false);
            attackAffector.enabled = true;

        }
        public void SetAttached()
        {
            towerState = State.Attached;
            fireParticleSystem.gameObject.SetActive(true);
            attackAffector.enabled = false;
        }



    }
}

