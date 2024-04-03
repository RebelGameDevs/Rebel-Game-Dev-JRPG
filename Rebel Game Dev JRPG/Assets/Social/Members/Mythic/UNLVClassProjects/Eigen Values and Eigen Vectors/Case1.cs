using RebelGameDevs.Utils.UnrealIntegration;
using UnityEngine;
namespace Mythic
{
    public class Case1 : UnrealObject
    {
        //Defines:
        public struct Matrix2x2
        {
            public float m00, m01, m10, m11;

            public Matrix2x2(float m00, float m01, float m10, float m11)
            {
                this.m00 = m00;
                this.m01 = m01;
                this.m10 = m10;
                this.m11 = m11;
            }

            public Vector2 Solve(Vector2 b)
            {
                float det = m00 * m11 - m01 * m10;
                if (Mathf.Approximately(det, 0))
                {
                    Debug.LogError("Matrix is singular, cannot solve equation");
                    return Vector2.zero;
                }

                float invDet = 1f / det;
                float x = (m11 * b.x - m01 * b.y) * invDet;
                float y = (-m10 * b.x + m00 * b.y) * invDet;

                return new Vector2(x, y);
            }
        }
        private float a = 10f; // 10
        private float b = 5f; // 5
        private float c = 10f; //10
        protected override void BeginPlay()
        {
            CalculateEigenValues();
        }
        private void CalculateEigenValues()
        {
            /* 
                Calculate the coefficients of the characteristic polynomial:
                    - for a 2 x 2 matrix the characteristic polynomial is: ax^2 + bx + c = 0
            */
            float coeff_a = 1; //a = 1:
            float coeff_b = -2 * a; //b = -2a:
            float coeff_c = (a * a) - (b * c);//c = a^2 - bc:

            /* 
                Calculate the discriminant: ax^2 + bx + c = 0: where b^2 - 4ac:
                    - the discriminant tells us if the nature of the root is quadratic.
                    if it's positive its real, if it's zero it's repeated (real or equal).
                    if it's negative it's complex:
            */
            float discriminant = coeff_b * coeff_b - 4 * coeff_a * coeff_c;

            if (discriminant >= 0)
            {
                // Calculate eigenvalues
                float lambda1 = (-coeff_b + Mathf.Sqrt(discriminant)) / (2 * coeff_a);
                float lambda2 = (-coeff_b - Mathf.Sqrt(discriminant)) / (2 * coeff_a);

                //Negation as we go over the equal sign cause => lambda + number = 0; => lambda = -number;
                lambda1 = -lambda1;
                lambda2 = -lambda2;

                Debug.Log("Eigenvalue 1: " + lambda1);
                Debug.Log("Eigenvalue 2: " + lambda2);
                CalculateEigenVectors(lambda1, lambda2);
            }
            else
            {
                Debug.Log("Complex eigenvalues. Cannot calculate.");
            }
        }
        private void CalculateEigenVectors(float eigenValue1, float eigenValue2)//(A - lambda * I) * v = 0
        {
            // For each eigenvalue, solve the equation (A - lambda I) * v = 0
            Debug.Log("Eigenvector 1: " + SolveEigenVector(eigenValue1));
            Debug.Log("Eigenvector 2: " + SolveEigenVector(eigenValue2));
        }
        private Vector2 SolveEigenVector(float eigenValue)
        {
             // Create the matrix A - lambda I
            Matrix2x2 matrix = new Matrix2x2(1 - eigenValue, b, c, 1 - eigenValue);

            // Solve the equation (A - lambda I) * v = 0
            Vector2 v = matrix.Solve(new Vector2(1, 0)); // Arbitrary initial vector

            return v;
        }
    }
}
