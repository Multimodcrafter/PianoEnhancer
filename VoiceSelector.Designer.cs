namespace PianoEnhancer
{
    partial class VoiceSelector
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
            this.PresetLabel = new System.Windows.Forms.Label();
            this.PresetDropDown = new System.Windows.Forms.ComboBox();
            this.PresetSaveButton = new System.Windows.Forms.Button();
            this.PresetDeleteButton = new System.Windows.Forms.Button();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.SplitPointLabel = new System.Windows.Forms.Label();
            this.SplitPointComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // PresetLabel
            // 
            this.PresetLabel.AutoSize = true;
            this.PresetLabel.Location = new System.Drawing.Point(13, 13);
            this.PresetLabel.Name = "PresetLabel";
            this.PresetLabel.Size = new System.Drawing.Size(60, 25);
            this.PresetLabel.TabIndex = 0;
            this.PresetLabel.Text = "Preset";
            // 
            // PresetDropDown
            // 
            this.PresetDropDown.FormattingEnabled = true;
            this.PresetDropDown.Location = new System.Drawing.Point(79, 10);
            this.PresetDropDown.Name = "PresetDropDown";
            this.PresetDropDown.Size = new System.Drawing.Size(296, 33);
            this.PresetDropDown.TabIndex = 1;
            this.PresetDropDown.SelectedIndexChanged += new System.EventHandler(this.PresetDropDownSelectedIndexChanged);
            // 
            // PresetSaveButton
            // 
            this.PresetSaveButton.Location = new System.Drawing.Point(381, 10);
            this.PresetSaveButton.Name = "PresetSaveButton";
            this.PresetSaveButton.Size = new System.Drawing.Size(112, 33);
            this.PresetSaveButton.TabIndex = 2;
            this.PresetSaveButton.Text = "Save";
            this.PresetSaveButton.UseVisualStyleBackColor = true;
            this.PresetSaveButton.Click += new System.EventHandler(this.PresetSaveButtonClick);
            // 
            // PresetDeleteButton
            // 
            this.PresetDeleteButton.Location = new System.Drawing.Point(500, 10);
            this.PresetDeleteButton.Name = "PresetDeleteButton";
            this.PresetDeleteButton.Size = new System.Drawing.Size(112, 33);
            this.PresetDeleteButton.TabIndex = 3;
            this.PresetDeleteButton.Text = "Delete";
            this.PresetDeleteButton.UseVisualStyleBackColor = true;
            this.PresetDeleteButton.Click += new System.EventHandler(this.PresetDeleteButtonClick);
            // 
            // ApplyButton
            // 
            this.ApplyButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ApplyButton.Location = new System.Drawing.Point(13, 748);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(803, 34);
            this.ApplyButton.TabIndex = 4;
            this.ApplyButton.Text = "Apply";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButtonClick);
            // 
            // SplitPointLabel
            // 
            this.SplitPointLabel.AutoSize = true;
            this.SplitPointLabel.Location = new System.Drawing.Point(13, 51);
            this.SplitPointLabel.Name = "SplitPointLabel";
            this.SplitPointLabel.Size = new System.Drawing.Size(94, 25);
            this.SplitPointLabel.TabIndex = 5;
            this.SplitPointLabel.Text = "Split point";
            // 
            // SplitPointComboBox
            // 
            this.SplitPointComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SplitPointComboBox.FormattingEnabled = true;
            this.SplitPointComboBox.Location = new System.Drawing.Point(114, 51);
            this.SplitPointComboBox.Name = "SplitPointComboBox";
            this.SplitPointComboBox.Size = new System.Drawing.Size(83, 33);
            this.SplitPointComboBox.TabIndex = 6;
            // 
            // VoiceSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 794);
            this.Controls.Add(this.SplitPointComboBox);
            this.Controls.Add(this.SplitPointLabel);
            this.Controls.Add(this.ApplyButton);
            this.Controls.Add(this.PresetDeleteButton);
            this.Controls.Add(this.PresetSaveButton);
            this.Controls.Add(this.PresetDropDown);
            this.Controls.Add(this.PresetLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VoiceSelector";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "VoiceSelector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PresetLabel;
        private System.Windows.Forms.ComboBox PresetDropDown;
        private System.Windows.Forms.Button PresetSaveButton;
        private System.Windows.Forms.Button PresetDeleteButton;
        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.Label SplitPointLabel;
        private System.Windows.Forms.ComboBox SplitPointComboBox;
    }
}