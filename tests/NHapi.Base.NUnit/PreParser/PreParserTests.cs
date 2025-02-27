// <copyright file="PreParserTests.cs" company="nHapiNET">
// Copyright (c) nHapiNET. All rights reserved.
// </copyright>

namespace NHapi.Base.NUnit.PreParser
{
    using global::NUnit.Framework;

    using NHapi.Base.PreParser;

    [TestFixture]
    public class PreParserTests
    {
        private const string ER7MESSAGE = "MSH|^~\\&|x|x|x|x|199904140038||ADT^A01||P|2.2\r" +
                                       "PID|||||Smith&Booth&Jones^X^Y\r" +
                                       "NTE|a||one~two~three\r" +
                                       "NTE|b||four\r";

        private const string XMLMESSAGE = "<?xml version=\"1.0\" standalone=\"no\"?><ADT_A01 xmlns=\"urn:hl7-org:v2xml\"><MSH><MSH.1>|</MSH.1><MSH.2>^~\\&amp;</MSH.2><MSH.3><HD.1>x</HD.1></MSH.3><MSH.4><HD.1>x</HD.1></MSH.4><MSH.5><HD.1>x</HD.1></MSH.5><MSH.6><HD.1>x</HD.1></MSH.6><MSH.7><TS.1>199904140038</TS.1></MSH.7><MSH.9><MSG.1>ADT</MSG.1><MSG.2>A01</MSG.2></MSH.9><MSH.11><PT.1>P</PT.1></MSH.11><MSH.12><VID.1>2.4</VID.1></MSH.12></MSH><PID><PID.5><XPN.1><FN.1>Smith</FN.1><FN.2>Booth</FN.2><FN.3>Jones</FN.3></XPN.1><XPN.2>X</XPN.2><XPN.3>Y</XPN.3></PID.5></PID><NTE><NTE.1>a</NTE.1><NTE.3>one</NTE.3><NTE.3>two</NTE.3><NTE.3>three</NTE.3></NTE><NTE><NTE.1>b</NTE.1><NTE.3>four</NTE.3></NTE></ADT_A01>";

        private const string ASTM1394MESSAGE = "H|\\^&|||yyy|||||||P|xxx|20171106\r" +
                                            "P|1|222|||SMITH\\Booth\\Jones^John\r" +
                                            "O|1|B230033043^Un.1 - Pos.3||yy^^^URINOCOLTURA1^screening1\\xx^^^URINOCOLTURA2^screening2|||||||||||URI||||||||||F\r" +
                                            "R|1|^^^URINOCOLTURA^screening^^CTC|180|||||F\r" +
                                            "C|1|a\r" +
                                            "C|2|b\r" +
                                            "L|1|N\r";

        private const string XMLASTM1394MESSAGE = @"<?xml version=""1.0"" encoding=""utf-8""?>
<QRY xmlns=""urn:hl7-org:v2xml"">
  <H>
    <H.1>|</H.1>
    <H.2>\^&amp;</H.2>
    <H.5>Strumento</H.5>
    <H.12>P</H.12>
    <H.13>LIS02-A2</H.13>
    <H.14>20171106</H.14>
  </H>
  <Q>
    <Q.1>1</Q.1>
    <Q.2>
      <RI.1>A*</RI.1>
      <RI.2>0000123</RI.2>
      <RI.3>0</RI.3>
    </Q.2>
    <Q.5>R</Q.5>
    <Q.6>20181001140023</Q.6>
    <Q.8>
      <CN.1>Doctor</CN.1>
    </Q.8>
    <Q.9>123-456-7890</Q.9>
    <Q.10>Extra</Q.10>
    <Q.12>N</Q.12>
  </Q>
  <L>
    <L.1>1</L.1>
    <L.2>N</L.2>
  </L>
</QRY>";

        private const string ASTM1238MESSAGE = "H|^~\\&|||139^glucos||ORU|||SSSS^LABORATORY||P|A.2.|20090129111700|\r" +
                                            "P|1||1738813|1738813|WALLACE^Gromet||19761120|M|\r" +
                                            "OBR|1|^8012986277||GLUM^GLUM^L^|||20080129105134||||R|||||WEICHR||3N|\r" +
                                            "OBX|1|NM|GLUM^GLUM^L^||353||||||F|20080129105134|\r" +
                                            "L|||1|5|\r";

        [TestCase(null, "Message encoding is not recognized")]
        [TestCase("", "Message encoding is not recognized")]
        [TestCase("   ", "Message encoding is not recognized")]
        [TestCase("NOTMSH", "Message encoding is not recognized")]
        [TestCase("MSH|", "Parse failed")]
        [TestCase("MSH.3", "Parse failed")]
        public void GetFields_MessageIsInvalid_ThrowsHl7Exception(string message, string expectedExceptionMessage)
        {
            // Arrange / Act / Assert
            Assert.That(
                () => PreParser.GetFields(message),
                Throws.TypeOf<HL7Exception>()
                    .With.Message.EqualTo(expectedExceptionMessage));
        }

