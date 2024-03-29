using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using iTextSharp.text;
using System.util.zlib;
/*
 * $Id: PngWriter.cs 159 2010-04-19 21:57:13Z psoares33 $
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

namespace iTextSharp.text.pdf.codec {
    public class PngWriter {
        private static readonly byte[] PNG_SIGNTURE = {(byte)137, 80, 78, 71, 13, 10, 26, 10};

        private static readonly byte[] IHDR = DocWriter.GetISOBytes("IHDR");
        private static readonly byte[] PLTE = DocWriter.GetISOBytes("PLTE");
        private static readonly byte[] IDAT = DocWriter.GetISOBytes("IDAT");
        private static readonly byte[] IEND = DocWriter.GetISOBytes("IEND");
        private static readonly byte[] iCCP = DocWriter.GetISOBytes("iCCP");

        private static uint[] crc_table;

        private Stream outp;

        public PngWriter(Stream outp) {
            this.outp = outp;
            outp.Write(PNG_SIGNTURE, 0, PNG_SIGNTURE.Length);
        }

        public void WriteHeader(int width, int height, int bitDepth, int colorType) {
            MemoryStream ms = new MemoryStream();
            OutputInt(width, ms);
            OutputInt(height, ms);
            ms.WriteByte((byte)bitDepth);
            ms.WriteByte((byte)colorType);
            ms.WriteByte(0);
            ms.WriteByte(0);
            ms.WriteByte(0);
            WriteChunk(IHDR, ms.ToArray());
        }

        public void WriteEnd() {
            WriteChunk(IEND, new byte[0]);
        }

        public void WriteData(byte[] data, int stride) {
            MemoryStream stream = new MemoryStream();
            ZDeflaterOutputStream zip = new ZDeflaterOutputStream(stream, 5);
            int k;
            for (k = 0; k < data.Length - stride; k += stride) {
                zip.WriteByte(0);
                zip.Write(data, k, stride);
            }
            int remaining = data.Length - k;
            if (remaining > 0){
                zip.WriteByte(0);
                zip.Write(data, k, remaining);
            }
            zip.Finish();
            WriteChunk(IDAT, stream.ToArray());
        }

        public void WritePalette(byte[] data) {
            WriteChunk(PLTE, data);
        }

        public void WriteIccProfile(byte[] data) {
            MemoryStream stream = new MemoryStream();
            stream.WriteByte((byte)'I');
            stream.WriteByte((byte)'C');
            stream.WriteByte((byte)'C');
            stream.WriteByte(0);
            stream.WriteByte(0);
            ZDeflaterOutputStream zip = new ZDeflaterOutputStream(stream, 5);
            zip.Write(data, 0, data.Length);
            zip.Finish();
            WriteChunk(iCCP, stream.ToArray());
        }

        private static void make_crc_table() {
            if (crc_table != null)
                return;
            uint[] crc2 = new uint[256];
            for (uint n = 0; n < 256; n++) {
                uint c = n;
                for (int k = 0; k < 8; k++) {
                    if ((c & 1) != 0)
                        c = 0xedb88320U ^ (c >> 1);
                    else
                        c = c >> 1;
                }
                crc2[n] = c;
            }
            crc_table = crc2;
        }

        private static uint update_crc(uint crc, byte[] buf, int offset, int len) {
            uint c = crc;

            if (crc_table == null)
                make_crc_table();
            for (int n = 0; n < len; n++) {
                c = crc_table[(c ^ buf[n + offset]) & 0xff] ^ (c >> 8);
            }
            return c;
        }

        private static uint crc(byte[] buf, int offset, int len) {
            return update_crc(0xffffffffU, buf, offset, len) ^ 0xffffffffU;
        }

        private static uint crc(byte[] buf) {
            return update_crc(0xffffffffU, buf, 0, buf.Length) ^ 0xffffffffU;
        }

        public void OutputInt(int n) {
            OutputInt(n, outp);
        }

        public static void OutputInt(int n, Stream s) {
            s.WriteByte((byte)(n >> 24));
            s.WriteByte((byte)(n >> 16));
            s.WriteByte((byte)(n >> 8));
            s.WriteByte((byte)n);
        }

        public void WriteChunk(byte[] chunkType, byte[] data) {
            OutputInt(data.Length);
            outp.Write(chunkType, 0, 4);
            outp.Write(data, 0, data.Length);
            uint c = update_crc(0xffffffffU, chunkType, 0, chunkType.Length);
            c = update_crc(c, data, 0, data.Length) ^ 0xffffffffU;
            OutputInt((int)c);
        }
    }
}
