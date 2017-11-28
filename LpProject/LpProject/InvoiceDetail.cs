// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvoiceDetail.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   开票请款
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LpProject
{
    /// <summary>
    /// 开票请款
    /// </summary>
    public class InvoiceDetail
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 开票类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 签收人
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 开票金额
        /// </summary>
        public decimal Amount { get; set; }
    }
}