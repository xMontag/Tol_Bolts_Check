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
			
			
			
			//myTable.Columns.Add("BoltGroups", typeof(ArrayList));
			return myTable;
		}
		
		//сортировка таблицы
		public static DataTable TolSortDataTable(DataTable myTable)
		{
			DataView dv = myTable.DefaultView;
			dv.Sort = "funBolt ASC, standardBolt ASC, gradeBolt ASC, diaBolt ASC, lengthBolt ASC, desBolt ASC, t ASC, washer1 ASC, washer2 ASC, washer3 ASC, nut1 ASC, nut2 ASC";
			DataTable sortedDT = dv.ToTable();
			
			int i = 0;
			foreach (DataRow row in sortedDT.Rows)
			{
				row[0] = ++i;
			}
			return sortedDT;
		}
		
		
		//объеденение одинаковых строк
		public static DataTable TolCompressTable(DataTable myTable, string name)
		{
			DataTable compressedDT = TolCreateDateTable(name);
			DataTable TMP1myTable = myTable.Copy();
			DataTable TMP2myTable = myTable.Copy();
			int count = 0;
			ArrayList columns = new ArrayList(){ "funBolt" , "desBolt" , "t"};
			string tmp1;
			string tmp2;
			int nBolts = 0;
			DataRow TMPmyRow;
			
			while (true)
			{
				
				if (TMP2myTable.Rows.Count == 0)
				{
					break;
				}
				tmp1 = TolGetStringInRow(TMP1myTable.Rows[0], columns);
				TMPmyRow = TMP1myTable.Rows[0];
				for (int i = 0; i < TMP2myTable.Rows.Count; i++)
				{
					tmp2 = TolGetStringInRow(TMP1myTable.Rows[i], columns);
					if (tmp1.Equals(tmp2))
					{
						nBolts += (int)TMP2myTable.Rows[i]["nBolts"];
						TMP1myTable.Rows[i].Delete();
					}
					TMP2myTable = TMP1myTable.Copy();
					
				}
				compressedDT.Rows.Add(TMPmyRow);
				compressedDT.Rows[count]["nBolts"] = nBolts;
				compressedDT.Rows[count]["id"] = count;
				count++;

			}
			
			return compressedDT;
		}
		
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
		
			myTable.Rows.Add(null, b.funBolt, b.desBolt, b.t, b.washersBolt[0], b.washersBolt[1], b.washersBolt[2], b.nutsBolt[0], b.nutsBolt[1], b.nBolts, b.standardBolt, b.gradeBoltR, b.lengthBolt, b.diaBolt);
		}
	}
}
