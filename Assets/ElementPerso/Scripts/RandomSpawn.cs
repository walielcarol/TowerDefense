using System;
using System.Collections.Generic;
using Core.Extensions;
using Core.Utilities;
using TowerDefense.Agents;
using TowerDefense.Agents.Data;
using TowerDefense.Nodes;
using UnityEngine;


namespace TowerDefense.Level{

    [RequireComponent(typeof(WaveManager))]
    public class RandomSpawn : Wave
    {
        public WaveManager waveManager;

        public AgentConfiguration[] bosses;

        public int[] weights;

        public Node startNode;

        float time = 1.5f;
        

        private void Awake()
        {

            waveManager = GetComponent<WaveManager>();
            waveManager.spawningCompleted += RepeatSpawning;
        }


        void RepeatSpawning()
        {
            InvokeRepeating("SpawnBoss", 0, time);
        }

        void SpawnBoss()
        {



            int sum = 0;
            foreach(int i in weights )
            {
                sum += i;
            }
            int randomNum = new System.Random().Next(1, sum+1);
            int randomIndex = 0;
            for (int n = 0;n<weights.Length ;n++ )
            {
                if (randomNum - weights[n] < 0)
                {
                    randomIndex = n;
                    break;
                }
                
                    randomNum -= weights[n];
                
            }



             
            SpawnAgent(bosses[randomIndex], startNode);
               
            
        }

        




    }

}

