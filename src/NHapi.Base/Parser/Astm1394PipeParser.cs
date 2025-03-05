namespace NHapi.Base.Parser
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Text;
    using System.Text.RegularExpressions;

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

        private static IDictionary StructurePatternMap => StructurePatternsCollection.Instance.Maps;

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
        protected override string GetStructureName(string message, string version, EncodingCharacters encodingCharacters, ref bool explicitlyDefined)
        {
            var messageStructure = string.Empty;
            explicitlyDefined = true;

            var segmentNames = ConcatenateSegmentNames(message, encodingCharacters);

            if (StructurePatternMap.Contains(version))
            {
                var p = (NameValueCollection)StructurePatternMap[version];
                foreach (string key in p)
                {
                    var value = p[key];
                    if (Regex.IsMatch(segmentNames, value))
                    {
                        messageStructure = key;
                        break;
                    }
                }
            }

            if (messageStructure == string.Empty)
            {
                throw new HL7Exception(
                    $"Can't determine message structure from the following segments '{segmentNames}'.",
                    ErrorCode.UNSUPPORTED_MESSAGE_TYPE);
            }

            return messageStructure;
        }

        protected override void EnsurePresenceOfMessageTypeAndEventName(IMessage source, ISegment msh)
        {
            // do nothing
        }

        private static string ConcatenateSegmentNames(string message, EncodingCharacters encodingCharacters)
        {
            var segments = message.Split('\r');
            var stringBuilder = new StringBuilder();
            foreach (var segment in segments)
            {
                var fields = segment.Split(encodingCharacters.FieldSeparator);
                if (fields.Length > 0)
                {
                    stringBuilder.Append(fields[0]);
                }
            }
            return stringBuilder.ToString();
        }
    }
}
