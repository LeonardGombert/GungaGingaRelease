using System;
using UnityEngine;
using UnityEngine.UI;

namespace Kubika.Game
{
    public class CubePopulator : MonoBehaviour
    {
        public GameObject prefab;
        
        public int numberToCreate;
        GridLayoutGroup gridGroup;

        CubeTypes cubeTypes;

        // Start is called before the first frame update
        void Start()
        {
            gridGroup = GetComponent<GridLayoutGroup>();

            numberToCreate = (int)CubeTypes.ChaosBall;
            gridGroup.constraintCount = numberToCreate;

            PopulateGrid();
        }

        private void PopulateGrid()
        {
            GameObject newObj;

            for (int i = 0; i < numberToCreate; i++)
            {
                newObj = Instantiate(prefab, transform);
                newObj.GetComponent<Image>().color = UnityEngine.Random.ColorHSV();
                newObj.GetComponent<CubeSelector>().selectedCubeType = (CubeTypes) i + 1;
            }
        }

        public void StaticCubePopulator()
        {
            GameObject newObj;

            for (int i = (int)CubeTypes.RotatorLocker; i < numberToCreate; i++)
            {
                newObj = Instantiate(prefab, transform);
                newObj.GetComponent<Image>().color = UnityEngine.Random.ColorHSV();
                newObj.GetComponent<CubeSelector>().selectedCubeType = (CubeTypes)i + 1;
            }
        }
    }
}
