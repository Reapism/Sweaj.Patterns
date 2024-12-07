namespace Sweaj.Patterns.Math
{
    /// <summary>
    /// A set of extended mathematical formulas.
    /// </summary>
    public static class MathEx
    {
        public static double PercentIncrease(double startValue, double finalValue)
        {
            var difference = finalValue - startValue;
            var percent = (difference / System.Math.Abs(startValue)) * 100;

            return percent;
        }

        /// <summary>
        /// Percent error is the relative size of the difference between an experimental or estimated value, and the true, accepted value. 
        /// It compares the difference in values to the expected actual value and tells you how far off your experimental or observed value is.
        /// </summary>
        /// <param name="experimental">The Experimental value is the observed result of an experiment. Other terms you may see to represent this value are measured, observed, estimated and approximate.</param>
        /// <param name="theoretical">The Theoretical value in chemistry, physics or science experimentation in general, is the established ideal value you would expect as a result of an experiment.
        /// Other terms you may see to represent this value are accepted, actual, expected, exact and true. This value is in the denominator of the percent error equation.</param>
        /// <returns>The percent error.</returns>
        public static double PercentError(double experimental, double theoretical)
        {
            var difference = System.Math.Abs((experimental - theoretical) / theoretical);
            var percentError = difference * 100;

            return percentError;
        }


    }
}
