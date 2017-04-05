/*
 * Created by SharpDevelop.
 * User: Vitalii.Zotov
 * Date: 03.04.2017
 * Time: 14:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data;
using System.Collections;
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
			myTable.Columns.Add("funBolt", typeof(string));
			myTable.Columns.Add("desBolt", typeof(string));
			myTable.Columns.Add("t", typeof(double));
			myTable.Columns.Add("washer1", typeof(string));
			myTable.Columns.Add("washer2", typeof(string));
			myTable.Columns.Add("washer3", typeof(string));
			myTable.Columns.Add("nut1", typeof(string));
			myTable.Columns.Add("nut2", typeof(string));
			myTable.Columns.Add("nBolts", typeof(int));
			myTable.Columns.Add("standardBolt", typeof(string));
			myTable.Columns.Add("gradeBolt", typeof(string));
			myTable.Columns.Add("lengthBolt", typeof(double));
			myTable.Columns.Add("diaBolt", typeof(double));
			myTable.Columns.Add("GUIDBolt", typeof(string));
			
			
			
			//myTable.Columns.Add("BoltGroups", typeof(ArrayList));
			return myTable;
		}
		
		//сортировка таблицы
		public static DataTable TolSortDataTable(DataTable myTable)
		{
			DataView dv = myTable.DefaultView;
			dv.Sort = "funBolt ASC, standardBolt ASC, gradeBolt ASC, diaBolt ASC, lengthBolt ASC, desBolt ASC, t ASC, washer1 ASC, washer2 ASC, washer3 ASC, nut1 ASC, nut2 ASC";
			DataTable sortedDT = dv.ToTable();
			
			return TolClearIDinDataTable(sortedDT);
		}
		
		
		//объеденение одинаковых строк
		public static DataTable TolCompressTable(DataTable myTable, string name)
		{
			DataTable TMP1myTable = myTable.Copy();
			DataTable TMP2myTable = myTable.Copy();
			int count = 0;
			ArrayList columns = new ArrayList(){ "funBolt", "desBolt", "t", "washer1", "washer2", "washer3", "nut1", "nut2" }; // по каким колонкам сравнивать
			string tmp1;
			string tmp2;
			int nBolts = 0;
			int nDel = 0; // кол-во удаленных строк
			string GUIDBolt = "";
			
			
			
			
			for (int j = 0; j < TMP1myTable.Rows.Count; j++)
			{
				tmp1 = TolGetStringInRow(TMP1myTable.Rows[j], columns);
				
				for (int i = j; i < TMP2myTable.Rows.Count; i++)
				{
					tmp2 = TolGetStringInRow(TMP2myTable.Rows[i], columns);
					
					if (tmp1.Equals(tmp2))
					{
						nBolts += (int)TMP2myTable.Rows[i]["nBolts"];
						
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
				TMP1myTable.Rows[j]["nBolts"] = nBolts;
				TMP1myTable.Rows[j]["GUIDBolt"] = GUIDBolt;
				//TMP1myTable.Rows[j]["id"] = count;
				nBolts = 0;
				GUIDBolt = "";
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
		
		
		
		//добавление строки в таблицу
		public static void TolAddBoltRow(DataTable myTable, TolBoltGroup b)
		{
			myTable.Rows.Add(null, b.funBolt, b.desBolt, b.t, b.washersBolt[0], b.washersBolt[1], b.washersBolt[2], b.nutsBolt[0], b.nutsBolt[1], b.nBolts, b.standardBolt, b.gradeBoltR, b.lengthBolt, b.diaBolt, b.GUIDBolt);
		}
	}
}
