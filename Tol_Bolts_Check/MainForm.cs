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
		void buttonStartBoltsCheckClick(object sender, EventArgs e)
		{
			
			BoltGroup myBoltGroup = TolUtils.PickBoltGroup() as BoltGroup;
			
			string funBolt = ""; // призначення метизу
			string desBolt = ""; // позначення метизу
			double t = 0; // товщина пакету
			string[] washersBolt = new string[] {"" , "" , ""}; // шайби
			string[] nutsBolt = new string[] {"" , ""}; // гайки
			int n = 0; // кількість
			
			myBoltGroup.GetReportProperty("NAME_SHORT", ref funBolt);
			
			double diaBolt = 0;
			double lengthBolt = 0;
			string matBolt = "";
			string gradeBolt = "";
			string finishBolt = "";
			string standardBolt = "";
			string gapBolt = "";
			
			myBoltGroup.GetReportProperty("DIAMETER", ref diaBolt);
			myBoltGroup.GetReportProperty("LENGTH", ref lengthBolt);
			myBoltGroup.GetReportProperty("MATERIAL", ref matBolt);
			myBoltGroup.GetReportProperty("GRADE", ref gradeBolt);
			myBoltGroup.GetReportProperty("FINISH", ref finishBolt);
			myBoltGroup.GetReportProperty("BOLT_STANDARD", ref standardBolt);
			myBoltGroup.GetUserProperty("BOLT_USERFIELD_1", ref gapBolt);
			
			// список отрезков где 0-ой это полный отрезок болта
			
			ArrayList myBoltGroupLS = new ArrayList(TolUtils.CheckOneBoltGroup(myBoltGroup));
			
			double[] tpl = new double[myBoltGroupLS.Count-1]; // толщины пакета
			double tplSum = 0; // сумма толщин
			double gapLength = 0; // зазор
			
			for (int i = 1; i < myBoltGroupLS.Count; i++) {
				tpl[i-1] = (myBoltGroupLS[i] as LineSegment).Length();
			}
			
					
			double tLength = (new LineSegment((myBoltGroupLS[1] as LineSegment).Point1 , (myBoltGroupLS[myBoltGroupLS.Count - 1] as LineSegment).Point2)).Length();
			
			
			
			foreach (double a in tpl) {
				tplSum = tplSum + a;
			}
			
			gapLength = tLength - tplSum;
			
			if (gapBolt.Equals("gap"))
			{
				//LineSegment tmpLS;
				//tmpLS = myBoltGroupLS[0] as LineSegment;
				t = (myBoltGroupLS[0] as LineSegment).Length();
			} else {
				t = (myBoltGroupLS[0] as LineSegment).Length();
			}
			
			string pattern = @"^.+\<.+\>.*";
			
			string gradeBoltR = Regex.IsMatch(gradeBolt, pattern) ? "." + gradeBolt.Substring(0, gradeBolt.IndexOf('<')) : "";
			string matBoltR = Regex.IsMatch(matBolt, pattern) ? "." + matBolt.Substring(0, matBolt.IndexOf('<')) : "";
			string finishBoltR = Regex.IsMatch(finishBolt, pattern) ? "." + finishBolt.Substring(0, finishBolt.IndexOf('<')) : "";
			
			
			
			pattern = @"^\DIN";
			
			string standardBoltR = Regex.IsMatch(standardBolt, pattern) ? " " + standardBolt : " ГОСТ "  + standardBolt;
			
			
			desBolt = "Болт M" + diaBolt + "x" + lengthBolt + gradeBoltR + matBoltR + finishBoltR + standardBoltR;
			
			
			
				
			consoleBoltsCheck.Text = "Start!" + "\r\n";
			consoleBoltsCheck.AppendText(funBolt + "\r\n");
			consoleBoltsCheck.AppendText(desBolt + "\r\n");
			consoleBoltsCheck.AppendText(gapLength + " - зазор" + "\r\n");
			consoleBoltsCheck.AppendText(tplSum + " - сумма толщин материалов" + "\r\n");
			consoleBoltsCheck.AppendText(tLength + " - длина пакета по крайним элементам" + "\r\n");
	
		}

	}
}
