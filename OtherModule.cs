namespace TuneAsSharp
{
    public static class OtherModule
    {
        public static int MethodWithTweakableValue()
        {
            return (int) Tune.tv(334);
        }
    }
}