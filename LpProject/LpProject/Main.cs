using System;
using System.Windows.Forms;

namespace LpProject
{
    using System.Drawing;
    using System.Globalization;
    using System.Linq;

    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            DbHelper.Load();
            this.bindingSource1.DataSource = DbHelper.Projects;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ModifyProject p = new ModifyProject();
            p.Show();
            DbHelper.CurrentProject = new Project()
            {
                Id = Guid.NewGuid(),
                Index = DbHelper.Projects.Max(r => r.Index) + 1
            };
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = new Guid(this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            DbHelper.CurrentProject = DbHelper.Projects.First(r => r.Id == id);
            ModifyProject p = new ModifyProject();
            p.Show();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(this.dataGridView1.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1, CultureInfo.CurrentUICulture),
                    e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.bindingSource1.DataSource = DbHelper.Projects.Where(r => this.comboBox1.SelectedItem.ToString() == "" || this.comboBox1.SelectedItem.ToString() == r.Type);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.bindingSource1.DataSource = DbHelper.Projects.Where(
                r => r.Code.Contains(this.textBox1.Text.Trim()) || r.Name.Contains(this.textBox1.Text.Trim()));
        }
    }
}
