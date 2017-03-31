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
		private System.Windows.Forms.Button buttonSelectedBoltsCheck;
		private System.Windows.Forms.Button buttonClear;
		
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
			this.buttonSelectedBoltsCheck = new System.Windows.Forms.Button();
			this.buttonClear = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonStartBoltsCheck
			// 
			this.buttonStartBoltsCheck.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonStartBoltsCheck.Location = new System.Drawing.Point(446, 13);
			this.buttonStartBoltsCheck.Name = "buttonStartBoltsCheck";
			this.buttonStartBoltsCheck.Size = new System.Drawing.Size(85, 25);
			this.buttonStartBoltsCheck.TabIndex = 0;
			this.buttonStartBoltsCheck.Text = "pick one bolt";
			this.buttonStartBoltsCheck.UseVisualStyleBackColor = true;
			this.buttonStartBoltsCheck.Click += new System.EventHandler(this.buttonStartBoltsCheckClick);
			// 
			// consoleBoltsCheck
			// 
			this.consoleBoltsCheck.Font = new System.Drawing.Font("Lucida Console", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.consoleBoltsCheck.Location = new System.Drawing.Point(12, 106);
			this.consoleBoltsCheck.Multiline = true;
			this.consoleBoltsCheck.Name = "consoleBoltsCheck";
			this.consoleBoltsCheck.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.consoleBoltsCheck.Size = new System.Drawing.Size(1310, 472);
			this.consoleBoltsCheck.TabIndex = 1;
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(12, 13);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(418, 25);
			this.progressBar1.TabIndex = 2;
			// 
			// checkBoxCreateReport
			// 
			this.checkBoxCreateReport.Location = new System.Drawing.Point(13, 57);
			this.checkBoxCreateReport.Name = "checkBoxCreateReport";
			this.checkBoxCreateReport.Size = new System.Drawing.Size(104, 26);
			this.checkBoxCreateReport.TabIndex = 3;
			this.checkBoxCreateReport.Text = "create report";
			this.checkBoxCreateReport.UseVisualStyleBackColor = true;
			// 
			// buttonSelectedBoltsCheck
			// 
			this.buttonSelectedBoltsCheck.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonSelectedBoltsCheck.Location = new System.Drawing.Point(549, 13);
			this.buttonSelectedBoltsCheck.Name = "buttonSelectedBoltsCheck";
			this.buttonSelectedBoltsCheck.Size = new System.Drawing.Size(85, 25);
			this.buttonSelectedBoltsCheck.TabIndex = 4;
			this.buttonSelectedBoltsCheck.Text = "selected bolts";
			this.buttonSelectedBoltsCheck.UseVisualStyleBackColor = true;
			this.buttonSelectedBoltsCheck.Click += new System.EventHandler(this.buttonSelectedBoltsCheckClick);
			// 
			// buttonClear
			// 
			this.buttonClear.Location = new System.Drawing.Point(658, 15);
			this.buttonClear.Name = "buttonClear";
			this.buttonClear.Size = new System.Drawing.Size(75, 23);
			this.buttonClear.TabIndex = 5;
			this.buttonClear.Text = "clear";
			this.buttonClear.UseVisualStyleBackColor = true;
			this.buttonClear.Click += new System.EventHandler(this.clearClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1334, 604);
			this.Controls.Add(this.buttonClear);
			this.Controls.Add(this.buttonSelectedBoltsCheck);
			this.Controls.Add(this.checkBoxCreateReport);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.consoleBoltsCheck);
			this.Controls.Add(this.buttonStartBoltsCheck);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "MainForm";
			this.Text = "Tol_Bolts_Check";
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
