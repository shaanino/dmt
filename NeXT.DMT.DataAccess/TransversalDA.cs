using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using NeXT.DMT.Entities;
using System.Xml;

namespace NeXT.DMT.DataAccess
{
    public static class TransversalDA
    {
        /// <summary>
        /// Gets all persons from the person list file
        /// </summary>
        /// <returns></returns>
        public static Collection<PersonBE> GetAllPersons()
        {
            //get the xml file path
            String xmlPath = ConstantBE.CONFIGPATH + '\\' + ConstantBE.PERSONFILE;

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(xmlPath);

            //retrive all  the nodes having tag "Application"
            XmlNodeList xmlNodeList = xmlDoc.SelectNodes("//Persons/Person");

            Collection<PersonBE> personCol = new Collection<PersonBE>();

            foreach (XmlElement item in xmlNodeList)
            {
                PersonBE newPerson = new PersonBE();

                //newPerson.ID = int.Parse(item.ChildNodes[0].InnerText);
                newPerson.name = item.ChildNodes[1].InnerText;
                newPerson.id = item.ChildNodes[0].InnerText;

                personCol.Add(newPerson);
            }

            return personCol;
        }

        /// <summary>
        /// Gets a person details by ID.
        /// </summary>
        /// <param name="personID">The person ID.</param>
        /// <returns></returns>
        public static PersonBE GetPersonDetailsByID(String personID)
        {
            PersonBE personDetails = new PersonBE();

            //filter the collection to find the person
            //details
            personDetails = GetAllPersons()
                            .Where(p => p.id.Equals(personID))
                            .FirstOrDefault();

            return personDetails;
        }
    }
}
