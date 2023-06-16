namespace ThirasaraTest
{
    partial class FarmerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FarmerForm));
            this.lblNic = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.fieldDataGridView = new System.Windows.Forms.DataGridView();
            this.btnPnD = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.fertilizerDataGridView = new System.Windows.Forms.DataGridView();
            this.btnAddField = new System.Windows.Forms.Button();
            this.cropCyleDataGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.environmentDataGridView = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnPredict = new System.Windows.Forms.Button();
            this.lblNicNo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fieldDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fertilizerDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cropCyleDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.environmentDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNic
            // 
            this.lblNic.AutoSize = true;
            this.lblNic.BackColor = System.Drawing.Color.Transparent;
            this.lblNic.Location = new System.Drawing.Point(1231, 25);
            this.lblNic.Name = "lblNic";
            this.lblNic.Size = new System.Drawing.Size(29, 16);
            this.lblNic.TabIndex = 9;
            this.lblNic.Text = "text";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.Navy;
            this.label6.Location = new System.Drawing.Point(441, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(229, 26);
            this.label6.TabIndex = 18;
            this.label6.Text = "Cultivator Dashboard";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Navy;
            this.label7.Location = new System.Drawing.Point(51, 155);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 19);
            this.label7.TabIndex = 19;
            this.label7.Text = "My Field Data";
            // 
            // fieldDataGridView
            // 
            this.fieldDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.fieldDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.fieldDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fieldDataGridView.Location = new System.Drawing.Point(28, 193);
            this.fieldDataGridView.Name = "fieldDataGridView";
            this.fieldDataGridView.RowHeadersWidth = 51;
            this.fieldDataGridView.Size = new System.Drawing.Size(392, 170);
            this.fieldDataGridView.TabIndex = 23;
            this.fieldDataGridView.SelectionChanged += new System.EventHandler(this.fieldDataGridView_SelectionChanged);
            // 
            // btnPnD
            // 
            this.btnPnD.Location = new System.Drawing.Point(148, 623);
            this.btnPnD.Name = "btnPnD";
            this.btnPnD.Size = new System.Drawing.Size(147, 37);
            this.btnPnD.TabIndex = 24;
            this.btnPnD.Text = "Pests and Diseases";
            this.btnPnD.UseVisualStyleBackColor = true;
            this.btnPnD.Click += new System.EventHandler(this.btnPnD_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.LightPink;
            this.btnLogout.Location = new System.Drawing.Point(1234, 623);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 31);
            this.btnLogout.TabIndex = 25;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // fertilizerDataGridView
            // 
            this.fertilizerDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.fertilizerDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.fertilizerDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fertilizerDataGridView.Location = new System.Drawing.Point(883, 184);
            this.fertilizerDataGridView.Name = "fertilizerDataGridView";
            this.fertilizerDataGridView.RowHeadersWidth = 51;
            this.fertilizerDataGridView.Size = new System.Drawing.Size(406, 179);
            this.fertilizerDataGridView.TabIndex = 26;
            // 
            // btnAddField
            // 
            this.btnAddField.Location = new System.Drawing.Point(28, 623);
            this.btnAddField.Name = "btnAddField";
            this.btnAddField.Size = new System.Drawing.Size(101, 37);
            this.btnAddField.TabIndex = 27;
            this.btnAddField.Text = "Add Field";
            this.btnAddField.UseVisualStyleBackColor = true;
            this.btnAddField.Click += new System.EventHandler(this.btnAddField_Click);
            // 
            // cropCyleDataGridView
            // 
            this.cropCyleDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.cropCyleDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cropCyleDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cropCyleDataGridView.Location = new System.Drawing.Point(28, 438);
            this.cropCyleDataGridView.Name = "cropCyleDataGridView";
            this.cropCyleDataGridView.RowHeadersWidth = 51;
            this.cropCyleDataGridView.Size = new System.Drawing.Size(1281, 162);
            this.cropCyleDataGridView.TabIndex = 28;
            this.cropCyleDataGridView.SelectionChanged += new System.EventHandler(this.cropCyleDataGridView_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(464, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 19);
            this.label1.TabIndex = 29;
            this.label1.Text = "My Environment Data";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(51, 391);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 19);
            this.label3.TabIndex = 30;
            this.label3.Text = "My Crops";
            // 
            // environmentDataGridView
            // 
            this.environmentDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.environmentDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.environmentDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.environmentDataGridView.Location = new System.Drawing.Point(447, 194);
            this.environmentDataGridView.Name = "environmentDataGridView";
            this.environmentDataGridView.RowHeadersWidth = 51;
            this.environmentDataGridView.Size = new System.Drawing.Size(408, 169);
            this.environmentDataGridView.TabIndex = 31;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Navy;
            this.label8.Location = new System.Drawing.Point(911, 155);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(149, 19);
            this.label8.TabIndex = 32;
            this.label8.Text = "Required Fertilizer";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Location = new System.Drawing.Point(12, 10);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(232, 104);
            this.pictureBox2.TabIndex = 33;
            this.pictureBox2.TabStop = false;
            // 
            // btnPredict
            // 
            this.btnPredict.Location = new System.Drawing.Point(316, 623);
            this.btnPredict.Name = "btnPredict";
            this.btnPredict.Size = new System.Drawing.Size(147, 37);
            this.btnPredict.TabIndex = 34;
            this.btnPredict.Text = "Predict Crops";
            this.btnPredict.UseVisualStyleBackColor = true;
            this.btnPredict.Click += new System.EventHandler(this.btnPredict_Click);
            // 
            // lblNicNo
            // 
            this.lblNicNo.AutoSize = true;
            this.lblNicNo.BackColor = System.Drawing.Color.Transparent;
            this.lblNicNo.Location = new System.Drawing.Point(1189, 25);
            this.lblNicNo.Name = "lblNicNo";
            this.lblNicNo.Size = new System.Drawing.Size(27, 16);
            this.lblNicNo.TabIndex = 35;
            this.lblNicNo.Text = "NIC";
            // 
            // FarmerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGreen;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1350, 681);
            this.Controls.Add(this.lblNicNo);
            this.Controls.Add(this.btnPredict);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.environmentDataGridView);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cropCyleDataGridView);
            this.Controls.Add(this.btnAddField);
            this.Controls.Add(this.fertilizerDataGridView);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnPnD);
            this.Controls.Add(this.fieldDataGridView);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblNic);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FarmerForm";
            this.Text = "Thirasara Dashboard";
            this.Load += new System.EventHandler(this.UserForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fieldDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fertilizerDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cropCyleDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.environmentDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblNic;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView fieldDataGridView;
        private System.Windows.Forms.Button btnPnD;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.DataGridView fertilizerDataGridView;
        private System.Windows.Forms.Button btnAddField;
        private System.Windows.Forms.DataGridView cropCyleDataGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView environmentDataGridView;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnPredict;
        private System.Windows.Forms.Label lblNicNo;
    }
}