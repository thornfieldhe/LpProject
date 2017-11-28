// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionSchedule.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   收款进度
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LpProject
{
    /// <summary>
    /// 收款进度
    /// </summary>
    public class CollectionSchedule
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
        /// 收款方式
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }
    }
}