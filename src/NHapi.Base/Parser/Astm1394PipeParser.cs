namespace NHapi.Base.Parser
{
    using System.Text;

    using NHapi.Base.Model;
    using NHapi.Base.Util;
    using NHapi.Base.Validation;

    public class Astm1394PipeParser : PipeParser
    {

        /// <summary>
        /// Creates a new AstmPipeParser.
        /// </summary>
        public Astm1394PipeParser()
            : base(new DefaultModelClassFactory(), MessageConstants.ASTM1394)
        {
        }

        /// <summary>
        /// Creates a new AstmPipeParser.
        /// <param name="factory">Looks up classes for message model components.</param>
        /// </summary>
        public Astm1394PipeParser(IModelClassFactory factory)
            : base(factory, MessageConstants.ASTM1394)
        {
        }

        protected override EncodingCharacters GetValidEncodingCharacters(char fieldSep, ISegment header)
        {
            var encCharString = Terser.Get(header, 2, 0, 1, 1);

            if (encCharString == null)
            {
                encCharString = this.MessageConstants.EncodingCharacters;
                Terser.Set(header, 2, 0, 1, 1, encCharString);
            }

            if (encCharString.Length != this.MessageConstants.EncodingCharacters.Length)
            {
                throw new HL7Exception(
                    $"Encoding characters ({this.MessageConstants.HeaderSegmentName}-2) value '{encCharString}' invalid -- must be {this.MessageConstants.EncodingCharacters.Length} characters", ErrorCode.DATA_TYPE_ERROR);
            }

            return EncodingCharacters.FromField(encCharString, fieldSep, this.MessageConstants);
        }

        /// <inheritdoc />
        protected override string GetStructure(string message, EncodingCharacters encodingCharacters, MessageConstants messageConstants, ref bool explicitlyDefined)
        {
            var messageStructure = "ORD";
            var stringBuilder = new StringBuilder();
            explicitlyDefined = true;
            var segments = message.Split('\r');
            foreach (var segment in segments)
            {
                var fields = segment.Split(encodingCharacters.FieldSeparator);
                if (fields.Length > 0)
                {
                    stringBuilder.Append(fields[0]);
                }
            }
            return messageStructure;
        }

        protected override void EnsurePresenceOfMessageTypeAndEventName(IMessage source, ISegment msh)
        {
            // do nothing
        }
    }
}
