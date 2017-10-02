namespace OptimizationTests
{
    /// <summary>
    /// Transient: New instance is created for each request
    /// Singleton: Same instance is returned for each request
    /// </summary>
    public enum LifeCycle
    {
        Transient,
        Singleton
    }
}
