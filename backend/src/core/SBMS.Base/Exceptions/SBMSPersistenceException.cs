namespace SBMS.Base.Exceptions
{
    public class SBMSPersistenceException : ApplicationException
    {
        public string MessageLogger { get; }

        public SBMSPersistenceException(string message, string messageLogger)
            : base(message)
        {
            // Concatenar el mensaje base con la info adicional
            MessageLogger = $"{message} - {messageLogger}";
        }
    }
}