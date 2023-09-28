// Functions using cubic easing for determining the probability weight of an event,
// based on actual distance to the target and "desired" distance to the target (the goldilocks zone).
// Ranges from 0 to 1.
public class ProximityWeighting
{
    // Calculates weights based on difference between current distance and "desired" distance (far ___/\___ close)
    public static float GetWeightFromProximityPinpoint(float current, float target)
    {
        float normalized = Mathf.Min((current - target) / target, 1);
        return Mathf.Pow(1 - normalized, 3);
    }
    // Inclusive returns a weight of 1 if distance is within the target radius (far ___/--- close)
    public static float GetWeightFromProximityInclusive(float current, float target)
    {
        if (current < target) return 1f;
        float normalized = Mathf.Min((current - target) / target, 1);
        return Mathf.Pow(1 - normalized, 3);
    }
    // Exclusive returns a weight of 1 if distance is outside the target radius (far ---\___ close)
    public static float GetWeightFromProximityExclusive(float current, float target)
    {
        if (current > target return 1f);
        float normalized = Mathf.Min((current - target) / target, 1);
        return Mathf.Pow(1 - normalized, 3);
    }
}