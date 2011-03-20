namespace Raconteur.Helpers
{
    public static class XmlDocument
    {
        public static System.Xml.XmlDocument Load(string Xml)
        {
            var Doc = new System.Xml.XmlDocument();

            Doc.LoadXml(Xml);

            return Doc;
        }
    }
}