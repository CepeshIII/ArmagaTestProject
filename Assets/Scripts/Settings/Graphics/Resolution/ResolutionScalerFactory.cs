public static class ResolutionScalerFactory
{
    public static IResolutionScaler Create()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        return new WebResolutionScaler();
#else
        return new DefaultResolutionScaler();
#endif
    }
}
