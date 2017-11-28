// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Project.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   项目主表
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LpProject
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 项目主表
    /// </summary>
    public class Project
    {
        /// <summary>
        /// 序号
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 合同顺序号
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 合同名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 合同总金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 签约人
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 合同应收款
        /// </summary>
        public decimal TotalAmount2 { get; set; }

        /// <summary>
        /// 签约日期
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 收款质保金
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// 合同类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 收款进度
        /// </summary>
        public List<CollectionSchedule> Collections { get; set; }

        /// <summary>
        /// 开票情况
        /// </summary>
        public List<InvoiceDetail> Invoices { get; set; }

        /// <summary>
        /// 费用明细
        /// </summary>
        public List<CostDetails> Costs { get; set; }
    }
}