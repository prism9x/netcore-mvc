using Prism.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.Application.DTOs
{
    /// <summary>
    /// Đối tượng trả về khi truy vấn login
    /// </summary>
    public class ResponseModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public ActionType Action {  get; set; } = ActionType.Get;
    }
}
