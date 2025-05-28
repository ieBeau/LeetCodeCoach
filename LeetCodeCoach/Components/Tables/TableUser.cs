namespace LeetCodeCoach.Components.Tables
{
    public class TableUser : ComponentBase
    {
        private DataGridView DataGridView { get; set; } = new DataGridView();

        private DataGridViewTextBoxColumn questionNumber { get; set; } = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn questionId { get; set; } = new DataGridViewTextBoxColumn();
        private DataGridViewLinkColumn questionName { get; set; } = new DataGridViewLinkColumn();
        private DataGridViewTextBoxColumn questionDifficulty { get; set; } = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn questionTime { get; set; } = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn questionDate { get; set; } = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn questionDelete { get; set; } = new DataGridViewTextBoxColumn();


        private DataGridViewCellStyle dataGridViewCellStyle1 { get; set; } = new DataGridViewCellStyle();
        private DataGridViewCellStyle dataGridViewCellStyle2 { get; set; } = new DataGridViewCellStyle();
        private DataGridViewCellStyle dataGridViewCellStyle3 { get; set; } = new DataGridViewCellStyle();
        private DataGridViewCellStyle dataGridViewCellStyle4 { get; set; } = new DataGridViewCellStyle();
        private DataGridViewCellStyle dataGridViewCellStyle5 { get; set; } = new DataGridViewCellStyle();
        private DataGridViewCellStyle dataGridViewCellStyle6 { get; set; } = new DataGridViewCellStyle();
        private DataGridViewCellStyle dataGridViewCellStyle7 { get; set; } = new DataGridViewCellStyle();

        public TableUser()
        {
            //
            // DataGridView metadata
            //
            DataGridView.Name = "DataGridUser";
            DataGridView.RowHeadersVisible = false;
            DataGridView.EnableHeadersVisualStyles = false;
            DataGridView.BackgroundColor = Color.FromArgb(25, 25, 25);
            DataGridView.GridColor = Color.FromArgb(50, 50, 50);
            DataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 15, 15);
            DataGridView.RowHeadersDefaultCellStyle.ForeColor = Color.White;
            DataGridView.RowHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            DataGridView.Size = new Size(694, 665);
            DataGridView.TabIndex = 8;

            DataGridView.AdvancedColumnHeadersBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.Single;
            DataGridView.AdvancedColumnHeadersBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single;
            DataGridView.AdvancedColumnHeadersBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.Single;
            DataGridView.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single;
            DataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 15, 15);
            //
            // Create columns
            //
            DataGridView.Columns.AddRange(new DataGridViewColumn[] { questionNumber, questionId, questionName, questionDifficulty, questionTime, questionDate, questionDelete });
            //
            // Cell Style - headers
            //
            DataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            DataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 15, 15);
            DataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
            DataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
            DataGridView.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridView.AllowUserToResizeColumns = false;
            DataGridView.AllowUserToResizeRows = false;
            DataGridView.AllowUserToAddRows = false;
            DataGridView.AllowUserToDeleteRows = false;
            DataGridView.AllowUserToOrderColumns = false;
            //
            // Cell Style - questionNumber
            //
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(15, 15, 15);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            //
            // Cell Style - questionId
            //
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(15, 15, 15);
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            //
            // Cell Style - questionName
            //
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(15, 15, 15);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            //
            // Cell Style - questionDifficulty
            //
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = Color.White;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(15, 15, 15);
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            //
            // Cell Style - questionTime
            //
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(15, 15, 15);
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            //
            // Cell Style - questionDate
            //
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = Color.White;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(15, 15, 15);
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            //
            // Cell Style - questionDelete
            //
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle7.ForeColor = Color.Red;
            dataGridViewCellStyle7.BackColor = Color.FromArgb(15, 15, 15);
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.False;
            //
            // Function
            //
            DataGridView.CellContentClick += OnClick_Question;
            DataGridView.CellContentClick += OnClick_Delete;
            DataGridView.CellMouseEnter += OnMouseEnter_Delete;
            DataGridView.CellMouseLeave += OnMouseLeave_Delete;
            // 
            // questionNumber
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            questionNumber.DefaultCellStyle = dataGridViewCellStyle1;
            questionNumber.HeaderText = "#";
            questionNumber.Name = "questionNumber";
            questionNumber.Width = 35;
            questionNumber.ReadOnly = true;
            // 
            // questionId
            // 
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            questionId.DefaultCellStyle = dataGridViewCellStyle2;
            questionId.HeaderText = "ID";
            questionId.Name = "questionId";
            questionId.Width = 35;
            questionId.ReadOnly = true;
            // 
            // questionName
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            questionName.DefaultCellStyle = dataGridViewCellStyle3;
            questionName.HeaderText = "Name";
            questionName.Name = "questionName";
            questionName.Width = 300;
            questionName.ReadOnly = true;
            // 
            // questionDifficulty
            // 
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            questionDifficulty.DefaultCellStyle = dataGridViewCellStyle4;
            questionDifficulty.HeaderText = "Difficulty";
            questionDifficulty.Name = "questionDifficulty";
            questionDifficulty.Width = 75;
            questionDifficulty.ReadOnly = true;
            // 
            // questionTime
            // 
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            questionTime.DefaultCellStyle = dataGridViewCellStyle5;
            questionTime.HeaderText = "Time";
            questionTime.Name = "questionTime";
            questionTime.Width = 85;
            questionTime.ReadOnly = true;
            //
            // questionDate
            //
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            questionDate.DefaultCellStyle = dataGridViewCellStyle6;
            questionDate.HeaderText = "Date";
            questionDate.Name = "questionDate";
            questionDate.Width = 85;
            questionDate.ReadOnly = true;
            //
            // questionDelete
            //
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            questionDelete.DefaultCellStyle = dataGridViewCellStyle7;
            questionDelete.HeaderText = "Delete";
            questionDelete.Name = "questionDelete";
            questionDelete.Width = 75;
            questionDelete.ReadOnly = true;
        }

        public DataGridView Build()
        {
            return DataGridView;
        }

        private void OnMouseEnter_Delete(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 6) // Assuming the link is in column 1
            {
                DataGridView.Cursor = Cursors.Hand;
            }
        }

        private void OnMouseLeave_Delete(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 6) // Assuming the link is in column 1
            {
                DataGridView.Cursor = Cursors.Default;
            }
        }

        private void OnClick_Question(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1) // Assuming the link is in column 1
            {
                var linkCell = DataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell;
                if (linkCell != null && linkCell.Tag is string url)
                {
                    try
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = url,
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to open the link: {ex.Message}");
                    }
                }
            }
        }

        private void OnClick_Delete(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 6) // Assuming the link is in column 1
            {
                if (MessageBox.Show("Are you sure you want to delete this question?", "Delete Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        int questionId = (int)DataGridView.Rows[e.RowIndex].Cells[0].Value;

                        DataGridView.Rows.RemoveAt(e.RowIndex);

                        _form.UpdateDeleteQuestion(questionId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to delete question: {ex.Message}");
                    }
                }
            }
        }
    }
}