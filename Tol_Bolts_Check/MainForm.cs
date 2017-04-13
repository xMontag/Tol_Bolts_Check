/*
 * Created by SharpDevelop.
 * User: Vitalii.Zotov
 * Date: 24.03.2017
 * Time: 19:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;

using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;


using System.Linq;
using System.Linq.Expressions;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Globalization;

using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Solid;

namespace Tol_Bolts_Check
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public static DataSet boltsSet = new DataSet("Bolts");
		public static DataTable boltsUnsortedUncompressedTable = TolDataSet.TolCreateDateTable("boltsUnsortedUncompressed");
		public static DataTable boltsSortedUncompressedTable = TolDataSet.TolCreateDateTable("boltsSortedUncompressed");
		public static DataTable boltsUnsortedCompressedTable = TolDataSet.TolCreateDateTable("boltsUnsortedCompressed");
		public static DataTable boltsSortedCompressedTable = TolDataSet.TolCreateDateTable("boltsSortedCompressed");
		public static DataTable boltsCurrentTable = boltsUnsortedUncompressedTable;
		
		public static DataView dv = new DataView();
		public static int count = 0;
		private bool sortedStatus = true;
		private bool compressedStatus = true;
		
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			changeCurrentTable();
			
			//boltsGridView.Columns[10].Visible = false;
		}
			
		//изменение текущей таблицы для отображения
		private void changeCurrentTable()
		{
			if (this.sortedStatus == true && this.compressedStatus == true)
			{
				boltsCurrentTable = boltsSortedCompressedTable;
			}
			else if (this.sortedStatus == true && this.compressedStatus == false)
			{
				boltsCurrentTable = boltsSortedUncompressedTable;
			}
			else if (this.sortedStatus == false && this.compressedStatus == true)
			{
				boltsCurrentTable = boltsUnsortedCompressedTable;
			}
			else
			{
				boltsCurrentTable = boltsUnsortedUncompressedTable;
			}
			
			dv.Table = boltsCurrentTable;
			boltsGridView.DataSource = dv;
			//for (int i = 0; i < boltsGridView.Rows.Count; i++) {
				//if ((int)dv.ToTable().Rows[i]["erorrBolt"] > 0)
				//{
				//	boltsGridView.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
				//}
			//}
			
			
			
			
			//MessageBox.Show(boltsCurrentTable.ToString());
		}
		
		
		//экспорт текущей таблицы в XML
		void buttonXMLReportClick(object sender, EventArgs e)
		{
			
			string fileName = @"SdUa_Tol_Вд_Перелік болтових пакетів.SdUaReport";
			if (TolDataSet.TolExportXMLFile(boltsCurrentTable, fileName))
			{
				MessageBox.Show("Report created!");
			}
			else
			{
				MessageBox.Show("Ooops! There was some error!");
			}
			
		}
		
		//изменение надписи на кнопках + изменеение текущей	таблицы
		void buttonSortedClick(object sender, EventArgs e)
		{
			if (this.sortedStatus)
			{
				this.sortedStatus = false;
				buttonSorted.Text = "unsorted";
			}
			else
			{
				this.sortedStatus = true;
				buttonSorted.Text = "sorted";
			}
			changeCurrentTable();
			
		}
		
		void buttonCompressedClick(object sender, EventArgs e)
		{
			if (this.compressedStatus)
			{
				this.compressedStatus = false;
				buttonCompressed.Text = "uncompressed";
			}
			else
			{
				this.compressedStatus = true;
				buttonCompressed.Text = "compressed";
			}
			changeCurrentTable();
			
		}
		
		void buttonSelectedBoltsCheckClick(object sender, EventArgs e)
		{
			ModelObjectEnumerator myEnum = TolUtils.TolGetSelctedObjects();
			BoltGroup myBoltGroup;
			
			
			while(myEnum.MoveNext())
			{
				ModelObject myObject = myEnum.Current;
				if (myObject != null) 
				{
					try
					{
						myBoltGroup = myObject as BoltGroup;
						if (TolUtils.TolTestOfBolt(myBoltGroup))
						{
							TolBoltGroup myBolt = new TolBoltGroup(myBoltGroup);
							TolDataSet.TolAddBoltRow(boltsUnsortedUncompressedTable, myBolt );
							count++;
							consoleBoltsCheck.AppendText(View.ViewBoltListInTextArea(count,myBolt));
						}
					}
					
					catch (Exception ee)
					
					{
						//MessageBox.Show(ee.ToString());
					}
					
					
				
				}
			}
			boltsSortedUncompressedTable = TolDataSet.TolSortDataTable(boltsUnsortedUncompressedTable);
			boltsUnsortedCompressedTable = TolDataSet.TolCompressTable(boltsUnsortedUncompressedTable, boltsUnsortedCompressedTable.TableName);
			boltsSortedCompressedTable = TolDataSet.TolSortDataTable(TolDataSet.TolCompressTable(boltsUnsortedUncompressedTable, boltsUnsortedCompressedTable.TableName));
			changeCurrentTable();
		}
		
		void clearClick(object sender, EventArgs e)
		{
			consoleBoltsCheck.Text = "";
			boltsUnsortedUncompressedTable = TolDataSet.TolCreateDateTable(boltsUnsortedUncompressedTable.TableName);
			boltsUnsortedCompressedTable = TolDataSet.TolCreateDateTable(boltsUnsortedCompressedTable.TableName);
			boltsSortedUncompressedTable = TolDataSet.TolCreateDateTable(boltsSortedUncompressedTable.TableName);
			boltsSortedCompressedTable = TolDataSet.TolCreateDateTable(boltsSortedCompressedTable.TableName);
			changeCurrentTable();
			count = 0;
		}
		
		void buttonSelectBoltsClick(object sender, EventArgs e)
		{

			try
			{
				string a = "" + dv.ToTable().Rows[boltsGridView.CurrentCell.RowIndex]["GUIDBolt"];
				TolUtils.TolSelectObjectsByStringGUID(a);
			}
			catch (Exception ee)
			{
				MessageBox.Show(ee.ToString());
			}

		}
		
		void buttonStartBoltsCheckClick(object sender, EventArgs e)
		{			
			BoltGroup myBoltGroup = TolUtils.PickBoltGroup() as BoltGroup;
			if (TolUtils.TolTestOfBolt(myBoltGroup))
			{
				TolBoltGroup myBolt = new TolBoltGroup(myBoltGroup);
				TolDataSet.TolAddBoltRow(boltsUnsortedUncompressedTable, myBolt);
				count++;
				consoleBoltsCheck.AppendText(View.ViewBoltListInTextArea(count,myBolt));
				boltsSortedUncompressedTable = TolDataSet.TolSortDataTable(boltsUnsortedUncompressedTable);
				boltsUnsortedCompressedTable = TolDataSet.TolCompressTable(boltsUnsortedUncompressedTable, boltsUnsortedCompressedTable.TableName);
				boltsSortedCompressedTable = TolDataSet.TolSortDataTable(TolDataSet.TolCompressTable(boltsUnsortedUncompressedTable, boltsUnsortedCompressedTable.TableName));
				changeCurrentTable();
			}
			else
			{
				MessageBox.Show("та то не болт!!! йолопе!");
			}
		}

	}
}
