using SOAP_client.Helpers;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace SOAP_client
{
    public class CallWebService
    {
        public CallWebService()
        {
            var _url = "http://service4.spotimage.fr:8665/ws/farmstarModulationRequest";
            var _action = "net/infoterra/farmstar/ws/model/modulation/xml/modulationRequestPort/modulationRequest ";

            var soapEnvelopeXml = CreateSoapEnvelope();
            var webRequest = CreateWebRequest(_url, _action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // begin async call to web request.
            var asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            string soapResult;
            using (var webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (var rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
                Console.Write(soapResult);
            }
        }

        private HttpWebRequest CreateWebRequest(string url, string action)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private XmlDocument CreateSoapEnvelope()
        {
            var soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(
                $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:x.=""xml.modulation.model.ws.farmstar.infoterra.net""><soapenv:Header/><soapenv:Body><x.:modulationRequest>{CreateSoapRequest()}</x.:modulationRequest></soapenv:Body></soapenv:Envelope>");
            return soapEnvelopeDocument;
        }

        private string CreateSoapRequest()
        {
            // The name of the request and the expected return file. The standard format is: 
            // <numsaisi_parcelle> _ <code_produit> _ <timestamp_de_the_requete>
            var RequestName = "POCWiuzRequest";
            // Customer's IP
            var requestIp = IpAddress.GetIpAddress().ToString();
            var requestTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            RequestName += $"_{requestTime}";
            //Code of operator
            var exploitantId = "ADS_d31ecb24dfd4187dee585427b70cfc08";
            //Organization Code
            var exploitantOrganisme = "ADS";



            var parametresProjection = "WGS84";

            //Product code
            var productCode = "COLZA02";
            //Entered number of the parcel
            var productParcelle = "I-568586-4";
            //
            var productLibelle = "PRECO_AZOTE_COLZA";
            var productSurfaceDeclaree = 13.31;
            //Type of product chosen
            var parametresTypeazote = Typeazote.Solide;
            //Quantity to add or subtract in nitrogen unit
            var parametresAjoutazote = 60;
            var parametresConcentrationAzote = 27;
            var sb = new StringBuilder();

            sb.Append($@"<x.:Request name=""{RequestName}"" ip=""{requestIp}"" time=""{requestTime}"">");
            sb.Append($@"<x.:Exploitant id=""{exploitantId}"" organisme=""{exploitantOrganisme}""/>");
            sb.Append($@"<x.:Product code=""{productCode}"" parcelle=""{productParcelle}"" libelle=""{productLibelle}"" surfacedeclaree=""{productSurfaceDeclaree}""/>");
            sb.Append(@"<x.:Personnalisation>");
            sb.Append($@"<x.:Parametres format=""SHP"" projection=""{parametresProjection}"" concentrationazote=""{parametresConcentrationAzote}"" typeazote=""{parametresTypeazote}"" ajoutazote=""{parametresAjoutazote}"" ajoutpourcentage=""False"" marque=""John Deere""  console=""GS2630"" />");
            sb.Append(@"</x.:Personnalisation>");
            sb.Append(@"</x.:Request>");

            Console.WriteLine(sb.ToString());

            return sb.ToString();
        }


        private void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (var stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }


        public enum Typeazote
        {
            Solide,
            Liquide
        }
    }
}