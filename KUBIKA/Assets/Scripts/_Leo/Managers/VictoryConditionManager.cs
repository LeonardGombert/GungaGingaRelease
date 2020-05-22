using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.Game
{
    public class VictoryConditionManager : MonoBehaviour
    {
        private static VictoryConditionManager _instance;
        public static VictoryConditionManager instance { get { return _instance; } }

        private int currentVictoryPoints;
        public int levelVictoryPoints;

        _VictoryCube[] victoryCubes;

        private void Awake()
        {
            if (_instance != null && _instance != this) Destroy(this);
            else _instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        // Call when a new level is loaded
        void CheckVictoryCubes()
        {
            victoryCubes = FindObjectsOfType<_VictoryCube>();

            foreach (var item in victoryCubes)
            {
                levelVictoryPoints++;
            }
        }

        public void IncrementVictory()
        {
            Debug.Log("I've been touched by a Victory cube");
            currentVictoryPoints++;
        }

        public void DecrementVictory()
        {
            Debug.Log("I've lost track of a Victory cube");
            currentVictoryPoints--;
        }
    }
}