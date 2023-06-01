namespace ThirasaraTest
{
    partial class PestDiseaseForm
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
            this.txtApriori = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtApriori
            // 
            this.txtApriori.Location = new System.Drawing.Point(12, 16);
            this.txtApriori.Multiline = true;
            this.txtApriori.Name = "txtApriori";
            this.txtApriori.Size = new System.Drawing.Size(606, 298);
            this.txtApriori.TabIndex = 0;
            // 
            // PestDiseaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 330);
            this.Controls.Add(this.txtApriori);
            this.Name = "PestDiseaseForm";
            this.Text = "PestDiseaseForm";
            this.Load += new System.EventHandler(this.PestDiseaseForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtApriori;
    }
}