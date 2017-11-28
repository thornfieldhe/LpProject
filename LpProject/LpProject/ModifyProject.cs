using System;
using System.Windows.Forms;

namespace LpProject
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;

    public partial class ModifyProject : Form
    {
        public ModifyProject()
        {
            InitializeComponent();
            BindBaseInfo();
            this.bindingSource1.DataSource = DbHelper.CurrentProject.Costs;
            this.bindingSource2.DataSource = DbHelper.CurrentProject.Invoices;
            this.bindingSource3.DataSource = DbHelper.CurrentProject.Collections;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DbHelper.CurrentProject.Name = this.textBox1.Text.Trim();
            DbHelper.CurrentProject.Code = this.textBox11.Text.Trim();
            DbHelper.CurrentProject.User = this.textBox10.Text.Trim();
            DbHelper.CurrentProject.Date = this.dateTimePicker1.Text.Trim();
            DbHelper.CurrentProject.Type = this.comboBox1.Text.Trim();
            DbHelper.CurrentProject.Year = this.comboBox2.Text.Trim();
            if (decimal.TryParse(this.textBox6.Text.Trim(), out var totalAmount))
            {
                DbHelper.CurrentProject.TotalAmount = totalAmount;
            }
            else
            {
                MessageBox.Show("合同金额不为数字");
            }

            if (decimal.TryParse(this.textBox4.Text.Trim(), out var amount))
            {
                DbHelper.CurrentProject.Amount = amount;
            }
            else
            {
                MessageBox.Show("收款质保金不为数字");
            }

            if (decimal.TryParse(this.textBox5.Text.Trim(), out var totalAmount2))
            {
                DbHelper.CurrentProject.TotalAmount2 = totalAmount2;
            }
            else
            {
                MessageBox.Show("合同应收款不为数字");
            }

            if (DbHelper.Projects.All(r => r.Id != DbHelper.CurrentProject.Id))
            {
                DbHelper.Projects.Add(DbHelper.CurrentProject);
            }


            DbHelper.Save();

            MessageBox.Show("合同基本信息保存成功");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.tabControl1.SelectedIndex)
            {
                case 0:
                    BindBaseInfo();
                    break;
                case 1:
                    this.bindingSource1.DataSource = DbHelper.Projects.First(r => r.Id == DbHelper.CurrentProject.Id).Costs;
                    break;
            }
        }

        private void BindBaseInfo()
        {
            if (DbHelper.CurrentProject == null)
            {
                DbHelper.CurrentProject = new Project();
            }

            this.textBox1.Text = DbHelper.CurrentProject.Name;
            this.textBox11.Text = DbHelper.CurrentProject.Code;
            this.textBox10.Text = DbHelper.CurrentProject.User;
            this.dateTimePicker1.Text = DbHelper.CurrentProject.Date;
            this.comboBox1.Text = DbHelper.CurrentProject.Type;
            this.comboBox2.Text = DbHelper.CurrentProject.Year;
            this.textBox5.Text = DbHelper.CurrentProject.TotalAmount2.ToString();
            this.textBox4.Text = DbHelper.CurrentProject.Amount.ToString();
            this.textBox6.Text = DbHelper.CurrentProject.TotalAmount.ToString();
            this.textBox7.Text = DbHelper.CurrentProject.Index.ToString();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(this.dataGridView1.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1, CultureInfo.CurrentUICulture),
                    e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void button2_Click(object sender, EventArgs e)
        {
            var list = new List<CostDetails>();
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                var cost = new CostDetails();
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("请选择日期");
                }
                else
                {
                    cost.Date = row.Cells[0].Value.ToString();
                }

                if (row.Cells[1].Value != null)
                {
                    cost.Summary = row.Cells[1].Value.ToString();
                }

                if (row.Cells[2].Value == null)
                {
                    MessageBox.Show("请选择付款方式");
                }
                else
                {
                    cost.Type = row.Cells[2].Value.ToString();
                }

                if (row.Cells[3].Value == null)
                {
                    MessageBox.Show("请录入金额");
                }
                else
                {
                    if (decimal.TryParse(row.Cells[3].Value.ToString(), out var amount))
                    {
                        cost.Amount = amount;
                    }
                }

                if (row.Cells[4].Value == null)
                {
                    MessageBox.Show("请选择成本类型");
                }
                else
                {
                    cost.Category = row.Cells[4].Value.ToString();
                }

                if (row.Cells[5].Value != null)
                {
                    cost.Note = row.Cells[5].Value.ToString();
                }

                list.Add(cost);
            }

            DbHelper.CurrentProject.Costs = list;
            DbHelper.Save();
            MessageBox.Show("成本明细保存成功");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.bindingSource1.DataSource = DbHelper.CurrentProject.Costs.Where(
                r => r.Summary.Contains(this.textBox2.Text.Trim()) || r.Note.Contains(this.textBox2.Text.Trim()));
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.bindingSource1.DataSource = DbHelper.CurrentProject.Costs.Where(r => this.comboBox3.SelectedItem.ToString() == "" || this.comboBox3.SelectedItem.ToString() == r.Category);
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.bindingSource2.DataSource = DbHelper.CurrentProject.Invoices.Where(r => this.comboBox4.SelectedItem.ToString() == "" || this.comboBox4.SelectedItem.ToString() == r.Type);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var list = new List<InvoiceDetail>();
            foreach (DataGridViewRow row in this.dataGridView2.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                var cost = new InvoiceDetail();
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("请选择日期");
                }
                else
                {
                    cost.Date = row.Cells[0].Value.ToString();
                }

                if (row.Cells[1].Value == null)
                {
                    MessageBox.Show("请选开票类型");
                }
                else
                {
                    cost.Type = row.Cells[1].Value.ToString();
                }

                if (row.Cells[2].Value == null)
                {
                    MessageBox.Show("请录入金额");
                }
                else
                {
                    if (decimal.TryParse(row.Cells[2].Value.ToString(), out var amount))
                    {
                        cost.Amount = amount;
                    }
                }

                if (row.Cells[3].Value != null)
                {
                    cost.User = row.Cells[3].Value.ToString();
                }

                list.Add(cost);
            }

            DbHelper.CurrentProject.Invoices = list;
            DbHelper.Save();
            MessageBox.Show("开票信息保存成功");
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.bindingSource1.DataSource = DbHelper.CurrentProject.Costs.Where(r => this.comboBox6.SelectedItem.ToString() == "" || this.comboBox6.SelectedItem.ToString() == r.Type);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var list = new List<CollectionSchedule>();
            foreach (DataGridViewRow row in this.dataGridView3.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                var cost = new CollectionSchedule();
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("请选择日期");
                }
                else
                {
                    cost.Date = row.Cells[0].Value.ToString();
                }

                if (row.Cells[1].Value != null)
                {
                    cost.Summary = row.Cells[1].Value.ToString();
                }

                if (row.Cells[2].Value == null)
                {
                    MessageBox.Show("请选择收款方式");
                }
                else
                {
                    cost.Type = row.Cells[2].Value.ToString();
                }

                if (row.Cells[3].Value == null)
                {
                    MessageBox.Show("请录入金额");
                }
                else
                {
                    if (decimal.TryParse(row.Cells[3].Value.ToString(), out var amount))
                    {
                        cost.Amount = amount;
                    }
                }

                if (row.Cells[4].Value != null)
                {
                    cost.Note = row.Cells[4].Value.ToString();
                }

                list.Add(cost);
            }

            DbHelper.CurrentProject.Collections = list;
            DbHelper.Save();
            MessageBox.Show("收款进度保存成功");
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            this.bindingSource3.DataSource = DbHelper.CurrentProject.Collections.Where(
                r => r.Summary.Contains(this.textBox3.Text.Trim()) || r.Note.Contains(this.textBox3.Text.Trim()));
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.bindingSource3.DataSource = DbHelper.CurrentProject.Collections.Where(r => this.comboBox5.SelectedItem.ToString() == "" || this.comboBox5.SelectedItem.ToString() == r.Type);
        }
    }
}
