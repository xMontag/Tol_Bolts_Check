using System;
using System.Globalization;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Solid;

namespace Tol_Bolts_Check
{
	/// <summary>
	/// Description of TolBoltGroup.
	/// </summary>
	public class TolBoltGroup
	{
		public string funBolt = ""; // призначення метизу
		public string desBolt = ""; // позначення метизу
		public double t = 0; // товщина пакету
		public string[] washersBolt = new string[] { "" , "" , "" }; // шайби
		public string[] nutsBolt = new string[] { "" , "" }; // гайки
		public double diaBolt = 0; // діаметр
		public double lengthBolt = 0; // довжина болта
		public string matBolt = ""; // поле material болта
		public string gradeBolt = ""; // поле garde болта
		public string gradeBoltR = "";
		public string finishBolt = ""; // поле finish болта
		public string standardBolt = ""; // стандарт болта
		public string type3Bolt = ""; // структура шайб и гаек ХоХоо
		public string gapBolt = ""; // зазор
		public int nBolts = 0; // число болтов
		public double gapLength = 0; // зазор
		// шайбы
		public int nWashersBolt = 0; // число шайб в болту
		public int[] nWashersOfWasherBolt = new int[] { 0 , 0 , 0 }; // число шайб по каждой шайбе
		public string[] typeOfWasherBolt = new string[] { "" , "" , "" }; // госты шайб
		// гайки
		public int nNutsBolt = 0; // общее число гаек
		public int[] nNutsOfNutBolt = new int[] { 0 , 0 }; // число гаек в каждой гайке
		public string[] typeOfNutBolt = new string[] { "" , "" }; // госты гаек
		public BoltGroup tolBoltGroup;
		public string GUIDBolt;
		
		public TolBoltGroup(BoltGroup myBoltGroup)
		{
			tolBoltGroup = myBoltGroup;
			myBoltGroup.GetReportProperty("NAME_SHORT", ref funBolt);
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
			
			GUIDBolt = myBoltGroup.Identifier.GUID.ToString();
			
			myBoltGroup.GetUserProperty("BOLT_USERFIELD_1", ref gapBolt);
			
			// список отрезков где 0-ой это полный отрезок болта
			
			ArrayList myBoltGroupLS = new ArrayList(TolUtils.CheckOneBoltGroup(myBoltGroup));
			
			double[] tpl = new double[myBoltGroupLS.Count-1]; // толщины пакета
			double tplSum = 0; // сумма толщин
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
				t = Math.Round(tLength, tRound);
			} else {
				t = Math.Round(tplSum, tRound);
			}
			
			string pattern = @"^.+\<.+\>.*";
			gradeBoltR = Regex.IsMatch(gradeBolt, pattern) ? "." + gradeBolt.Substring(0, gradeBolt.IndexOf('<')) : "";
			string matBoltR = Regex.IsMatch(matBolt, pattern) ? "." + matBolt.Substring(0, matBolt.IndexOf('<')) : "";
			string finishBoltR = Regex.IsMatch(finishBolt, pattern) ? "." + finishBolt.Substring(0, finishBolt.IndexOf('<')) : "";
			
			
			
			pattern = @"^\DIN";
			
			string standardBoltR = Regex.IsMatch(standardBolt, pattern) ? " " + standardBolt : " ГОСТ "  + standardBolt;
			
			
			desBolt = "Болт M" + diaBolt + "x" + lengthBolt + gradeBoltR + matBoltR + finishBoltR + standardBoltR;
			
			//первая шайба
			if (nWashersOfWasherBolt[0] == 1)
			{
				washersBolt[0] = "ГОСТ " + typeOfWasherBolt[0];
			}
			else
			{
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
				nutsBolt[0] = "-";
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
		
		
			
		
		}
	}
}
