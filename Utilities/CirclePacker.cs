using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

//ToDo: Update this to take in a list of Radii, to handle a list of Units with different sizes.
public static class CirclePacker {
    public static List<Circle> mCircles = new List<Circle>();
    public static Circle mDraggingCircle = null;
    public static Vector2 mPackingCenter;
    public static float mMinSeparation = 0f;

    public static List<GameObject> Spawn(GameObject prefab, Vector3 center, int count, float radius,float minSeparation = 0, int iterations = 5, long iterationCounter = 1) {
        List<GameObject> spawnedObjects = new List<GameObject>();
        mMinSeparation = minSeparation;
        mPackingCenter = new Vector2(center.x, center.z);
        GenerateCircles(mPackingCenter, count, radius, radius);
        for (int i = 0; i < iterations; i++) {
            OnFrameMove(iterationCounter);
        }

        for (int i = 0; i < mCircles.Count; i++) {
            Vector3 pos = new Vector3(mCircles[i].mCenter.x, 0, mCircles[i].mCenter.y);
            Quaternion rot = Quaternion.identity;
            GameObject prefabSpawned =  GameObject.Instantiate(prefab, pos, rot);
            spawnedObjects.Add(prefabSpawned);
            prefabSpawned.name = prefabSpawned.name.Replace("(Clone)",string.Format(" ({0})",i));
        }

        return spawnedObjects;
    }

    public static List<Vector3> GetPoints(Vector3 center, int count, float radius,float minSeparation = 0, int iterations = 5, long iterationCounter = 1) {
        mMinSeparation = minSeparation;
        mPackingCenter = new Vector2(center.x, center.z);
        List<Vector3> points = new List<Vector3>();
        GenerateCircles(mPackingCenter, count, radius, radius);
        for (int i = 0; i < iterations; i++) {
            OnFrameMove(iterationCounter);
        }

        for (int i = 0; i < mCircles.Count; i++) {
            points.Add(new Vector3(mCircles[i].mCenter.x, 0, mCircles[i].mCenter.y));
        }

        return points;
    }

    /// <summary>
    /// Generates a number of Packing circles in the constructor.
    /// Random distribution is linear
    /// </summary>
    public static void GenerateCircles(Vector2 pPackingCenter, int pNumCircles, double pMinRadius, double pMaxRadius) {
        mPackingCenter = pPackingCenter;

        // Create random circles
        mCircles.Clear();
        Random Rnd = new Random(System.DateTime.Now.Millisecond);
        for (int i = 0; i < pNumCircles; i++) {
            Vector2 nCenter = new Vector2((float) (mPackingCenter.x +
                                                   Rnd.NextDouble() * pMinRadius),
                (float) (mPackingCenter.y +
                         Rnd.NextDouble() * pMinRadius));

            float nRadius = (float) (pMinRadius + Rnd.NextDouble() *
                (pMaxRadius - pMinRadius));
            mCircles.Add(new Circle(nCenter, nRadius));
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="?"></param>
    /// <returns></returns>
    private static float DistanceToCenterSq(Circle pCircle) {
        return Vector2.Distance(pCircle.mCenter, mPackingCenter);
    }

    /// <summary>
    ///
    /// </summary>
    private static int Comparer(Circle p1, Circle P2) {
        float d1 = DistanceToCenterSq(p1);
        float d2 = DistanceToCenterSq(P2);
        if (d1 < d2)
            return 1;
        else if (d1 > d2)
            return -1;
        else return 0;
    }

    /// <summary>
    ///
    /// </summary>
    public static void OnFrameMove(long iterationCounter) {
        // Sort circles based on the distance to center
        mCircles.Sort(Comparer);

        float minSeparationSq = mMinSeparation * mMinSeparation;
        for (int i = 0; i < mCircles.Count - 1; i++) {
            for (int j = i + 1; j < mCircles.Count; j++) {
                if (i == j)
                    continue;

                Vector2 AB = mCircles[j].mCenter - mCircles[i].mCenter;
                float r = mCircles[i].mRadius + mCircles[j].mRadius;

                // Length squared = (dx * dx) + (dy * dy);
                float d = AB.SqrMagnitude() - minSeparationSq;
                float minSepSq = Math.Min(d, minSeparationSq);
                d -= minSepSq;

                if (d < (r * r) - 0.01) {
                    AB.Normalize();

                    AB *= (float) ((r - Math.Sqrt(d)) * 0.5f);

                    if (mCircles[j] != mDraggingCircle)
                        mCircles[j].mCenter += AB;
                    if (mCircles[i] != mDraggingCircle)
                        mCircles[i].mCenter -= AB;
                }
            }
        }


        /*
        float damping = 0.1f / (float) (iterationCounter);
        for (int i = 0; i < mCircles.Count; i++) {
            if (mCircles[i] != mDraggingCircle) {
                Vector2 v = mCircles[i].mCenter - mPackingCenter;
                v *= damping;
                mCircles[i].mCenter -= v;
            }
        }
        */
    }
}

[Serializable]
public class Circle {
    public Vector2 mCenter;
    public float mRadius;

    public Circle(Vector2 iCenter, float Radius) {
        mCenter = iCenter;
        mRadius = Radius;
    }

    public override string ToString() {
        return "Rad: " + mRadius + " _ Center: " + mCenter.ToString();
    }
}