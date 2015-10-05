using System;
using System.Xml;
using NeXT.DMT.Entities;

namespace NeXT.DMT.DataAccess
{
    public static class McopDA
    {
        /// <summary>
        /// Generates the mcop file for setup.
        /// </summary>
        /// <param name="mcop">The mcop file settings</param>
        public static void GenerateMcopFileForSetup(McopBE mcop, DeliveryInfoBE deliveryInfo, String environment, String deliverable)
        {
            //create the xmldocument
            XmlDocument mcopXml = new XmlDocument();

            //set the declaration to "<?xml version="1.0" encoding="utf-8"?>"
            XmlDeclaration xmlDeclaration = mcopXml.CreateXmlDeclaration("1.0", "utf-8", null);

            //create the rootnode and append it to the document <maspCopy>
            XmlElement rootNode = mcopXml.CreateElement("maspCopy");
            mcopXml.InsertBefore(xmlDeclaration, mcopXml.DocumentElement);
            mcopXml.AppendChild(rootNode);

            #region Servers

            //create the parent node <servers> 
            XmlElement parentNodeServers = mcopXml.CreateElement("servers");

            //<servers pattern="{s}"> 
            XmlAttribute attributeServerPattern = mcopXml.CreateAttribute("pattern");
            attributeServerPattern.Value = "{s}";
            parentNodeServers.Attributes.Append(attributeServerPattern);

            foreach (String item in mcop.ServersCol)
            {
                //<add> 
                XmlElement childNodeServerAdd = mcopXml.CreateElement("add");

                //<add name='MYSERVER'> 
                XmlAttribute attributeAdd = mcopXml.CreateAttribute("name");
                attributeAdd.Value = item;

                childNodeServerAdd.Attributes.Append(attributeAdd);

                parentNodeServers.AppendChild(childNodeServerAdd);
            }

            #endregion

            #region DateTimeFormats

            //create the parent node <dateTimeFormats> 
            XmlElement parentNodeDateTimeFormats = mcopXml.CreateElement("dateTimeFormats");

            //<dateTimeFormats pattern="{d}"> 
            XmlAttribute attributeDateTimeFormatsPattern = mcopXml.CreateAttribute("pattern");
            attributeDateTimeFormatsPattern.Value = "{d}";
            parentNodeDateTimeFormats.Attributes.Append(attributeDateTimeFormatsPattern);

            //<add> 
            XmlElement childNodeLongIsoDateAdd = mcopXml.CreateElement("add");

            //<add name="longisodate" value="yyyy_MM_dd HH mm" default="true" />
            //<add name="longisodate" value="yyyy_MM_dd HH mm" default="false" />
            XmlAttribute attributeLongIsoDateName = mcopXml.CreateAttribute("name");
            attributeLongIsoDateName.Value = "longisodate";
            childNodeLongIsoDateAdd.Attributes.Append(attributeLongIsoDateName);

            XmlAttribute attributeLongIsoDateValue = mcopXml.CreateAttribute("value");
            attributeLongIsoDateValue.Value = "yyyy_MM_dd HH mm";
            childNodeLongIsoDateAdd.Attributes.Append(attributeLongIsoDateValue);

            XmlAttribute attributeLongIsoDateDefault = mcopXml.CreateAttribute("default");

            //<add> 
            XmlElement childNodeShortIsoDateAdd = mcopXml.CreateElement("add");

            //<add name="shortisodate" value="yyyy_MM_dd" default="true" />
            //<add name="shortisodate" value="yyyy_MM_dd" default="false" />
            XmlAttribute attributeShortIsoDateName = mcopXml.CreateAttribute("name");
            attributeShortIsoDateName.Value = "shortisodate";
            childNodeShortIsoDateAdd.Attributes.Append(attributeShortIsoDateName);

            XmlAttribute attributeShortIsoDateValue = mcopXml.CreateAttribute("value");
            attributeShortIsoDateValue.Value = "yyyy_MM_dd";
            childNodeShortIsoDateAdd.Attributes.Append(attributeShortIsoDateValue);

            XmlAttribute attributeShortIsoDateDefault = mcopXml.CreateAttribute("default");

            if (mcop.isLongIsoDateDefault)
            {
                attributeLongIsoDateDefault.Value = "true";
                attributeShortIsoDateDefault.Value = "false";
            }
            else
            {
                attributeLongIsoDateDefault.Value = "false";
                attributeShortIsoDateDefault.Value = "true";
            }

            childNodeLongIsoDateAdd.Attributes.Append(attributeLongIsoDateDefault);
            childNodeShortIsoDateAdd.Attributes.Append(attributeShortIsoDateDefault);

            parentNodeDateTimeFormats.AppendChild(childNodeLongIsoDateAdd);
            parentNodeDateTimeFormats.AppendChild(childNodeShortIsoDateAdd);

            #endregion

            #region Actions

            //create the parent node <actions> 
            XmlElement parentNodeActions = mcopXml.CreateElement("actions");

            #region Backup
            
            //add a comment to the file for humans :)
            XmlComment commentBackupAdd = mcopXml.CreateComment("backup");
            parentNodeActions.AppendChild(commentBackupAdd);

            //<add>
            XmlElement childNodeAddBackup = mcopXml.CreateElement("add");

            //src
            XmlAttribute attributeBackupSRC = mcopXml.CreateAttribute("src");
            attributeBackupSRC.Value = mcop.PhysicalPath;
            childNodeAddBackup.Attributes.Append(attributeBackupSRC);

            //dst
            XmlAttribute attributeBackupDST = mcopXml.CreateAttribute("dst");
            attributeBackupDST.Value = mcop.BackupPath;
            childNodeAddBackup.Attributes.Append(attributeBackupDST);

            //do
            XmlAttribute attributeBackupDO = mcopXml.CreateAttribute("do");
            attributeBackupDO.Value = mcop.FirstAction;
            childNodeAddBackup.Attributes.Append(attributeBackupDO);
            
            //new
            XmlAttribute attributeBackupNEW = mcopXml.CreateAttribute("new");
            attributeBackupNEW.Value = mcop.isNewFirstAction.ToString().ToLower();
            childNodeAddBackup.Attributes.Append(attributeBackupNEW);

            //overwrite
            XmlAttribute attributeBackupOverWrite = mcopXml.CreateAttribute("overwrite");
            attributeBackupOverWrite.Value = mcop.isOverWriteFirstAction.ToString().ToLower();
            childNodeAddBackup.Attributes.Append(attributeBackupOverWrite);

            //append it to the parent node
            parentNodeActions.AppendChild(childNodeAddBackup);

            #endregion

            #region Setup

            //add a comment to the file for humans :)
            XmlComment commentSetupAdd = mcopXml.CreateComment("setup");
            parentNodeActions.AppendChild(commentSetupAdd);

            //<add>
            XmlElement childNodeAddSetup = mcopXml.CreateElement("add");

            //src
            XmlAttribute attributeSetupSRC = mcopXml.CreateAttribute("src");
            attributeSetupSRC.Value = mcop.DeliverablePath;
            childNodeAddSetup.Attributes.Append(attributeSetupSRC);

            //dst
            XmlAttribute attributeSetupDST = mcopXml.CreateAttribute("dst");
            attributeSetupDST.Value = mcop.PhysicalPath;
            childNodeAddSetup.Attributes.Append(attributeSetupDST);

            //do
            XmlAttribute attributeSetupDO = mcopXml.CreateAttribute("do");
            attributeSetupDO.Value = mcop.SecondAction;
            childNodeAddSetup.Attributes.Append(attributeSetupDO);

            //new
            XmlAttribute attributeSetupNEW = mcopXml.CreateAttribute("new");
            attributeSetupNEW.Value = mcop.isNewSecondAction.ToString().ToLower();
            childNodeAddSetup.Attributes.Append(attributeSetupNEW);

            //overwrite
            XmlAttribute attributeSetupOverWrite = mcopXml.CreateAttribute("overwrite");
            attributeSetupOverWrite.Value = mcop.isOverWriteSecondAction.ToString().ToLower();
            childNodeAddSetup.Attributes.Append(attributeSetupOverWrite);

            //append it to the parent node
            parentNodeActions.AppendChild(childNodeAddSetup);

            #endregion

            #endregion

            //appending all the parent nodes to the root node
            rootNode.AppendChild(parentNodeServers);
            rootNode.AppendChild(parentNodeDateTimeFormats);
            rootNode.AppendChild(parentNodeActions);

            String mcopFileName = GetMcopName(deliveryInfo.AppQuadri, deliveryInfo.GetVersion(true, false), 
                deliverable, "SETUP");

            String mcopSavePath = DirectoryManagementDA.GetDirectoryPath(deliveryInfo, environment, deliverable, "SETUP");

            mcopXml.Save(mcopSavePath + '\\' + mcopFileName);
        }

