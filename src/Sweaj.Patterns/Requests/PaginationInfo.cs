namespace Sweaj.Patterns.Requests
{
    public sealed class PaginationInfo
    {
        private PaginationInfo(int totalPages, int pageOffset, long rowsPerPage, long totalRowCount)
        {
            TotalPages = totalPages;
            PageOffset = pageOffset;
            RowsPerPage = rowsPerPage;
            TotalRowCount = totalRowCount;
        }

        public int TotalPages { get; }
        public int PageOffset { get; }
        public long RowsPerPage { get; }
        public long TotalRowCount { get; }

        /// <summary>
        /// The next page.
        /// </summary>
        /// <returns>A new <see cref="PaginationInfo"/> object representing the next page, otherwise itself.</returns>
        public PaginationInfo NextPage()
        {
            if (PageOffset >= TotalPages)
            {
                return this;
            }

            return new PaginationInfo(TotalPages, PageOffset + 1, RowsPerPage, TotalRowCount);
        }

        /// <summary>
        /// The previous  page.
        /// </summary>
        /// <returns>A new <see cref="PaginationInfo"/> object representing the next page, otherwise itself.</returns>
        public PaginationInfo PreviousPage()
        {
            if (PageOffset <= 1)
            {
                return this;
            }

            return new PaginationInfo(TotalPages, PageOffset - 1, RowsPerPage, TotalRowCount);
        }

        public static PaginationInfo FromPage(int totalPages, long rowsPerPage)
        {
            var totalRowCount = Guard.Against.NegativeOrZero(totalPages) * Guard.Against.NegativeOrZero(rowsPerPage);
            const int pageOffset = 0;
            return new PaginationInfo(totalPages, pageOffset, rowsPerPage, totalRowCount);
        }


        public static PaginationInfo FromTotal(long rowsPerPage, long totalRowCount)
        {
            Guard.Against.NegativeOrZero(totalRowCount);
            Guard.Against.NegativeOrZero(rowsPerPage);
            Guard.Against.OutOfRange(rowsPerPage, nameof(rowsPerPage), 0, totalRowCount);

            var (totalPages, remainder) = Math.DivRem(totalRowCount, rowsPerPage);
            if (remainder > 0)
            {
                totalPages++;
            }

            if (totalPages > int.MaxValue || totalPages == -1)
            {
                throw new ArgumentOutOfRangeException("Total pages is out of range.");
            }

            const int pageOffset = 0;

            return new PaginationInfo((int)totalPages, pageOffset, rowsPerPage, totalRowCount);
        }
    }
}
