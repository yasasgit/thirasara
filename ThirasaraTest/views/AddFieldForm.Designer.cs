namespace ThirasaraTest
{
    partial class AddFieldForm
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
            this.btnSubmit = new System.Windows.Forms.Button();
            this.sizeHaTextBox = new System.Windows.Forms.TextBox();
            this.fieldLocationTextBox = new System.Windows.Forms.TextBox();
            this.soilNitrogenTextBox = new System.Windows.Forms.TextBox();
            this.soilPhosphorusTextBox = new System.Windows.Forms.TextBox();
            this.soilPotassiumTextBox = new System.Windows.Forms.TextBox();
            this.soilPhTextBox = new System.Windows.Forms.TextBox();
            this.soilTextureComboBox = new System.Windows.Forms.ComboBox();
            this.txtSoilTexture = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(623, 379);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 0;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // sizeHaTextBox
            // 
            this.sizeHaTextBox.Location = new System.Drawing.Point(225, 12);
            this.sizeHaTextBox.Name = "sizeHaTextBox";
            this.sizeHaTextBox.Size = new System.Drawing.Size(100, 20);
            this.sizeHaTextBox.TabIndex = 1;
            // 
            // fieldLocationTextBox
            // 
            this.fieldLocationTextBox.Location = new System.Drawing.Point(225, 78);
            this.fieldLocationTextBox.Name = "fieldLocationTextBox";
            this.fieldLocationTextBox.Size = new System.Drawing.Size(100, 20);
            this.fieldLocationTextBox.TabIndex = 2;
            // 
            // soilNitrogenTextBox
            // 
            this.soilNitrogenTextBox.Location = new System.Drawing.Point(225, 137);
            this.soilNitrogenTextBox.Name = "soilNitrogenTextBox";
            this.soilNitrogenTextBox.Size = new System.Drawing.Size(100, 20);
            this.soilNitrogenTextBox.TabIndex = 3;
            // 
            // soilPhosphorusTextBox
            // 
            this.soilPhosphorusTextBox.Location = new System.Drawing.Point(225, 209);
            this.soilPhosphorusTextBox.Name = "soilPhosphorusTextBox";
            this.soilPhosphorusTextBox.Size = new System.Drawing.Size(100, 20);
            this.soilPhosphorusTextBox.TabIndex = 4;
            // 
            // soilPotassiumTextBox
            // 
            this.soilPotassiumTextBox.Location = new System.Drawing.Point(225, 278);
            this.soilPotassiumTextBox.Name = "soilPotassiumTextBox";
            this.soilPotassiumTextBox.Size = new System.Drawing.Size(100, 20);
            this.soilPotassiumTextBox.TabIndex = 5;
            // 
            // soilPhTextBox
            // 
            this.soilPhTextBox.Location = new System.Drawing.Point(225, 346);
            this.soilPhTextBox.Name = "soilPhTextBox";
            this.soilPhTextBox.Size = new System.Drawing.Size(100, 20);
            this.soilPhTextBox.TabIndex = 6;
            // 
            // soilTextureComboBox
            // 
            this.soilTextureComboBox.FormattingEnabled = true;
            this.soilTextureComboBox.Items.AddRange(new object[] {
            "1000",
            "1001",
            "1002",
            "1003",
            "1004",
            "1005",
            "1006",
            "1007",
            "1008",
            "1009",
            "1010",
            "1011"});
            this.soilTextureComboBox.Location = new System.Drawing.Point(225, 403);
            this.soilTextureComboBox.Name = "soilTextureComboBox";
            this.soilTextureComboBox.Size = new System.Drawing.Size(121, 21);
            this.soilTextureComboBox.TabIndex = 7;
            // 
            // txtSoilTexture
            // 
            this.txtSoilTexture.Location = new System.Drawing.Point(398, 403);
            this.txtSoilTexture.Name = "txtSoilTexture";
            this.txtSoilTexture.Size = new System.Drawing.Size(100, 20);
            this.txtSoilTexture.TabIndex = 8;
            // 
            // AddFieldForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtSoilTexture);
            this.Controls.Add(this.soilTextureComboBox);
            this.Controls.Add(this.soilPhTextBox);
            this.Controls.Add(this.soilPotassiumTextBox);
            this.Controls.Add(this.soilPhosphorusTextBox);
            this.Controls.Add(this.soilNitrogenTextBox);
            this.Controls.Add(this.fieldLocationTextBox);
            this.Controls.Add(this.sizeHaTextBox);
            this.Controls.Add(this.btnSubmit);
            this.Name = "AddFieldForm";
            this.Text = "AddFieldForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TextBox sizeHaTextBox;
        private System.Windows.Forms.TextBox fieldLocationTextBox;
        private System.Windows.Forms.TextBox soilNitrogenTextBox;
        private System.Windows.Forms.TextBox soilPhosphorusTextBox;
        private System.Windows.Forms.TextBox soilPotassiumTextBox;
        private System.Windows.Forms.TextBox soilPhTextBox;
        private System.Windows.Forms.ComboBox soilTextureComboBox;
        private System.Windows.Forms.TextBox txtSoilTexture;
    }
}