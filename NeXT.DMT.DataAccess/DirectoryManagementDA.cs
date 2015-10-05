using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NeXT.DMT.Entities;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace NeXT.DMT.DataAccess
{
    public static class DirectoryManagementDA
    {

        /// <summary>
        /// Check if a directory already exist or not
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool CheckIfDirectoryExist(String path)
        {
            String pathToBeChecked = ConstantBE.OUTPUTPATH + path;

            if (Directory.Exists(pathToBeChecked))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Determind what will be name of the next external directory
        /// </summary>
        /// <param name="app">The app.</param>
        /// <param name="deliveryType">(FULL OR DELTA)</param>
        /// <returns></returns>
        public static String GetTheNextExternalDirectory(ApplicationBE app, String deliveryType)
        {
            //path to the root directory
            String rootInternalDirectoryPath = ConstantBE.OUTPUTPATH + '\\' + app.FolderName + "\\Externals";

            //get all sub folders in the root directory
            String[] directories = Directory.GetDirectories(rootInternalDirectoryPath);

            String nextExternalDirectoryName = String.Empty;

            //have not done any external delivery
            //therfore should be 1
            if (directories.Count() == 0)
            {
                //{0}_Liv_{1}_({2})
                nextExternalDirectoryName =  String.Format(ConstantBE.EXTERNALSDIRECTORYFORMAT, app.Quadri, "1", deliveryType);
            }
            else
            {
                //The HARD WAY

                /*
                //empty int collection
                Collection<int> intCol = new Collection<int>();

                //we get the integer part in the folders
                foreach (String dir in directories)
                {
                    //search for the interger part in the string
                    //and extract it
                    var integerPath = Regex.Match(dir, @"\d+");

                    intCol.Add(
                        int.Parse(integerPath.Value));
                }

                //.Max() will return the biggest digit in the collection
                //nextExternalDirectoryName = String.Format(ContantsBE.EXTERNALSDIRECTORYFORMAT, app.Quadri, intCol.Max() + 1, deliveryType);

                */

                //The EASY WAY
                //incrementing the subdirectorycount should be the next number
                //for the delivery folder

                //{0}_Liv_{1}_({2})
                nextExternalDirectoryName = String.Format(ConstantBE.EXTERNALSDIRECTORYFORMAT, app.Quadri, directories.Count() + 1, deliveryType);

            }


            return nextExternalDirectoryName;
        }

        /// <summary>
        /// Copy the file to a new location
        /// Rename it and overwrite any existing one
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static void CopyFileAndRename(String from, String to)
        {
            File.Copy(from, to, true);
        }

        /// <summary>
        /// Create the folder structure for an ITF
        /// If the folder already exists everything will be deleted
        /// </summary>
        /// <param name="rootPath">The path.</param>
        /// <param name="isINTorUAT">if set to <c>true</c> [is IN tor UAT].</param>
        /// <param name="isSTAorPROD">if set to <c>true</c> [is ST aor PROD].</param>
        /// <param name="isDelvDB">if set to <c>true</c> [is delv DB].</param>
        /// <param name="isDelvWEB">if set to <c>true</c> [is delv WEB].</param>
        /// <param name="isDelvWS">if set to <c>true</c> [is delv WS].</param>
        /// <param name="isDelvBATCH">if set to <c>true</c> [is delv BATCH].</param>
        /// <param name="isDelvK2PROCESS">if set to <c>true</c> [is delv k2 PROCESS].</param>
        public static void CreateDirectoryStructure3(String rootPath, bool isINTorUAT, bool isSTAorPROD,  bool isDelvDB, bool isDelvWEB, bool isDelvWS, bool isDelvBATCH, bool isDelvK2PROCESS)
        {
            String rootDirectoryPath = ConstantBE.OUTPUTPATH + rootPath;

            //check if folder exists and delete everything
            if (Directory.Exists(rootDirectoryPath))
            {
                Directory.Delete(rootDirectoryPath, true);
            }

            //create the root folder
            Directory.CreateDirectory(rootDirectoryPath);

            //create sub folders based on what is being delivered
            if (isDelvDB)
            {
                Directory.CreateDirectory(rootDirectoryPath + "\\DB");
            }

            if (isDelvWEB)
            {
                Directory.CreateDirectory(rootDirectoryPath + "\\WEB");
            }

            if (isDelvWS)
            {
                Directory.CreateDirectory(rootDirectoryPath + "\\WS");
            }

            if (isDelvBATCH)
            {
                Directory.CreateDirectory(rootDirectoryPath + "\\BATCH");
            }

            if (isDelvK2PROCESS)
            {
                Directory.CreateDirectory(rootDirectoryPath + "\\K2PROCESS");
            }

            //only create the MCOP directory if the following are
            //being delivered
            if ((isDelvWEB) || (isDelvWS) || (isDelvBATCH))
            {
                //create the root directory
                Directory.CreateDirectory(rootDirectoryPath + "\\MCOP_FILES");

                //delivering on INT and UAT
                if (isINTorUAT)
                {
                    Directory.CreateDirectory(rootDirectoryPath + "\\MCOP_FILES\\INT");
                    Directory.CreateDirectory(rootDirectoryPath + "\\MCOP_FILES\\UAT");

                    Directory.CreateDirectory(rootDirectoryPath + "\\MCOP_FILES\\INT\\SETUP");
                    Directory.CreateDirectory(rootDirectoryPath + "\\MCOP_FILES\\UAT\\SETUP");

                    Directory.CreateDirectory(rootDirectoryPath + "\\MCOP_FILES\\INT\\ROLLBACK");
                    Directory.CreateDirectory(rootDirectoryPath + "\\MCOP_FILES\\UAT\\ROLLBACK");

                    Directory.CreateDirectory(rootDirectoryPath + "\\MCOP_FILES\\INT\\LOGS");
                    Directory.CreateDirectory(rootDirectoryPath + "\\MCOP_FILES\\UAT\\LOGS");
                }

                //delivering on STA and PROD
                if (isSTAorPROD)
                {
                    Directory.CreateDirectory(rootDirectoryPath + "\\MCOP_FILES\\STA");
                    Directory.CreateDirectory(rootDirectoryPath + "\\MCOP_FILES\\PROD");

                    Directory.CreateDirectory(rootDirectoryPath + "\\MCOP_FILES\\STA\\SETUP");
                    Directory.CreateDirectory(rootDirectoryPath + "\\MCOP_FILES\\PROD\\SETUP");

                    Directory.CreateDirectory(rootDirectoryPath + "\\MCOP_FILES\\STA\\ROLLBACK");
                    Directory.CreateDirectory(rootDirectoryPath + "\\MCOP_FILES\\PROD\\ROLLBACK");

                    Directory.CreateDirectory(rootDirectoryPath + "\\MCOP_FILES\\STA\\LOGS");
                    Directory.CreateDirectory(rootDirectoryPath + "\\MCOP_FILES\\PROD\\LOGS");
                }

                //create the root backup directory
                Directory.CreateDirectory(rootDirectoryPath + "\\BACKUPS");

                //delivery is for INT or UAT
                if (isINTorUAT)
                {
                    Directory.CreateDirectory(rootDirectoryPath + "\\BACKUPS\\INT");
                    Directory.CreateDirectory(rootDirectoryPath + "\\BACKUPS\\UAT");

                    if (isDelvWEB)
                    {
                        Directory.CreateDirectory(rootDirectoryPath + "\\BACKUPS\\INT\\WEB");
                        Directory.CreateDirectory(rootDirectoryPath + "\\BACKUPS\\UAT\\WEB");
                    }

                    if (isDelvWS)
                    {
                        Directory.CreateDirectory(rootDirectoryPath + "\\BACKUPS\\INT\\WS");
                        Directory.CreateDirectory(rootDirectoryPath + "\\BACKUPS\\UAT\\WS");
                    }

                    if (isDelvBATCH)
                    {
                        Directory.CreateDirectory(rootDirectoryPath + "\\BACKUPS\\INT\\BATCH");
                        Directory.CreateDirectory(rootDirectoryPath + "\\BACKUPS\\UAT\\BATCH");
                    }
                }

                //delivery is for STA or PROD
                if (isSTAorPROD)
                {
                    Directory.CreateDirectory(rootDirectoryPath + "\\BACKUPS\\STA");
                    Directory.CreateDirectory(rootDirectoryPath + "\\BACKUPS\\PROD");

                    if (isDelvWEB)
                    {
                        Directory.CreateDirectory(rootDirectoryPath + "\\BACKUPS\\STA\\WEB");
                        Directory.CreateDirectory(rootDirectoryPath + "\\BACKUPS\\PROD\\WEB");
                    }

                    if (isDelvWS)
                    {
                        Directory.CreateDirectory(rootDirectoryPath + "\\BACKUPS\\STA\\WS");
                        Directory.CreateDirectory(rootDirectoryPath + "\\BACKUPS\\PROD\\WS");
                    }

                    if (isDelvBATCH)
                    {
                        Directory.CreateDirectory(rootDirectoryPath + "\\BACKUPS\\STA\\BATCH");
                        Directory.CreateDirectory(rootDirectoryPath + "\\BACKUPS\\PROD\\BATCH");
                    }
                }
            
            }

        }


        /// <summary>
        /// Creates the directory structure for the deployment
        /// Prepare yourself to see many IF statements !!
        /// </summary>
        /// <param name="deliveryInfo">The create directory.</param>
        public static void CreateDirectoryStructure(DeliveryInfoBE deliveryInfo)
        {
#warning should check structure for folder well. some deliverables don't have logs files 
            String rootDirectoryPathInternals = String.Empty;
            String rootDirectoryPathExternals = String.Empty;

            #region Int and Uat

            if (deliveryInfo.isINT || deliveryInfo.isUAT)
            {
                rootDirectoryPathInternals = deliveryInfo.RootDirectoryPath + '\\' + "Internals" + '\\' + "ITF" + deliveryInfo.ITFNo;

                //check if folder exists and delete everything
                if (Directory.Exists(rootDirectoryPathInternals))
                {
                    //there no format drive function at the moment 
                    //i wrote this :)
                    Directory.Delete(rootDirectoryPathInternals, true);
                }

                //create the root folder
                Directory.CreateDirectory(rootDirectoryPathInternals);

                #region Deliverables Directories
                
                //this folder should always be created else
                //where the hell are you going to save the generated documents ?
                Directory.CreateDirectory(rootDirectoryPathInternals + "\\RFC");

                //create sub folders based on what is being delivered
                //this you can optimise :)
                if (deliveryInfo.isDelvWEB)
                {
                    Directory.CreateDirectory(rootDirectoryPathInternals + "\\WEB");
                }
                if (deliveryInfo.isDelvWS)
                {
                    Directory.CreateDirectory(rootDirectoryPathInternals + "\\WS");
                }
                if (deliveryInfo.isDelvDB)
                {
                    Directory.CreateDirectory(rootDirectoryPathInternals + "\\DB");
                    Directory.CreateDirectory(rootDirectoryPathInternals + "\\DB\\LOGS");

                    if (deliveryInfo.isINT)
                    {
                        Directory.CreateDirectory(rootDirectoryPathInternals + "\\DB\\LOGS\\INT");
                    }

                    if (deliveryInfo.isUAT)
                    {
                        Directory.CreateDirectory(rootDirectoryPathInternals + "\\DB\\LOGS\\UAT");
                    }
                }
                if (deliveryInfo.isDelvK2PROCESS)
                {
                    Directory.CreateDirectory(rootDirectoryPathInternals + "\\K2PROCESS");

                    Directory.CreateDirectory(rootDirectoryPathInternals + "\\K2PROCESS\\LOGS");

                    if (deliveryInfo.isINT)
                    {
                        Directory.CreateDirectory(rootDirectoryPathInternals + "\\K2PROCESS\\LOGS\\INT");
                    }

                    if (deliveryInfo.isUAT)
                    {
                        Directory.CreateDirectory(rootDirectoryPathInternals + "\\K2PROCESS\\LOGS\\UAT");
                    }
                }
                if (deliveryInfo.isDelvREPORT)
                {
                    Directory.CreateDirectory(rootDirectoryPathInternals + "\\REPORT");
                }
                if (deliveryInfo.isDelvBATCH)
                {
                    Directory.CreateDirectory(rootDirectoryPathInternals + "\\BATCH");
                }

                #endregion

                #region Mcops Directories
                
                //only create the MCOP directory if the following are
                //being delivered WEB / WS / BATCH / REPORT
                if ((deliveryInfo.isDelvWEB) || (deliveryInfo.isDelvWS) || (deliveryInfo.isDelvBATCH))
                {
                    //create the root directory
                    Directory.CreateDirectory(rootDirectoryPathInternals + "\\MCOP_FILES");

                    if (deliveryInfo.isINT)
                    {
                        Directory.CreateDirectory(rootDirectoryPathInternals + "\\MCOP_FILES\\INT");

                        Directory.CreateDirectory(rootDirectoryPathInternals + "\\MCOP_FILES\\INT\\SETUP");

                        Directory.CreateDirectory(rootDirectoryPathInternals + "\\MCOP_FILES\\INT\\ROLLBACK");

                        Directory.CreateDirectory(rootDirectoryPathInternals + "\\MCOP_FILES\\INT\\LOGS");

                    }

                    if (deliveryInfo.isUAT)
                    {
                        Directory.CreateDirectory(rootDirectoryPathInternals + "\\MCOP_FILES\\UAT");

                        Directory.CreateDirectory(rootDirectoryPathInternals + "\\MCOP_FILES\\UAT\\SETUP");

                        Directory.CreateDirectory(rootDirectoryPathInternals + "\\MCOP_FILES\\UAT\\ROLLBACK");

                        Directory.CreateDirectory(rootDirectoryPathInternals + "\\MCOP_FILES\\UAT\\LOGS");

                    }
                }

                #endregion

                #region Backup Directories

                //only create the backup directories if the following are
                //being delivered WEB / WS / BATCH / REPORT
                if ((deliveryInfo.isDelvWEB) || (deliveryInfo.isDelvWS) || (deliveryInfo.isDelvBATCH) || (deliveryInfo.isDelvREPORT))
                {
                    //create the root directory
                    Directory.CreateDirectory(rootDirectoryPathInternals + "\\BACKUPS");

                    if (deliveryInfo.isINT)
                    {
                        //create the sub directories
                        Directory.CreateDirectory(rootDirectoryPathInternals + "\\BACKUPS\\INT");

                        if (deliveryInfo.isDelvWEB)
                        {
                            Directory.CreateDirectory(rootDirectoryPathInternals + "\\BACKUPS\\INT\\WEB");
                        }

                        if (deliveryInfo.isDelvWS)
                        {
                            Directory.CreateDirectory(rootDirectoryPathInternals + "\\BACKUPS\\INT\\WS");
                        }

                        if (deliveryInfo.isDelvBATCH)
                        {
                            Directory.CreateDirectory(rootDirectoryPathInternals + "\\BACKUPS\\INT\\BATCH");
                        }

                        if (deliveryInfo.isDelvREPORT)
                        {
                            Directory.CreateDirectory(rootDirectoryPathInternals + "\\BACKUPS\\INT\\REPORT");
                        }

                    }

                    if (deliveryInfo.isUAT)
                    {
                        //create the sub directories
                        Directory.CreateDirectory(rootDirectoryPathInternals + "\\BACKUPS\\UAT");

                        if (deliveryInfo.isDelvWEB)
                        {
                            Directory.CreateDirectory(rootDirectoryPathInternals + "\\BACKUPS\\UAT\\WEB");
                        }

                        if (deliveryInfo.isDelvWS)
                        {
                            Directory.CreateDirectory(rootDirectoryPathInternals + "\\BACKUPS\\UAT\\WS");
                        }

                        if (deliveryInfo.isDelvBATCH)
                        {
                            Directory.CreateDirectory(rootDirectoryPathInternals + "\\BACKUPS\\UAT\\BATCH");
                        }

                        if (deliveryInfo.isDelvREPORT)
                        {
                            Directory.CreateDirectory(rootDirectoryPathInternals + "\\BACKUPS\\UAT\\REPORT");
                        }
                    }
                }

                #endregion

            }

            #endregion

            #region Staging and Prod

            if (deliveryInfo.isSTA || deliveryInfo.isPROD)
            {
                rootDirectoryPathExternals = deliveryInfo.RootDirectoryPath + '\\' + "Externals" + '\\' + deliveryInfo.NextExternalDirectory;

                //check if folder exists and delete everything
                if (Directory.Exists(rootDirectoryPathExternals))
                {
                    //there no format drive function at the moment 
                    //i wrote this :)
                    Directory.Delete(rootDirectoryPathExternals, true);
                }

                //create the root folder
                Directory.CreateDirectory(rootDirectoryPathExternals);

                #region Deliverables Directories

#warning should check this line if it is documentation or documentations?
                //this folder should always be created else
                //where the hell are you going to save the generated documents ?
                Directory.CreateDirectory(rootDirectoryPathExternals + "\\DOCUMENTATION");

                //create sub folders based on what is being delivered
                //this you can optimise :)
                if (deliveryInfo.isDelvWEB)
                {
                    Directory.CreateDirectory(rootDirectoryPathExternals + "\\WEB");
                }
                if (deliveryInfo.isDelvWS)
                {
                    Directory.CreateDirectory(rootDirectoryPathExternals + "\\WS");
                }
                if (deliveryInfo.isDelvDB)
                {
                    Directory.CreateDirectory(rootDirectoryPathExternals + "\\DB");
                    Directory.CreateDirectory(rootDirectoryPathExternals + "\\DB\\LOGS");

                    if (deliveryInfo.isSTA)
                    {
                        Directory.CreateDirectory(rootDirectoryPathExternals + "\\DB\\LOGS\\STA");
                    }

                    if (deliveryInfo.isPROD)
                    {
                        Directory.CreateDirectory(rootDirectoryPathExternals + "\\DB\\LOGS\\PROD");
                    }
                }
                if (deliveryInfo.isDelvK2PROCESS)
                {
                    Directory.CreateDirectory(rootDirectoryPathExternals + "\\K2PROCESS");
                    Directory.CreateDirectory(rootDirectoryPathExternals + "\\K2PROCESS\\LOGS");

                    if (deliveryInfo.isSTA)
                    {
                        Directory.CreateDirectory(rootDirectoryPathExternals + "\\K2PROCESS\\LOGS\\STA");
                    }

                    if (deliveryInfo.isPROD)
                    {
                        Directory.CreateDirectory(rootDirectoryPathExternals + "\\K2PROCESS\\LOGS\\PROD");
                    }
                }
                if (deliveryInfo.isDelvREPORT)
                {
                    Directory.CreateDirectory(rootDirectoryPathExternals + "\\REPORT");
                }
                if (deliveryInfo.isDelvBATCH)
                {
                    Directory.CreateDirectory(rootDirectoryPathExternals + "\\BATCH");
                }

                #endregion

                #region Mcops Directories

                //only create the MCOP directory if the following are
                //being delivered WEB / WS / BATCH / REPORT
                if ((deliveryInfo.isDelvWEB) || (deliveryInfo.isDelvWS) || (deliveryInfo.isDelvBATCH))
                {
                    //create the root directory
                    Directory.CreateDirectory(rootDirectoryPathExternals + "\\MCOP_FILES");

                    if (deliveryInfo.isSTA)
                    {
                        Directory.CreateDirectory(rootDirectoryPathExternals + "\\MCOP_FILES\\STA");

                        Directory.CreateDirectory(rootDirectoryPathExternals + "\\MCOP_FILES\\STA\\SETUP");

                        Directory.CreateDirectory(rootDirectoryPathExternals + "\\MCOP_FILES\\STA\\ROLLBACK");

                        Directory.CreateDirectory(rootDirectoryPathExternals + "\\MCOP_FILES\\STA\\LOGS");

                    }

                    if (deliveryInfo.isPROD)
                    {
                        Directory.CreateDirectory(rootDirectoryPathExternals + "\\MCOP_FILES\\PROD");

                        Directory.CreateDirectory(rootDirectoryPathExternals + "\\MCOP_FILES\\PROD\\SETUP");

                        Directory.CreateDirectory(rootDirectoryPathExternals + "\\MCOP_FILES\\PROD\\ROLLBACK");

                        Directory.CreateDirectory(rootDirectoryPathExternals + "\\MCOP_FILES\\PROD\\LOGS");

                    }
                }

                #endregion

                #region Backup Directories

                //only create the backup directories if the following are
                //being delivered WEB / WS / BATCH / REPORT
                if ((deliveryInfo.isDelvWEB) || (deliveryInfo.isDelvWS) || (deliveryInfo.isDelvBATCH) || (deliveryInfo.isDelvREPORT))
                {
                    //create the root directory
                    Directory.CreateDirectory(rootDirectoryPathExternals + "\\BACKUPS");

                    if (deliveryInfo.isSTA)
                    {
                        //create the sub directories
                        Directory.CreateDirectory(rootDirectoryPathExternals + "\\BACKUPS\\STA");

                        if (deliveryInfo.isDelvWEB)
                        {
                            Directory.CreateDirectory(rootDirectoryPathExternals + "\\BACKUPS\\STA\\WEB");
                        }

                        if (deliveryInfo.isDelvWS)
                        {
                            Directory.CreateDirectory(rootDirectoryPathExternals + "\\BACKUPS\\STA\\WS");
                        }

                        if (deliveryInfo.isDelvBATCH)
                        {
                            Directory.CreateDirectory(rootDirectoryPathExternals + "\\BACKUPS\\STA\\BATCH");
                        }

                        if (deliveryInfo.isDelvREPORT)
                        {
                            Directory.CreateDirectory(rootDirectoryPathExternals + "\\BACKUPS\\STA\\REPORT");
                        }

                    }

                    if (deliveryInfo.isPROD)
                    {
                        //create the sub directories
                        Directory.CreateDirectory(rootDirectoryPathExternals + "\\BACKUPS\\PROD");

                        if (deliveryInfo.isDelvWEB)
                        {
                            Directory.CreateDirectory(rootDirectoryPathExternals + "\\BACKUPS\\PROD\\WEB");
                        }

                        if (deliveryInfo.isDelvWS)
                        {
                            Directory.CreateDirectory(rootDirectoryPathExternals + "\\BACKUPS\\PROD\\WS");
                        }

                        if (deliveryInfo.isDelvBATCH)
                        {
                            Directory.CreateDirectory(rootDirectoryPathExternals + "\\BACKUPS\\PROD\\BATCH");
                        }

                        if (deliveryInfo.isDelvREPORT)
                        {
                            Directory.CreateDirectory(rootDirectoryPathExternals + "\\BACKUPS\\PROD\\REPORT");
                        }
                    }
                }

                #endregion
            }

            #endregion

        }

        /// <summary>
        /// Build the distribution directory path
        /// </summary>
        /// <param name="appQuadri">The app quadri.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="deliverable">The deliverable.</param>
        /// <param name="itfNo">The itf no.</param>
        /// <param name="nextExternalDirectory">The next external directory.</param>
        /// <returns></returns>
        public static String GetDistributionDirectoryPath(String appQuadri, String delvPath, String deliverable, String itfNo, String nextExternalDirectory)
        {
            String distributionPath = String.Empty;

            ApplicationBE app =  ApplicationDA.GetApplication(appQuadri);

            //the path will always start output path follows by the application
            //folder name
            //Example : \\LIFESTYLE\FileService\Factories_Dev\Livraisons\IPRI_(MASP)\Internals\ITF564\DB
            distributionPath = ConstantBE.OUTPUTPATH + '\\' + app.FolderName;

            //if INT or UAT we append Internals and the itf number
            //else Enternals and next external directory
            if (delvPath == "Internals")
            {
                distributionPath += '\\' + "Internals" + '\\' + "ITF" + itfNo;
            }
            else
            {
                distributionPath += '\\' + "Externals" + '\\' + nextExternalDirectory;
            }

            //append the deliverable
            distributionPath += '\\' + deliverable;

            return distributionPath;
        }

        public static String GetDirectoryPath(DeliveryInfoBE delvInfo, String delvEnvironment, String deliverable, String delvType)
        {
            String directoryPath = delvInfo.RootDirectoryPath;

            #region Delivery Environment

            if ((delvEnvironment == "INT") || (delvEnvironment == "UAT"))
            {
                directoryPath += '\\' + "Internals" + '\\' + "ITF" + delvInfo.ITFNo;
            }
            else
            {
                directoryPath += '\\' + "Externals" + '\\' + delvInfo.NextExternalDirectory;
            }

            #endregion

            #region WEB or WS or BATCH

            if ((deliverable == "WEB") || (deliverable == "WS") || (deliverable == "BATCH")) 
            {
                directoryPath += '\\' + "MCOP_FILES" + '\\' + delvEnvironment + '\\' + delvType;
            }

            #endregion

            #region DB

            if (deliverable == "DB")
            {
                if (delvType == "SETUP")
                {
                    directoryPath += '\\' + "DB";
                }
                else
                {
                    directoryPath += '\\' + "DB" + '\\' + "LOGS" + '\\' + delvEnvironment;
                }
            }

            #endregion

            return directoryPath;
        }

        public static String GetDirectoryPathForMCOP(DeliveryInfoBE delvInfo, String delvEnvironment, String deliverable, bool isSetup)
        {
            String directoryPath = delvInfo.RootDirectoryPath;

            #region Delivery Environment

            if ((delvEnvironment == "INT") || (delvEnvironment == "UAT"))
            {
                directoryPath += '\\' + "Internals" + '\\' + "ITF" + delvInfo.ITFNo;
            }
            else
            {
                directoryPath += '\\' + "Externals" + '\\' + delvInfo.NextExternalDirectory;
            }

            #endregion

            #region WEB or WS or BATCH

            if (isSetup)
            {

                if (deliverable == "WEB")
                {
                    directoryPath += '\\' + "WEB";
                }
                else if (deliverable == "WS")
                {
                    directoryPath += '\\' + "WS";
                }
                else
                {
                    directoryPath += '\\' + "BATCH";
                }
            }
            else
            {
                if (deliverable == "WEB")
                {
                    directoryPath += '\\' + "BACKUPS" + '\\' + "WEB" + '\\' + delvEnvironment;
                }
                else if (deliverable == "WS")
                {
                    directoryPath += '\\' + "BACKUPS" + '\\' + "WS" + '\\' + delvEnvironment;
                }
                else
                {
                    directoryPath += '\\' + "BACKUPS" + '\\' + "BATCH" + '\\' + delvEnvironment;
                }

            }

            #endregion

            return directoryPath;
        }

    }
}
