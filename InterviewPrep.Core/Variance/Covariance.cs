using System;

namespace InterviewPrep.Core.Variance
{
    public class Variance : ICloneable, IEquatable<Variance>
    {
        //no return covariance available
        public object Clone()
        {
            return this;
        }

        //no parameter contravariance available
        public bool Equals(Variance other)
        {
            throw new NotImplementedException();
        }
    }
}