using PathCreation;
using Unity.VisualScripting;
using UnityEngine;

namespace PathCreation.Examples {

    [ExecuteInEditMode]
    public class PathPlacer : PathSceneTool {

        [SerializeField] private int maxObjects;
        public GameObject prefab1;
        public GameObject prefab2;
        public GameObject holder;
        public GameObject[] ballsOnPath;
        public GameObject[] groovesOnPath;
        public int BallArrayIndex;
        public int GrooveArrayIndex;
        public float spacing = 3;

        const float minSpacing = .1f;

        void Generate () {
            if (pathCreator != null && prefab1 != null && holder != null) {
                DestroyObjects ();

                VertexPath path = pathCreator.path;
                
                spacing = Mathf.Max(minSpacing, spacing);
                float dst = 0;
                ballsOnPath = new GameObject[maxObjects];
                groovesOnPath = new GameObject[maxObjects];
                BallArrayIndex = 0;
                GrooveArrayIndex = 0;
                while (dst < path.length) {
                    Vector3 point = path.GetPointAtDistance (dst);
                    Quaternion rot = path.GetRotationAtDistance (dst);

                    ballsOnPath[BallArrayIndex] = Instantiate (prefab1, point, rot, holder.transform);
                    ballsOnPath[BallArrayIndex].GetComponent<PositionTaker>().MyDistanceOnPath = dst;

                    groovesOnPath[GrooveArrayIndex] = Instantiate(prefab2, point, rot, holder.transform);
                    groovesOnPath[GrooveArrayIndex].GetComponent<Groove>().MyDistanceOnPath = dst;
                    GrooveArrayIndex++;

                    BallArrayIndex++;
                    dst += spacing;
                }
            }
        }

        void DestroyObjects () {
            int numChildren = holder.transform.childCount;
            for (int i = numChildren - 1; i >= 0; i--) {
                DestroyImmediate (holder.transform.GetChild (i).gameObject, false);
            }
        }

        protected override void PathUpdated () {
            if (pathCreator != null) {
                Generate ();
            }
        }
    }
}