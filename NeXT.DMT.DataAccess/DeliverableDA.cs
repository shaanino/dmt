using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NeXT.DMT.Entities;
using System.Collections.ObjectModel;
using System.Xml;

namespace NeXT.DMT.DataAccess
{
    public static class DeliverableDA
    {
        /// <summary>
        /// Fetch all the deliverables from the XML file
        /// </summary>
        /// <returns></returns>
        public static Collection<DeliverableBE> GetAllDeliverables()
        {
            //get the xml file path
            String xmlPath = ConstantBE.CONFIGPATH + ConstantBE.DELIVERABLELISTFILE;

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(xmlPath);

            //retrive all  the nodes having tag "Deliverable"
            XmlNodeList xmlNodeList = xmlDoc.SelectNodes("//Deliverables/Deliverable");

            Collection<DeliverableBE> deliverableCol = new Collection<DeliverableBE>();

            foreach (XmlElement item in xmlNodeList)
            {
                DeliverableBE newDeliverable = new DeliverableBE();

                newDeliverable.ID = int.Parse(item.ChildNodes[0].InnerText);
                newDeliverable.Name = item.ChildNodes[1].InnerText;

                //convert it to upper in case it is not in the xml file
                newDeliverable.Code = item.ChildNodes[2].InnerText.ToUpper();

                deliverableCol.Add(newDeliverable);
            }

            return deliverableCol;
        }
    }
}
