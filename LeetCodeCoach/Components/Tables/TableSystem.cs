using LeetCodeCoach.Controllers.Data;
using LeetCodeCoach.Models;

namespace LeetCodeCoach.Components.Tables
{
    public class TableSystem : ComponentBase
    {
        private DataGridView DataGridView { get; set; } = new DataGridView();
    
        private DataGridViewTextBoxColumn questionId { get; set; } = new DataGridViewTextBoxColumn();
        private DataGridViewLinkColumn questionName { get; set; } = new DataGridViewLinkColumn();
        private DataGridViewTextBoxColumn questionDifficulty { get; set; } = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn questionCompletion { get; set; } = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn questionBest { get; set; } = new DataGridViewTextBoxColumn();

        private DataGridViewCellStyle dataGridViewCellStyle1 { get; set; } = new DataGridViewCellStyle();
        private DataGridViewCellStyle dataGridViewCellStyle2 { get; set; } = new DataGridViewCellStyle();
        private DataGridViewCellStyle dataGridViewCellStyle3 { get; set; } = new DataGridViewCellStyle();
        private DataGridViewCellStyle dataGridViewCellStyle4 { get; set; } = new DataGridViewCellStyle();
        private DataGridViewCellStyle dataGridViewCellStyle5 { get; set; } = new DataGridViewCellStyle();

        public TableSystem()
        {
            //
            // DataGridView metadata
            //
            DataGridView.Name = "DataGridSystem";
            DataGridView.RowHeadersVisible = false;
            DataGridView.EnableHeadersVisualStyles = false;
            DataGridView.BackgroundColor = Color.FromArgb(25, 25, 25);
            DataGridView.GridColor = Color.FromArgb(50, 50, 50);
            DataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 15, 15);
            DataGridView.RowHeadersDefaultCellStyle.ForeColor = Color.White;
            DataGridView.RowHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            DataGridView.Size = new Size(593, 665);
            DataGridView.TabIndex = 8;
            
            DataGridView.AdvancedColumnHeadersBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.Single;
            DataGridView.AdvancedColumnHeadersBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single;
            DataGridView.AdvancedColumnHeadersBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.Single;
            DataGridView.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single;
            DataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 15, 15);
            //
            // Create columns
            //
            DataGridView.Columns.AddRange(new DataGridViewColumn[] { questionId, questionName, questionDifficulty, questionCompletion, questionBest });
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
            // Cell Style - questionId
            //
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(15, 15, 15);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            //
            // Cell Style - questionName
            //
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(15, 15, 15);
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            //
            // Cell Style - questionDifficulty
            //
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(15, 15, 15);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            //
            // Cell Style - questionCompletion
            //
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = Color.White;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(15, 15, 15);
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            //
            // Cell Style - questionBest
            //
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(15, 15, 15);
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            //
            // Functions
            //
            DataGridView.CellContentClick += DataGridView_CellContentClick;
            // 
            // questionId
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            questionId.DefaultCellStyle = dataGridViewCellStyle1;
            questionId.HeaderText = "ID";
            questionId.Name = "questionId";
            questionId.Width = 40;
            questionId.ReadOnly = true;
            // 
            // questionName
            // 
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            questionName.DefaultCellStyle = dataGridViewCellStyle2;
            questionName.HeaderText = "Name";
            questionName.Name = "questionName";
            questionName.Width = 300;
            questionName.ReadOnly = true;
            // 
            // questionDifficulty
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            questionDifficulty.DefaultCellStyle = dataGridViewCellStyle3;
            questionDifficulty.HeaderText = "Difficulty";
            questionDifficulty.Name = "questionDifficulty";
            questionDifficulty.Width = 75;
            questionDifficulty.ReadOnly = true;
            // 
            // questionCompletion
            // 
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            questionCompletion.DefaultCellStyle = dataGridViewCellStyle4;
            questionCompletion.HeaderText = "Completion";
            questionCompletion.Name = "questionCompletion";
            questionCompletion.Width = 90;
            questionCompletion.ReadOnly = true;
            //
            // questionBest
            //
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            questionBest.DefaultCellStyle = dataGridViewCellStyle5;
            questionBest.HeaderText = "Best";
            questionBest.Name = "questionBest";
            questionBest.Width = 85;
            questionBest.ReadOnly = true;
        }
        
        public DataGridView Build()
        {
            return DataGridView;
        }

        private void DataGridView_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                DataGridViewCell cell = DataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell is DataGridViewLinkCell linkCell && linkCell.Tag is string url)
                {
                    try
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = url,
                            UseShellExecute = true
                        });

                        int questionId = int.Parse(DataGridView.Rows[e.RowIndex].Cells[0].Value?.ToString() ?? "0");
                        List<string>? questionTags = Data.Instance.Topics?[_form.CurrentTopicId].questions[e.RowIndex].tags ?? new List<string>();
                        string questionTitle = linkCell.Value?.ToString() ?? string.Empty;
                        string questionDifficulty = DataGridView.Rows[e.RowIndex].Cells[2].Value?.ToString() ?? string.Empty;
                        string questionLink = linkCell.Tag?.ToString() ?? string.Empty;

                        _form.CurrentQuestion = new Question {
                            id = questionId, 
                            user_id = User.Id,
                            question_id = questionId,
                            topic_id = int.Parse(_form.CurrentTopicId),
                            tags = questionTags,
                            title = questionTitle, 
                            difficulty = questionDifficulty, 
                            url = questionLink
                        };

                        // Timer.PanelSize.Width = questionTitle.Length * 10 + 50;
                        Timer.SetPanelWidth(questionTitle.Length * 20); // Adjusted width for the timer panel

                        Panel QuestionTimer = Timer.Build();
                        QuestionTimer.Location = new Point((_form.Width / 2) - (QuestionTimer.Width / 2), (_form.Height / 2) - (QuestionTimer.Height / 2));
                        
                        _form.TablesPanel?.Controls.Add(QuestionTimer);
                        _form.TablesPanel?.Controls.SetChildIndex(QuestionTimer, 0);

                        _form.CurrentQuestionTime = DateTime.UtcNow;
                        Timer.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to open the link: {ex.Message}");
                    }
                }
            }
        }

    }
}