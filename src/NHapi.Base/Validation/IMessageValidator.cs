﻿/*
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
    using NHapi.Base.Model;

    /// <summary>
    /// Contract for validation of parsed and encoded messages.
    /// </summary>
    public interface IMessageValidator
    {
        /// <summary>
        /// Validates the message.
        /// </summary>
        /// <param name="message">a parsed message to validate</param>
        /// <returns> true if the message is OK.</returns>
        bool Validate(IMessage message);

        /// <summary>
        /// Validates the message.
        /// </summary>
        /// <param name="message">an ER7 or XML encoded message to validate.</param>
        /// <param name="isXML">true if XML, false if ER7.</param>
        /// <param name="version">HL7 version (e.g. "2.2") to which the message belongs.</param>
        /// <returns> true if the message is OK.</returns>
        bool Validate(string message, bool isXML, string version);
    }
}