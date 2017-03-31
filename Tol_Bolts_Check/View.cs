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
		public static string ViewBoltListInTextArea(int num, TolBoltGroup a)
		{
			int n = 10;
			int[] length = new int[] { 5 , 15 , 50 , 10 , 20 , 20 , 20 , 20 , 20 , 6 };
			string tmpString;
			string myList = "";
			tmpString = "" + num;
			myList = myList + tmpString.PadRight(length[0]);
			
			tmpString = "" + a.funBolt;
			myList = myList + tmpString.PadRight(length[1]);
			
			tmpString = "" + a.desBolt;
			myList = myList + tmpString.PadRight(length[2]);
			
			tmpString = "" + a.t;
			myList = myList + tmpString.PadRight(length[3]);
			
			tmpString = "" + a.washersBolt[0];
			myList = myList + tmpString.PadRight(length[4]);
			
			tmpString = "" + a.washersBolt[1];
			myList = myList + tmpString.PadRight(length[5]);
			
			tmpString = "" + a.washersBolt[2];
			myList = myList + tmpString.PadRight(length[6]);
			
			tmpString = "" + a.nutsBolt[0];
			myList = myList + tmpString.PadRight(length[7]);
			
			tmpString = "" + a.nutsBolt[1];
			myList = myList + tmpString.PadRight(length[8]);
			
			tmpString = "" + a.nBolts;
			myList = myList + tmpString.PadRight(length[9]);
			
			myList = myList + "\r\n";
			
			return myList;
		}
	}
}
