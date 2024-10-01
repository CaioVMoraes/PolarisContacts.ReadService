using PolarisContacts.ReadService.Domain.Enuns;

namespace PolarisContacts.ReadService.Domain
{
    public class EntityMessage
    {
        public OperationType Operation { get; set; }
        public EntityType EntityType { get; set; }
        public object EntityData { get; set; }
    }
}
