
namespace NHapi.Model.LIS02A2.Datatype
{
    using NHapi.Base.Model;

    ///<summary>
    /// <p>The ST (String) data type. </p>
    ///</summary>
    public class ST : AbstractPrimitive
    {
        /// <summary>Return the version
        /// <returns>2.1</returns>
        ///</summary>


        ///<summary>Construct the type
        ///<param name="theMessage">message to which this Type belongs</param>
        ///</summary>
        public ST(IMessage theMessage) : base(theMessage)
        {
        }



        ///<summary>Construct the type
        ///<param name="message">message to which this Type belongs</param>
        ///<param name="description">The description of this type</param>
        ///</summary>
        public ST(IMessage message, string description) : base(message, description)
        {
        }
    }
}
