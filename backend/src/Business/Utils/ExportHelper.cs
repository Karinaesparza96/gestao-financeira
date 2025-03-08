using iTextSharp.text;
using iTextSharp.text.pdf;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
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
            var quantidadeCampos = props.Count;
            var numeroCampoAtual = 0;
            foreach (PropertyDescriptor prop in props)
            {
                numeroCampoAtual++;
                sb.Append(prop.DisplayName); // header
                if (numeroCampoAtual < quantidadeCampos) sb.Append(";");
            }
            sb.AppendLine();
            foreach (T item in data)
            {
                numeroCampoAtual = 0;
                foreach (PropertyDescriptor prop in props)
                {
                    numeroCampoAtual++;
                    sb.Append(prop.Converter.ConvertToString(
                         prop.GetValue(item)));
                    if (numeroCampoAtual < quantidadeCampos) sb.Append(";");
                }
                sb.AppendLine();
            }
            return RemoverAcentos(sb).ToString();
        }

        public static void GetPDF<T>(this IEnumerable<T> data, string outputPath = "c:\\output.pdf", string titulo = "Relatório")
        {
            ExportListToPdf(data.ToList(), outputPath, titulo);
        }

        public static void ExportListToPdf<T>(List<T> list, string outputPath, string titulo)
        {
            const string LINHAS_PARAGRAFO = "--------------------------------------------------------------------";

            using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                iTextSharp.text.Document document = new iTextSharp.text.Document();
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
                document.Open();

                if (list != null && list.Count > 0)
                {
                    document.Add(new Paragraph(titulo));
                    document.Add(new Paragraph(LINHAS_PARAGRAFO));
                    foreach (var item in list)
                    {
                        foreach (PropertyDescriptor prop in props)
                        {

                            document.Add(new Paragraph(prop.DisplayName + ": " + prop.Converter.ConvertToString(prop.GetValue(item))));

                        }
                        document.Add(new Paragraph(LINHAS_PARAGRAFO));
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
        private static StringBuilder RemoverAcentos(StringBuilder textoArquivo)
        {
            const string CR = "\r";
            const string LF = "\n";
            const string CRLF = "\r\n";

            textoArquivo.Replace(CRLF, LF)
                           .Replace(CR, LF)
                           .Replace(LF, CRLF);

            var textoNormalizado = textoArquivo.ToString().Normalize(NormalizationForm.FormD);
            var textoFormatado = new StringBuilder();
            const int INICIO_INDICE = 0;
            for (int indice = INICIO_INDICE; indice < textoNormalizado.Length; indice++)
            {
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(textoNormalizado[indice]);
                if (category != UnicodeCategory.NonSpacingMark)
                {
                    textoFormatado.Append(textoNormalizado[indice]);
                }
            }
            return textoFormatado;
        }

    }
}
