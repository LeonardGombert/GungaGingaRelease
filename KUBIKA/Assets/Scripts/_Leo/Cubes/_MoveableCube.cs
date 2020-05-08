namespace Kubika.Game
{
    public class _MoveableCube : CubeBase
    {
        // Start is called before the first frame update
        new void Start()
        {
            base.Start();
            isStatic = false;
        }

        // Update is called once per frame
        new void Update()
        {
            base.Update();
            CheckIfFalling();//gridRef.kuboGrid[myIndex - 1].cubeLayers = CubeLayers.cubeMoveable;
        }

        private void CheckIfFalling()
        {
            if (gridRef.kuboGrid[myIndex - 2].cubeLayers == CubeLayers.cubeEmpty)
            {
                //"garbage collection"
                //-1 because you're looking at the curretn NODE
                gridRef.kuboGrid[myIndex - 1].cubeOnPosition = null;
                gridRef.kuboGrid[myIndex - 1].cubeLayers = CubeLayers.cubeEmpty;

                //falling     

                //set the new index
                myIndex = gridRef.kuboGrid[myIndex - 2].nodeIndex;
                
                //move to updated index
                transform.position = gridRef.kuboGrid[myIndex - 1].worldPosition;
                //set updated index to cubeMoveable
                gridRef.kuboGrid[myIndex - 1].cubeLayers = CubeLayers.cubeMoveable;
            }
        }
    }
}