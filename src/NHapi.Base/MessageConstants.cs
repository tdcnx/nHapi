using NHapi;

namespace NHapi.Base
{
    /// <summary>
    /// Message constants for different message types.
    /// </summary>
    public class MessageConstants
    {
        public const int FIELDDELIMITERLENGTH = 1;

        private static readonly MessageConstants HL7Constants = new MessageConstants("MSH", "{3}", "^~\\&", new DelimiterIndex(3, 4, 5, 6, 7), 11, null, new string[] { "MSH", "FSH", "BSH" });
        private static readonly MessageConstants ASTM1394Constants = new MessageConstants("H", "{1}", "\\^&", new DelimiterIndex(1, 3, 2, 4, -1), 12);
        private static readonly MessageConstants ASTM1238Constants = new MessageConstants("H", "{1,3}", "^~\\&", new DelimiterIndex(1, 2, 3, 4, 5), 12, new int[] { 1, 3 });

        private readonly string headerSegmentName;
        private readonly string segmentNameRegexQuantifier;
        private readonly string encodingCharacters;
        private readonly DelimiterIndex delimiterIndices;
        private readonly int versionIdFieldIndex;
        private readonly int[] segmentNameSizes;
        private readonly string[] delimiterDefiningSegments;

        /// <summary>
        /// Creates a new instance of MessageConstants.
        /// </summary>
        /// <param name="headerSegmentName">Header segment name.</param>
        /// <param name="segmentNameRegexQuantifier">A regular expression quantifier that limits the segment name's number of characters.</param>
        /// <param name="encodingCharacters">The encoding characters</param>
        /// <param name="delimiterIndices">Delimiter indices for encoding characters based on its position at the start of the message.</param>
        /// <param name="versionIdFieldIndex">Field index of Version.</param>
        /// <param name="segmentNameSizes">The sizes of the segment name.</param>
        /// <param name="delimiterDefiningSegments">Names of segments that has field for delimiters.</param>
        public MessageConstants(
            string headerSegmentName,
            string segmentNameRegexQuantifier,
            string encodingCharacters,
            DelimiterIndex delimiterIndices,
            int versionIdFieldIndex,
            int[] segmentNameSizes = null,
            string[] delimiterDefiningSegments = null)
        {
            this.headerSegmentName = headerSegmentName;
            this.segmentNameRegexQuantifier = segmentNameRegexQuantifier;
            this.encodingCharacters = encodingCharacters;
            this.delimiterIndices = delimiterIndices;
            this.versionIdFieldIndex = versionIdFieldIndex;
            this.segmentNameSizes = segmentNameSizes;

            if (this.segmentNameSizes == null)
            {
                this.segmentNameSizes = new int[] { headerSegmentName.Length };
            }

            this.delimiterDefiningSegments = delimiterDefiningSegments;

            if (this.delimiterDefiningSegments == null)
            {
                this.delimiterDefiningSegments = new string[] { headerSegmentName };
            }

        }


        /// <summary>
        /// Default message constants.
        /// </summary>
        public static MessageConstants Default => HL7Constants;

        /// <summary>
        /// HL7 message constants.
        /// </summary>
        public static MessageConstants HL7 => HL7Constants;

        /// <summary>
        /// ASTM 1394 message constants.
        /// </summary>
        public static MessageConstants ASTM1394 => ASTM1394Constants;

        /// <summary>
        /// ASTM 1238 message constants.
        /// </summary>
        public static MessageConstants ASTM1238 => ASTM1238Constants;

        /// <summary>
        /// Header segment name
        /// </summary>
        public string HeaderSegmentName { get => headerSegmentName; }

        /// <summary>
        /// A regular expression quantifier that limits the segment name's number of characters.
        /// </summary>
        public string SegmentNameRegexQuantifier { get => segmentNameRegexQuantifier; }

        /// <summary>
        /// Encoding characters
        /// </summary>
        public string EncodingCharacters { get => encodingCharacters; }

        /// <summary>
        /// Delimiter indices for encoding characters based on its position at the start of the message.
        /// </summary>
        public DelimiterIndex DelimiterIndices { get => delimiterIndices; }

        /// <summary>
        /// Field separator index relative to the start of message.
        /// </summary>
        public int FieldIndexRelativeToMessage { get => delimiterIndices.FieldSeparatorIndex; }

        /// <summary>
        /// Field separator index relative to the start of field.
        /// </summary>
        public int FieldIndexRelativeToField { get => delimiterIndices.FieldSeparatorIndex - headerSegmentName.Length - FIELDDELIMITERLENGTH; }

        /// <summary>
        /// Component separator index relative to the start of message.
        /// </summary>
        public int ComponentIndexRelativeToMessage { get => delimiterIndices.ComponentSeparatorIndex; }

        /// <summary>
        /// Component separator index relative to the start of field.
        /// </summary>
        public int ComponentIndexRelativeToField { get => delimiterIndices.ComponentSeparatorIndex - headerSegmentName.Length - FIELDDELIMITERLENGTH; }

        /// <summary>
        /// Repetition separator index relative to the start of message.
        /// </summary>
        public int RepetitionIndexRelativeToMessage { get => delimiterIndices.RepetitionSeparatorIndex; }

        /// <summary>
        /// Repetition separator index relative to the start of field.
        /// </summary>
        public int RepetitionIndexRelativeToField { get => delimiterIndices.RepetitionSeparatorIndex - headerSegmentName.Length - FIELDDELIMITERLENGTH; }

        /// <summary>
        /// Escape character index relative to the start of message.
        /// </summary>
        public int EscapeIndexRelativeToMessage { get => delimiterIndices.EscapeCharacterIndex; }

        /// <summary>
        /// Escape character index relative to the start of field.
        /// </summary>
        public int EscapeIndexRelativeToField { get => delimiterIndices.EscapeCharacterIndex - headerSegmentName.Length - FIELDDELIMITERLENGTH; }

        /// <summary>
        /// Sub-component separator index relative to the start of message.
        /// </summary>
        public int SubcomponentIndexRelativeToMessage { get => delimiterIndices.SubcomponentSeparatorIndex; }

        /// <summary>
        /// Sub-component separator index relative to the start of field.
        /// </summary>
        public int SubcomponentIndexRelativeToField { get => delimiterIndices.SubcomponentSeparatorIndex - headerSegmentName.Length - FIELDDELIMITERLENGTH; }

        /// <summary>
        /// Segment name sizes.
        /// </summary>
        public int[] SegmentNameSizes { get => segmentNameSizes; }

        /// <summary>
        /// Field index of Version.
        /// </summary>
        public int VersionIdFieldIndex { get => versionIdFieldIndex; }

        /// <summary>
        /// Names of segments that has field for delimiters.
        /// </summary>
        public string[] DelimiterDefiningSegments { get => delimiterDefiningSegments; }
    }
}