        [TestCase(null, "Message encoding is not recognized")]
        [TestCase("", "Message encoding is not recognized")]
        [TestCase("   ", "Message encoding is not recognized")]
        [TestCase("NOTH", "Message encoding is not recognized")]
        [TestCase("H|", "Parse failed")]
        [TestCase("H.3", "Parse failed")]
        public void GetFields_Astm1394MessageIsInvalid_ThrowsHl7Exception(string message, string expectedExceptionMessage)
        {
            // Arrange / Act / Assert
            Assert.That(
                () => PreParser.GetFields(MessageConstants.ASTM1394, message),
                Throws.TypeOf<HL7Exception>()
                    .With.Message.EqualTo(expectedExceptionMessage));
        }

        [TestCase(null, "Message encoding is not recognized")]
        [TestCase("", "Message encoding is not recognized")]
        [TestCase("   ", "Message encoding is not recognized")]
        [TestCase("NOTH", "Message encoding is not recognized")]
        [TestCase("MSH|", "Message encoding is not recognized")]
        [TestCase("H|", "Parse failed")]
        [TestCase("H.3", "Parse failed")]
        [TestCase("H|^~\\&||\rOOBRR|1|\r", "Message encoding is not recognized")]
        public void GetFields_Astm1238MessageIsInvalid_ThrowsHl7Exception(string message, string expectedExceptionMessage)
        {
            // Arrange / Act / Assert
            Assert.That(
                () => PreParser.GetFields(MessageConstants.ASTM1238, message),
                Throws.TypeOf<HL7Exception>()
                    .With.Message.EqualTo(expectedExceptionMessage));
        }

        [TestCase("NTE(0)-1", "a")]
        [TestCase("NTE(1)-1", "b")]
        [TestCase("NTE-3(0)", "one")]
        [TestCase("NTE-3(1)", "two")]
        [TestCase("PID-5", "Smith")]
        [TestCase("PID-5-1-1", "Smith")]
        [TestCase("MSH-9-2", "A01")]
        [TestCase("PID-5-1-2", "Booth")]
        public void GetFields_Er7MessageIsValid_ReturnsExpectedResult(string pathSpec, string expectedValue)
        {
            // Arrange / Act
            var results = PreParser.GetFields(ER7MESSAGE, pathSpec);

            // Assert
            Assert.That(results, Does.Contain(expectedValue));
        }

        [TestCase("C(0)-2", "a")]
        [TestCase("C(1)-2", "b")]
        [TestCase("O-4(0)", "yy")]
        [TestCase("O-4(1)", "xx")]
        [TestCase("P-5", "SMITH")]
        [TestCase("P-5-1-1", "SMITH")]
        [TestCase("P-5(0)", "SMITH")]
        [TestCase("P-5(1)", "Booth")]
        [TestCase("P-5(1)-1", "Booth")]
        [TestCase("P-5(2)", "Jones")]
        [TestCase("H-13", "xxx")]
        [TestCase("P-5(1)-1", "Booth")]
        public void GetFields_Astm1394MessageIsValid_ReturnsExpectedResult(string pathSpec, string expectedValue)
        {
            // Arrange / Act
            var results = PreParser.GetFields(MessageConstants.ASTM1394, ASTM1394MESSAGE, pathSpec);

            // Assert
            Assert.That(results, Does.Contain(expectedValue));
        }
        

        [TestCase("H-5", "139")]
        [TestCase("H-5-2", "glucos")]
        [TestCase("P-5-2", "Gromet")]
        public void GetFields_Astm1238MessageIsValid_ReturnsExpectedResult(string pathSpec, string expectedValue)
        {
            // Arrange / Act
            var results = PreParser.GetFields(MessageConstants.ASTM1238, ASTM1238MESSAGE, pathSpec);

            // Assert
            Assert.That(results, Does.Contain(expectedValue));
        }


        [TestCase("NTE(0)-1", "a")]
        [TestCase("NTE(1)-1", "b")]
        [TestCase("NTE-3(0)", "one")]
        [TestCase("NTE-3(1)", "two")]
        [TestCase("PID-5", "Smith")]
        [TestCase("PID-5-1-1", "Smith")]
        [TestCase("MSH-9-2", "A01")]
        [TestCase("PID-5-1-2", "Booth")]
        public void GetFields_XmlMessageIsValid_ReturnsExpectedResult(string pathSpec, string expectedValue)
        {
            // Arrange / Act
            var results = PreParser.GetFields(XMLMESSAGE, pathSpec);

            // Assert
            Assert.That(results, Does.Contain(expectedValue));
        }


        [TestCase("H-13", "LIS02-A2")]
        [TestCase("Q-2-2", "0000123")]
        public void GetFields_XmlAstm1394MessageIsValid_ReturnsExpectedResult(string pathSpec, string expectedValue)
        {
            // Arrange / Act
            var results = PreParser.GetFields(MessageConstants.ASTM1394, XMLASTM1394MESSAGE, pathSpec);

            // Assert
            Assert.That(results, Does.Contain(expectedValue));
        }
    }
}