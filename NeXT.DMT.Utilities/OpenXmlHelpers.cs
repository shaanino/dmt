using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using DocumentFormat.OpenXml;

namespace NeXT.DMT.Utilities
{
    /// <summary>
    /// Utility class for Open XML Document
    /// </summary>
    public static class OpenXmlHelpers
    {
        /// <summary>
        /// Get ContentControl from word document
        /// </summary>
        /// <param name="pDoc">Document mainpart</param>
        /// <param name="pTag">ContentControl tag to find</param>
        /// <returns>The ContentControl</returns>
        public static SdtBlock GetContentControl(MainDocumentPart pDoc, string pTag)
        {
            SdtBlock cc = pDoc.Document.Body.Descendants<SdtBlock>().Where
                     (r => r.SdtProperties.GetFirstChild<Tag>().Val == pTag).Single();

            return cc;
        }

        /// <summary>
        /// Get ContentControl from a ContentControl
        /// </summary>
        /// <param name="pBlock">Root Content Control</param>
        /// <param name="pTag">ContentControl tag to find</param>
        /// <returns>The ContentControl</returns>
        public static SdtBlock GetContentControl(SdtBlock pBlock, string pTag)
        {
            SdtBlock cc = pBlock.Descendants<SdtBlock>().Where
                      (r => r.SdtProperties.GetFirstChild<Tag>().Val == pTag).Single();

            return cc;
        }

        /// <summary>
        /// Generates a hyperlink and embed it
        /// in a paragraph tag
        /// </summary>
        /// <param name="mainDocPart">The main doc part.</param>
        /// <param name="hyperLink">The hyper link.</param>
        /// <returns></returns>
        public static Paragraph GenerateParagraphWithHyperLink(MainDocumentPart mainDocPart, String hyperLink)
        {
            //this will be display as
            //the text
            String urlLabel = hyperLink;

            //build the hyperlink
            //file:// ensure that document does not corrupt
            System.Uri uri = new Uri(@"file://" + hyperLink);

            //add it to the document
            HyperlinkRelationship rel = mainDocPart.AddHyperlinkRelationship(uri, true);

            //get the hyperlink id
            string relationshipId = rel.Id;

            //create the new paragraph tag
            Paragraph newParagraph = new Paragraph(
                new DocumentFormat.OpenXml.Wordprocessing.Hyperlink(
                    new ProofError() { Type = ProofingErrorValues.GrammarStart },
                    new DocumentFormat.OpenXml.Wordprocessing.Run(
                        new DocumentFormat.OpenXml.Wordprocessing.RunProperties(
                            new RunStyle() { Val = "Hyperlink" }),
                            new DocumentFormat.OpenXml.Wordprocessing.Text(urlLabel)
                            )) { History = OnOffValue.FromBoolean(true), Id = relationshipId });

            return newParagraph;
        }

        /// <summary>
        /// Get a stream from a document template path
        /// </summary>
        /// <param name="pFilePath">The document path</param>
        /// <returns>The stream</returns>
        public static MemoryStream GetTemplateStream(string pFilePath)
        {
            using (FileStream lFs = File.Open(pFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                MemoryStream lMs = new MemoryStream();

                lMs.SetLength(lFs.Length);
                lFs.Read(lMs.GetBuffer(), 0, (int)lFs.Length);

                return lMs;
            }
        }
    }
}
