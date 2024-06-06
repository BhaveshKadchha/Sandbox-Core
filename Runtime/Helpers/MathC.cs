namespace Sandbox.Helper
{
    public sealed class MathC
    {
        public static float Map(float currentOrigin, float currentEnd, float targetOrigin, float targetEnd, float t)
        {
            return ULerp(targetOrigin, targetEnd, UInverseLerp(currentOrigin, currentEnd, t));
        }

        public static float ULerp(float a, float b, float t)
        {
            return (1 - t) * a + t * b;
        }

        public static float UInverseLerp(float a, float b, float t)
        {
            return (t - a) / (b - a);
        }
    }
}