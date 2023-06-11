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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PestDiseaseForm));
            this.txtApriori = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtApriori
            // 
            this.txtApriori.Location = new System.Drawing.Point(97, 205);
            this.txtApriori.Multiline = true;
            this.txtApriori.Name = "txtApriori";
            this.txtApriori.Size = new System.Drawing.Size(645, 211);
            this.txtApriori.TabIndex = 0;
            // 
            // PestDiseaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(830, 469);
            this.Controls.Add(this.txtApriori);
            this.Name = "PestDiseaseForm";
            this.Text = "Pests and Diseases";
            this.Load += new System.EventHandler(this.PestDiseaseForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtApriori;
    }
}