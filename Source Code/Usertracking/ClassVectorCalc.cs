using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect.Toolkit.FaceTracking;


/*
 * Recommended bibliography:
 *  
 * The Cross Product (2014-06-28)
 * http://www.math.oregonstate.edu/home/programs/undergrad/CalculusQuestStudyGuides/vcalc/crossprod/crossprod.html
 * 
 * A Vector Type for C# (2014-06-28)
 * http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
 * 
 * 
 * Overloading assignment operator in C# (2014-06-28)
 * http://stackoverflow.com/questions/4537803/overloading-assignment-operator-in-c-sharp
 * 
 * 
 * Ray casting (2014-06-28)
 * http://www.cs.princeton.edu/courses/archive/fall00/cs426/lectures/raycast/sld001.htm
 */

namespace Usertracking
{
    public class VectorCalc
    {
        public struct Point3D
        {
            public float X;
            public float Y;
            public float Z;

            public Point3D(float x, float y, float z)
            {
                this.X = x;
                this.Y = y;
                this.Z = z;
            }

            public Point3D(float scalar)
            {
                this.X = scalar;
                this.Y = scalar;
                this.Z = scalar;
            }

            //this is required for easy compatibility with the Kinect.Facetracking.Vector2DF structure
            public static implicit operator Point3D(Vector3DF point)
            {
                return (
                    new Point3D(
                            point.X,
                            point.Y,
                            point.Z
                            )
                );
            }

            //this is required for easy compatibility with the Kinect.Facetracking.Vector2DF structure
            public static implicit operator Point3D(float scalar)
            {
                return (
                    new Point3D(
                            scalar,
                            scalar,
                            scalar
                            )
                );
            }

            public static Point3D operator + (Point3D point1, Point3D point2){
                return (
                    new Point3D(
                        point1.X+point2.X,
                        point1.Y+point2.Y,
                        point1.Z+point2.Z
                        )
                    );
            }

            public static Point3D operator - (Point3D point1, Point3D point2){
                return (
                    new Point3D(
                        point1.X-point2.X,
                        point1.Y-point2.Y,
                        point1.Z-point2.Z
                        )
                    );
            }

            public static Point3D operator / (Point3D point1, float scalar){
                return (
                    new Point3D(
                        point1.X/scalar,
                        point1.Y/scalar,
                        point1.Z/scalar
                        )
                    );
            }

            public static Point3D operator * (Point3D point1, float scalar){
                return (
                    new Point3D(
                        point1.X*scalar,
                        point1.Y*scalar,
                        point1.Z*scalar
                        )
                    );
            }

            public static Point3D operator *(float scalar, Point3D point1)
            {
                return (
                    new Point3D(
                        point1.X * scalar,
                        point1.Y * scalar,
                        point1.Z * scalar
                        )
                    );
            }
        }

        public struct Vector3D
        {
            public Point3D origin;

            public float X; //X non-normalized direction
            public float Y; //Y non-normalized direction
            public float Z; //Z non-normalized direction

            public Vector3D(float origin_x, float origin_y, float origin_z, float displacement_x, float displacement_y, float displacement_z)
            {


                this.origin.X = origin_x;
                this.origin.Y = origin_y;
                this.origin.Z = origin_z;

                this.X = displacement_x;
                this.Y = displacement_y;
                this.Z = displacement_z;
            }

            
            public Vector3D(Point3D origin_point, Point3D displacement_vector)
            {

                this.origin = origin_point;
                this.X = displacement_vector.X;
                this.Y = displacement_vector.Y;
                this.Z = displacement_vector.Z;
            }

            public Vector3D(Point3D origin_point, float displacement_x, float displacement_y, float displacement_z)
            {

                this.origin = origin_point;
                this.X = displacement_x;
                this.Y = displacement_y;
                this.Z = displacement_z;
            }

            public float Magnitude()
            {
                return (float)Math.Sqrt((double) this.X * this.X + this.Y * this.Y + this.Z * this.Z);
            }

            public Vector3D Direction()
            {
                float mag;

                mag = this.Magnitude();

                if (mag > 0)
                {
                    return (
                        new Vector3D(
                            this.origin,
                            this.X / mag,
                            this.Y / mag,
                            this.Z / mag
                            )
                        );
                }
                else
                {
                    return (
                        new Vector3D(
                            this.origin,
                            0,
                            0,
                            0
                            )
                        );
                }

            }

            public static Vector3D operator +(Vector3D vector1, Vector3D vector2)
            {
                Vector3D v3;

                v3.X = vector1.X + vector2.X;
                v3.Y = vector1.Y + vector2.Y;
                v3.Z = vector1.Z + vector2.Z;

                v3.origin = vector1.origin;

                return v3;
            }

