﻿using System;
using System.Globalization;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Solid;

namespace Tol_Bolts_Check
{
	/// <summary>
	/// Description of TolUtils.
	/// </summary>
	
	
	
	
	public class TolUtils
	{
		private readonly Model _Model = new Model();
		private static GraphicsDrawer GraphicsDrawer = new GraphicsDrawer();
		private readonly static Color TextColor = new Color(1, 0, 1);
		
		// выбрать болт
		public static BoltGroup PickBoltGroup()
		{
			Picker picker = new Picker();            
            BoltGroup myBoltGroup = picker.PickObject(Picker.PickObjectEnum.PICK_ONE_BOLTGROUP, "pick BOLT GROUP") as BoltGroup;
			return myBoltGroup;
		}
		
		
		// получить выделенные объекты
		public static ModelObjectEnumerator TolGetSelctedObjects()
		{
			Tekla.Structures.Model.UI.ModelObjectSelector a = new Tekla.Structures.Model.UI.ModelObjectSelector();
			return a.GetSelectedObjects();

		}
		
		public static void TolSelectObjectsByStringGUID(string stringGUID)
		{
			Model m = new Model();
			//Identifier myIdentifier;
			ArrayList ObjectsToSelect = new ArrayList();
			Tekla.Structures.Model.UI.ModelObjectSelector MS = new Tekla.Structures.Model.UI.ModelObjectSelector();
			
			foreach (string myGUID in stringGUID.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
			{
				ObjectsToSelect.Add(m.SelectModelObject(m.GetIdentifierByGUID(myGUID)));
			}

			
			MS.Select(ObjectsToSelect);
			m.CommitChanges();
		}
		
		public static bool TolTestOfBolt (BoltGroup b)
		{
			
			bool a = true;
			
			if (!b.Bolt)
			{
				a = false;
			}
			
			string standardBolt = "";
			b.GetReportProperty("BOLT_STANDARD", ref standardBolt);
			
			if (b.BoltStandard.Contains("Null") || standardBolt.Contains("TMP"))
			{
				a = false;
			}
			
			return a;
		}

		
		
		// проверка группы болтов
		public static ArrayList CheckOneBoltGroup(BoltGroup myBoltGroup)
        {
			bool[] boltChecks = new bool[1] { true };
            
			double boltLength = 0,
			       boltDiameter = 0;
			// число деталей в пакете
			int boltNParts = 1;
			myBoltGroup.GetReportProperty("BOLT_NPARTS", ref boltNParts);

			// спислк деталей пакета

			List<Part> boltedParts = new List<Part>();
			boltedParts.Add(myBoltGroup.PartToBoltTo);
			if (boltNParts > 2)
			{
				foreach (Part part in myBoltGroup.OtherPartsToBolt)
				{
					boltedParts.Add(part);
				}
			}
			if (boltNParts > 1)
			{
				boltedParts.Add(myBoltGroup.PartToBeBolted);
			}

			// другие свойства болта
			myBoltGroup.GetReportProperty("LENGTH",ref boltLength);
			myBoltGroup.GetReportProperty("DIAMETER", ref boltDiameter);

			double boltExtraLenght = myBoltGroup.ExtraLength;
			double boltCutLength = myBoltGroup.CutLength;

			// список отрезков по каждому болту

			ArrayList boltLines = new ArrayList();
			// система координат болта
			CoordinateSystem boltCS = new CoordinateSystem(myBoltGroup.FirstPosition, myBoltGroup.GetCoordinateSystem().AxisX, myBoltGroup.GetCoordinateSystem().AxisY);
			Matrix transformationMatrix = MatrixFactory.ToCoordinateSystem(boltCS);

			//Console.WriteLine(boltCS.Origin);
			//Console.WriteLine(myBoltGroup.SecondPosition);
			//Console.WriteLine(transformationMatrix.Transform(myBoltGroup.SecondPosition));
			foreach (Point p in myBoltGroup.BoltPositions)
			{
				Point myPoint = new Point(transformationMatrix.Transform(p));
				Point pStart = new Point(myPoint.X, myPoint.Y, myPoint.Z - boltCutLength / 2);
				Point pEnd = new Point(myPoint.X, myPoint.Y, myPoint.Z + boltCutLength / 2);
				LineSegment ls = new LineSegment(MatrixFactory.FromCoordinateSystem(boltCS).Transform(pStart), MatrixFactory.FromCoordinateSystem(boltCS).Transform(pEnd));
				//GraphicsDrawer.DrawText(ls.Point1, "start", TextColor);
				//GraphicsDrawer.DrawText(ls.Point2, "end", TextColor);
				boltLines.Add(ls);
			}

			// список отрезков пересечения каждого болта с каждой деталью пакета + проверка на правильность в модели CHECK - 0

			ArrayList myBoltArrayLS = new ArrayList();

			foreach (LineSegment ls in boltLines)
			{
				ArrayList myBoltSubArrayLS = new ArrayList();
				myBoltSubArrayLS.Add(ls);
				foreach (Part part in boltedParts)
				{
					
					ArrayList intersections = new ArrayList(part.GetSolid().Intersect(ls));
					LineSegment myLineSegment = new LineSegment();
					if (intersections.Count == 0)
					{
						//Console.WriteLine("check 1 NO!");
						myLineSegment = new LineSegment(ls.Point1, ls.Point2);
						myBoltSubArrayLS.Add(myLineSegment);
						boltChecks[0] = false;
						//break;
					}
					else if (intersections.Count % 2 != 0)
					{
						myLineSegment = new LineSegment(intersections[0] as Point, intersections[intersections.Count - 1] as Point);
						myBoltSubArrayLS.Add(myLineSegment);
						boltChecks[0] = false;
					}
					else
					{
						myLineSegment = new LineSegment(intersections[0] as Point, intersections[intersections.Count - 1] as Point);
						myBoltSubArrayLS.Add(myLineSegment);
					} 	
				}
				myBoltArrayLS.Add(myBoltSubArrayLS);

			}


			// проверка болтового поля на правильность в модели CHECK - 0

			foreach (ArrayList a in myBoltArrayLS)
			{
				if (!(compareArrayLS(myBoltArrayLS[0] as ArrayList, a)))
				{
					boltChecks[0] = false;
					//break;
				}

			}

			// составление списка отрезков пересечений одного болта где под индексом 0 полный отрезок болта

			ArrayList myBoltArrayOneLS = new ArrayList();
			foreach (LineSegment ls in myBoltArrayLS[0] as ArrayList)
			{
				myBoltArrayOneLS.Add(TransformLineSegmentToCS(ls, boltCS));

			}

			Point firstPoint = (myBoltArrayOneLS[0] as LineSegment).Point1;
			// вывод точек в консоль
			//PrintConsole(myBoltArrayOneLS);
			// сортировка отрезков
			myBoltArrayOneLS = SortArrayLS(myBoltArrayOneLS, firstPoint);
			//PrintConsole(myBoltArrayOneLS);
			//Console.WriteLine(boltChecks[0]);
			
			ArrayList res = new ArrayList();
			res.Add(myBoltArrayOneLS);
			res.Add(boltChecks);
			return res;
		}

		// проверка отрезков пересечения с деталью на одинаковость во всех болтах болтового поля

		public static bool compareArrayLS(ArrayList Arr1, ArrayList Arr2)
		{
			//Console.WriteLine(Arr1.Count + " " + Arr2.Count);
			if (Arr1.Count != Arr2.Count)
			{
				return false;
			}
			else
			{
				LineSegment l1 = Arr1[0] as LineSegment;;
				LineSegment l2 = Arr2[0] as LineSegment;;
				LineSegment ls1;
				LineSegment ls2;

				for (int i = 1; i < Arr1.Count; i++)
				{
					ls1 = Arr1[i] as LineSegment;
					ls2 = Arr2[i] as LineSegment;

					LineSegment segm1Start = new LineSegment(l1.Point1, ls1.Point1);
					LineSegment segm1End = new LineSegment(l1.Point1, ls1.Point2);
					LineSegment segm2Start = new LineSegment(l2.Point1, ls2.Point1);
					LineSegment segm2End = new LineSegment(l2.Point1, ls2.Point2);

					//Console.WriteLine(segm1Start.Length() + " " + segm2Start.Length());
					//Console.WriteLine(segm2End.Length() + " " + segm2End.Length());
					if ((Math.Abs(segm1Start.Length() - segm2Start.Length()) > 0.1) && (Math.Abs(segm1End.Length() - segm2End.Length()) > 0.1))
					{
						return false;
					}
				}
			}
			return true;
		}

		// перевод отрезка из одной системы координат в другую

		public static LineSegment TransformLineSegmentToCS(LineSegment ls, CoordinateSystem cs)
		{
			Matrix transformationMatrix = MatrixFactory.ToCoordinateSystem(cs);

			LineSegment newLS = new LineSegment(new Point(transformationMatrix.Transform(ls.Point1)), new Point(transformationMatrix.Transform(ls.Point2)));
			return newLS;
		}

		// соритировка отрезков от максимально приближенного к точке до удаленного

		public static ArrayList SortArrayLS(ArrayList arr, Point p)
		{
			ArrayList newArr = new ArrayList();
			foreach (LineSegment ls in arr)
			{
				Point p1_r = new Point(ls.Point1);
				Point p2_r = new Point(ls.Point2);
				Point p1 = new Point();
				Point p2 = new Point();
				LineSegment ls1 = new LineSegment(p, p1_r);
				LineSegment ls2 = new LineSegment(p, p2_r);
				if (ls1.Length() <= ls2.Length())
				{
					p1 = p1_r;
					p2 = p2_r;
				}
				else
				{
					p2 = p1_r; 
					p1 = p2_r;
				}
				newArr.Add(new LineSegment(p1, p2));
			}
			for (int j = 0; j < newArr.Count - 1; j++)
			{
				bool f = false;
				for (int i = 0; i < newArr.Count - j - 1; i++)
				{
					LineSegment ls1 = new LineSegment(p, (newArr[i] as LineSegment).Point1);
					LineSegment ls2 = new LineSegment(p, (newArr[i + 1] as LineSegment).Point1);

					if (ls1.Length() > ls2.Length())
					{
						object temp = newArr[i];
						newArr[i] = newArr[i + 1];
						newArr[i + 1] = temp;
						f = true;
					}
				}
				if (f == false)
				{
					break;
				}
			}

			return newArr;
		}
		
		//сортировка списка отрезков болта

		public static ArrayList TolSort(ArrayList arr)
		{
			for (int j = 0; j <= arr.Count - 1; j++)
			{
				bool f = false;
				for (int i = 0; i < arr.Count - j - 1; i++)
				{
					if ((int)arr[i] > (int)arr[i + 1])
					{
						object temp = arr[i];
						arr[i] = arr[i + 1];
						arr[i + 1] = temp;
						f = true;	
					}
				}
				if (f == false)
				{
					break;
				}

			}
			return arr;
		}
		
		
		

		// вывод списка отрезков

		public static void PrintConsole(ArrayList arr)
		{
			foreach (LineSegment ls in arr)
			{
				Console.WriteLine(ls.Point1 + " " + ls.Point2 + " " + ls.Length());
			}
			Console.WriteLine();
		}

			
    }
}
