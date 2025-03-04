namespace NHapi.Base.Parser
{
    using NHapi.Base.Validation;

    public class AstmXmlParser : DefaultXMLParser
    {
        /// <summary>
        /// Creates a new AstmPipeParser.
        /// </summary>
        public AstmXmlParser()
            : this(new DefaultModelClassFactory())
        {
        }

        /// <summary>
        /// Creates a new AstmPipeParser.
        /// <param name="factory">Looks up classes for message model components.</param>
        /// </summary>
        public AstmXmlParser(IModelClassFactory factory)
            : base(factory, MessageConstants.ASTM1394)
        {
        }
    }
}
