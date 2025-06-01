using Sweaj.Patterns.Rest;

namespace Sweaj.Patterns.Tests.Rest
{
    public class PaginationInfoTests
    {
        [Fact]
        public void FromTotal_CalculatesTotalPagesCorrectly()
        {
            var pagination = PaginationInfo.FromTotal(10, 95);
            Assert.Equal(10, pagination.TotalPages);
            Assert.Equal(0, pagination.PageOffset);
            Assert.Equal(10, pagination.RowsPerPage);
            Assert.Equal(95, pagination.TotalRowCount);
        }

        [Fact]
        public void Create_CreatesValidInstance()
        {
            var pagination = PaginationInfo.Create(5, 2, 10, 50);
            Assert.Equal(5, pagination.TotalPages);
            Assert.Equal(2, pagination.PageOffset);
            Assert.Equal(10, pagination.RowsPerPage);
            Assert.Equal(50, pagination.TotalRowCount);
        }

        [Theory]
        [InlineData(-1, 0, 10, 50)]
        [InlineData(5, -1, 10, 50)]
        [InlineData(5, 5, 10, 50)]
        [InlineData(5, 2, -10, 50)]
        [InlineData(5, 2, 10, -50)]
        public void Create_Throws_WhenArgumentsInvalid(int totalPages, int pageOffset, long rowsPerPage, long totalRowCount)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => PaginationInfo.Create(totalPages, pageOffset, rowsPerPage, totalRowCount));
        }

        [Fact]
        public void NextPage_IncrementsOffsetCorrectly()
        {
            var pagination = PaginationInfo.Create(3, 0, 10, 30);
            var next = pagination.NextPage();
            Assert.Equal(1, next.PageOffset);
        }

        [Fact]
        public void PreviousPage_DecrementsOffsetCorrectly()
        {
            var pagination = PaginationInfo.Create(3, 1, 10, 30);
            var previous = pagination.PreviousPage();
            Assert.Equal(0, previous.PageOffset);
        }

        [Theory]
        [InlineData(0, true, false)]
        [InlineData(1, false, false)]
        [InlineData(2, false, true)]
        public void IsFirstAndLastPage_ReturnsCorrectly(int offset, bool isFirst, bool isLast)
        {
            var pagination = PaginationInfo.Create(3, offset, 10, 30);
            Assert.Equal(isFirst, pagination.IsFirstPage());
            Assert.Equal(isLast, pagination.IsLastPage());
        }

        [Theory]
        [InlineData(0, 10, 0, 9)]
        [InlineData(1, 10, 10, 19)]
        [InlineData(2, 10, 20, 29)]
        [InlineData(2, 10, 20, 25)]
        public void FirstAndLastRowIndex_CalculateCorrectly(int offset, long rowsPerPage, long expectedFirst, long expectedLast)
        {
            var pagination = PaginationInfo.Create(3, offset, rowsPerPage, expectedLast + 1);
            Assert.Equal(expectedFirst, pagination.FirstRowIndex());
            Assert.Equal(expectedLast, pagination.LastRowIndex());
        }

        [Theory]
        [InlineData(15, true)]
        [InlineData(9, false)]
        public void ContainsRow_ChecksCorrectRange(long rowIndex, bool expected)
        {
            var pagination = PaginationInfo.Create(3, 1, 10, 30);
            Assert.Equal(expected, pagination.ContainsRow(rowIndex));
        }

        [Theory]
        [InlineData(5, 0)]
        [InlineData(15, 1)]
        [InlineData(25, 2)]
        public void GetPageForRow_ReturnsCorrectPageIndex(long rowIndex, int expectedPage)
        {
            var pagination = PaginationInfo.Create(3, 0, 10, 30);
            Assert.Equal(expectedPage, pagination.GetPageForRow(rowIndex));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(30)]
        public void GetPageForRow_Throws_WhenRowIndexOutOfBounds(long rowIndex)
        {
            var pagination = PaginationInfo.Create(3, 0, 10, 30);
            Assert.Throws<ArgumentOutOfRangeException>(() => pagination.GetPageForRow(rowIndex));
        }

        [Fact]
        public void WithPage_ChangesPageOffsetCorrectly()
        {
            var pagination = PaginationInfo.Create(5, 2, 10, 50);
            var newPage = pagination.WithPage(3);
            Assert.Equal(3, newPage.PageOffset);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(5)]
        public void WithPage_Throws_WhenPageIndexOutOfRange(int pageIndex)
        {
            var pagination = PaginationInfo.Create(5, 2, 10, 50);
            Assert.Throws<ArgumentOutOfRangeException>(() => pagination.WithPage(pageIndex));
        }
    }

}
