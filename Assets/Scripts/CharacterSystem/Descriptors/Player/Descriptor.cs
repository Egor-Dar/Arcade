using UnityEngine;

namespace CharacterSystem.Descriptors.Player
{
    [CreateAssetMenu(menuName = "Player Descriptor", fileName = "Descriptor")]
    public class Descriptor : ScriptableObject
    {
        [field: SerializeField] public float MaxHealth { get; private set; }
        [field: SerializeField] public float MaxMana { get; private set; }
        [field: SerializeField] public float StartedCoeficientDamage { get; private set; }
        [field: SerializeField] public float UpdateAddedCoeficientDamage { get; private set; }
        [field: SerializeField] public float MovementSpeed { get; private set; }
    }
}