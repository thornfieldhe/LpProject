// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CostDetails.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   成本明细
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LpProject
{
    /// <summary>
    /// 成本明细
    /// </summary>
    public class CostDetails
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 付款金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 成本类型
        /// </summary>
        public string Category { get; set; }
    }
}