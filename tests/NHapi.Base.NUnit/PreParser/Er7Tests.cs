namespace NHapi.Base.NUnit.PreParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using global::NUnit.Framework;

    using NHapi.Base.PreParser;

    [TestFixture]
    public class Er7Tests
    {
        private const string MESSAGE = "MSH|^~\\&|x|x|x|x|199904140038||ADT^A01||P|2.2\r" +
                                       "PID|||||Smith&Booth&Jones^X^Y\r" +
                                       "NTE|a||one~two~three\r" +
                                       "NTE|b||four\r";

        [TestCase("MSH|")]
        [TestCase("MSH|^")]
        [TestCase("MSH|^~")]
        [TestCase("MSH|^~\\")]
        public void TryParseMessage_MessageNotLongEnough_ReturnsFalse(string input)
        {
            // Arrange
            var pathSpecs = Array.Empty<DatumPath>();

            // Act
            var result = Er7.TryParseMessage(input, pathSpecs, out _);

            // Assert
            Assert.That(result, Is.False);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void TryParseMessage_MessageIsNullEmptyOrWhiteSpace_ReturnsFalse(string input)
        {
            // Arrange
            var pathSpecs = Array.Empty<DatumPath>();

            // Act
            var result = Er7.TryParseMessage(input, pathSpecs, out _);

            // Assert
            Assert.That(result, Is.False);
        }

        [TestCase("MSH|^~\\&|\rG")]
        [TestCase("MSH|^~\\&|\rGR")]
        public void TryParseMessage_MessageSegmentIsTooShort_ArgumentOutOfRangeExceptionIsIgnored(string input)
        {
            // Arrange
            var pathSpecs = Array.Empty<DatumPath>();

            // Act
            var result = Er7.TryParseMessage(input, pathSpecs, out _);

            // Assert
            Assert.That(result, Is.True);
        }

        [TestCase("NTE(0)-1", "a")]
        [TestCase("NTE(1)-1", "b")]
        [TestCase("NTE-3(0)", "one")]
        [TestCase("NTE-3(1)", "two")]
        [TestCase("PID-5", "Smith")]
        [TestCase("PID-5-1-1", "Smith")]
        [TestCase("MSH-9-2", "A01")]
        [TestCase("PID-5-1-2", "Booth")]
        public void TryParseMessage_ValidADT_A01Er7_ReturnsExpectedResult(string pathSpec, string expectedValue)
        {
            // Arrange
            var pathSpecs = new List<DatumPath> { pathSpec.FromPathSpec() };

            // Act
            var parsed = Er7.TryParseMessage(MESSAGE, pathSpecs, out var results);
            var actualResults = results.Select(r => r.Value).ToArray();

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(parsed, Is.True);
                Assert.That(actualResults, Does.Contain(expectedValue));
            });
        }

        [TestCase("C(0)-2", "a")]
        [TestCase("C(1)-2", "b")]
        [TestCase("O-4(0)-5", "screening1")]
        [TestCase("O-4(1)-5", "screening2")]
        [TestCase("P-2", "222")]
        [TestCase("R-2-7", "CTC")]
        [TestCase("H-5", "yyy")]
        public void TryParseMessage_ValidEr7WithSingleCharacterSegmentName_ReturnsExpectedResult(string pathSpec, string expectedValue)
        {
            // Arrange
            var pathSpecs = new List<DatumPath> { pathSpec.FromPathSpec(MessageConstants.ASTM1394) };
            var message = "H|\\^&|||yyy|||||||P|xxx|20171106\r"
            + "P|1|222|||SMITH^John\r"
            + "O|1|B230033043^Un.1 - Pos.3||^^^URINOCOLTURA^screening1\\^^^URINOCOLTURA^screening2|||||||||||URI||||||||||F\r"
            + "R|1|^^^URINOCOLTURA^screening^^CTC|180|||||F\r"
            + "C|1|a\r"
            + "C|2|b\r"
            + "L|1|N\r";

            // Act
            var parsed = Er7.TryParseMessage(message, pathSpecs, MessageConstants.ASTM1394, out var results);
            var actualResults = results.Select(r => r.Value).ToArray();

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(parsed, Is.True);
                Assert.That(actualResults, Does.Contain(expectedValue));
            });
        }

        [TestCase("OBX-3-3", "L")]
        [TestCase("OBR-2-2", "8012986277")]
        [TestCase("P-5", "WALLACE")]
        [TestCase("H-5", "139")]
        public void TryParseMessage_ValidEr7WithVariableLengthSegmentName_ReturnsExpectedResult(string pathSpec, string expectedValue)
        {
            // Arrange
            var pathSpecs = new List<DatumPath> { pathSpec.FromPathSpec(MessageConstants.ASTM1238) };
            var message = "H|^~\\&|||139^glucos||ORU|||SSSS^LABORATORY||P|A.2.|20090129111700|\r"
                        + "P|1||1738813|1738813|WALLACE^Gromet||19761120|M|\r"
                        + "OBR|1|^8012986277||GLUM^GLUM^L^|||20080129105134||||R|||||WEICHR||3N|\r"
                        + "OBX|1|NM|GLUM^GLUM^L^||353||||||F|20080129105134|\r"
                        + "L|||1|5|\r";

            // Act
            var parsed = Er7.TryParseMessage(message, pathSpecs, MessageConstants.ASTM1238, out var results);
            var actualResults = results.Select(r => r.Value).ToArray();

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(parsed, Is.True);
                Assert.That(actualResults, Does.Contain(expectedValue));
            });
        }
    }
}