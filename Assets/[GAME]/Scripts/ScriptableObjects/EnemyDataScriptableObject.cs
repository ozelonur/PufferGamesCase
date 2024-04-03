using _GAME_.Scripts.Models;
using UnityEngine;

namespace _GAME_.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Enemy Data", menuName = "Puffer Games/Enemy Data", order = 1)]
    public class EnemyDataScriptableObject : ScriptableObject
    {
        public EnemyData data;
    }
}