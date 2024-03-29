using System;
using iTextSharp.text.error_messages;

/*
 * $Id: PdfBoolean.cs 106 2009-12-07 12:23:50Z psoares33 $
 * 
 *
 * This file is part of the iText project.
 * Copyright (c) 1998-2012 1T3XT BVBA
 * Authors: Bruno Lowagie, Paulo Soares, et al.
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License version 3
 * as published by the Free Software Foundation with the addition of the
 * following permission added to Section 15 as permitted in Section 7(a):
 * FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY 1T3XT,
 * 1T3XT DISCLAIMS THE WARRANTY OF NON INFRINGEMENT OF THIRD PARTY RIGHTS.
 *
 * This program is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU Affero General Public License for more details.
 * You should have received a copy of the GNU Affero General Public License
 * along with this program; if not, see http://www.gnu.org/licenses or write to
 * the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
 * Boston, MA, 02110-1301 USA, or download the license from the following URL:
 * http://itextpdf.com/terms-of-use/
 *
 * The interactive user interfaces in modified source and object code versions
 * of this program must display Appropriate Legal Notices, as required under
 * Section 5 of the GNU Affero General Public License.
 *
 * In accordance with Section 7(b) of the GNU Affero General Public License,
 * you must retain the producer line in every PDF that is created or manipulated
 * using iText.
 *
 * You can be released from the requirements of the license by purchasing
 * a commercial license. Buying such a license is mandatory as soon as you
 * develop commercial activities involving the iText software without
 * disclosing the source code of your own applications.
 * These activities include: offering paid services to customers as an ASP,
 * serving PDFs on the fly in a web application, shipping iText with a closed
 * source product.
 *
 * For more information, please contact iText Software Corp. at this
 * address: sales@itextpdf.com
 */

namespace iTextSharp.text.pdf {

    /**
     * <CODE>PdfBoolean</CODE> is the bool object represented by the keywords <VAR>true</VAR> or <VAR>false</VAR>.
     * <P>
     * This object is described in the 'Portable Document Format Reference Manual version 1.3'
     * section 4.2 (page 37).
     *
     * @see        PdfObject
     * @see        BadPdfFormatException
     */

    public class PdfBoolean : PdfObject {
    
        // static membervariables (possible values of a bool object)
        public static readonly PdfBoolean PDFTRUE = new PdfBoolean(true);
        public static readonly PdfBoolean PDFFALSE = new PdfBoolean(false);
        /** A possible value of <CODE>PdfBoolean</CODE> */
        public const string TRUE = "true";
    
        /** A possible value of <CODE>PdfBoolean</CODE> */
        public const string FALSE = "false";
    
        // membervariables
    
        /** the bool value of this object */
        private bool value;
    
        // constructors
    
        /**
         * Constructs a <CODE>PdfBoolean</CODE>-object.
         *
         * @param        value            the value of the new <CODE>PdfObject</CODE>
         */
    
        public PdfBoolean(bool value) : base(BOOLEAN) {
            if (value) {
                this.Content = TRUE;
            }
            else {
                this.Content = FALSE;
            }
            this.value = value;
        }
    
        /**
         * Constructs a <CODE>PdfBoolean</CODE>-object.
         *
         * @param        value            the value of the new <CODE>PdfObject</CODE>, represented as a <CODE>string</CODE>
         *
         * @throws        BadPdfFormatException    thrown if the <VAR>value</VAR> isn't '<CODE>true</CODE>' or '<CODE>false</CODE>'
         */
    
        public PdfBoolean(string value) : base(BOOLEAN, value) {
            if (value.Equals(TRUE)) {
                this.value = true;
            }
            else if (value.Equals(FALSE)) {
                this.value = false;
            }
            else {
                throw new BadPdfFormatException(MessageLocalization.GetComposedMessage("the.value.has.to.be.true.of.false.instead.of.1", value));
            }
        }
    
        // methods returning the value of this object
    
        /**
         * Returns the primitive value of the <CODE>PdfBoolean</CODE>-object.
         *
         * @return        the actual value of the object.
         */
    
        public bool BooleanValue {
            get {
                return value;
            }
        }

        public override string ToString() {
            return value ? TRUE : FALSE;
        }
    }
}
