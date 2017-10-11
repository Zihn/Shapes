using UnityEngine;
using System.Collections.Generic;

public class OverlapDetector : MonoBehaviour {
//	
//	Vector3[] otherVertices;
//	Vector3[] shapeVertices;
//	Vector3 otherCentre; 
//	Vector3 centre; 
//	List<Vector3> overlapVertices;
//	List<Vector3[]> lines;
//	Vector3[] linePoints;
//	ShapeCreator SC;
//
//	void Awake() {
//		SC = gameObject.GetComponent<ShapeCreator> ();
//	}
//
//	public Vector3[] DetectOverlap(Shape shape, Shape otherShape)
//	{
//		Vector3[] otherVertices = otherShape.getVertices();
//		Vector3[] shapeVertices = shape.getVertices();
//		Vector3 otherCentre = otherShape.GetCentre(); 
//		Vector3 centre = shape.GetCentre(); 
//		List<Vector3> overlapVertices = new List<Vector3>();
//		// Each shape has two lines intersecting the other shape's boundary
//		// The first two are from the 'other' shape
//		// Each line has one point in and one out the other shape's area
//		Vector3[,] linePoints = new Vector3[4, 2];
//
//		// Index of i and j vertices where i or j is the outer vertex,
//		// Depending on position.. The outer vertex will be replaced by
//		// the overlapping vertex. And marks the start/end of the overlap
//		// boundary from either shape 1 or 2. 
//
//		int[] overlapLineIdx = new int[4];
//		int idx = 0;
//		int iIdx = 0;
//
//		int j = otherVertices.Length - 1;
//		int l = j + 1;
//
//		Vector3 vj = otherVertices [j]+otherCentre;
//		bool prevInside = shape.IsInShape(vj);
//
//		for (int i = 0; i < l; i++)
//		{
//			Vector3 vi = otherVertices [i]+otherCentre;
//
//			bool inside = shape.IsInShape(vi);
//			if (inside) {
//				if (prevInside != inside) {
//					linePoints [iIdx, 0] = vi;
//					linePoints [iIdx, 1] = vj;
//					overlapVertices.Add (vi);
//					overlapLineIdx [iIdx] = idx;
//					iIdx++;
//					idx++;
//				}
//				overlapVertices.Add (vi);
//				idx++;
//			} else {
//				if (prevInside != inside) {
//					linePoints [iIdx, 0] = vj;
//					linePoints [iIdx, 1] = vi;
//					overlapVertices.Add (vj);
//					overlapLineIdx [iIdx] = idx;
//					iIdx++;
//					idx++;
//				}
//			}
//			j = i;
//			prevInside = inside;
//			vj = vi;
//		}
//
//		// Do it again
//		j = shapeVerts.Length - 1;
//		l = j + 1;
//
//		vj = shapeVerts [j]+centre;
//		prevInside = otherShape.IsInShape(vj);
//
//		for (int i = 0; i < l; i++)
//		{
//			Vector3 vi = shapeVerts [i]+centre;
//
//			bool inside = otherShape.IsInShape(vi);
//			if (inside) {
//				if (prevInside != inside) {
//					linePoints [iIdx, 0] = vi;
//					linePoints [iIdx, 1] = vj;
//					overlapVertices.Add (vi);
//					overlapLineIdx [iIdx] = idx;
//					iIdx++;
//					idx++;
//				}
//				overlapVertices.Add (vi);
//				idx++;
//			} else {
//				if (prevInside != inside) {
//					linePoints [iIdx, 0] = vi;
//					linePoints [iIdx, 1] = vj;
//					overlapVertices.Add (vj);
//					overlapLineIdx [iIdx] = idx;
//					iIdx++;
//					idx++;
//				}
//			}
//			j = i;
//			prevInside = inside;
//			vj = vi;
//		}
//
//		// Check the linepoints
//		if (iIdx == 4) {
//			for (int h = 0; h < 2; h++) {
//				Vector2 p11 = linePoints [h, 0];
//				Vector2 p12 = linePoints [h, 1];
//				for (int g = 0; g < 2; g++) {
//					Vector2 p21 = linePoints [g + 2, 0];
//					Vector2 p22 = linePoints [g + 2, 1];
//					Vector2 intersectPoint;
//					bool intersect = LineSegmentsIntersect (p11, p12, p21, p22, out intersectPoint);
//					//					intersectPoint = new Vector3 (intersectPoint.x, intersectPoint.y, 0f);
//					if (intersect) {
//						overlapVertices [overlapLineIdx [h]] = intersectPoint; 
//						overlapVertices [overlapLineIdx [g + 2]] = intersectPoint;
//					}
//				}
//			}
//		}
//		return overlapVertices.ToArray();
//	}
//
//	public Vector3[] GetOverlapVertices(Shape otherShape){
//		List<Vector3> overlapVertices = new List<Vector3>();
//		List<Vector3[]> lines = new List<Vector3[]>();
//		Vector3[] linePoints = new Vector3[2];
//		Vector3[] otherVertices = otherShape.getVertices();
//		Vector3 otherCentre = otherShape.centre; 
//		// Index of i and j vertices where i or j is the outer vertex,
//		// Depending on position.. The outer vertex will be replaced by
//		// the overlapping vertex. And marks the start/end of the overlap
//		// boundary from either shape 1 or 2. 
//
//		List<int> overlapLineIdx = new List<int>();
//		int idx = 0;
//
//		int j = otherVertices.Length - 1;
//		int l = j + 1;
//
//		Vector3 vj = otherVertices [j] + otherCentre;
//		bool prevInside = IsInShape(vj);
//
//		for (int i = 0; i < l; i++)
//		{
//			Vector3 vi = otherVertices [i]+otherCentre;
//
//			bool inside = IsInShape(vi);
//			if (inside) {
//				if (prevInside != inside) {
//					linePoints [0] = vi;
//					linePoints [1] = vj;
//					overlapVertices.Add (vi);
//					overlapLineIdx.Add(idx);
//					idx++;
//				}
//				overlapVertices.Add (vi);
//				idx++;
//			} else {
//				if (prevInside != inside) {
//					linePoints [0] = vj;
//					linePoints [1] = vi;
//					overlapVertices.Add (vj);
//					overlapLineIdx.Add(idx);
//					idx++;
//				}
//			}
//			j = i;
//			prevInside = inside;
//			vj = vi;
//		}
//	}
//
//	/// <summary>
//	/// Test whether two line segments intersect. If so, calculate the intersection point.
//	/// see discussion: "http://stackoverflow.com/a/14143738/292237"/ 
//	/// code from: "http://www.codeproject.com/Tips/862988/Find-the-Intersection-Point-of-Two-Line-Segments">
//	/// </summary>
//	/// <param name="p">Vector to the start point of p.</param>
//	/// <param name="p2">Vector to the end point of p.</param>
//	/// <param name="q">Vector to the start point of q.</param>
//	/// <param name="q2">Vector to the end point of q.</param>
//	/// <param name="intersection">The point of intersection, if any.</param>
//	/// <param name="considerOverlapAsIntersect">Do we consider overlapping lines as intersecting?
//	/// </param>
//	/// <returns>True if an intersection point was found.</returns>
//	bool LineSegmentsIntersect(Vector2 p, Vector2 p2, Vector2 q, Vector2 q2, 
//		out Vector2 intersection)
//	{
//		intersection = new Vector2();
//		SC.DrawDebugLine (p, p2, 1);
//		SC.DrawDebugLine (q, q2, 1);
//
//		Vector2 r = p2 - p;
//		Vector2 s = q2 - q;
//		float rxs = r.x*s.y - r.y*s.x;  //Cross product: v × w = vx wy − vy wx
//		Vector2 qp = q - p;
//		Vector2 pq = p - q;
//		float qpxr = qp.x*r.y - qp.y*r.x;
//		float qpxs = qp.x*s.y - qp.y*s.x;
//
//		// If r x s = 0 and (q - p) x r = 0, then the two lines are collinear.
//		if (rxs == 0 && qpxr == 0)
//		{
//			return false;
//		}
//
//		// If r x s = 0 and (q - p) x r != 0, then the two lines are parallel and non-intersecting.
//		if (rxs == 0 && qpxr != 0)
//			return false;
//
//		// t = (q - p) x s / (r x s)
//		float t = qpxs/rxs;
//
//		// u = (q - p) x r / (r x s)
//
//		float u = qpxr/rxs;
//
//		// If r x s != 0 and 0 <= t <= 1 and 0 <= u <= 1
//		// the two line segments meet at the point p + t r = q + u s.
//		if (rxs != 0 && (0 <= t && t <= 1) && (0 <= u && u <= 1))
//		{
//			// We can calculate the intersection point using either t or u.
//			intersection = p + t*r;
//
//			// An intersection was found.
//			return true;
//		}
//		// Otherwise, the two line segments are not parallel but do not intersect.
//		return false;
//	}
}
