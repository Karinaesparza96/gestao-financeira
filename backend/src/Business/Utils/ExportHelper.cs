using iTextSharp.text;
using iTextSharp.text.pdf;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Text;
using static iTextSharp.text.pdf.AcroFields;

namespace Business.Utils
{
    public static class ExportHelper
    {

        public static string convertBase64(string lcString)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(lcString));
        }

        public static string getCSV<T>(this IEnumerable<T> data)
        {
            StringBuilder sb = new StringBuilder();
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in props)
            {
                sb.Append(prop.DisplayName); // header
                sb.Append("\t");
            }
            sb.AppendLine();
            foreach (T item in data)
            {
                foreach (PropertyDescriptor prop in props)
                {
                    sb.Append(prop.Converter.ConvertToString(
                         prop.GetValue(item)));
                    sb.Append("\t");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public static void GetPDF<T>(this IEnumerable<T> data, string outputPath = "d:\\output.pdf")
        {
            ExportListToPdf(data.ToList(), outputPath);
        }

        public static void ExportListToPdf<T>(List<T> list, string outputPath)
        {
            using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                iTextSharp.text.Document document = new iTextSharp.text.Document();
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
                document.Open();

                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        //document.Add(new Paragraph(item.ToString()));
                        foreach (PropertyDescriptor prop in props)
                        {

                            //document.Add(new Paragraph(item.ToString()));
                            document.Add(new Paragraph(prop.DisplayName + ": " + prop.Converter.ConvertToString(prop.GetValue(item))));

                        }
                        document.Add(new Paragraph("----------------------------------"));
                    }
                }
                else
                {
                    document.Add(new Paragraph("Relatório sem retorno de dados após consulta"));
                }

                document.Close();
                writer.Close();
            }
        }



        // Outros exemplos de exportação
        /*
        public static void WriteTsv<T>(this IEnumerable<T> data, TextWriter output)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in props)
            {
                output.Write(prop.DisplayName); // header
                output.Write("\t");
            }
            output.WriteLine();
            foreach (T item in data)
            {
                foreach (PropertyDescriptor prop in props)
                {
                    output.Write(prop.Converter.ConvertToString(
                         prop.GetValue(item)));
                    output.Write("\t");
                }
                output.WriteLine();
            }

            //return output.ToString();
        }
        public static string CsvLinq<T>(this IEnumerable<T> myList)
        {
            return String.Join(",", myList.Select(x => x.ToString()).ToArray());
        }

        public static string CreateCSVTextFile<T>(List<T> data, string seperator = ",")
        {
            var properties = typeof(T).GetProperties();
            var result = new StringBuilder();

            foreach (var row in data)
            {
                var values = properties.Select(p => p.GetValue(row, null));
                var line = string.Join(seperator, values);
                result.AppendLine(line);
            }

            return result.ToString();
        }

        private static string CreateCSVTextFile<T>(List<T> data)
        {
            var properties = typeof(T).GetProperties();
            var result = new StringBuilder();

            foreach (var row in data)
            {
                var values = properties.Select(p => p.GetValue(row, null))
                                       .Select(v => StringToCSVCell(Convert.ToString(v)));
                var line = string.Join(",", values);
                result.AppendLine(line);
            }

            return result.ToString();
        }

        private static string StringToCSVCell(string str)
        {
            bool mustQuote = (str.Contains(",") || str.Contains("\"") || str.Contains("\r") || str.Contains("\n"));
            if (mustQuote)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("\"");
                foreach (char nextChar in str)
                {
                    sb.Append(nextChar);
                    if (nextChar == '"')
                        sb.Append("\"");
                }
                sb.Append("\"");
                return sb.ToString();
            }

            return str;
        }

        public static string ToCsv<T>(IEnumerable<T> objectlist, string separator = ",")
        {
            Type t = typeof(T);
            PropertyInfo[] fields = t.GetProperties();

            string header = String.Join(separator, fields.Select(f => f.Name).ToArray());

            StringBuilder csvdata = new StringBuilder();
            csvdata.AppendLine(header);

            foreach (var o in objectlist)
                csvdata.AppendLine(ToCsvFields(separator, fields, o));

            return csvdata.ToString();
        }

        public static string ToCsvFields(string separator, PropertyInfo[] fields, object o)
        {
            StringBuilder linie = new StringBuilder();

            foreach (var f in fields)
            {
                if (linie.Length > 0)
                    linie.Append(separator);

                var x = f.GetValue(o);

                if (x != null)
                    linie.Append(x.ToString());
            }

            return linie.ToString();
        }    


        private static void CreateHeader<T>(List<T> list, StreamWriter sw)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            for (int i = 0; i < properties.Length - 1; i++)
            {
                sw.Write(properties[i].Name + ",");
            }
            var lastProp = properties[properties.Length - 1].Name;
            sw.Write(lastProp + sw.NewLine);
        }

        private static void CreateRows<T>(List<T> list, StreamWriter sw)
        {
            foreach (var item in list)
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                for (int i = 0; i < properties.Length - 1; i++)
                {
                    var prop = properties[i];
                    sw.Write(prop.GetValue(item) + ",");
                }
                var lastProp = properties[properties.Length - 1];
                sw.Write(lastProp.GetValue(item) + sw.NewLine);
            }
        }

        public static void CreateCSV<T>(List<T> list, string filePath)
        {
            StringBuilder sb = new StringBuilder();

            using (StreamWriter sw = new StreamWriter("d:\\teste4.csv"))
            {
                CreateHeader(list, sw);
                CreateRows(list, sw);
            }
        }
        */

    }
}
