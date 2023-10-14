using Fillers.Element;

namespace CharacterSystem.Player
{
    public interface IStatsReplenishable
    {
        void Replenish(Type type, float value);
    }
}