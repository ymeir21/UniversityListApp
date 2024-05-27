namespace UniversityListApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            statisticsLabel = new Label();
            universityList = new ListBox();
            CountriesDataGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)CountriesDataGridView).BeginInit();
            SuspendLayout();
            // 
            // statisticsLabel
            // 
            statisticsLabel.AutoSize = true;
            statisticsLabel.Location = new Point(23, 395);
            statisticsLabel.Name = "statisticsLabel";
            statisticsLabel.Size = new Size(50, 20);
            statisticsLabel.TabIndex = 1;
            statisticsLabel.Text = "label1";
            // 
            // universityList
            // 
            universityList.FormattingEnabled = true;
            universityList.Location = new Point(695, 38);
            universityList.Name = "universityList";
            universityList.Size = new Size(353, 344);
            universityList.TabIndex = 3;
            // 
            // CountriesDataGridView
            // 
            CountriesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            CountriesDataGridView.Location = new Point(23, 38);
            CountriesDataGridView.MultiSelect = false;
            CountriesDataGridView.Name = "CountriesDataGridView";
            CountriesDataGridView.ReadOnly = true;
            CountriesDataGridView.RowHeadersWidth = 51;
            CountriesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            CountriesDataGridView.Size = new Size(653, 344);
            CountriesDataGridView.TabIndex = 4;
            CountriesDataGridView.MouseDoubleClick += CountriesDataGridView_MouseDoubleClick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1066, 504);
            Controls.Add(CountriesDataGridView);
            Controls.Add(universityList);
            Controls.Add(statisticsLabel);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)CountriesDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label statisticsLabel;
        private ListBox universityList;
        private DataGridView CountriesDataGridView;
    }
}
