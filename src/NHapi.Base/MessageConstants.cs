using NHapi;

namespace NHapi.Base
{
    /// <summary>
    /// Message constants for different message types.
    /// </summary>
    public class MessageConstants
    {
        private static readonly MessageConstants HL7Constants = new MessageConstants("MSH", "{3}", "^~\\&", new DelimiterIndex(3, 4, 5, 6, 7));
        private static readonly MessageConstants ASTM1394Constants = new MessageConstants("H", "{1}", "\\^&", new DelimiterIndex(1, 3, 2, 4, -1));
        private static readonly MessageConstants ASTM1238Constants = new MessageConstants("H", "{1,3}", "^~\\&", new DelimiterIndex(1, 2, 3, 4, 5), new int[] {1, 3});

        private readonly string headerSegmentName;
        private readonly string segmentNameRegexQuantifier;
        private readonly string encodingCharacters;
        private readonly DelimiterIndex delimiterIndices;
        private readonly int[] headerNameSizes;

        /// <summary>
        /// Creates a new instance of MessageConstants.
        /// </summary>
        /// <param name="headerSegmentName">Header segment name.</param>
        /// <param name="segmentNameRegexQuantifier">A reqular expression quantifier that will determine the segment name's number of characters.</param>
        /// <param name="encodingCharacters">The encoding characters</param>
        /// <param name="delimiterIndices">Delimiter indices for encoding characters based on its position at the start of the message.</param>
        /// <param name="headerNameSizes">The sizes of the header segment name.</param>
        public MessageConstants(
            string headerSegmentName,
            string segmentNameRegexQuantifier,
            string encodingCharacters,
            DelimiterIndex delimiterIndices,
            int[] headerNameSizes = null
            )
        {
            this.headerSegmentName = headerSegmentName;
            this.segmentNameRegexQuantifier = segmentNameRegexQuantifier;
            this.encodingCharacters = encodingCharacters;
            this.delimiterIndices = delimiterIndices;
            this.headerNameSizes = headerNameSizes;
            if (this.headerNameSizes == null)
            {
                this.headerNameSizes = new int[] { headerSegmentName.Length };
            }
        }

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
        /// Header name sizes
        /// </summary>
        public int[] HeaderNameSizes { get => headerNameSizes; }
    }
}
