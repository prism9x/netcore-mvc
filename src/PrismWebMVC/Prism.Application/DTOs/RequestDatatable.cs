using Microsoft.AspNetCore.Mvc;

namespace Prism.Application.DTOs
{
    /// <summary>
    /// Đối tượng khi request dữ liệu khi call api datatable
    /// </summary>
    public class RequestDatatable
    {
        /// <summary>
        /// Kích thước size bảng
        /// </summary>
        [BindProperty(Name = "length")]
        public int PageSize { get; set; }

        /// <summary>
        /// Page Index
        /// </summary>
        [BindProperty(Name = "start")]
        public int SkipItems { get; set; }

        /// <summary>
        /// Từ khoá cho việc tìm kiếm
        /// </summary>
        [BindProperty(Name = "search[value]")]
        public string? Keyword { get; set; }

        public int Draw { get; set; }
    }
}