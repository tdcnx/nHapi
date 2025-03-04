using System;

using NHapi.Base;
using NHapi.Base.Log;
using NHapi.Base.Model;
using NHapi.Base.Parser;

using NHapi.Model.LIS02A2.Datatype;

namespace NHapi.Model.LIS02A2.Segment
{
    ///<summary>
    /// Represents a HEADER RECORD. 
    /// This segment has the following fields:<ol>
    ///<li>H-1: FIELD SEPARATOR (ST)</li>
    ///<li>H-2: DELIMITER DEFINITION (ST)</li>
    ///<li>H-3: MESSAGE CONTROL ID (ST)</li>
    ///<li>H-4: ACCESS PASSWORD (ST)</li>
    ///<li>H-5: SENDER NAME OR ID (ST)</li>
    ///<li>H-6: SENDER STREET ADDRESS (ST)</li>
    ///<li>H-7: RESERVED FIELD (ST)</li>
    ///<li>H-8: SENDER TELEPHONE NUMBER (ST)</li>
    ///<li>H-9: CHARACTERISTICS OF SENDER (ST)</li>
    ///<li>H-10: RECEIVER ID (ST)</li>
    ///<li>H-11: COMMENT OR SPECIAL INSTRUCTIONS (ST)</li>
    ///<li>H-12: PROCESSING ID (ST)</li>
    ///<li>H-13: VERSION NO (ST)</li>
    ///<li>H-14: DATE AND TIME OF MESSAGE (DT)</li>
    ///</ol>
    /// The get...() methods return data from individual fields.  These methods 
    /// do not throw exceptions and may therefore have to handle exceptions internally.  
    /// If an exception is handled internally, it is logged and null is returned.  
    /// This is not expected to happen - if it does happen this indicates not so much 
    /// an exceptional circumstance as a bug in the code for this class.
    ///</summary>
    [Serializable]
    public class H : AbstractSegment
    {
        /**
         * Creates an H RECORD object that belongs to the given 
         * message.  
         */
        public H(IGroup parent, IModelClassFactory factory) : base(parent, factory)
        {
            var message = Message;
            try
            {
                this.Add(typeof(ST), true, 1, 1, new System.Object[] { message }, "Field Separator");
                this.Add(typeof(ST), true, 1, 3, new System.Object[] { message }, "Delimiter Definition");
                this.Add(typeof(ST), true, 1, 255, new System.Object[] { message }, "Message Control ID");
                this.Add(typeof(ST), false, 1, 255, new System.Object[] { message }, "Access Password");
                this.Add(typeof(ST), false, 1, 255, new System.Object[] { message }, "Sender Name or ID");
                this.Add(typeof(ST), false, 1, 255, new System.Object[] { message }, "Sender Street Address");
                this.Add(typeof(ST), false, 1, 255, new System.Object[] { message }, "Reserved Field");
                this.Add(typeof(ST), false, 40, 255, new System.Object[] { message }, "Sender Telephone Number");
                this.Add(typeof(ST), false, 1, 255, new System.Object[] { message }, "Characteristics of Sender");
                this.Add(typeof(ST), false, 1, 255, new System.Object[] { message }, "Receiver ID");
                this.Add(typeof(ST), false, 1, 255, new System.Object[] { message }, "Comment or Special Instructions");
                this.Add(typeof(ST), false, 1, 255, new System.Object[] { message }, "Processing ID");
                this.Add(typeof(ST), false, 1, 255, new System.Object[] { message }, "Version No");
                this.Add(typeof(DT), false, 1, 255, new System.Object[] { message }, "Date and Time of Message");
            }
            catch (HL7Exception he)
            {
                HapiLogFactory.GetHapiLog(GetType()).Error("Can't instantiate " + GetType().Name, he);
            }
        }

        // TODO: Add getter methods for the fields in this segment.
    }
}
