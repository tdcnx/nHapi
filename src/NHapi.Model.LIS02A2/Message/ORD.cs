namespace NHapi.Model.LIS02A2.Message
{
    using NHapi.Base.Model;
    using NHapi.Base.Parser;
    using NHapi.Model.LIS02A2.Segment;

    ///<summary>
    /// Represents a Order message structure (see chapter [5.2]). This structure contains the 
    /// following elements:
    ///<ol>
    ///<li>0: H (MESSAGE HEADER) </li>
    ///<li>1: L (TERMINATOR RECORD) </li>
    ///</ol>
    ///</summary>
    public class ORD : AbstractMessage
    {
        ///<summary>
        /// Creates a new ORD Group with DefaultModelClassFactory. 
        ///</summary> 
        public ORD() : this(new DefaultModelClassFactory())
        {
        }

        ///<summary> 
        /// Creates a new ORD Group with custom IModelClassFactory.
        ///</summary>
        public ORD(IModelClassFactory theFactory) : base(theFactory)
        {
            this.Add(typeof(H), true, false);
            // TODO: add more segments / groups.
            this.Add(typeof(L), true, false);
        }
        /// <summary>
        /// Version
        /// </summary>
        public override string Version
        {
            get
            {
                return Constants.VERSION;
            }
        }

        // TODO: Add getter methods for segments / groups.
    }
}
