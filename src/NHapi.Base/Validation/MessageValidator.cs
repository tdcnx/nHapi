/*
  The contents of this file are subject to the Mozilla Public License Version 1.1
  (the "License"); you may not use this file except in compliance with the License.
  You may obtain a copy of the License at http://www.mozilla.org/MPL/
  Software distributed under the License is distributed on an "AS IS" basis,
  WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License for the
  specific language governing rights and limitations under the License.

  The Original Code is "MessageValidator.java".  Description:
  "Validates parsed message against MessageRules that are enabled according to runtime
  configuration information."

  The Initial Developer of the Original Code is University Health Network. Copyright (C)
  2002.  All Rights Reserved.

  Contributor(s): ______________________________________.

  Alternatively, the contents of this file may be used under the terms of the
  GNU General Public License (the "GPL"), in which case the provisions of the GPL are
  applicable instead of those above.  If you wish to allow use of your version of this
  file only under the terms of the GPL and not to allow others to use your version
  of this file under the MPL, indicate your decision by deleting  the provisions above
  and replace  them with the notice and other provisions required by the GPL License.
  If you do not delete the provisions above, a recipient may use your version of
  this file under either the MPL or the GPL.
*/

namespace NHapi.Base.Validation
{
    using System;

    using NHapi.Base.Log;
    using NHapi.Base.Model;
    using NHapi.Base.Util;

    /// <summary> Validation utilities for parsed and encoded messages.
    ///
    /// </summary>
    /// <author>  Bryan Tripp.
    /// </author>
    public class MessageValidator : IMessageValidator
    {
        private static readonly IHapiLog OurLog;

        private readonly IValidationContext myContext;
        private readonly bool failOnError;
        private readonly MessageConstants messageConstants;

        static MessageValidator()
        {
            OurLog = HapiLogFactory.GetHapiLog(typeof(MessageValidator));
        }

        /// <param name="theContext">context that determines which validation rules apply.
        /// </param>
        /// <param name="theFailOnErrorFlag">
        /// </param>
        public MessageValidator(IValidationContext theContext, bool theFailOnErrorFlag)
            : this(theContext, theFailOnErrorFlag, MessageConstants.Default)
        {
        }

        /// <param name="theContext">context that determines which validation rules apply.
        /// </param>
        /// <param name="theFailOnErrorFlag">
        /// </param>
        /// <param name="messageConstants">
        /// </param>
        public MessageValidator(IValidationContext theContext, bool theFailOnErrorFlag, MessageConstants messageConstants)
        {
            myContext = theContext;
            failOnError = theFailOnErrorFlag;
            this.messageConstants = messageConstants;
        }

        [Obsolete("This method has been replaced by 'Validate'.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "StyleCop.CSharp.NamingRules",
            "SA1300:Element should begin with upper-case letter",
            Justification = "As this is a public member, we will duplicate the method and mark this one as obsolete.")]
        public virtual bool validate(IMessage message)
        {
            return Validate(message);
        }

        /// <param name="message">a parsed message to validate (note that MSH-9-1 and MSH-9-2 must be valued).
        /// </param>
        /// <returns> true if the message is OK.
        /// </returns>
        /// <throws>  HL7Exception if there is at least one error and this validator is set to fail on errors. </throws>
        public virtual bool Validate(IMessage message)
        {
            var t = new Terser(message);
            var rules = myContext.getMessageRules(message.Version, t.Get($"{messageConstants.HeaderSegmentName}-9-1"), t.Get($"{messageConstants.HeaderSegmentName}-9-2"));

            ValidationException toThrow = null;
            var result = true;
            for (var i = 0; i < rules.Length; i++)
            {
                var ex = rules[i].test(message);
                for (var j = 0; j < ex.Length; j++)
                {
                    result = false;
                    OurLog.Error("Invalid message", ex[j]);
                    if (failOnError && toThrow == null)
                    {
                        toThrow = ex[j];
                    }
                }
            }

            if (toThrow != null)
            {
                throw new HL7Exception("Invalid message", toThrow);
            }

            return result;
        }

        [Obsolete("This method has been replaced by 'Validate'.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "StyleCop.CSharp.NamingRules",
            "SA1300:Element should begin with upper-case letter",
            Justification = "As this is a public member, we will duplicate the method and mark this one as obsolete.")]
        public virtual bool validate(string message, bool isXML, string version)
        {
            return Validate(message, isXML, version);
        }

        /// <param name="message">an ER7 or XML encoded message to validate.
        /// </param>
        /// <param name="isXML">true if XML, false if ER7.
        /// </param>
        /// <param name="version">HL7 version (e.g. "2.2") to which the message belongs.
        /// </param>
        /// <returns> true if the message is OK.
        /// </returns>
        /// <throws>  HL7Exception if there is at least one error and this validator is set to fail on errors. </throws>
        public virtual bool Validate(string message, bool isXML, string version)
        {
            var rules = myContext.getEncodingRules(version, isXML ? "XML" : "ER7");
            ValidationException toThrow = null;
            var result = true;
            for (var i = 0; i < rules.Length; i++)
            {
                var ex = rules[i].test(message);
                for (var j = 0; j < ex.Length; j++)
                {
                    result = false;
                    OurLog.Error("Invalid message", ex[j]);
                    if (failOnError && toThrow == null)
                    {
                        toThrow = ex[j];
                    }
                }
            }

            if (toThrow != null)
            {
                throw new HL7Exception("Invalid message", toThrow);
            }

            return result;
        }
    }
}