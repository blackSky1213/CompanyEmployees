namespace Entities.Exceptions
{
    public sealed class CollectionByIdsBadRequestException : BadRequestException
    {
        public CollectionByIdsBadRequestException() :
            base("Colelction count mismatch comparing to ids.")
        { }
    }
}
