using System;
using NHapi.Base;
using NHapi.Base.Model;
using NHapi.Base.Parser;
using NHapi.Base.Log;

using NHapi.Model.LIS02A2.Datatype;

namespace NHapi.Model.LIS02A2.Segment
{
    ///<summary>
    /// Represents a TERMINATOR RECORD. 
    /// This segment has the following fields:<ol>
    ///<li>L-1: SEQUENCE NUMBER (ST)</li>
    ///<li>L-2: TERMINATION CODE (CM)</li>
    ///</ol>
    /// The get...() methods return data from individual fields.  These methods 
    /// do not throw exceptions and may therefore have to handle exceptions internally.  
    /// If an exception is handled internally, it is logged and null is returned.
    /// This is not expected to happen - if it does happen this indicates not so much 
    /// an exceptional circumstance as a bug in the code for this class.
    ///</summary>
    [Serializable]
    public class L : AbstractSegment
    {
        /**
         * Creates an L RECORD object that belongs to the given 
         * message.  
         */
        public L(IGroup parent, IModelClassFactory factory) : base(parent, factory)
        {
            var message = Message;
            try
            {
                this.Add(typeof(ST), true, 1, 5, new System.Object[] { message }, "Sequence Number");
                this.Add(typeof(ST), false, 1, 10, new System.Object[] { message }, "Termination Code");
            }
            catch (HL7Exception he)
            {
                HapiLogFactory.GetHapiLog(GetType()).Error("Can't instantiate " + GetType().Name, he);
            }
        }

        // TODO: Add getter methods for the fields in this segment.
    }
}
