namespace BusinessSolutions.Common.Core.Validation
{
    public interface IEntityValidator<in Entity> where Entity : class
    {
        ValidationResult Validate(Entity entity);
    }    
}
