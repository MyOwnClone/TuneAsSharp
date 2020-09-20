namespace TuneAsSharp
{
    public static class OtherModule
    {
        public static int MethodWithTweakableValue()
        {
            return (int) TweakAS.tv(334);
        }
    }
}