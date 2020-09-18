namespace TweakAsSharp
{
    public class OtherModule
    {
        public static int MethodWithTweakableValue()
        {
            return (int) TweakAS.tv(333);
        }
    }
}