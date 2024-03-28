using UnityEngine;

namespace _GAME_.Scripts.EditorHelpers
{
    public class Waypoint : MonoBehaviour
    {
        #region Serialized Fields

        [Header("Configurations")] [SerializeField]
        private float radius;

        [SerializeField] private Color color;

        #endregion

        private void OnDrawGizmos()
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}