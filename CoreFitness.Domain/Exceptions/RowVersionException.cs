namespace CoreFitness.Domain.Exceptions
{
    public class MissingRowVersionException : DomainException
    {
        public MissingRowVersionException() : base("Rowversion is required") { }
    }
}