        /// <summary>
        /// Generates the mcop file for roll back.
        /// </summary>
        /// <param name="mcop">The mcop file setting</param>
        public static void GenerateMcopFileForRollBack(McopBE mcop, DeliveryInfoBE deliveryInfo, String environment, String deliverable)
        {
            //create the xmldocument
            XmlDocument mcopXml = new XmlDocument();

            //set the declaration to "<?xml version="1.0" encoding="utf-8"?>"
            XmlDeclaration xmlDeclaration = mcopXml.CreateXmlDeclaration("1.0", "utf-8", null);

            //create the rootnode and append it to the document <maspCopy>
            XmlElement rootNode = mcopXml.CreateElement("maspCopy");
            mcopXml.InsertBefore(xmlDeclaration, mcopXml.DocumentElement);
            mcopXml.AppendChild(rootNode);

            #region Servers

            //create the parent node <servers> 
            XmlElement parentNodeServers = mcopXml.CreateElement("servers");

            //<servers pattern="{s}"> 
            XmlAttribute attributeServerPattern = mcopXml.CreateAttribute("pattern");
            attributeServerPattern.Value = "{s}";
            parentNodeServers.Attributes.Append(attributeServerPattern);

            foreach (String item in mcop.ServersCol)
            {
                //<add> 
                XmlElement childNodeServerAdd = mcopXml.CreateElement("add");

                //<add name='MYSERVER'> 
                XmlAttribute attributeAdd = mcopXml.CreateAttribute("name");
                attributeAdd.Value = item;

                childNodeServerAdd.Attributes.Append(attributeAdd);

                parentNodeServers.AppendChild(childNodeServerAdd);
            }

            #endregion

            #region DateTimeFormats

            //create the parent node <dateTimeFormats> 
            XmlElement parentNodeDateTimeFormats = mcopXml.CreateElement("dateTimeFormats");

            //<dateTimeFormats pattern="{d}"> 
            XmlAttribute attributeDateTimeFormatsPattern = mcopXml.CreateAttribute("pattern");
            attributeDateTimeFormatsPattern.Value = "{d}";
            parentNodeDateTimeFormats.Attributes.Append(attributeDateTimeFormatsPattern);

            //<add> 
            XmlElement childNodeLongIsoDateAdd = mcopXml.CreateElement("add");

            //<add name="longisodate" value="yyyy_MM_dd HH mm" default="true" />
            //<add name="longisodate" value="yyyy_MM_dd HH mm" default="false" />
            XmlAttribute attributeLongIsoDateName = mcopXml.CreateAttribute("name");
            attributeLongIsoDateName.Value = "longisodate";
            childNodeLongIsoDateAdd.Attributes.Append(attributeLongIsoDateName);

            XmlAttribute attributeLongIsoDateValue = mcopXml.CreateAttribute("value");
            attributeLongIsoDateValue.Value = "yyyy_MM_dd HH mm";
            childNodeLongIsoDateAdd.Attributes.Append(attributeLongIsoDateValue);

            XmlAttribute attributeLongIsoDateDefault = mcopXml.CreateAttribute("default");

            //<add> 
            XmlElement childNodeShortIsoDateAdd = mcopXml.CreateElement("add");

            //<add name="shortisodate" value="yyyy_MM_dd" default="true" />
            //<add name="shortisodate" value="yyyy_MM_dd" default="false" />
            XmlAttribute attributeShortIsoDateName = mcopXml.CreateAttribute("name");
            attributeShortIsoDateName.Value = "shortisodate";
            childNodeShortIsoDateAdd.Attributes.Append(attributeShortIsoDateName);

            XmlAttribute attributeShortIsoDateValue = mcopXml.CreateAttribute("value");
            attributeShortIsoDateValue.Value = "yyyy_MM_dd";
            childNodeShortIsoDateAdd.Attributes.Append(attributeShortIsoDateValue);

            XmlAttribute attributeShortIsoDateDefault = mcopXml.CreateAttribute("default");

            if (mcop.isLongIsoDateDefault)
            {
                attributeLongIsoDateDefault.Value = "true";
                attributeShortIsoDateDefault.Value = "false";
            }
            else
            {
                attributeLongIsoDateDefault.Value = "false";
                attributeShortIsoDateDefault.Value = "true";
            }

            childNodeLongIsoDateAdd.Attributes.Append(attributeLongIsoDateDefault);
            childNodeShortIsoDateAdd.Attributes.Append(attributeShortIsoDateDefault);

            parentNodeDateTimeFormats.AppendChild(childNodeLongIsoDateAdd);
            parentNodeDateTimeFormats.AppendChild(childNodeShortIsoDateAdd);

            #endregion

            #region Actions

            //create the parent node <actions> 
            XmlElement parentNodeActions = mcopXml.CreateElement("actions");

            #region Delete

            //add a comment to the file for humans :)
            XmlComment commentBackupAdd = mcopXml.CreateComment("delete");
            parentNodeActions.AppendChild(commentBackupAdd);

            //<add>
            XmlElement childNodeAddBackup = mcopXml.CreateElement("add");

            //src
            XmlAttribute attributeBackupSRC = mcopXml.CreateAttribute("src");
            attributeBackupSRC.Value = mcop.PhysicalPath;
            childNodeAddBackup.Attributes.Append(attributeBackupSRC);

            //dst
            XmlAttribute attributeBackupDST = mcopXml.CreateAttribute("dst");
            attributeBackupDST.Value = String.Empty;
            childNodeAddBackup.Attributes.Append(attributeBackupDST);

            //do
            XmlAttribute attributeBackupDO = mcopXml.CreateAttribute("do");
            attributeBackupDO.Value = mcop.FirstAction;
            childNodeAddBackup.Attributes.Append(attributeBackupDO);

            //append it to the parent node
            parentNodeActions.AppendChild(childNodeAddBackup);

            #endregion

            #region Rollback

            //add a comment to the file for humans :)
            XmlComment commentSetupAdd = mcopXml.CreateComment("rollback");
            parentNodeActions.AppendChild(commentSetupAdd);

            //<add>
            XmlElement childNodeAddSetup = mcopXml.CreateElement("add");

            //src
            XmlAttribute attributeSetupSRC = mcopXml.CreateAttribute("src");
            attributeSetupSRC.Value = mcop.BackupPath;
            childNodeAddSetup.Attributes.Append(attributeSetupSRC);

            //dst
            XmlAttribute attributeSetupDST = mcopXml.CreateAttribute("dst");
            attributeSetupDST.Value = mcop.PhysicalPath;
            childNodeAddSetup.Attributes.Append(attributeSetupDST);

            //do
            XmlAttribute attributeSetupDO = mcopXml.CreateAttribute("do");
            attributeSetupDO.Value = mcop.SecondAction;
            childNodeAddSetup.Attributes.Append(attributeSetupDO);

            //new
            XmlAttribute attributeSetupNEW = mcopXml.CreateAttribute("new");
            attributeSetupNEW.Value = mcop.isNewSecondAction.ToString().ToLower();
            childNodeAddSetup.Attributes.Append(attributeSetupNEW);

            //overwrite
            XmlAttribute attributeSetupOverWrite = mcopXml.CreateAttribute("overwrite");
            attributeSetupOverWrite.Value = mcop.isOverWriteSecondAction.ToString().ToLower();
            childNodeAddSetup.Attributes.Append(attributeSetupOverWrite);

            //append it to the parent node
            parentNodeActions.AppendChild(childNodeAddSetup);

            #endregion

            #endregion

            //appending all the parent nodes to the root node
            rootNode.AppendChild(parentNodeServers);
            rootNode.AppendChild(parentNodeDateTimeFormats);
            rootNode.AppendChild(parentNodeActions);

            String mcopFileName = GetMcopName(deliveryInfo.AppQuadri, deliveryInfo.GetVersion(false, false),
                deliverable, "ROLLBACK");

            String mcopSavePath = DirectoryManagementDA.GetDirectoryPath(deliveryInfo, environment, deliverable, "ROLLBACK");

            mcopXml.Save(mcopSavePath + '\\' + mcopFileName);
        }

        /// <summary>
        /// Gets the name of the mcop file.
        /// </summary>
        /// <param name="appQuadri">The app quadri.</param>
        /// <param name="version">The version.</param>
        /// <param name="deliverable">The deliverable.</param>
        /// <param name="delvType">Type of the delv.</param>
        /// <returns></returns>
        public static String GetMcopName(String appQuadri, String version, String deliverable, String delvType)
        {

            String mcopFile = String.Empty;

            //format it
            mcopFile = String.Format(ConstantBE.MCOPFORMAT, appQuadri, version, deliverable, delvType);

            return mcopFile;
        }
    }
}
