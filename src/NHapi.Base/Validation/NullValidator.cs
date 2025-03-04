namespace NHapi.Base.Validation
{
    using NHapi.Base.Model;

    /// <summary>
    /// Validator that does nothing.
    /// </summary>
    public class NullValidator : IMessageValidator
    {
        /// <inheritdoc/>
        public bool Validate(IMessage message)
        {
            return true;
        }

        /// <inheritdoc/>
        public bool Validate(string message, bool isXML, string version)
        {
            return true;
        }
    }
}
