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
		private System.Windows.Forms.Button buttonSelectedBoltsCheck;
		private System.Windows.Forms.Button buttonClear;
		private System.Windows.Forms.DataGridView boltsGridView;
		private System.Windows.Forms.Button buttonSorted;
		private System.Windows.Forms.Button buttonCompressed;
		private System.Windows.Forms.Button buttonXMLReport;
		private System.Windows.Forms.Button buttonSelectBolts;
		private System.Windows.Forms.Button buttonSelectBoltsInModel;
		
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
			this.buttonSelectedBoltsCheck = new System.Windows.Forms.Button();
			this.buttonClear = new System.Windows.Forms.Button();
			this.boltsGridView = new System.Windows.Forms.DataGridView();
			this.buttonSorted = new System.Windows.Forms.Button();
			this.buttonCompressed = new System.Windows.Forms.Button();
			this.buttonXMLReport = new System.Windows.Forms.Button();
			this.buttonSelectBoltsInModel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.boltsGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonStartBoltsCheck
			// 
			this.buttonStartBoltsCheck.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonStartBoltsCheck.Location = new System.Drawing.Point(459, 20);
			this.buttonStartBoltsCheck.Name = "buttonStartBoltsCheck";
			this.buttonStartBoltsCheck.Size = new System.Drawing.Size(85, 25);
			this.buttonStartBoltsCheck.TabIndex = 0;
			this.buttonStartBoltsCheck.Text = "pick one bolt";
			this.buttonStartBoltsCheck.UseVisualStyleBackColor = true;
			this.buttonStartBoltsCheck.Click += new System.EventHandler(this.buttonStartBoltsCheckClick);
			// 
			// consoleBoltsCheck
			// 
			this.consoleBoltsCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.consoleBoltsCheck.Font = new System.Drawing.Font("Lucida Console", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.consoleBoltsCheck.Location = new System.Drawing.Point(12, 67);
			this.consoleBoltsCheck.Multiline = true;
			this.consoleBoltsCheck.Name = "consoleBoltsCheck";
			this.consoleBoltsCheck.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.consoleBoltsCheck.Size = new System.Drawing.Size(1310, 45);
			this.consoleBoltsCheck.TabIndex = 1;
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(12, 20);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(418, 25);
			this.progressBar1.TabIndex = 2;
			// 
			// buttonSelectedBoltsCheck
			// 
			this.buttonSelectedBoltsCheck.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonSelectedBoltsCheck.Location = new System.Drawing.Point(550, 20);
			this.buttonSelectedBoltsCheck.Name = "buttonSelectedBoltsCheck";
			this.buttonSelectedBoltsCheck.Size = new System.Drawing.Size(85, 25);
			this.buttonSelectedBoltsCheck.TabIndex = 4;
			this.buttonSelectedBoltsCheck.Text = "selected bolts";
			this.buttonSelectedBoltsCheck.UseVisualStyleBackColor = true;
			this.buttonSelectedBoltsCheck.Click += new System.EventHandler(this.buttonSelectedBoltsCheckClick);
			// 
			// buttonClear
			// 
			this.buttonClear.Location = new System.Drawing.Point(641, 20);
			this.buttonClear.Name = "buttonClear";
			this.buttonClear.Size = new System.Drawing.Size(85, 25);
			this.buttonClear.TabIndex = 5;
			this.buttonClear.Text = "clear";
			this.buttonClear.UseVisualStyleBackColor = true;
			this.buttonClear.Click += new System.EventHandler(this.clearClick);
			// 
			// boltsGridView
			// 
			this.boltsGridView.AllowUserToAddRows = false;
			this.boltsGridView.AllowUserToDeleteRows = false;
			this.boltsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.boltsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.boltsGridView.Location = new System.Drawing.Point(13, 118);
			this.boltsGridView.Name = "boltsGridView";
			this.boltsGridView.ReadOnly = true;
			this.boltsGridView.Size = new System.Drawing.Size(1309, 619);
			this.boltsGridView.TabIndex = 6;
			// 
			// buttonSorted
			// 
			this.buttonSorted.Location = new System.Drawing.Point(732, 20);
			this.buttonSorted.Name = "buttonSorted";
			this.buttonSorted.Size = new System.Drawing.Size(85, 25);
			this.buttonSorted.TabIndex = 7;
			this.buttonSorted.Text = "sorted";
			this.buttonSorted.UseVisualStyleBackColor = true;
			this.buttonSorted.Click += new System.EventHandler(this.buttonSortedClick);
			// 
			// buttonCompressed
			// 
			this.buttonCompressed.Location = new System.Drawing.Point(823, 20);
			this.buttonCompressed.Name = "buttonCompressed";
			this.buttonCompressed.Size = new System.Drawing.Size(85, 25);
			this.buttonCompressed.TabIndex = 8;
			this.buttonCompressed.Text = "compressed";
			this.buttonCompressed.UseVisualStyleBackColor = true;
			this.buttonCompressed.Click += new System.EventHandler(this.buttonCompressedClick);
			// 
			// buttonXMLReport
			// 
			this.buttonXMLReport.Location = new System.Drawing.Point(914, 20);
			this.buttonXMLReport.Name = "buttonXMLReport";
			this.buttonXMLReport.Size = new System.Drawing.Size(85, 25);
			this.buttonXMLReport.TabIndex = 9;
			this.buttonXMLReport.Text = "xml report";
			this.buttonXMLReport.UseVisualStyleBackColor = true;
			this.buttonXMLReport.Click += new System.EventHandler(this.buttonXMLReportClick);
			// 
			// buttonSelectBoltsInModel
			// 
			this.buttonSelectBoltsInModel.Location = new System.Drawing.Point(1005, 20);
			this.buttonSelectBoltsInModel.Name = "buttonSelectBoltsInModel";
			this.buttonSelectBoltsInModel.Size = new System.Drawing.Size(85, 25);
			this.buttonSelectBoltsInModel.TabIndex = 10;
			this.buttonSelectBoltsInModel.Text = "select bolts";
			this.buttonSelectBoltsInModel.UseVisualStyleBackColor = true;
			this.buttonSelectBoltsInModel.Click += new System.EventHandler(this.buttonSelectBoltsClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1334, 749);
			this.Controls.Add(this.buttonSelectBoltsInModel);
			this.Controls.Add(this.buttonXMLReport);
			this.Controls.Add(this.buttonCompressed);
			this.Controls.Add(this.buttonSorted);
			this.Controls.Add(this.boltsGridView);
			this.Controls.Add(this.buttonClear);
			this.Controls.Add(this.buttonSelectedBoltsCheck);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.consoleBoltsCheck);
			this.Controls.Add(this.buttonStartBoltsCheck);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "MainForm";
			this.Text = "Tol_Bolts_Check";
			((System.ComponentModel.ISupportInitialize)(this.boltsGridView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
