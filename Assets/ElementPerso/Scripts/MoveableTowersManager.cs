using System;
using System.Collections.Generic;
using ActionGameFramework.Health;
using Core.Health;
using UnityEngine;
using TowerDefense.Targetting;
using TowerDefense.Towers.Placement;
using TowerDefense.Agents;
using TowerDefense.Affectors;

using Core.Utilities;
using TowerDefense.Level;



namespace TowerDefense.Towers
{
    public class MoveableTowersManager : Tower 
    {
        public Raycast raycast;
        public MoveableTower previousHit;
        public MoveableTower currentHit;

        private List<MoveableTower> towers;
        public List<SingleTowerPlacementArea> allBases;
        private List<SingleTowerPlacementArea> possibleBases;
        private List<Agent> agents;

        public float speed = 8;
        public Targetter homeTargetter;
        Agent nearestAgent = null;
        float waitTime = 5f;

        private AttackAffector[] attackAffectors;
        


        void Start()
        {

            possibleBases = new List<SingleTowerPlacementArea>();
            InvokeRepeating("Preparation", 0f, waitTime);
            raycast.onClick += AttachTowers;

        }

        private void Preparation()
        {
            towers = GetCurrentTowers();
            nearestAgent = GetNearestAgent();

            possibleBases.Clear();

            foreach (SingleTowerPlacementArea area in allBases)
            {
                area.isSelected = false;
            }
            foreach(MoveableTower tower in towers)
            {
                AttackAffector attackAffector = tower.gameObject.transform.GetChild(1).GetChild(0).GetComponent<AttackAffector>();
                attackAffector.enabled = true;
                SingleTowerPlacementArea selectedBase = SearchNearestBase(nearestAgent);
                IntVector2 gridPosition = selectedBase.WorldToGrid(selectedBase.transform.position, dimensions);
                selectedBase.Clear(gridPosition, dimensions);
                possibleBases.Add(selectedBase);
                tower.currentBase.Clear(gridPosition, dimensions);
                tower.currentBase = null;

            }

        }

        void ActivateAttackAffectors()
        {
            attackAffectors = FindObjectsOfType<AttackAffector>();

            for (int i = 0; i < attackAffectors.Length; i++)
            {
                attackAffectors[i].enabled = true;
            }
        }

        private void Update()
        {

            ActivateAttackAffectors();


            if (nearestAgent == null)
            {
                return;
            }
            else{

                for (int i = 0; i < towers.Count; i++)
                {

                    DeplaceTower(towers[i], possibleBases[i]);

                }
            }

            


        }

        // find the nearest agent to our home
        private Agent GetNearestAgent()
        {
            agents = new List<Agent>();
            agents.Clear();

            if(GameObject.FindGameObjectWithTag("Enemy") != null)
            {
                GameObject[] agentsGO = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject a in agentsGO)
                {
                    agents.Add(a.GetComponent<Agent>());
                }

                float distance = float.MaxValue;
                Agent nearest = agents[0];

                for (int i = 0; i < agents.Count; i++)
                {
                    float currentDistance = Vector3.Distance(agents[i].transform.position, homeTargetter.transform.position);
                    if (currentDistance < distance)
                    {
                        distance = currentDistance;
                        nearest = agents[i];
                    }
                }

                return nearest;
            }

            else{
                return null;
            }

        }

        private List<MoveableTower> GetCurrentTowers()
        {
            towers = new List<MoveableTower>();
            towers.Clear();

                GameObject[] towersGO = GameObject.FindGameObjectsWithTag("MoveableTower");
                foreach (GameObject towerGO in towersGO)
                {
                    if(towersGO != null &&towerGO.GetComponent<MoveableTower>().towerState == MoveableTower.State.Flying)
                    {
                        MoveableTower tower = towerGO.GetComponent<MoveableTower>();
                        towers.Add(tower);
                    }
                    
                }

            return towers;
        }


        // find the available bases for setting these moveable towers
      
        private SingleTowerPlacementArea SearchNearestBase(Agent nearestAgent)
        {
           
            if(nearestAgent == null)
            {
                return null;
            }

            else
            {
                float distance = float.MaxValue;
                SingleTowerPlacementArea nearest = null;

                for (int i = 0; i < allBases.Count; i++)
                {

                    if(allBases[i].isSelected == true)
                    {
                        continue;
                    }

                        float currentDistance = Vector3.Distance(nearestAgent.transform.position, allBases[i].transform.position);
                        if (currentDistance < distance)
                        {
                            distance = currentDistance;
                            nearest = allBases[i];
                        }


                }
                nearest.isSelected = true;
                return nearest;



            }

        }



        void DeplaceTower(MoveableTower tower, SingleTowerPlacementArea area)
        {
            if (area == null)
            { return; }

            else
            {

                Vector3 targetPosition = area.transform.position;
                float dist = Vector3.Distance(tower.transform.position, targetPosition);

                if (dist < 0.1f)
                {
                    tower.transform.position = targetPosition;
                    IntVector2 gridPosition = area.WorldToGrid(targetPosition, dimensions);
                    area.Occupy(gridPosition, dimensions);
                    tower.currentBase = area;
                    return;

                }

                else{

                    tower.transform.LookAt(targetPosition, Vector3.up);
                    tower.transform.position += tower.transform.forward * speed * Time.deltaTime;

                }
            }
        }

        public void AttachTowers(Raycast raycast)
        {

            if(raycast.currentHit != null && raycast.previousHit!= null )
            {
                previousHit = raycast.previousHit.gameObject.GetComponent<MoveableTower>();
                currentHit = raycast.currentHit.gameObject.GetComponent<MoveableTower>();

                    if (currentHit.towerState == MoveableTower.State.Attached)
                    {
                    currentHit.AttachedToFlying();
                        raycast.currentHit = null;
                        raycast.previousHit = null;

                }
                    else if (previousHit != currentHit)
                    {
                        ResetAllToFly();
                    previousHit.SetAttached();
                    currentHit.SetAttached();

                    }

            }

        }

        void ResetAllToFly()
        {

            towers = new List<MoveableTower>();
            towers.Clear();

            GameObject[] towersGO = GameObject.FindGameObjectsWithTag("MoveableTower");
            foreach (GameObject towerGO in towersGO)
            {

                MoveableTower tower = towerGO.GetComponent<MoveableTower>();
                tower.towerState = MoveableTower.State.Flying;

                tower.fireParticleSystem.gameObject.SetActive(false);


            }
        }


    }
}

