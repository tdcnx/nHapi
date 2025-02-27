namespace NHapi.Base
{
    /// <summary>
    /// Container for the different delimiter indices.
    /// </summary>
    public class DelimiterIndex
    {
        private readonly int fieldSeparatorIndex;
        private readonly int componentSeparatorIndex;
        private readonly int repetitionSeparatorIndex;
        private readonly int escapeCharacterIndex;
        private readonly int subcomponentSeparatorIndex;

        /// <summary>
        /// Create a delimiter indices container.
        /// </summary>
        /// <param name="fieldSeparatorIndex">Field separator index</param>
        /// <param name="componentSeparatorIndex">Component separator index</param>
        /// <param name="repetitionSeparatorIndex">Repetition separator index</param>
        /// <param name="escapeCharacterIndex">Escape character index</param>
        /// <param name="subcomponentSeparatorIndex">Sub-component separator index</param>
        public DelimiterIndex(
            int fieldSeparatorIndex,
            int componentSeparatorIndex,
            int repetitionSeparatorIndex,
            int escapeCharacterIndex,
            int subcomponentSeparatorIndex)
        {
            this.fieldSeparatorIndex = fieldSeparatorIndex;
            this.componentSeparatorIndex = componentSeparatorIndex;
            this.repetitionSeparatorIndex = repetitionSeparatorIndex;
            this.escapeCharacterIndex = escapeCharacterIndex;
            this.subcomponentSeparatorIndex = subcomponentSeparatorIndex;
        }

        /// <summary>
        /// Field separator index
        /// </summary>
        public int FieldSeparatorIndex { get => fieldSeparatorIndex; }

        /// <summary>
        /// Component separator index
        /// </summary>
        public int ComponentSeparatorIndex { get => componentSeparatorIndex; }

        /// <summary>
        /// Repetition separator index
        /// </summary>
        public int RepetitionSeparatorIndex { get => repetitionSeparatorIndex; }

        /// <summary>
        /// Escape character index
        /// </summary>
        public int EscapeCharacterIndex { get => escapeCharacterIndex; }

        /// <summary>
        /// Sub-component separator index
        /// </summary>
        public int SubcomponentSeparatorIndex { get => subcomponentSeparatorIndex; }
    }
}
