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
        [SerializeField] private Transform grenadeSpawn;
        [SerializeField] private GameObject bombDown;
        
        

        #endregion

        #region Private Variables

        private Scene _simulationScene;
        private PhysicsScene _physicsScene;
        private GameObject _bombRangeIndicator;
        
        private int _maxPhysicsFrameIteration;
        private float _force;
        private float _tilling;

        #endregion

        #region MonoBehaviour Methods

        private void Start()
        {
            CreatePhysicsScene();
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

        #endregion

        #region Public Methods

        public void SimulateTrajectory(Grenade grenade, Vector3 position, Vector3 velocity)
        {
            Grenade ghostObj = Instantiate(grenade, position, Quaternion.identity);
            SceneManager.MoveGameObjectToScene(ghostObj.gameObject, _simulationScene);

            ghostObj.Init(velocity, true);

            line.positionCount = _maxPhysicsFrameIteration;

            for (int i = 0; i < _maxPhysicsFrameIteration; i++)
            {
                _physicsScene.Simulate(Time.fixedDeltaTime);
                line.SetPosition(i, ghostObj.transform.position);
            }

            Vector3 lastPoint = line.GetPosition(line.positionCount - 1);

            if (_bombRangeIndicator == null)
            {
                _bombRangeIndicator = Instantiate(bombDown, lastPoint, Quaternion.identity);
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

        #endregion
    }
}