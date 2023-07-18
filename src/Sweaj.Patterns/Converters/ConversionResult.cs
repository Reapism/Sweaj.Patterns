using Sweaj.Patterns.Data.Services;

namespace Sweaj.Patterns.Converters
{
    /// <summary>
    /// An object that safely encapsulates whether 
    /// the conversion succeeded or failed.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class ConversionResult<TResult> : IValueProvider<TResult>
    {
        private ConversionResult(TResult? result, bool isSuccessful, string errorMessage = "")
        {
            IsSuccessful = isSuccessful;
            Value = isSuccessful ? Guard.Against.Null(result) : default;
            ErrorMessage = errorMessage;
        }
        /// <summary>
        /// The converted value.
        /// </summary>
        /// <remarks>If the conversion was successful, a non null Value can be accessed, otherwise <see langword="default"/>.</remarks>
        public TResult? Value { get; }

        /// <summary>
        /// A value indicated whether the conversion was successful.
        /// </summary>
        public bool IsSuccessful { get; }

        /// <summary>
        /// A message indicating the error for the result of an unsuccessful conversion.
        /// </summary>
        public string ErrorMessage { get; }

        /// <summary>
        /// Creates a successful <see cref="ConversionResult{TResult}"/>.
        /// </summary>
        /// <param name="result">The conversion result value.</param>
        /// <returns></returns>
        public static ConversionResult<TResult> FromSuccessful([NotNull, ValidatedNotNull] TResult result)
        {
            return new ConversionResult<TResult>(Guard.Against.Null(result, nameof(result)), true);
        }

        /// <summary>
        /// Creates a failed <see cref="ConversionResult{TResult}"/> with an errorMessage.
        /// </summary>
        /// <param name="errorMessage">The error message resulting in a failure.</param>
        /// <returns></returns>
        public static ConversionResult<TResult> FromFailure(string errorMessage)
        {
            return new ConversionResult<TResult>(default, false, errorMessage);
        }
    }
}
