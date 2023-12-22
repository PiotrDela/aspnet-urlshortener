namespace UrlShortener.Domain.Exceptions
{

    [Serializable]
    public class DuplicatedEntryException : Exception
    {
        public DuplicatedEntryException() { }
        public DuplicatedEntryException(string message) : base(message) { }
        public DuplicatedEntryException(string message, Exception inner) : base(message, inner) { }
        protected DuplicatedEntryException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
