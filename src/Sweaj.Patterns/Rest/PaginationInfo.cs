using System;

namespace Sweaj.Patterns.Rest
{
    /// <summary>
    /// Represents pagination information for a dataset, including total pages, current offset, rows per page, and total row count.
    /// </summary>
    public sealed class PaginationInfo
    {
        public PaginationInfo(int totalPages, int pageOffset, long rowsPerPage, long totalRowCount)
        {
            TotalPages = totalPages;
            PageOffset = pageOffset;
            RowsPerPage = rowsPerPage;
            TotalRowCount = totalRowCount;
        }

        /// <summary>
        /// Gets the total number of pages available based on the total row count and rows per page.
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// Gets the current page offset (zero-based).
        /// </summary>
        public int PageOffset { get; }

        /// <summary>
        /// Gets the number of rows displayed per page.
        /// </summary>
        public long RowsPerPage { get; }

        /// <summary>
        /// Gets the total number of rows across all pages.
        /// </summary>
        public long TotalRowCount { get; }

        /// <summary>
        /// Returns a new <see cref="PaginationInfo"/> instance representing the next page.
        /// </summary>
        /// <returns>
        /// A new instance with an incremented <see cref="PageOffset"/>, or the current instance if already on the last page.
        /// </returns>
        public PaginationInfo NextPage()
        {
            if (PageOffset >= TotalPages - 1)
            {
                return this;
            }

            return new PaginationInfo(TotalPages, PageOffset + 1, RowsPerPage, TotalRowCount);
        }

        /// <summary>
        /// Returns a new <see cref="PaginationInfo"/> instance representing the previous page.
        /// </summary>
        /// <returns>
        /// A new instance with a decremented <see cref="PageOffset"/>, or the current instance if already on the first page.
        /// </returns>
        public PaginationInfo PreviousPage()
        {
            if (PageOffset <= 0)
            {
                return this;
            }

            return new PaginationInfo(TotalPages, PageOffset - 1, RowsPerPage, TotalRowCount);
        }

        /// <summary>
        /// Determines whether the current page is the first page.
        /// </summary>
        /// <returns><c>true</c> if the current page is the first; otherwise, <c>false</c>.</returns>
        public bool IsFirstPage() => PageOffset == 0;

        /// <summary>
        /// Determines whether the current page is the last page.
        /// </summary>
        /// <returns><c>true</c> if the current page is the last; otherwise, <c>false</c>.</returns>
        public bool IsLastPage() => PageOffset >= TotalPages - 1;

        /// <summary>
        /// Gets the zero-based index of the first row on the current page.
        /// </summary>
        /// <returns>The row index of the first row.</returns>
        public long FirstRowIndex() => PageOffset * RowsPerPage;

        /// <summary>
        /// Gets the zero-based index of the last row on the current page.
        /// </summary>
        /// <returns>The row index of the last row.</returns>
        public long LastRowIndex()
        {
            var lastRow = (PageOffset + 1) * RowsPerPage - 1;
            return lastRow >= TotalRowCount ? TotalRowCount - 1 : lastRow;
        }

        /// <summary>
        /// Determines whether the specified row index is within the current page.
        /// </summary>
        /// <param name="rowIndex">The zero-based row index to test.</param>
        /// <returns><c>true</c> if the row is on the current page; otherwise, <c>false</c>.</returns>
        public bool ContainsRow(long rowIndex)
        {
            return rowIndex >= FirstRowIndex() && rowIndex <= LastRowIndex();
        }

        /// <summary>
        /// Gets the page number that contains the specified row index.
        /// </summary>
        /// <param name="rowIndex">The zero-based index of the row.</param>
        /// <returns>The zero-based page index that contains the specified row.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the row index is out of bounds.</exception>
        public int GetPageForRow(long rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= TotalRowCount)
            {
                throw new ArgumentOutOfRangeException(nameof(rowIndex), "Row index must be within the bounds of the total row count.");
            }

            return (int)(rowIndex / RowsPerPage);
        }

        /// <summary>
        /// Creates a new <see cref="PaginationInfo"/> instance for a specified page index.
        /// </summary>
        /// <param name="pageIndex">The zero-based page index to move to.</param>
        /// <returns>A new instance representing the specified page.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the page index is out of range.</exception>
        public PaginationInfo WithPage(int pageIndex)
        {
            if (pageIndex < 0 || pageIndex >= TotalPages)
            {
                throw new ArgumentOutOfRangeException(nameof(pageIndex), "Page index is out of range.");
            }

            return new PaginationInfo(TotalPages, pageIndex, RowsPerPage, TotalRowCount);
        }

        /// <summary>
        /// Creates a <see cref="PaginationInfo"/> instance based on the total number of rows and rows per page.
        /// </summary>
        /// <param name="rowsPerPage">The number of rows to be displayed per page. Must be greater than 0.</param>
        /// <param name="totalRowCount">The total number of rows in the dataset. Must be greater than 0.</param>
        /// <returns>A new <see cref="PaginationInfo"/> instance initialized with calculated total pages and default offset.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="rowsPerPage"/> or <paramref name="totalRowCount"/> are out of valid range.
        /// </exception>
        public static PaginationInfo FromTotal(long rowsPerPage, long totalRowCount)
        {
            Guard.Against.NegativeOrZero(totalRowCount);
            Guard.Against.NegativeOrZero(rowsPerPage);
            Guard.Against.OutOfRange(rowsPerPage, nameof(rowsPerPage), 0, totalRowCount);

            var (totalPages, remainder) = System.Math.DivRem(totalRowCount, rowsPerPage);
            if (remainder > 0)
            {
                totalPages++;
            }

            if (totalPages > int.MaxValue || totalPages < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(totalPages), "Total pages is out of range.");
            }

            const int pageOffset = 0;

            return new PaginationInfo((int)totalPages, pageOffset, rowsPerPage, totalRowCount);
        }

        /// <summary>
        /// Creates a <see cref="PaginationInfo"/> instance with the specified parameters.
        /// </summary>
        /// <param name="totalPages">The total number of pages. Must be non-negative and less than or equal to <see cref="int.MaxValue"/>.</param>
        /// <param name="pageOffset">The current page offset. Must be between 0 and <paramref name="totalPages"/> - 1.</param>
        /// <param name="rowsPerPage">The number of rows per page. Must be greater than zero.</param>
        /// <param name="totalRowCount">The total number of rows. Must be non-negative.</param>
        /// <returns>A new instance of <see cref="PaginationInfo"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when any parameter is outside of the expected range.</exception>
        public static PaginationInfo Create(int totalPages, int pageOffset, long rowsPerPage, long totalRowCount)
        {
            if (totalPages < 0 || totalPages > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(totalPages), "Total pages must be non-negative and within range of int.");

            if (pageOffset < 0 || pageOffset >= totalPages)
                throw new ArgumentOutOfRangeException(nameof(pageOffset), "Page offset must be within the range of total pages.");

            if (rowsPerPage <= 0)
                throw new ArgumentOutOfRangeException(nameof(rowsPerPage), "Rows per page must be greater than zero.");

            if (totalRowCount < 0)
                throw new ArgumentOutOfRangeException(nameof(totalRowCount), "Total row count must be non-negative.");

            return new PaginationInfo(totalPages, pageOffset, rowsPerPage, totalRowCount);
        }

    }
}
