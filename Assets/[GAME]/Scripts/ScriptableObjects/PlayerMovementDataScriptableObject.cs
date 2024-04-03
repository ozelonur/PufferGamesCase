using _GAME_.Scripts.Models;
using UnityEngine;

namespace _GAME_.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Player Movement Data", menuName = "Puffer Games/Player Movement Data", order = 1)]
    public class PlayerMovementDataScriptableObject : ScriptableObject
    {
        public PlayerMovementData movementData;
    }
}