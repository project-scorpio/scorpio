namespace Scorpio.Application.Dtos
{
    /// <summary>
    /// This interface is defined to standardize to set "Total Count of Items" to a DTO.
    /// </summary>

    public interface ITotalCount
    {
        /// <summary>
        /// Total count of Items.
        /// </summary>
        long TotalCount { get; set; }

    }
}
