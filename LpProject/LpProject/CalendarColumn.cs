// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarColumn.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   Summary
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LpProject
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// DataGridView自定义添加DateTimePicker控件日期列 参考http://msdn.microsoft.com/en-us/library/7tas5c80.aspx
    /// 涂聚文 缔友计算机信息技术有限公司
    /// 2011-11-16 捷为工作室
    /// </summary>
    public class GeovinDuCalendarColumn : DataGridViewColumn
    {
        /// <summary>
        ///
        /// </summary>
        public GeovinDuCalendarColumn()
            : base(new CalendarCell())
        {
        }
        /// <summary>
        ///
        /// </summary>
        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {

                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(CalendarCell)))
                {
                    throw new InvalidCastException("Must be a CalendarCell");
                }
                base.CellTemplate = value;
            }
        }
    }
    /// <summary>
    /// DataGridView 添加日期列
    /// 涂聚文 缔友计算机信息技术有限公司
    /// 2011-11-16 捷为工作室
    /// </summary>
    public class CalendarCell : DataGridViewTextBoxCell
    {

        public CalendarCell()
            : base()
        {

            this.Style.Format = "d";
        }

        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {

            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            CalendarEditingControl ctl =
                DataGridView.EditingControl as CalendarEditingControl;

            if (this.Value == null)
            {
                ctl.Value = (DateTime)this.DefaultNewRowValue;
            }
            else
            {
                ctl.Value = DateTime.Parse(this.Value.ToString());
            }
        }

        public override Type EditType
        {
            get
            {

                return typeof(CalendarEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {


                return typeof(DateTime);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {

                return DateTime.Now;
            }
        }
    }

    /// <summary>
    ///DataGridView 添加日期列
    /// 涂聚文 缔友计算机信息技术有限公司
    /// 2011-11-16 捷为工作室
    /// </summary>
    class CalendarEditingControl : DateTimePicker, IDataGridViewEditingControl
    {
        DataGridView dataGridView;
        private bool valueChanged = false;
        int rowIndex;

        public CalendarEditingControl()
        {
            this.Format = DateTimePickerFormat.Short;
        }


        public object EditingControlFormattedValue
        {
            get
            {
                return this.Value.ToShortDateString();
            }
            set
            {
                if (value is String)
                {
                    try
                    {

                        this.Value = DateTime.Parse((String)value);
                    }
                    catch
                    {

                        this.Value = DateTime.Now;
                    }
                }
            }
        }


        public object GetEditingControlFormattedValue(
            DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }


        public void ApplyCellStyleToEditingControl(
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.CalendarForeColor = dataGridViewCellStyle.ForeColor;
            this.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
        }


        public int EditingControlRowIndex
        {
            get
            {
                return rowIndex;
            }
            set
            {
                rowIndex = value;
            }
        }


        public bool EditingControlWantsInputKey(
            Keys key, bool dataGridViewWantsInputKey)
        {
            // Let the DateTimePicker handle the keys listed.
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return !dataGridViewWantsInputKey;
            }
        }


        public void PrepareEditingControlForEdit(bool selectAll)
        {
            // No preparation needs to be done.
        }


        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }


        public DataGridView EditingControlDataGridView
        {
            get
            {
                return dataGridView;
            }
            set
            {
                dataGridView = value;
            }
        }


        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }
            set
            {
                valueChanged = value;
            }
        }


        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }

        protected override void OnValueChanged(EventArgs eventargs)
        {

            valueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnValueChanged(eventargs);
        }
    }
}