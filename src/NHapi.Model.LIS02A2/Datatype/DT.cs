namespace NHapi.Model.LIS02A2.Datatype
{
    using NHapi.Base.Model;

    ///<summary>
    /// <p>The DT (Date time) data type. </p>
    ///</summary>
    public class DT : NHapi.Base.Model.Primitive.DT
    {
        /// <summary>Construct the type</summary>
        /// <param name="theMessage">message to which this Type belongs</param>
        public DT(IMessage theMessage)
            : base(theMessage)
        {
        }

        /// <summary>Construct the type</summary>
        /// <param name="theMessage">message to which this Type belongs</param>
        /// <param name="description">The description of this type</param>
        public DT(IMessage theMessage, string description)
            : base(theMessage, description)
        {
        }
    }
}
