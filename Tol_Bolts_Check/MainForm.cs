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
			string[] washersBolt = new string[] { "" , "" , "" }; // шайби
			string[] nutsBolt = new string[] { "" , "" }; // гайки
			
			
			
			myBoltGroup.GetReportProperty("NAME_SHORT", ref funBolt);
			
			double diaBolt = 0;
			double lengthBolt = 0;
			string matBolt = "";
			string gradeBolt = "";
			string finishBolt = "";
			string standardBolt = "";
			string type3Bolt = "";
			string gapBolt = "";
			
			//болт
			int nBolts = 0; //число болтов
			//double extraNBolts = 0.05; +5% к количеству болтов
			// шайбы
			int nWashersBolt = 0; // число шайб в болту
			int[] nWashersOfWasherBolt = new int[] { 0 , 0 , 0 }; //число шайб по каждой шайбе
			string[] typeOfWasherBolt = new string[] { "" , "" , "" }; //госты шайб
			// гайки
			int nNutsBolt = 0; // общее число гаек
			int[] nNutsOfNutBolt = new int[] { 0 , 0 }; // число гаек в каждой гайке
			string[] typeOfNutBolt = new string[] { "" , "" }; //госты гаек
			
			
			myBoltGroup.GetReportProperty("DIAMETER", ref diaBolt);
			myBoltGroup.GetReportProperty("LENGTH", ref lengthBolt);
			myBoltGroup.GetReportProperty("MATERIAL", ref matBolt);
			myBoltGroup.GetReportProperty("GRADE", ref gradeBolt);
			myBoltGroup.GetReportProperty("FINISH", ref finishBolt);
			myBoltGroup.GetReportProperty("BOLT_STANDARD", ref standardBolt);
			myBoltGroup.GetReportProperty("TYPE3", ref type3Bolt);
			myBoltGroup.GetReportProperty("NUMBER", ref nBolts);
			
			myBoltGroup.GetReportProperty("WASHER.NUMBER", ref nWashersBolt);
			myBoltGroup.GetReportProperty("WASHER.NUMBER1", ref nWashersOfWasherBolt[0]);
			myBoltGroup.GetReportProperty("WASHER.NUMBER2", ref nWashersOfWasherBolt[1]);
			myBoltGroup.GetReportProperty("WASHER.NUMBER3", ref nWashersOfWasherBolt[2]);
			myBoltGroup.GetReportProperty("WASHER.TYPE1", ref typeOfWasherBolt[0]);
			myBoltGroup.GetReportProperty("WASHER.TYPE2", ref typeOfWasherBolt[1]);
			myBoltGroup.GetReportProperty("WASHER.TYPE3", ref typeOfWasherBolt[2]);
			
			myBoltGroup.GetReportProperty("NUT.NUMBER", ref nNutsBolt);
			myBoltGroup.GetReportProperty("NUT.NUMBER1", ref nNutsOfNutBolt[0]);
			myBoltGroup.GetReportProperty("NUT.NUMBER2", ref nNutsOfNutBolt[1]);
			myBoltGroup.GetReportProperty("NUT.TYPE1", ref typeOfNutBolt[0]);
			myBoltGroup.GetReportProperty("NUT.TYPE2", ref typeOfNutBolt[1]);
			
			
			
			
			nWashersBolt = nWashersBolt/nBolts; // число шайб в болту
			for (int i = 0; i < nWashersOfWasherBolt.Length; i++) {
				nWashersOfWasherBolt[i] = nWashersOfWasherBolt[i]/nBolts;
			}
			
			nNutsBolt = nNutsBolt/nBolts; // число шайб в болту
			for (int i = 0; i < nNutsOfNutBolt.Length; i++) {
				nNutsOfNutBolt[i] = nNutsOfNutBolt[i]/nBolts;
			}
			
			
			myBoltGroup.GetUserProperty("BOLT_USERFIELD_1", ref gapBolt);
			
			
			
			// список отрезков где 0-ой это полный отрезок болта
			
			ArrayList myBoltGroupLS = new ArrayList(TolUtils.CheckOneBoltGroup(myBoltGroup));
			
			double[] tpl = new double[myBoltGroupLS.Count-1]; // толщины пакета
			double tplSum = 0; // сумма толщин
			double gapLength = 0; // зазор
			int tRound = 2; // чисел после запятой в толщине пакета
			int gapRound = 2; // чисел после запятой в толщине пакета
			
			for (int i = 1; i < myBoltGroupLS.Count; i++) {
				tpl[i-1] = (myBoltGroupLS[i] as LineSegment).Length();
			}
			
					
			double tLength = (new LineSegment((myBoltGroupLS[1] as LineSegment).Point1 , (myBoltGroupLS[myBoltGroupLS.Count - 1] as LineSegment).Point2)).Length();
			
			
			
			foreach (double a in tpl) {
				tplSum = tplSum + a;
			}
			
			gapLength = Math.Round(tLength - tplSum, gapRound);
			
			if (gapBolt.Equals("gap"))
			{
				//LineSegment tmpLS;
				//tmpLS = myBoltGroupLS[0] as LineSegment;
				t = Math.Round(tLength, tRound);
			} else {
				t = Math.Round(tplSum, tRound);
			}
			
			string pattern = @"^.+\<.+\>.*";
			
			string gradeBoltR = Regex.IsMatch(gradeBolt, pattern) ? "." + gradeBolt.Substring(0, gradeBolt.IndexOf('<')) : "";
			string matBoltR = Regex.IsMatch(matBolt, pattern) ? "." + matBolt.Substring(0, matBolt.IndexOf('<')) : "";
			string finishBoltR = Regex.IsMatch(finishBolt, pattern) ? "." + finishBolt.Substring(0, finishBolt.IndexOf('<')) : "";
			
			
			
			pattern = @"^\DIN";
			
			string standardBoltR = Regex.IsMatch(standardBolt, pattern) ? " " + standardBolt : " ГОСТ "  + standardBolt;
			
			
			desBolt = "Болт M" + diaBolt + "x" + lengthBolt + gradeBoltR + matBoltR + finishBoltR + standardBoltR;
			
			//первая шайба
			if (nWashersOfWasherBolt[0] == 1)
			{
				washersBolt[0] = "ГОСТ " + typeOfWasherBolt[0];
			} else {
				washersBolt[0] = "-";
			}
			
			//вторая шайба
			if (nWashersOfWasherBolt[1] == 1)
			{
				washersBolt[1] = "ГОСТ " + typeOfWasherBolt[1];
			}
			else if (nWashersOfWasherBolt[2] > 0)
			{
				washersBolt[1] = "ГОСТ " + typeOfWasherBolt[2];
				nWashersOfWasherBolt[2]--;
				
			}
			else
			{
				washersBolt[1] = "-";
			}
			
			//третья шайба
			if (nWashersOfWasherBolt[2] > 1)
			{
				washersBolt[2] = nWashersOfWasherBolt[2] + " x ГОСТ " + typeOfWasherBolt[2];
			}
			else if (nWashersOfWasherBolt[2] == 1)
			{
				washersBolt[2] = "ГОСТ " + typeOfWasherBolt[2];
			}
			else
			{
				washersBolt[2] = "-";
			}
			
			//первая гайка
			if (nNutsOfNutBolt[0] == 1)
			{
				nutsBolt[0] = "ГОСТ " + typeOfNutBolt[0];
			}
			else
			{
				nutsBolt[0] = "-" + nNutsOfNutBolt[0] + typeOfNutBolt[0];
			}
			
			//вторая гайка
			if (nNutsOfNutBolt[1] == 1)
			{
				nutsBolt[1] = "ГОСТ " + typeOfNutBolt[0];
			}
			else
			{
				nutsBolt[1] = "-";
			}
			
			
			// тестовый вывод	
