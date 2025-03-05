namespace NHapi.NUnit.Parser
{
    using System;
    using System.Collections.Generic;

    using global::NUnit.Framework;

    using NHapi.Base;
    using NHapi.Base.Parser;
    using NHapi.Base.Util;
    using NHapi.Model.LIS02A2.Message;
    using NHapi.Model.LIS02A2.Segment;

    [TestFixture]
    public class Astm1394Tests
    {
        [Test]
        public void ParseAstmMessage_AndCastToOrdMessage_ObjectIsNotNull()
        {
            // Arrange
            var message = "H|\\^&|||Strumento|||||||P|LIS02-A2|20171106\r"
                        + "L|1|N\r";

            var parser = new Astm1394PipeParser();

            // Act
            var parsedMessage = parser.Parse(message) as ORD;

            // Assert
            Assert.That(parsedMessage, Is.Not.Null);
        }

        [Test]
        public void ParseAstmMessage_RetrieveFieldValue_ValueRetrieved()
        {
            // Arrange
            var message = "H|\\^&|||Strumento|||||||P|LIS02-A2|20171106\r"
                        + "L|1|N\r";

            var parser = new Astm1394PipeParser();

            // Act
            var parsedMessage = parser.Parse(message);
            var terser = new Terser(parsedMessage);

            // Assert
            Assert.That(terser.Get("H-12"), Is.EqualTo("P"));
            Assert.That(terser.Get("L-2"), Is.EqualTo("N"));
            Assert.That(((H)parsedMessage.GetStructure("H")).GetField(14).GetValue(0).ToString(), Is.EqualTo("20171106"));
        }

        [Test]
        public void ParseAstmMessage_WithUnknownStructure_Hl7ExceptionIsThrown()
        {
            // Arrange
            var message = "H|\\^&|||Strumento|||||||P|LIS02-A2|20171106\r"
                        + "X|1\r"
                        + "L|1|N\r";

            var expectedExceptionMessage =
                "Can't determine message structure from the following segments 'HXL'.";
            var parser = new Astm1394PipeParser();

            // Act
            // Assert
            Assert.That(
                () => parser.Parse(message),
                Throws.TypeOf<HL7Exception>()
                    .With.Message.EqualTo(expectedExceptionMessage));
        }

        [Test]
        public void ParseMessage_ButWithHL7Message_ExceptionIsThrown()
        {
            // Arrange
            var message = "MSH|^~\\&|HIS|MedCenter|LIS|MedCenter|20060307110114||ORM^O01|MSGID20060307110114|P|2.3.1\r"
                        + "PID|||12001||Jones^John^^^Mr.||19670824|M|||123 West St.^^Denver^CO^80020^USA|||||||\r"
                        + "PV1||O|OP^PAREG^||||2342^Jones^Bob|||OP|||||||||2|||||||||||||||||||||||||20060307110111|\r"
                        + "ORC|NW|20060307110114\r"
                        + "OBR|1|20060307110114||003038^Urinalysis^L|||20060307110114";

            var parser = new Astm1394PipeParser();

            // Act
            // Assert
            Assert.That(
                () => parser.Parse(message),
                Throws.TypeOf<EncodingNotSupportedException>());
        }

        [Test]
        public void ParseAstmMessage_AndEncodeBack_MessageIsRestored()
        {
            // Arrange
            var message = "H|\\^&|||Strumento|||||||P|LIS02-A2|20171106\r"
                        + "L|1|N\r";

            var parser = new Astm1394PipeParser();

            // Act
            var parsed = parser.Parse(message);
            var generated = parser.Encode(parsed);

            // Assert
            Assert.That(generated, Is.EqualTo(message));
        }

        [Test]
        public void ParseAstmXmlMessage_EncodeIntoEr7AndBack_MessageIsRestored()
        {
            // Arrange
            var message = @"<?xml version=""1.0"" encoding=""utf-8""?>
<ORD xmlns=""urn:hl7-org:v2xml"">
  <H>
    <H.1>|</H.1>
    <H.2>\^&amp;</H.2>
    <H.5>Strumento</H.5>
    <H.12>P</H.12>
    <H.13>LIS02-A2</H.13>
    <H.14>20171106</H.14>
  </H>
  <L>
    <L.1>1</L.1>
    <L.2>N</L.2>
  </L>
</ORD>";
            var pipeParser = new Astm1394PipeParser();
            var xmlParser = new AstmXmlParser();

            // Act
            var parsedMessage = xmlParser.Parse(message);
            var er7Message = pipeParser.Encode(parsedMessage);
            var parsedMessage2 = pipeParser.Parse(er7Message);
            var encodedMessage = xmlParser.Encode(parsedMessage2);

            // Assert
            Assert.That(encodedMessage, Is.EqualTo(message));
        }

        public void ParseAstmMessage_EncodeIntoXmlAndBack_MessageIsRestored()
        {
            // Arrange
            var message = "H|\\^&|||Strumento|||||||P|LIS02-A2|20171106\r"
                        + "L|1|N\r";

            var pipeParser = new Astm1394PipeParser();
            var xmlParser = new AstmXmlParser();

            // Act
            var parsed = pipeParser.Parse(message);
            var generated = xmlParser.Encode(parsed);
            var msg2 = xmlParser.Parse(generated);
            var encoded = pipeParser.Encode(msg2);

            // Assert
            Assert.That(encoded, Is.EqualTo(message));
        }
    }
}
