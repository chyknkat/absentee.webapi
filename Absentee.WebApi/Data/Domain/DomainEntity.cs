namespace Absentee.WebApi.Data.Domain
{
    public abstract class DomainEntity
    {
        public virtual int Id { get; protected set; }

        protected DomainEntity() { }

        protected DomainEntity(int value)
        {
            Id = value;
        }

        public override bool Equals(object otherEntity)
        {
            // check nullity - equality with null is ALWAYS false.
            if (otherEntity == null)
            {
                return false;
            }
            // check symmetry - reference equality
            if (this == otherEntity)
            {
                return true;
            }
            // check value equality
            var other = (DomainEntity)otherEntity;
            if (!Id.Equals(other.Id))
            {
                return false;
            }
            // true if all checks are done.
            return true;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}