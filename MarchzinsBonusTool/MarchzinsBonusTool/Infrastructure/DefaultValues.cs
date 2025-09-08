namespace MarchzinsBonusTool.Infrastructure
{
    /// <summary>
    /// Default values for calculations.
    /// </summary>
    public class DefaultValues
    {
        public decimal Sparkapital { get; set; } = 100000m;
        public decimal NormalerZinssatz { get; set; } = 2.5m;
        public decimal BonusZinssatz { get; set; } = 5.0m;
        public decimal Steuersatz { get; set; } = 35m;
    }
}