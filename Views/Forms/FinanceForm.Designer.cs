namespace HomeFinanceApp.Views
{
    partial class FinanceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FinanceForm));
            table = new PictureBox();
            savingsPicture = new PictureBox();
            moneysPicture = new PictureBox();
            moneysLabel = new Label();
            savingsLabel = new Label();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)table).BeginInit();
            ((System.ComponentModel.ISupportInitialize)savingsPicture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)moneysPicture).BeginInit();
            SuspendLayout();
            // 
            // table
            // 
            table.Image = (Image)resources.GetObject("table.Image");
            table.Location = new Point(139, 90);
            table.Name = "table";
            table.Size = new Size(648, 352);
            table.SizeMode = PictureBoxSizeMode.StretchImage;
            table.TabIndex = 0;
            table.TabStop = false;
            // 
            // savingsPicture
            // 
            savingsPicture.Image = (Image)resources.GetObject("savingsPicture.Image");
            savingsPicture.Location = new Point(607, 144);
            savingsPicture.Name = "savingsPicture";
            savingsPicture.Size = new Size(70, 46);
            savingsPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            savingsPicture.TabIndex = 1;
            savingsPicture.TabStop = false;
            // 
            // moneysPicture
            // 
            moneysPicture.Image = (Image)resources.GetObject("moneysPicture.Image");
            moneysPicture.Location = new Point(434, 181);
            moneysPicture.Name = "moneysPicture";
            moneysPicture.Size = new Size(72, 34);
            moneysPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            moneysPicture.TabIndex = 3;
            moneysPicture.TabStop = false;
            moneysPicture.Visible = false;
            // 
            // moneysLabel
            // 
            moneysLabel.AutoSize = true;
            moneysLabel.Location = new Point(443, 158);
            moneysLabel.Name = "moneysLabel";
            moneysLabel.Size = new Size(0, 20);
            moneysLabel.TabIndex = 4;
            // 
            // savingsLabel
            // 
            savingsLabel.AutoSize = true;
            savingsLabel.Location = new Point(617, 121);
            savingsLabel.Name = "savingsLabel";
            savingsLabel.Size = new Size(0, 20);
            savingsLabel.TabIndex = 5;
            // 
            // button1
            // 
            button1.Location = new Point(837, 12);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 6;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // FinanceForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(968, 521);
            Controls.Add(button1);
            Controls.Add(savingsLabel);
            Controls.Add(moneysLabel);
            Controls.Add(moneysPicture);
            Controls.Add(savingsPicture);
            Controls.Add(table);
            Name = "FinanceForm";
            Text = "FinanceForm";
            ((System.ComponentModel.ISupportInitialize)table).EndInit();
            ((System.ComponentModel.ISupportInitialize)savingsPicture).EndInit();
            ((System.ComponentModel.ISupportInitialize)moneysPicture).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox table;
        private PictureBox savingsPicture;
        private PictureBox moneysPicture;
        private Label moneysLabel;
        private Label savingsLabel;
        private Button button1;
    }
}