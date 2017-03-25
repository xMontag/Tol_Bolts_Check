/*
 * Created by SharpDevelop.
 * User: Vitalii.Zotov
 * Date: 24.03.2017
 * Time: 19:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Tol_Bolts_Check
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button buttonStartBoltsCheck;
		private System.Windows.Forms.TextBox consoleBoltsCheck;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.CheckBox checkBoxCreateReport;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonStartBoltsCheck = new System.Windows.Forms.Button();
			this.consoleBoltsCheck = new System.Windows.Forms.TextBox();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.checkBoxCreateReport = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// buttonStartBoltsCheck
			// 
			this.buttonStartBoltsCheck.Location = new System.Drawing.Point(446, 12);
			this.buttonStartBoltsCheck.Name = "buttonStartBoltsCheck";
			this.buttonStartBoltsCheck.Size = new System.Drawing.Size(75, 23);
			this.buttonStartBoltsCheck.TabIndex = 0;
			this.buttonStartBoltsCheck.Text = "Старт!";
			this.buttonStartBoltsCheck.UseVisualStyleBackColor = true;
			this.buttonStartBoltsCheck.Click += new System.EventHandler(this.buttonStartBoltsCheckClick);
			// 
			// consoleBoltsCheck
			// 
			this.consoleBoltsCheck.Location = new System.Drawing.Point(12, 98);
			this.consoleBoltsCheck.Multiline = true;
			this.consoleBoltsCheck.Name = "consoleBoltsCheck";
			this.consoleBoltsCheck.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.consoleBoltsCheck.Size = new System.Drawing.Size(509, 189);
			this.consoleBoltsCheck.TabIndex = 1;
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(12, 12);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(418, 23);
			this.progressBar1.TabIndex = 2;
			// 
			// checkBoxCreateReport
			// 
			this.checkBoxCreateReport.Location = new System.Drawing.Point(13, 53);
			this.checkBoxCreateReport.Name = "checkBoxCreateReport";
			this.checkBoxCreateReport.Size = new System.Drawing.Size(104, 24);
			this.checkBoxCreateReport.TabIndex = 3;
			this.checkBoxCreateReport.Text = "зробити звіт";
			this.checkBoxCreateReport.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(533, 303);
			this.Controls.Add(this.checkBoxCreateReport);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.consoleBoltsCheck);
			this.Controls.Add(this.buttonStartBoltsCheck);
			this.Name = "MainForm";
			this.Text = "Tol_Bolts_Check";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
