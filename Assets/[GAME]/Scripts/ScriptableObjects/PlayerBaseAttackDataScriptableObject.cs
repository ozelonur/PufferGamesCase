using _GAME_.Scripts.Models;
using UnityEngine;

namespace _GAME_.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Player Base Attack Data", menuName = "Puffer Games/Player Base Attack Data", order = 1)]
    public class PlayerBaseAttackDataScriptableObject : ScriptableObject
    {
        public PlayerBaseAttackData data;
    }
}