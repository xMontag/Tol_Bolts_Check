/*
 * Created by SharpDevelop.
 * User: Vitalii.Zotov
 * Date: 24.03.2017
 * Time: 19:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
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
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
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
							consoleBoltsCheck.AppendText(View.ViewBoltListInTextArea(1,myBolt));
						}
					}
					catch (Exception ee)
					
					{
						//MessageBox.Show(ee.ToString());
					}
				}
					
			}
		}
		
		void clearClick(object sender, EventArgs e)
		{
			consoleBoltsCheck.Text = "";
		}
		
		void buttonStartBoltsCheckClick(object sender, EventArgs e)
		{			
			BoltGroup myBoltGroup = TolUtils.PickBoltGroup() as BoltGroup;
			if (TolUtils.TolTestOfBolt(myBoltGroup))
			{
				TolBoltGroup myBolt = new TolBoltGroup(myBoltGroup);
				consoleBoltsCheck.AppendText(View.ViewBoltListInTextArea(1,myBolt));
			}
			else
			{
				MessageBox.Show("та то не болт!!! йолопе!");
			}
		}

	}
}
