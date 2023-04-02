
namespace Ex05_OtheloForms
{
    partial class GameSettings
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
            this.ButtonBoardSize = new System.Windows.Forms.Button();
            this.ButtonSingleplayer = new System.Windows.Forms.Button();
            this.ButtonMultiplayer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButtonBoardSize
            // 
            this.ButtonBoardSize.Location = new System.Drawing.Point(12, 12);
            this.ButtonBoardSize.Name = "ButtonBoardSize";
            this.ButtonBoardSize.Size = new System.Drawing.Size(427, 75);
            this.ButtonBoardSize.TabIndex = 0;
            this.ButtonBoardSize.Text = "Board Size:6x6 (Click To Increase)";
            this.ButtonBoardSize.UseVisualStyleBackColor = true;
            this.ButtonBoardSize.Click += new System.EventHandler(this.ButtonBoardSize_Click);
            // 
            // ButtonSingleplayer
            // 
            this.ButtonSingleplayer.Location = new System.Drawing.Point(12, 93);
            this.ButtonSingleplayer.Name = "ButtonSingleplayer";
            this.ButtonSingleplayer.Size = new System.Drawing.Size(195, 48);
            this.ButtonSingleplayer.TabIndex = 1;
            this.ButtonSingleplayer.Text = "Play against the computer";
            this.ButtonSingleplayer.UseVisualStyleBackColor = true;
            this.ButtonSingleplayer.Click += new System.EventHandler(this.ButtonSingleplayer_Click);
            // 
            // ButtonMultiplayer
            // 
            this.ButtonMultiplayer.Location = new System.Drawing.Point(225, 93);
            this.ButtonMultiplayer.Name = "ButtonMultiplayer";
            this.ButtonMultiplayer.Size = new System.Drawing.Size(214, 48);
            this.ButtonMultiplayer.TabIndex = 2;
            this.ButtonMultiplayer.Text = "Play agaisnt a friend";
            this.ButtonMultiplayer.UseVisualStyleBackColor = true;
            this.ButtonMultiplayer.Click += new System.EventHandler(this.ButtonMultiplayer_Click);
            // 
            // GameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 176);
            this.Controls.Add(this.ButtonMultiplayer);
            this.Controls.Add(this.ButtonSingleplayer);
            this.Controls.Add(this.ButtonBoardSize);
            this.Name = "GameSettings";
            this.Text = "Othello - Game Settings";
            this.Load += new System.EventHandler(this.GameSettings_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonBoardSize;
        private System.Windows.Forms.Button ButtonSingleplayer;
        private System.Windows.Forms.Button ButtonMultiplayer;
    }
}