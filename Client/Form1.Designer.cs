namespace Client
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
            SendButton = new Button();
            SecondText = new TextBox();
            FirstText = new TextBox();
            ResultLabel = new Label();
            SuspendLayout();
            // 
            // SendButton
            // 
            SendButton.Location = new Point(195, 186);
            SendButton.Name = "SendButton";
            SendButton.Size = new Size(75, 23);
            SendButton.TabIndex = 0;
            SendButton.Text = "Send";
            SendButton.UseVisualStyleBackColor = true;
            SendButton.Click += SendButton_Click;
            // 
            // SecondText
            // 
            SecondText.Location = new Point(322, 144);
            SecondText.Name = "SecondText";
            SecondText.Size = new Size(100, 23);
            SecondText.TabIndex = 1;
            // 
            // FirstText
            // 
            FirstText.Location = new Point(53, 144);
            FirstText.Name = "FirstText";
            FirstText.Size = new Size(100, 23);
            FirstText.TabIndex = 2;
            // 
            // ResultLabel
            // 
            ResultLabel.AutoSize = true;
            ResultLabel.Location = new Point(210, 59);
            ResultLabel.Name = "ResultLabel";
            ResultLabel.Size = new Size(39, 15);
            ResultLabel.TabIndex = 3;
            ResultLabel.Text = "Result";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(464, 236);
            Controls.Add(ResultLabel);
            Controls.Add(FirstText);
            Controls.Add(SecondText);
            Controls.Add(SendButton);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button SendButton;
        private TextBox SecondText;
        private TextBox FirstText;
        private Label ResultLabel;
    }
}
