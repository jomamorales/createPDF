using System;
using System.Drawing;
using System.util;
/*
 * $Id: RegionTextRenderFilter.cs 158 2010-04-18 20:31:15Z psoares33 $
 *
 * This file is part of the iText project.
 * Copyright (c) 1998-2012 1T3XT BVBA
 * Authors: Kevin Day, Bruno Lowagie, Paulo Soares, et al.
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
namespace iTextSharp.text.pdf.parser {


    /**
     * A {@link RenderFilter} that only allows text within a specified rectangular region
     * @since 5.0.1
     */
    public class RegionTextRenderFilter : RenderFilter {

        /** the region to allow text from */
        private RectangleJ filterRect;
        
        /**
         * Constructs a filter
         * @param filterRect the rectangle to filter text against.  Note that this is a java.awt.Rectangle !
         */
        public RegionTextRenderFilter(RectangleJ filterRect) {
            this.filterRect = filterRect;
        }

        /**
         * Constructs a filter
         * @param filterRect the rectangle to filter text against.
         */
        public RegionTextRenderFilter(iTextSharp.text.Rectangle filterRect) {
            this.filterRect = new RectangleJ(filterRect);
        }
 
        /** 
         * @see com.itextpdf.text.pdf.parser.RenderFilter#allowText(com.itextpdf.text.pdf.parser.TextRenderInfo)
         */
        public override bool AllowText(TextRenderInfo renderInfo){
            LineSegment segment = renderInfo.GetBaseline();
            Vector startPoint = segment.GetStartPoint();
            Vector endPoint = segment.GetEndPoint();
            
            float x1 = startPoint[Vector.I1];
            float y1 = startPoint[Vector.I2];
            float x2 = endPoint[Vector.I1];
            float y2 = endPoint[Vector.I2];
            
            return filterRect.IntersectsLine(x1, y1, x2, y2);
        }
    }
}