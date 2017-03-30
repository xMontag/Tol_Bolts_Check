/*
 * Created by SharpDevelop.
 * User: Vitalii.Zotov
 * Date: 30.03.2017
 * Time: 15:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Tol_Bolts_Check
{
	/// <summary>
	/// Description of View.
	/// </summary>
	public class View
	{
		public static string ViewBoltListInTextArea(int num, string funBolt, string desBolt, double t, string[] washersBolt, string[] nutsBolt, int nBolts)
		{
			int n = 10;
			int[] length = new int[] { 5 , 15 , 35 , 5 , 20 , 20 , 20 , 20 , 20 , 6 };
			string tmpString;
			string myList = "";
			tmpString = "" + num;
			myList = myList + tmpString.PadRight(length[0]);
			
			tmpString = "" + funBolt;
			myList = myList + tmpString.PadRight(length[1]);
			
			tmpString = "" + desBolt;
			myList = myList + tmpString.PadRight(length[2]);
			
			tmpString = "" + t;
			myList = myList + tmpString.PadRight(length[3]);
			
			tmpString = "" + washersBolt[0];
			myList = myList + tmpString.PadRight(length[4]);
			
			tmpString = "" + washersBolt[1];
			myList = myList + tmpString.PadRight(length[5]);
			
			tmpString = "" + washersBolt[2];
			myList = myList + tmpString.PadRight(length[6]);
			
			tmpString = "" + nutsBolt[0];
			myList = myList + tmpString.PadRight(length[7]);
			
			tmpString = "" + nutsBolt[1];
			myList = myList + tmpString.PadRight(length[8]);
			
			tmpString = "" + nBolts;
			myList = myList + tmpString.PadRight(length[9]);
			
			myList = myList + "\r\n";
			return myList;
		}
	}
}
