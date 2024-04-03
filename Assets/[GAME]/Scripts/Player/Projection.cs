using _GAME_.Scripts.Player.Bullet;
using OrangeBear.EventSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _GAME_.Scripts.Player
{
    public class Projection : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private Transform obstacleParent;

        [SerializeField] private LineRenderer line;
        [SerializeField] private Grenade grenadePrefab;
        [SerializeField] private OilBomb oilBombPrefab;
        [SerializeField] private Transform grenadeSpawn;
        [SerializeField] private GameObject bombDownPrefab;
        [SerializeField] private Transform realGrenadeSpawnTransform;
        [SerializeField] private float glidingDuration = .1f;
        

        #endregion

        #region Private Variables

        private Scene _simulationScene;
        private PhysicsScene _physicsScene;
        private GameObject _bombRangeIndicator;
        
        private int _maxPhysicsFrameIteration;
        private float _force;
        private float _tilling;

        private Grenade _spawnedGrenade;
        private OilBomb _spawnedOilBomb;

        #endregion

        #region MonoBehaviour Methods

        private void Start()
        {
            CreatePhysicsScene();
        }

        #endregion

        #region Public Methods

        public void SimulateTrajectory(Grenade grenade, Vector3 position, Vector3 velocity)
        {
            Grenade ghostObj = Instantiate(grenade, position, Quaternion.identity);
            SceneManager.MoveGameObjectToScene(ghostObj.gameObject, _simulationScene);

            ghostObj.Init(velocity);

            line.positionCount = _maxPhysicsFrameIteration;

            for (int i = 0; i < _maxPhysicsFrameIteration; i++)
            {
                _physicsScene.Simulate(Time.fixedDeltaTime);
                line.SetPosition(i, ghostObj.transform.position);
            }

            Vector3 lastPoint = line.GetPosition(line.positionCount - 1);

            if (_bombRangeIndicator == null)
            {
                _bombRangeIndicator = Instantiate(bombDownPrefab, lastPoint, Quaternion.identity);
            }
            else
            {
                _bombRangeIndicator.transform.position = lastPoint;
            }

            Destroy(ghostObj.gameObject);
        }

        public void SimulateStatus(bool status, int frameCount, float force, float tilling)
        {
            line.enabled = status;

            if (!status)
            {
                _bombRangeIndicator.SetActive(false);
            }

            _maxPhysicsFrameIteration = frameCount;
            _force = force;
            line.material.mainTextureScale = new Vector2(tilling, line.material.mainTextureScale.y);
            
            
            SimulateTrajectory(grenadePrefab, grenadeSpawn.position, grenadeSpawn.forward * _force);
        }

        public void SpawnGrenade()
        {
            _spawnedGrenade = Instantiate(grenadePrefab, realGrenadeSpawnTransform);
            _spawnedGrenade.DisablePhysics();
            _spawnedGrenade.transform.localPosition = Vector3.zero;
            _spawnedGrenade.transform.localEulerAngles = Vector3.zero;
        }

        public void SpawnOilBomb()
        {
            _spawnedOilBomb = Instantiate(oilBombPrefab, realGrenadeSpawnTransform);
            _spawnedOilBomb.DisablePhysics();
            _spawnedOilBomb.transform.localPosition = Vector3.zero;
            _spawnedOilBomb.transform.localEulerAngles = Vector3.zero;
        }

        public void ThrowGrenadeToTarget()
        {
            if (_spawnedGrenade == null)
            {
                return;
            }

            _spawnedGrenade.transform.parent = null;
            
            FollowPath();
        }

        public void ThrowOilBombToTarget()
        {
            if (_spawnedOilBomb == null)
            {
                return;
            }

            _spawnedOilBomb.transform.parent = null;
            
            FollowPathOilBomb();
        }

        public void DisableLineAndIndicator()
        {
            if (_bombRangeIndicator != null)
            {
                Destroy(_bombRangeIndicator);
            }

            line.enabled = false;
        }

        #endregion
        
        

        #region Private Methods

        private void CreatePhysicsScene()
        {
            _simulationScene =
                SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
            _physicsScene = _simulationScene.GetPhysicsScene();

            foreach (Transform obj in obstacleParent)
            {
                GameObject ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
                ghostObj.GetComponent<Renderer>().enabled = false;
                SceneManager.MoveGameObjectToScene(ghostObj, _simulationScene);
            }
        }

        private void FollowPath()
        {
            Vector3[] positions = new Vector3[line.positionCount];
            line.GetPositions(positions);

            line.enabled = false;
            Destroy(_bombRangeIndicator);

            int highestPointIndex = FindHighestPointIndex(positions);
            Vector3[] path = ExtractPathFromHighestPoint(positions, highestPointIndex);

            float pathLength = CalculatePathLength(path);
            float duration = pathLength / glidingDuration;

            _spawnedGrenade.Go(path, duration);

        }
        
        private void FollowPathOilBomb()
        {
            Vector3[] positions = new Vector3[line.positionCount];
            line.GetPositions(positions);

            line.enabled = false;
            Destroy(_bombRangeIndicator);

            int highestPointIndex = FindHighestPointIndex(positions);
            Vector3[] path = ExtractPathFromHighestPoint(positions, highestPointIndex);

            float pathLength = CalculatePathLength(path);
            float duration = pathLength / glidingDuration;

            _spawnedOilBomb.Go(path, duration);

        }

        int FindHighestPointIndex(Vector3[] positions)
        {
            int highestPointIndex = 0;
            float highestY = float.NegativeInfinity;
            for (int i = 0; i < positions.Length; i++)
            {
                if (positions[i].y > highestY)
                {
                    highestY = positions[i].y;
                    highestPointIndex = i;
                }
            }
            return highestPointIndex;
        }

        private Vector3[] ExtractPathFromHighestPoint(Vector3[] positions, int highestPointIndex)
        {
            int pathLength = positions.Length - highestPointIndex;
            Vector3[] path = new Vector3[pathLength];
            for (int i = 0; i < pathLength; i++)
            {
                path[i] = positions[highestPointIndex + i];
            }
            return path;
        }

        private float CalculatePathLength(Vector3[] path)
        {
            float length = 0f;
            for (int i = 0; i < path.Length - 1; i++)
            {
                length += Vector3.Distance(path[i], path[i + 1]);
            }
            return length;
        }

        #endregion
    }
}