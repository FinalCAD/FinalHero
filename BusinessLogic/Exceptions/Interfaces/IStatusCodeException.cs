


namespace BusinessLogic.Exceptions
{
    /// <summary>
    /// Custom exception with status code
    /// </summary>
    public interface IStatusCodeException
    {
        public int Status { get; set; }
    }
}
