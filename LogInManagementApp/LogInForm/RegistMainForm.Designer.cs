namespace LogInForm
{
    partial class RegistMainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonRegit = new System.Windows.Forms.Button();
            this.buttonChange = new System.Windows.Forms.Button();
            this.buttonEnd = new System.Windows.Forms.Button();
            this.buttonView = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(38, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "ユーザー管理";
            // 
            // buttonRegit
            // 
            this.buttonRegit.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonRegit.Location = new System.Drawing.Point(67, 104);
            this.buttonRegit.Name = "buttonRegit";
            this.buttonRegit.Size = new System.Drawing.Size(150, 46);
            this.buttonRegit.TabIndex = 2;
            this.buttonRegit.Text = "新規登録";
            this.buttonRegit.UseVisualStyleBackColor = true;
            this.buttonRegit.Click += new System.EventHandler(this.buttonRegit_Click);
            // 
            // buttonChange
            // 
            this.buttonChange.Font = new System.Drawing.Font("MS UI Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonChange.Location = new System.Drawing.Point(67, 195);
            this.buttonChange.Name = "buttonChange";
            this.buttonChange.Size = new System.Drawing.Size(150, 46);
            this.buttonChange.TabIndex = 2;
            this.buttonChange.Text = "パスワード変更";
            this.buttonChange.UseVisualStyleBackColor = true;
            this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
            // 
            // buttonEnd
            // 
            this.buttonEnd.Font = new System.Drawing.Font("MS UI Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEnd.Location = new System.Drawing.Point(21, 387);
            this.buttonEnd.Name = "buttonEnd";
            this.buttonEnd.Size = new System.Drawing.Size(98, 39);
            this.buttonEnd.TabIndex = 2;
            this.buttonEnd.Text = "戻る";
            this.buttonEnd.UseVisualStyleBackColor = true;
            this.buttonEnd.Click += new System.EventHandler(this.buttonEnd_Click);
            // 
            // buttonView
            // 
            this.buttonView.Font = new System.Drawing.Font("MS UI Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonView.Location = new System.Drawing.Point(67, 294);
            this.buttonView.Name = "buttonView";
            this.buttonView.Size = new System.Drawing.Size(150, 46);
            this.buttonView.TabIndex = 2;
            this.buttonView.Text = "変更状況一覧";
            this.buttonView.UseVisualStyleBackColor = true;
            this.buttonView.Click += new System.EventHandler(this.buttonView_Click);
            // 
            // RegistMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 450);
            this.Controls.Add(this.buttonEnd);
            this.Controls.Add(this.buttonView);
            this.Controls.Add(this.buttonChange);
            this.Controls.Add(this.buttonRegit);
            this.Controls.Add(this.label1);
            this.Name = "RegistMainForm";
            this.Text = "RegistMainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonRegit;
        private System.Windows.Forms.Button buttonChange;
        private System.Windows.Forms.Button buttonEnd;
        private System.Windows.Forms.Button buttonView;
    }
}