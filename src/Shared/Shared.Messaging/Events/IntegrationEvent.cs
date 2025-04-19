namespace Shared.Messaging.Events
{
    public record IntegrationEvent
    {
        public static Guid EventId => Guid.NewGuid();
        public static DateTime OccurredOn => DateTime.Now;
        public string? EventType => GetType().AssemblyQualifiedName;
    }
}