            public static Vector3D crossProduct(Vector3D vector1, Vector3D vector2)
            {
                Vector3D v3;

                v3.origin = 0.0f; //defines X=0, Y=0 and Z = 0

                v3.X = vector1.Y * vector2.Z - vector1.Z * vector2.Y;
                v3.Y = vector1.Z * vector2.X - vector1.X * vector2.Z;
                v3.Z = vector1.X * vector2.Y - vector1.Y * vector2.X;


                return v3;
            }



            public static float dotProduct(Vector3D vector1, Vector3D vector2)
            {
                float scalar;

                scalar = vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;

                return scalar;
            }

            public static float dotProduct(Point3D point, Vector3D vector)
            {
                float scalar;

                scalar = point.X * vector.X + point.Y * vector.Y + point.Z * vector.Z;

                return scalar;
            }
        
            public static Vector3D operator *(float scalar, Vector3D vector){
                return (new Vector3D(vector.origin, scalar * vector.X, scalar * vector.Y, scalar * vector.Z));
            }
        }

        //Returns the positive orthogonal vector to the plane defined by three points sequenced counter-clockwise
        public static Vector3D OrthoVector(Vector3DF point1, Vector3DF point2, Vector3DF point3)
        {
            Vector3D v1;
            Vector3D v2;
            Vector3D v3;

            v1.origin = point1;
            v2.origin = point2;

            v1.X = point2.X - point1.X;
            v1.Y = point2.Y - point1.Y;
            v1.Z = point2.Z - point1.Z;

            v2.X = point3.X - point2.X;
            v2.Y = point3.Y - point2.Y;
            v2.Z = point3.Z - point2.Z;

            v3=Vector3D.crossProduct(v1, v2);
            
            //set middle point between 3 points
            v3.origin = point3;
            v3.origin = (v1.origin + v2.origin + v3.origin) / 2.0f; 

            return v3;
        }

        //Returns the positive orthogonal vector to the plane defined by three points sequenced counter-clockwise
        public static Vector3D OrthoVector(Vector3D vector1, Vector3D vector2)
        {
            return Vector3D.crossProduct(vector1, vector2);
        }





        public static Point3D middle(Point3D point1, Point3D point2){
            Point3D pm;

            pm = (point1 + point2) / 2.0f;

            return pm;
        }

        /* applied ray math from ray plane intersection from
        * http://www.cs.princeton.edu/courses/archive/fall00/cs426/lectures/raycast/sld017.htm
        */
        public static Point3D intersection(Vector3D ray, Point3D plane_point, Vector3D plane_normal_vector)
        {
            Point3D destiny_point;
            float d;

            d = Vector3D.dotProduct(plane_point, plane_normal_vector);

            /*
             * Ray define by: P=P0 + t.V
             * Plane defined by: P | N + d = 0
             * where: P0 is the origin point of the ray
             *        V is the normalized direction vector (eigenvector) of ray
             *        t is the eigenvalue of ray
             *        P is the intersection point between the ray and the plane
             *        N is the normal vector to the plane
             *        d is d = Q | N where Q is any point of the plane, it can be the (0,0,0) only if it belongs to the plane
             *        | is the dot product
             *        . is the scalar product
             *        
             * Substituting P we get:
             * 
             *    (P0 +t.V) | N = -d
             *    
             *     t = - (P0 | N + d) / (V | N)
             * 
             * Destiny point X = P0 + t . V
             */

            float denominator = Vector3D.dotProduct(ray.origin, plane_normal_vector);
            float t;

            if (denominator != 0)
                t = (Vector3D.dotProduct(ray.origin, plane_normal_vector) + d) / denominator * (-1.0f);
            else
                t = 0;

            Point3D V;

            V=new Point3D(ray.X, ray.Y, ray.Z);

            destiny_point = ray.origin + t * V;

            return destiny_point;
        }

        public static Point3D Scale(Point3D ScaleMin, Point3D ScaleMax, Point3D InputMin, Point3D InputMax, Point3D Input)
        {
            Point3D result;

            float InputXSize = (InputMax.X - InputMin.X);
            float InputYSize = (InputMax.Y - InputMin.Y);
            float InputZSize = (InputMax.Z - InputMin.Z);

            if (InputXSize != 0)
                result.X = (Input.X - InputMin.X) / InputXSize * (ScaleMax.X - ScaleMin.X) + ScaleMin.X;
            else
                result.X = 0;

            if (InputYSize != 0)
                result.Y = (Input.Y - InputMin.Y) / InputYSize * (ScaleMax.Y - ScaleMin.Y) + ScaleMin.Y;
            else
                result.Y = 0;

            if (InputZSize != 0)
                result.Z = (Input.Z - InputMin.Z) / InputZSize * (ScaleMax.Y - ScaleMin.Z) + ScaleMin.Z;
            else
                result.Z = 0;

            return result;
        }
        
    }
}
