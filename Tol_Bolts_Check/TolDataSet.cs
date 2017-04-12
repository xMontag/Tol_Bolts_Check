/*
 * Created by SharpDevelop.
 * User: Vitalii.Zotov
 * Date: 03.04.2017
 * Time: 14:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Solid;

namespace Tol_Bolts_Check
{
	/// <summary>
	/// Description of TolDataSet.
	/// </summary>
	public class TolDataSet
	{
		
		//создание таблицы
		public static DataTable TolCreateDateTable(string name)
		{
			DataTable myTable = new DataTable(name);
			DataColumn dc = myTable.Columns.Add("id", typeof(int));
			dc.AutoIncrement = true;
			dc.AutoIncrementSeed = 1;
			dc.AutoIncrementStep = 1;
			myTable.Columns.Add("Function", typeof(string));
			myTable.Columns.Add("BoltName", typeof(string));
			myTable.Columns.Add("BoltMaterialLength", typeof(double));
			myTable.Columns.Add("Washer1", typeof(string));
			myTable.Columns.Add("Washer2", typeof(string));
			myTable.Columns.Add("Washer3", typeof(string));
			myTable.Columns.Add("Nut1", typeof(string));
			myTable.Columns.Add("Nut2", typeof(string));
			myTable.Columns.Add("Quantity", typeof(int));
			myTable.Columns.Add("standardBolt", typeof(string));
			myTable.Columns.Add("gradeBolt", typeof(string));
			myTable.Columns.Add("diaBolt", typeof(double));
			myTable.Columns.Add("lengthBolt", typeof(double));
			myTable.Columns.Add("BoltMaterialLengthTekla", typeof(string));
			myTable.Columns.Add("erorrBolt", typeof(int));
			myTable.Columns.Add("GUIDBolt", typeof(string));
			
			
			
			//myTable.Columns.Add("BoltGroups", typeof(ArrayList));
			return myTable;
		}
		
		//сортировка таблицы
		public static DataTable TolSortDataTable(DataTable myTable)
		{
			DataView dv = myTable.DefaultView;
			dv.Sort = "Function ASC, standardBolt ASC, gradeBolt ASC, diaBolt ASC, lengthBolt ASC, BoltName ASC, BoltMaterialLength ASC, Washer1 ASC, Washer2 ASC, Washer3 ASC, Nut1 ASC, Nut2 ASC";
			DataTable sortedDT = dv.ToTable();
			
			return TolClearIDinDataTable(sortedDT);
		}
		
		
		//объеденение одинаковых строк
		public static DataTable TolCompressTable(DataTable myTable, string name)
		{
			DataTable TMP1myTable = myTable.Copy();
			DataTable TMP2myTable = myTable.Copy();
			int count = 0;
			ArrayList columns = new ArrayList(){ "Function", "BoltName", "BoltMaterialLength", "Washer1", "Washer2", "Washer3", "Nut1", "Nut2" }; // по каким колонкам сравнивать
			string tmp1;
			string tmp2;
			int nBolts = 0;
			int nDel = 0; // кол-во удаленных строк
			string GUIDBolt = "";
			int errorBolt = 0;
			ArrayList boltMaterialLengthTekla = new ArrayList();
			
			
			
			
			for (int j = 0; j < TMP1myTable.Rows.Count; j++)
			{
				tmp1 = TolGetStringInRow(TMP1myTable.Rows[j], columns);
				
				for (int i = j; i < TMP2myTable.Rows.Count; i++)
				{
					tmp2 = TolGetStringInRow(TMP2myTable.Rows[i], columns);
					
					if (tmp1.Equals(tmp2))
					{
						
						nBolts += (int)TMP2myTable.Rows[i]["Quantity"];
						boltMaterialLengthTekla.Add((string)TMP2myTable.Rows[i]["BoltMaterialLengthTekla"]);
						errorBolt += (int)TMP2myTable.Rows[i]["erorrBolt"];
						if (i != j)
						{
							TMP1myTable.Rows[i-nDel].Delete();
							nDel++;
							GUIDBolt += " " + (string)TMP2myTable.Rows[i]["GUIDBolt"];
						}
						else
						{
							GUIDBolt += "" + (string)TMP2myTable.Rows[i]["GUIDBolt"];
							
						}
					}
					
				}
				nDel = 0;
				TMP1myTable.Rows[j]["Quantity"] = Math.Round(nBolts * 1.05, 0);
				TMP1myTable.Rows[j]["GUIDBolt"] = GUIDBolt;
				TMP1myTable.Rows[j]["erorrBolt"] = errorBolt;
				boltMaterialLengthTekla = new ArrayList(boltMaterialLengthTekla.Cast<object>().Distinct().ToArray());
				TMP1myTable.Rows[j]["BoltMaterialLengthTekla"] = String.Join(" " , (String[]) boltMaterialLengthTekla.ToArray(typeof(string)));
				nBolts = 0;
				errorBolt = 0;
				GUIDBolt = "";
				boltMaterialLengthTekla.Clear();
				//count++;
				TMP2myTable = TMP1myTable.Copy();
			}
			
			return TolClearIDinDataTable(TMP1myTable);
		}
		
		//очистка первой колонки с ID
		public static DataTable TolClearIDinDataTable(DataTable dt)
		{
			int i = 0;
			foreach (DataRow row in dt.Rows)
			{
				row[0] = ++i;
			}
			return dt;
		}
		
		
		//получение строки из укзазанного рядка и нужных колонок
		public static string TolGetStringInRow(DataRow myRow, ArrayList columns)
		{
			string myString = "";
			foreach (string c in columns)
			{
				myString = myString + myRow[c];
			}
			return myString;
		}
		
		
		
		//добавление рядка в таблицу
		public static void TolAddBoltRow(DataTable myTable, TolBoltGroup b)
		{
			myTable.Rows.Add(null, b.funBolt, b.desBolt, b.t, b.washersBolt[0], b.washersBolt[1], b.washersBolt[2], b.nutsBolt[0], b.nutsBolt[1], b.nBolts, b.standardBolt, b.gradeBoltR, b.diaBolt, b.lengthBolt, b.materialLengthBoltTekla.ToString(), b.errorBolt, b.GUIDBolt);
		}
		
		public static string TolTitleReports(Model mo)
		{
			string XMLstr = "<?xml version=\"1.0\" encoding=\"windows-1251\"?>\r\n" + "<Object>\r\n" + "<ReportType>BPL</ReportType>\r\n";
			
			string title = "";
			
			string langString = "Українська";
			
			string projectNumber = "";
			
			string[] nameString = new string[]{ "" , "" , "" , "" , ""};

			
			mo.GetProjectInfo().GetUserProperty("SdUa_Language", ref langString);
			mo.GetProjectInfo().GetUserProperty("SdUa_ProjectNumber", ref projectNumber);
			mo.GetProjectInfo().GetUserProperty("SdUa_NameString1", ref nameString[0]);
			mo.GetProjectInfo().GetUserProperty("SdUa_NameString2", ref nameString[1]);
			mo.GetProjectInfo().GetUserProperty("SdUa_NameString3", ref nameString[2]);
			mo.GetProjectInfo().GetUserProperty("SdUa_NameString4", ref nameString[3]);
			mo.GetProjectInfo().GetUserProperty("SdUa_NameString5", ref nameString[4]);
			
			for (int i = 0; i < nameString.Length; i++)
			{
				if (String.IsNullOrEmpty(nameString[i]))
				{
					nameString[i] = "";
				}
				else
				{
					nameString[i] = @"<Name>" + nameString[i] + @"</Name>";
				}
			}
			
			title = XMLstr + "<Language>" + langString + "</Language>\r\n" + "<Number>" + projectNumber + "</Number>\r\n" + nameString[0] + nameString[1] + nameString[2] + nameString[3] + nameString[4] + "\r\n";

			//MessageBox.Show(title);
			return title;
		}
		
		public static bool TolExportXMLFile(DataTable myTable , string fileName)
		{
			DataTable XMLTable = myTable.Copy();
			int a = XMLTable.Columns.Count;
			int nDel = 10;
			
			for (int i = nDel; i < a; i++) {
				XMLTable.Columns.RemoveAt(nDel);
				
			}
			XMLTable.Columns.Remove("id");

			// записать в файл     
			
			Model mo = new Model();
			string puth = mo.GetInfo().ModelPath + @"\Reports\" + fileName;

			

			
			
			
			
			XMLTable.TableName = "Bolt";
			DataSet myDataSet = new DataSet("Bolts");
			
			myDataSet.Tables.Add(XMLTable);
			
			string myText = TolTitleReports(mo);
			
			try
			{
				myDataSet.WriteXml(puth);
				//FileStream fs = new FileStream(puth,FileMode.Open,FileAccess.ReadWrite);
				
				StreamReader reader = new StreamReader( puth );
				string tmp = reader.ReadToEnd();
				reader.Close();
				
				string patern = "<?xml version=\"1.0\" standalone=\"yes\"?>";
				
				
				tmp = tmp.Replace( patern, myText );
				
				Encoding enc = Encoding.GetEncoding(1251);
				
				StreamWriter writer = new StreamWriter(puth, false, enc);
				
				writer.Write(tmp + "\r\n</Object>");
				writer.Close();
				
				
				return true;
			}
			catch (Exception ede)
			{
				return false;
			}
			
			
		}
	}
}
