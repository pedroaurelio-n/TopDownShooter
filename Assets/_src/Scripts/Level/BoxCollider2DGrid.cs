using UnityEngine;
using UnityEditor;
 
namespace PedroAurelio.TopDownShooter
{
    [CustomEditor(typeof(BoxCollider2DGrid))]
    public class BuildGridInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var grid = (BoxCollider2DGrid)target;
            
            if (GUILayout.Button("Build Grid"))
                grid.BuildGrid();
            
            if (GUILayout.Button("Destroy Grid"))
                grid.DestroyGrid();
        }
    }

    public class BoxCollider2DGrid : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D cellPrefab;
        [SerializeField] private Vector2 cellCount;
        [SerializeField] private Vector2 minPosition;
        [SerializeField] private Vector2 maxPosition;

        private float _cellSizeX;
        private float _cellSizeY;

        private void OnValidate()
        {
            if (minPosition.x > maxPosition.x - 0.1f)
                maxPosition.x = minPosition.x + 0.1f;
            
            if (minPosition.y > maxPosition.y - 0.1f)
                maxPosition.y = minPosition.y + 0.1f;
        }

        public void BuildGrid()
        {
            if (transform.childCount > 0)
                DestroyGrid();
            
            var cellNumber = 0;

            _cellSizeX = (maxPosition.x - minPosition.x) / cellCount.x;
            _cellSizeY = (maxPosition.y - minPosition.y) / cellCount.y;

            
            for (int y = 0; y < cellCount.y; y++)
            {
                for (int x = 0; x < cellCount.x; x++)
                {
                    var cellPosition = minPosition + new Vector2(_cellSizeX * x, _cellSizeY * y);
                    var cellCenterOffset = new Vector2(_cellSizeX * 0.5f, _cellSizeY * 0.5f);

                    var cell = Instantiate(cellPrefab, cellPosition + cellCenterOffset, Quaternion.identity, transform);

                    cell.size = new Vector2(_cellSizeX, _cellSizeY);

                    cell.name = $"Cell{cellNumber}";
                    cellNumber++;
                }
            }
        }

        public void DestroyGrid()
        {
            if (transform.childCount <= 0)
                return;

            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
    }
}