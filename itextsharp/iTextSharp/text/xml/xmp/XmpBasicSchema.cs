using System;
/*
 * $Id: XmpBasicSchema.cs 106 2009-12-07 12:23:50Z psoares33 $
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

namespace iTextSharp.text.xml.xmp {

    /**
    * An implementation of an XmpSchema.
    */
    public class XmpBasicSchema : XmpSchema {
        
        /** default namespace identifier*/
        public const String DEFAULT_XPATH_ID = "xmp";
        /** default namespace uri*/
        public const String DEFAULT_XPATH_URI = "http://ns.adobe.com/xap/1.0/";
        /** An unordered array specifying properties that were edited outside the authoring application. Each item should contain a single namespace and XPath separated by one ASCII space (U+0020). */
        public const String ADVISORY = "xmp:Advisory";
        /** The base URL for relative URLs in the document content. If this document contains Internet links, and those links are relative, they are relative to this base URL. This property provides a standard way for embedded relative URLs to be interpreted by tools. Web authoring tools should set the value based on their notion of where URLs will be interpreted. */
        public const String BASEURL = "xmp:BaseURL";
        /** The date and time the resource was originally created. */
        public const String CREATEDATE = "xmp:CreateDate";
        /** The name of the first known tool used to create the resource. If history is present in the metadata, this value should be equivalent to that of xmpMM:History�s softwareAgent property. */
        public const String CREATORTOOL = "xmp:CreatorTool";
        /** An unordered array of text strings that unambiguously identify the resource within a given context. */
        public const String IDENTIFIER = "xmp:Identifier";
        /** The date and time that any metadata for this resource was last changed. */
        public const String METADATADATE = "xmp:MetadataDate";
        /** The date and time the resource was last modified. */
        public const String MODIFYDATE = "xmp:ModifyDate";
        /** A short informal name for the resource. */
        public const String NICKNAME = "xmp:Nickname";
        /** An alternative array of thumbnail images for a file, which can differ in characteristics such as size or image encoding. */
        public const String THUMBNAILS = "xmp:Thumbnails";
        
        /**
        * @param shorthand
        * @throws IOException
        */
        public XmpBasicSchema() : base("xmlns:" + DEFAULT_XPATH_ID + "=\"" + DEFAULT_XPATH_URI + "\"") {
        }
        
        /**
        * Adds the creatortool.
        * @param creator
        */
        public void AddCreatorTool(String creator) {
            this[CREATORTOOL] = creator;
        }
        
        /**
        * Adds the creation date.
        * @param date
        */
        public void AddCreateDate(String date) {
            this[CREATEDATE] = date;
        }
        
        /**
        * Adds the modification date.
        * @param date
        */
        public void AddModDate(String date) {
            this[MODIFYDATE] = date;
        }

	    /**
	    * Adds the meta data date.
	    * @param date
	    */
	    public void AddMetaDataDate(String date) {
		    this[METADATADATE] = date;
	    }

        /** Adds the identifier.
        * @param id
        */
        public void AddIdentifiers(String[] id) {
            XmpArray array = new XmpArray(XmpArray.UNORDERED);
            for (int i = 0; i < id.Length; i++) {
                array.Add(id[i]);
            }
            SetProperty(IDENTIFIER, array);
        }

        /** Adds the nickname.
        * @param name
        */
        public void AddNickname(String name) {
            this[NICKNAME] = name;
        }
    }
}