//			consoleBoltsCheck.Text = "Start!" + "\r\n";
//			consoleBoltsCheck.AppendText(gapLength + " - зазор" + "\r\n");
//			consoleBoltsCheck.AppendText(type3Bolt + " - type3" + "\r\n");
//			consoleBoltsCheck.AppendText(nWashersBolt + " - общее число шайб в болту" + "\r\n");
//			
//			
//			for (int i = 0; i < nWashersOfWasherBolt.Length; i++)
//			{
//				consoleBoltsCheck.AppendText(nWashersOfWasherBolt[i] + "-" + typeOfWasherBolt[i] + " - число шайб в " + (i+1) +"-й" + "\r\n");
//			}
//			
//			for (int i = 0; i < nNutsOfNutBolt.Length; i++)
//			{
//				consoleBoltsCheck.AppendText(nNutsOfNutBolt[i] + "-" + typeOfNutBolt[i] + " - число гаек в " + (i+1) +"-й" + "\r\n");
//			}
			
			
			consoleBoltsCheck.AppendText(View.ViewBoltListInTextArea(1,funBolt,desBolt,t,washersBolt,nutsBolt,nBolts));
			
//			consoleBoltsCheck.AppendText(funBolt + " - Призначення" + "\r\n");
//			consoleBoltsCheck.AppendText(desBolt + " - Позначення метизу" + "\r\n");
//			consoleBoltsCheck.AppendText(t + " - Товщина пакету, мм" + "\r\n");
//			consoleBoltsCheck.AppendText(washersBolt[0] + " - Шайба під головку" + "\r\n");
//			consoleBoltsCheck.AppendText(washersBolt[1] + " - Шайба під гайку" + "\r\n");
//			consoleBoltsCheck.AppendText(washersBolt[2] + " - Додаткова шайба під гайку" + "\r\n");
//			consoleBoltsCheck.AppendText(nutsBolt[0] + " - Гайка" + "\r\n");
//			consoleBoltsCheck.AppendText(nutsBolt[1] + " - Контргайка" + "\r\n");
//			consoleBoltsCheck.AppendText(nBolts + " - Кількість" + "\r\n");
		}

	}
}
