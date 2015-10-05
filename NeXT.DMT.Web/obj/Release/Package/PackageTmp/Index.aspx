<%@ Page Title="" Language="C#" MasterPageFile="~/DMT.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" 
Inherits="NeXT.DMT.Web.Index" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
	<link href="css/metro.css" rel="stylesheet" type="text/css" />
    <link href="css/colorbox.css" rel="stylesheet" type="text/css" />

    <link href="css/token-input-facebook.css" rel="stylesheet" type="text/css" />

    <link href="css/base/jquery.ui.core.css" rel="stylesheet" type="text/css" />
    <link href="css/base/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="css/base/jquery.ui.datepicker.css" rel="stylesheet" type="text/css" />
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">

	<div class="metro-pivot">
        
		<div class='pivot-item'>
			<h3>
                <img src="images/step1.png" alt="Step 1" />
            </h3> 
	
			<p>
				<span>
					<asp:Label ID="LabelApplicationList" runat="server"
					AssociatedControlID="DropDownListApplicationList" meta:resourcekey="LabelApplicationListResource" />
				</span>
				<asp:DropDownList ID="DropDownListApplicationList" runat="server" 
								  CssClass="textbox floatRight"  meta:resourcekey="DropDownListApplicationListResource">
				</asp:DropDownList>
			</p>
            			
            <p>
				<span>
					<asp:Label ID="LabelIFT" runat="server" 
					AssociatedControlID="TextBoxIFT" meta:resourcekey="LabelIFTResource" />
				</span>
				<asp:TextBox ID="TextBoxIFT" MaxLength="4" Width="40px" CssClass="textbox" runat="server" />
                <asp:HyperLink ID="HyperLinkCreateNew" NavigateUrl="https://www.niteen.me" meta:resourcekey="HyperLinkCreateNewResource" runat="server" />
			</p>

            <p>
				<span>
					<asp:Label ID="LabelNewVersion" runat="server" 
					AssociatedControlID="TextBoxNewVersion" meta:resourcekey="LabelNewVersionResource" />
				</span>
				<asp:TextBox ID="TextBoxNewVersion" MaxLength="9" Width="50px" CssClass="textbox" runat="server" />
			</p>

            <p>
				<span>
					<asp:Label ID="LabelOldVersion" runat="server" 
					AssociatedControlID="TextBoxOldVersion" meta:resourcekey="LabelOldVersionResource" />
				</span>
				<asp:TextBox ID="TextBoxOldVersion" MaxLength="9" Width="50px" CssClass="textbox" runat="server" />
			</p>

			<p>
				<span>
					<asp:Label ID="LabelIsFull" runat="server" 
					AssociatedControlID="RadioButtonListLabelIsFull" meta:resourcekey="LabelApplicationLabelIsFullResource" />
				</span>

				<asp:RadioButtonList ID="RadioButtonListLabelIsFull" RepeatDirection="Horizontal" runat="server">
					<asp:ListItem Value="FULL" meta:resourcekey="RadioButtonListLabelIsFullYesResource" />
					<asp:ListItem Value="DELTA" Selected="True" meta:resourcekey="RadioButtonListLabelIsFullNoResource" />
				</asp:RadioButtonList>
			</p>

            <p>
				<span style="visibility:hidden">
					<asp:Label ID="LabelIsCorrective" runat="server" 
					AssociatedControlID="RadioButtonListLabelIsCorrective" meta:resourcekey="LabelIsCorrectiveResource" />
				</span>
                
                <asp:RadioButtonList ID="RadioButtonListLabelIsCorrective" RepeatDirection="Horizontal" runat="server">
					<asp:ListItem Value="EVOLUTIVE" meta:resourcekey="RadioButtonListLabelIsEvolutiveResource" />
                    <asp:ListItem Value="CORRECTIVE" Selected="True" meta:resourcekey="RadioButtonListLabelIsCorrectiveResource" />
				</asp:RadioButtonList>
			</p>

			<p>
				<span>
					<asp:Label ID="LabelEnvironments" runat="server" 
					AssociatedControlID="CheckBoxListEnvironments" meta:resourcekey="LabelEnvironmentsResource" />
				</span>
				<asp:CheckBoxList ID="CheckBoxListEnvironments" RepeatDirection="Horizontal" runat="server">
					<asp:ListItem Value="INT" meta:resourcekey="CheckBoxListEnvironmentsListItemINT" />
					<asp:ListItem Value="UAT" meta:resourcekey="CheckBoxListEnvironmentsListItemUAT" />
					<asp:ListItem Value="STA" meta:resourcekey="CheckBoxListEnvironmentsListItemSTA" />
					<asp:ListItem Value="PROD" meta:resourcekey="CheckBoxListEnvironmentsListItemPROD" />
				</asp:CheckBoxList>
			</p>

			<p>
				<span>
					<asp:Label ID="LabelDeliverables" runat="server" 
					AssociatedControlID="CheckBoxListDeliverables" meta:resourcekey="LabelDeliverablesResource" />
				</span>
				<asp:CheckBoxList ID="CheckBoxListDeliverables" RepeatDirection="Horizontal" runat="server">
					<asp:ListItem Value="WEB" meta:resourcekey="CheckBoxListDeliverablesListItemWebResource" class="delvListItemWEB"/>
					<asp:ListItem Value="WS" meta:resourcekey="CheckBoxListDeliverablesListItemWebServiceResource" class="delvListItemWS" />
                    <asp:ListItem Value="K2WS" meta:resourcekey="CheckBoxListDeliverablesListItemK2WebServiceResource" class="delvListItemK2WS" />
					<asp:ListItem Value="DB" meta:resourcekey="CheckBoxListDeliverablesListItemDatabaseResource" class="delvListItemDB" />
					<asp:ListItem Value="K2PROCESS" meta:resourcekey="CheckBoxListDeliverablesListItemWorkflowResource" class="delvListItemK2PROCESS" />
					<asp:ListItem Value="REPORT" meta:resourcekey="CheckBoxListDeliverablesListItemReportResource" class="delvListItemREPORT" />
					<asp:ListItem Value="BATCH" meta:resourcekey="CheckBoxListDeliverablesListItemBatchResource" class="delvListItemBATCH" />
				</asp:CheckBoxList>
			</p>


        <div id="divDeliverables">

            <div id="divWebDeliverable">
                <p >
			        <span>
				        <asp:Label ID="LabelWeb" AssociatedControlID="TextBoxWebSVNLink" meta:resourcekey="LabelWebResource" 
					        runat="server" />
			        </span>
			        <asp:TextBox ID="TextBoxWebSVNLink" CssClass="textbox svnlinkTextBox" Width="600px" runat="server" />
		        </p>
                <p>
                    <input type="hidden" name="HiddenWebDeliverable" class="hiddenFieldDelCode" value="WEB" />
		        </p>
            </div>

            <div id="divWebServiceDeliverable">
			    <p>
			        <span>
				        <asp:Label ID="LabelWebService" AssociatedControlID="TextBoxWebServiceSVNLink" meta:resourcekey="LabelWebServiceResource"
					        runat="server" />
			        </span>
			        <asp:TextBox ID="TextBoxWebServiceSVNLink" CssClass="textbox svnlinkTextBox" Width="600px" runat="server" />
		        </p>
                <p>
                    <input type="hidden" name="HiddenWebServiceDeliverable" class="hiddenFieldDelCode" value="WS" />
		        </p>
            </div>

            <div id="divDatabaseDeliverable">
			    <p>
			        <span>
				        <asp:Label ID="LabelDatabase" AssociatedControlID="TextBoxDatabaseSVNLink" meta:resourcekey="LabelDatabaseResource"
					        runat="server" />
			        </span>
			        <asp:TextBox ID="TextBoxDatabaseSVNLink" CssClass="textbox svnlinkTextBox" Width="600px" runat="server" />
		        </p>
                <p>
                    <input type="hidden" name="HiddenDBDeliverable" class="hiddenFieldDelCode" value="DB" />
		        </p>
            </div>

            <div id="divK2ProcessDeliverable">
			    <p>
			        <span>
				        <asp:Label ID="LabelK2Process" AssociatedControlID="TextBoxK2ProcessSVNLink" meta:resourcekey="LabelK2ProcessResource"
					        runat="server" />
			        </span>
			        <asp:TextBox ID="TextBoxK2ProcessSVNLink" CssClass="textbox svnlinkTextBox" Width="600px" runat="server" />
		        </p>
                <p>
                    <input type="hidden" name="HiddenK2Deliverable" class="hiddenFieldDelCode" value="K2PROCESS" />
		        </p>
            </div>

            <div id="divReportDeliverable">
			    <p>
			        <span>
				        <asp:Label ID="LabelReport" AssociatedControlID="TextBoxReportSVNLink" meta:resourcekey="LabelReportResource"
					        runat="server" />
			        </span>
			        <asp:TextBox ID="TextBoxReportSVNLink" CssClass="textbox svnlinkTextBox" Width="600px" runat="server" />
		        </p>
                <p>
                    <input type="hidden" name="HiddenReportDeliverable" class="hiddenFieldDelCode" value="REPORT" />
		        </p>
            </div>

            <div id="divBatchDeliverable">
			    <p>
			        <span>
				        <asp:Label ID="LabelBatchFile" AssociatedControlID="TextBoxBatchFileSVNLink" meta:resourcekey="LabelBatchFileResource"
					        runat="server" />
			        </span>
			        <asp:TextBox ID="TextBoxBatchFileSVNLink" CssClass="textbox svnlinkTextBox" Width="600px" runat="server" />
		        </p>
                <p>
                    <input type="hidden" name="HiddenBatchDeliverable" class="hiddenFieldDelCode" value="BATCH" />
		        </p>
            </div>

		</div>

            <asp:Button ID="ButtonNextStep1" CssClass="test" OnClientClick="return false;" meta:resourcekey="ButtonNextResource" runat="server" />

		</div>

		<div id="divBLInformation" class="pivot-item">
		    <h3>
                <img src="images/step2.png" alt="Step 2"/>
            </h3>
		    <p>
			<span>
				<asp:Label ID="LabelPreparedBY" AssociatedControlID="TextBoxPreparedBY" meta:resourcekey="LabelPreparedBYResource"
					runat="server" />
			</span>
			<asp:TextBox ID="TextBoxPreparedBY" CssClass="textbox autosuggestTextBox" Width="150px" runat="server" />
		    </p>

		    <p>
			<span>
				<asp:Label ID="LabelApprovedBY" AssociatedControlID="TextBoxApprovedBY" meta:resourcekey="LabelApprovedBYResource"
					runat="server" />
			</span>
			<asp:TextBox ID="TextBoxApprovedBY" CssClass="textbox autosuggestTextBox" Width="150px" runat="server" />
		    </p>

		    <p>
			<span>
				<asp:Label ID="LabelDeliveryDate" AssociatedControlID="TextBoxDeliveryDate" meta:resourcekey="LabelDeliveryDateResource"
					runat="server" />
			</span>
			<asp:TextBox ID="TextBoxDeliveryDate" CssClass="textbox datetimePickerTextBox" Width="100px" runat="server" />
		    </p>

            <div id="divChangeAndQC">
                <div class="changeAndQCElements">
                    <p>
				        <span>
					        <asp:Label ID="LabelChangeQCID" runat="server" 
					        AssociatedControlID="TextBoxChangeQCID" meta:resourcekey="LabelChangeQCIDResource" />
				        </span>
                        <asp:TextBox ID="TextBoxChangeQCID" CssClass="textbox changeQcTextBox expandableTextBox" TextMode="MultiLine" Width="200px" runat="server" />
			        </p>
                    <p>
				        <span>
					        <asp:Label ID="LabelChangeQCIDShortDescription" runat="server" 
					        AssociatedControlID="TextBoxChangeQCIDShortDescription" meta:resourcekey="LabelChangeQCIDShortDescriptionResource" />
				        </span>
                        <asp:TextBox ID="TextBoxChangeQCIDShortDescription" CssClass="textbox changeShortDescTextBox expandableTextBox" TextMode="MultiLine" Width="600px" runat="server" />
			        </p>
                    <%--<asp:HyperLink ID="HyperLinkChangeQCRemove" CssClass="removeButton removeButtonChangeQC" NavigateUrl="#"  runat="server" />--%>
                </div>

            </div><!--divChangeAndQC-->

            <p>
                <asp:Button ID="ButtonAddChangeAndQC" runat="server" CausesValidation="False"
                meta:resourcekey="ButtonAddChangeAndQCResource"
                OnClientClick="return false;"/>
            </p>


            <div id="divDocumentations">
                <div class="delvDocumentations">
                    
                    <p>
				        <span>
					        <asp:Label ID="LabelDelvDocumentationName" runat="server" 
					        AssociatedControlID="TextBoxDelvDocumentationName" meta:resourcekey="LabelDelvDocumentationNameResource" />
				        </span>
                        <asp:TextBox ID="TextBoxDelvDocumentationName" CssClass="textbox delvDocName" Width="200px" runat="server" />
			        </p>

                    <p>
				        <span>
					        <asp:Label ID="LabelDelvDocumentationLink" runat="server" 
					        AssociatedControlID="TextBoxDelvDocumentationLink" meta:resourcekey="LabelDelvDocumentationLinkResource" />
				        </span>
                        <asp:TextBox ID="TextBoxDelvDocumentationLink" CssClass="textbox delvDocLink" Width="600px" runat="server" />
			        </p>

                    <p>
				        <span>
					        <asp:Label ID="LabelDelvDocumentationRemark" runat="server" 
					        AssociatedControlID="TextBoxDelvDocumentationRemark" meta:resourcekey="LabelDelvDocumentationRemarkResource" />
				        </span>
                        <asp:TextBox ID="TextBoxDelvDocumentationRemark" CssClass="textbox delvDocRemark" Width="600px" runat="server" />
			        </p>
                    
                    <asp:HyperLink ID="HyperLinkDocumentationRemove" CssClass="removeButton removeButtonChangeQC" NavigateUrl="#"  runat="server" />
                </div>

            </div><!--divDocumentations-->

             <asp:Button ID="ButtonPreviousStep2" OnClientClick="return false;" meta:resourcekey="ButtonPreviousResource" runat="server" />
             <asp:Button ID="ButtonNextStep2" OnClientClick="return false;" meta:resourcekey="ButtonNextResource" runat="server" />
	    </div>

		<div id="divRFCInformation" class="pivot-item">
			<h3>
                <img src="images/step3.png" alt="Step 3" />
            </h3>
			
<%--			<p>
				<span>
					<asp:Label ID="LabelISITEntity" AssociatedControlID="TextBoxISITEntity" meta:resourcekey="LabelISITEntityResource"
						runat="server" />
				</span>
				<asp:TextBox ID="TextBoxISITEntity" Text="COS" CssClass="textbox" Width="50px" runat="server" />
			</p>--%>

			<p>
				<span>
					<asp:Label ID="LabelSubmittedBY" AssociatedControlID="TextBoxSubmittedBY" meta:resourcekey="LabelSubmittedBYResource"
						runat="server" />
				</span>
				<asp:TextBox ID="TextBoxSubmittedBY" CssClass="textbox autosuggestTextBox" Width="150px" runat="server" />
			</p>
			
			<p>
				<span>
					<asp:Label ID="LabelSubmittedDate" AssociatedControlID="TextBoxSubmittedDate" meta:resourcekey="LabelSubmittedDateResource"
						runat="server" />
				</span>
				<asp:TextBox ID="TextBoxSubmittedDate" CssClass="textbox datetimePickerTextBox" Width="150px" runat="server" />
			</p>

			<p>
				<span>
					<asp:Label ID="LabelQualificationOfRequest" runat="server" 
					AssociatedControlID="RadioButtonListQualificationOfRequest" meta:resourcekey="LabelQualificationOfRequestResource" />
				</span>
				<asp:RadioButtonList ID="RadioButtonListQualificationOfRequest" RepeatDirection="Horizontal" runat="server">
					<asp:ListItem Value="Major" meta:resourcekey="RadioButtonListQualificationOfRequestMajorResource" />
					<asp:ListItem Value="Medium"  Selected="True"  meta:resourcekey="RadioButtonListQualificationOfRequestMediumResource" />
					<asp:ListItem Value="Minor" meta:resourcekey="RadioButtonListQualificationOfRequestMinorResource" />
				</asp:RadioButtonList>
			</p>

			<p>
				<span>
					<asp:Label ID="LabelPriority" runat="server" 
					AssociatedControlID="RadioButtonListPriority" meta:resourcekey="LabelPriorityResource" />
				</span>
				<asp:RadioButtonList ID="RadioButtonListPriority" RepeatDirection="Horizontal" runat="server">
					<asp:ListItem Value="1" meta:resourcekey="RadioButtonListPriorityOneResource" />
					<asp:ListItem Value="2"  Selected="True"  meta:resourcekey="RadioButtonListPriorityTwoResource" />
					<asp:ListItem Value="3" meta:resourcekey="RadioButtonListPriorityThreeResource" />
				</asp:RadioButtonList>
			</p>

			<p>
				<span>
					<asp:Label ID="LabelRegulatedEnvironment" runat="server" 
					AssociatedControlID="RadioButtonListRegulatedEnvironment" meta:resourcekey="LabelRegulatedEnvironmentResource" />
				</span>
				<asp:RadioButtonList ID="RadioButtonListRegulatedEnvironment" RepeatDirection="Horizontal" runat="server">
					<asp:ListItem Value="GxPImpact" meta:resourcekey="RadioButtonListRegulatedEnvironmentGXPImpactResource" />
					<asp:ListItem Value="SOAImpact" meta:resourcekey="RadioButtonListRegulatedEnvironmentSOAImpactResource" />
					<asp:ListItem Value="NoImpact" Selected="True" meta:resourcekey="RadioButtonListRegulatedEnvironmentNoImpactResource" />
				</asp:RadioButtonList>
			</p>
            
            <div id="divUATExpectedFinish">
                
                <p>
				    <span>
					    <asp:Label ID="LabelExpectedDate" AssociatedControlID="TextBoxExpectedDate" meta:resourcekey="LabelExpectedDateResource"
						    runat="server" />
				
			    	    <asp:TextBox ID="TextBoxExpectedDate" CssClass="textbox datetimePickerTextBox" Width="150px" runat="server" />
                    </span>
                </p>
                
                <p>
            	<span>
					<asp:Label ID="LabelExpectedTime" AssociatedControlID="TextBoxExpectedTime" meta:resourcekey="LabelExpectedTimeResource"
						runat="server" />
		
                    <asp:TextBox ID="TextBoxExpectedTime" CssClass="textbox" Width="150px" runat="server" />
                </span>
                </p>

                <p>
                <span>
					<asp:Label ID="LabelExpectedDuration" AssociatedControlID="TextBoxExpectedDuration" meta:resourcekey="LabelExpectedDurationResource"
						runat="server" />

                    <asp:TextBox ID="TextBoxExpectedDuration" Text="1h" CssClass="textbox" Width="25px" runat="server" />
				</span>
                </p>
            </div>

            <p>
				<span>
					<asp:Label ID="LabelChangeReason" AssociatedControlID="TextBoxChangeReason" meta:resourcekey="LabelChangeReasonResource"
						runat="server" />
				</span>
				<asp:TextBox ID="TextBoxChangeReason" CssClass="textbox" Width="600px" runat="server" />
            </p>

            <asp:Button ID="ButtonPreviousStep3" OnClientClick="return false;" meta:resourcekey="ButtonPreviousResource" runat="server" />
            <asp:Button ID="ButtonNextStep3" OnClientClick="return false;" meta:resourcekey="ButtonNextResource" runat="server" />
		</div>

        <div id="divMCOPFiles" class="pivot-item">
			<h3>
                <img src="images/step4.png" alt="Step 4"/>
            </h3> 

            <div id="divWebMCOP">
                
                <p>
                    <span>
                        <asp:CheckBox ID="CheckBoxHPSD1" runat="server" />
                    </span>

                    <asp:Label ID="LabelCheckBoxHPSD1" AssociatedControlID="CheckBoxHPSD1" meta:resourcekey="LabelCheckBoxHPSD1Resource" runat="server" />
                </p>

                <p>
                    <span>
                        <asp:CheckBox ID="CheckBoxHPSD2" runat="server" />
                    </span>

                    <asp:Label ID="LabelCheckBoxHPSD2" AssociatedControlID="CheckBoxHPSD2" meta:resourcekey="LabelCheckBoxHPSD2Resource" runat="server" />
                </p>

                <p>
                    <span>
                        <asp:CheckBox ID="CheckBoxCodeReview" runat="server" />
                    </span>

                    <asp:Label ID="LabelCheckBoxCodeReview" AssociatedControlID="CheckBoxCodeReview" meta:resourcekey="LabelCheckBoxCodeReviewResource" runat="server" />
                </p>

                <p>
                    <span>
                        <asp:CheckBox ID="CheckBoxQC" runat="server" />
                    </span>

                    <asp:Label ID="LabelCheckBoxQC" AssociatedControlID="CheckBoxQC" meta:resourcekey="LabelCheckBoxQCResource" runat="server" />
                </p>

                <p>
                    <span>
                        <asp:CheckBox ID="CheckBoxDocVersionTracking" runat="server" />
                    </span>

                    <asp:Label ID="LabelCheckBoxDocVersionTracking" AssociatedControlID="CheckBoxDocVersionTracking" meta:resourcekey="CheckBoxDocVersionTrackingResource" runat="server" />
                </p>

            </div>

            <asp:Button ID="ButtonPreviousStep4" OnClientClick="return false;" meta:resourcekey="ButtonPreviousResource" runat="server" />
                        <asp:Button ID="ButtonGenerate" meta:resourcekey="ButtonGenerateResource" 
            CausesValidation="false" OnClientClick="return false;" runat="server" />
		</div>

	</div><!--metro-pivot-->

    <div id="ajaxloading" style="visibility:hidden">
        <img src="images/loader_l.gif" alt="loading..." />
    </div><!--Ajax Loading-->

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMessages" runat="server">
    <div id="divUIMessage" class="testing">
    </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJavaScript" runat="server">
	
	<script src="js/jmetro.js" type="text/javascript"></script>
    <script src="js/jquery.tokeninput.js" type="text/javascript"></script>
    <script src="js/functions.js" type="text/javascript"></script>
    <script src="js/jqueryui/jquery.ui.core.min.js" type="text/javascript"></script>
    <script src="js/jqueryui/jquery.ui.widget.min.js" type="text/javascript"></script>
    <script src="js/jqueryui/jquery.ui.datepicker.min.js" type="text/javascript"></script>
    <script src="js/jquery.expandable.js" type="text/javascript"></script>
    <script src="js/colorbox.js" type="text/javascript"></script>

	<script type="text/javascript">
	    $(document).ready(function () {

	        var globalErrorMessage;

	        //initialise metro style
	        $("div .metro-pivot").metroPivot();

	        //initialise autosuggest
	        $('div .autosuggestTextBox').tokenInput("ajax/AutoSuggestHandler.ashx");

	        //initialise datetimepicker
	        $('div .datetimePickerTextBox').datepicker();

	        $('div .expandableTextBox').expandable();

	        //solve checkbox display problem
	        //if user refresh 
	        getDeliverablesCheckbox('<%=CheckBoxListDeliverables.ClientID%>');

	        FilterDeliverables();

	        $('#<%=DropDownListApplicationList.ClientID%>').change(function () {

	            FilterDeliverables();

	        });

	        $('#<%=CheckBoxListDeliverables.ClientID%>').change(function () {

	            getDeliverablesCheckbox('<%=CheckBoxListDeliverables.ClientID%>');

	        });

	        $('#<%=ButtonAddChangeAndQC.ClientID%>').click(function () {

	            var html2 = '<p>';
	            html2 += '<span><label title="Change / QC ID">Change / QC ID </label></span>';
	            html2 += '<input class="textbox changeqcID" type="text" style="width:50px;" maxlength="6">'
	            html2 += '</p>';
	            html2 += '<p>';
	            html2 += '<span><label title="Short description">Short Description </label></span>';
	            html2 += '<input class="textbox changeqcID" type="text" style="width:600px;" maxlength="120">'
	            html2 += '</p>';

	            var html = $('.delvDocumentations:first-child').clone();
	            $('#divDocumentations').append(html);
	        });

	        $('#<%=ButtonNextStep1.ClientID%>').click(function (event) {

	            if (validateStep1() == true) {
	                displayUIMessage("3");
	                globalErrorMessage = '';
	                pivot_goTo_ByIndex(1);
	            }
	            else {
	                $("#divUIMessage").html(globalErrorMessage);
	                displayUIMessage("0");
	            }
	        });

	        $('#<%=ButtonNextStep2.ClientID%>').click(function (event) {

	            if (validateStep2() == true) {
	                displayUIMessage("3");
	                globalErrorMessage = '';
	                pivot_goTo_ByIndex(1);
	            }
	            else {
	                $("#divUIMessage").html(globalErrorMessage);
	                displayUIMessage("0");
	            }

	        });

	        $('#<%=ButtonPreviousStep2.ClientID%>').click(function (event) {

	            globalErrorMessage = '';
	            displayUIMessage("3");
	            pivot_goTo_ByIndex(3);
	            $("#divUIMessage").html(globalErrorMessage);

	        });

	        $('#<%=ButtonNextStep3.ClientID%>').click(function (event) {

	            if (validateStep3() == true) {
	                displayUIMessage("3");
	                globalErrorMessage = '';
	                pivot_goTo_ByIndex(1);
	            }
	            else {
	                $("#divUIMessage").html(globalErrorMessage);
	                displayUIMessage("0");
	            }

	        });

	        $('#<%=ButtonPreviousStep3.ClientID%>').click(function (event) {

	            globalErrorMessage = '';
	            displayUIMessage("3");
	            pivot_goTo_ByIndex(3);
	            $("#divUIMessage").html(globalErrorMessage);

	        });


	        $('#<%=ButtonPreviousStep4.ClientID%>').click(function (event) {

	            globalErrorMessage = '';
	            displayUIMessage("3");
	            pivot_goTo_ByIndex(3);
	            $("#divUIMessage").html(globalErrorMessage);

	        });


	        $(".removeButtonChangeQC").live("click", function (event) {

	            //prevent the default behaviour
	            //scrolling up when you click on the hyperlink
	            event.preventDefault();

	            var childrenCount = $('#divDocumentations').children().length;

	            //don't remove the last element
	            if (childrenCount != 1) {
	                $(this).parent().remove();
	            }
	        });

	        $('#<%=HyperLinkCreateNew.ClientID%>').click(function (event) {

	            //prevent the default behaviour
	            //scrolling up when you click on the hyperlink
	            event.preventDefault();

	            //show the modal box
	            $('#<%=HyperLinkCreateNew.ClientID%>').colorbox({ iframe: true, innerWidth: 425, innerHeight: 344 });

	        });

	        $('#<%=ButtonGenerate.ClientID%>').click(function () {

	            if (validateStep4() == true) {
	                showHideLoader(1);

	                $.ajax({
	                    type: "POST",
	                    dataType: "json",
	                    url: "ajax/DMTHandler.ashx",
	                    data: getAllJsonObject()
	                }).done(function (response) {
	                    displayMessage(response);
	                    showHideLoader(2);
	                });
	            }
	            else {
	                $("#divUIMessage").html(globalErrorMessage);
	                displayUIMessage("0");
	            }
	        });

	        function getAllJsonObject() {

	            var jsonObject = '{'

	            jsonObject += '"Step1" :' + getJsonObjectForStep1() + ',';

	            jsonObject += '"Step2" :' + getJsonObjectForStep2() + ',';

	            jsonObject += '"Step3" :' + getJsonObjectForStep3();

	            jsonObject += '}';

	            return jsonObject;
	        }

	        function getJsonObjectForStep1() {

	            var jsonObject = '{';

	            jsonObject += '"ApplicationQuadri" : "' + $('#<%=DropDownListApplicationList.ClientID%>').val() + '",';

	            jsonObject += '"ITFNumber" : "' + $('#<%=TextBoxIFT.ClientID%>').val() + '",';

	            jsonObject += '"NewVersion" : "' + $('#<%=TextBoxNewVersion.ClientID%>').val() + '",';

	            jsonObject += '"OldVersion" : "' + $('#<%=TextBoxOldVersion.ClientID%>').val() + '",';

	            jsonObject += '"DeltaOrFull" : "' + getSelectValueByID('<%=RadioButtonListLabelIsFull.ClientID%>') + '",';

	            jsonObject += '"CorrectOrEvol" : "' + getSelectValueByID('<%=RadioButtonListLabelIsCorrective.ClientID%>') + '",';

	            jsonObject += '"Environments" : ' + getJsonObjectForCheckBox('<%=CheckBoxListEnvironments.ClientID%>', 'Environment') + ',';

	            jsonObject += '"Deliverables" : ' + getJsonObjectForCheckBox('<%=CheckBoxListDeliverables.ClientID%>', 'Deliverable') + ',';

	            jsonObject += '"SVNLinkAndVersion" : ' + getJsonObjectForSVNLinkAndVersion('divDeliverables', '.activeDeliverable');

	            jsonObject += '}';

	            return jsonObject;
	        }

	        function getJsonObjectForStep2() {

	            var jsonObject = '{';

	            jsonObject += '"PreparedBy" : "' + $('#<%=TextBoxPreparedBY.ClientID%>').val() + '",';

	            jsonObject += '"ApprovedBy" : "' + $('#<%=TextBoxApprovedBY.ClientID%>').val() + '",';

	            jsonObject += '"DeliveryDate" : "' + $('#<%=TextBoxDeliveryDate.ClientID%>').val() + '",';

	            jsonObject += '"ChangeQCAndDescription" : "' + replaceBreakLineWithVerticalBar($('#<%=TextBoxChangeQCID.ClientID%>').val() + ';' + $('#<%=TextBoxChangeQCIDShortDescription.ClientID%>').val()) + '",';

	            jsonObject += '"Documentation" :' + getJsonObjectForDocumentation('divDocumentations', 'delvDocumentations');

	            jsonObject += '}';

	            return jsonObject;
	        }

	        function getJsonObjectForStep3() {

	            var jsonObject = '{';

	            jsonObject += '"SubmittedBy" : "' + $('#<%=TextBoxSubmittedBY.ClientID%>').val() + '",';

	            jsonObject += '"SubmittedDate" : "' + $('#<%=TextBoxSubmittedDate.ClientID%>').val() + '",';

	            jsonObject += '"QualificationOfRequest" : "' + $('#<%=RadioButtonListQualificationOfRequest.ClientID%> :checked').val() + '",';

	            jsonObject += '"Priority" : "' + $('#<%=RadioButtonListPriority.ClientID%> :checked').val() + '",';

	            jsonObject += '"RegulatedEnvironment" : "' + $('#<%=RadioButtonListRegulatedEnvironment.ClientID%> :checked').val() + '",';

	            jsonObject += '"ExpectedDate" : "' + $('#<%=TextBoxExpectedDate.ClientID%>').val() + '",';

	            jsonObject += '"ExpectedStartTime" : "' + $('#<%=TextBoxExpectedTime.ClientID%>').val() + '",';

	            jsonObject += '"ExpectedDuration" : "' + $('#<%=TextBoxExpectedDuration.ClientID%>').val() + '",';

	            jsonObject += '"ChangeReason" : "' + $('#<%=TextBoxChangeReason.ClientID%>').val() + '"';

	            jsonObject += '}';

	            return jsonObject;
	        }

	        function validateStep1() {

	            var isValid = true;

	            //clear it
	            globalErrorMessage = '';

	            //ITF Validation
	            if ($('#<%=TextBoxIFT.ClientID%>').val() == '') {
	                globalErrorMessage += '<li> Enter the IFT Number </li>';
	                isValid = false;
	            }

	            //New and Old Versions
	            if (($('#<%=TextBoxNewVersion.ClientID%>').val() == '') || ($('#<%=TextBoxOldVersion.ClientID%>').val() == '')) {
	                globalErrorMessage += '<li> Enter the New and Old Version </li>';
	                isValid = false;
	            }

	            if ($('#<%=CheckBoxListEnvironments.ClientID%> :checked').length == 0) {
	                globalErrorMessage += '<li> Select at least one Deployment Environment </li>';
	                isValid = false;
	            }

	            if ($('#<%=CheckBoxListDeliverables.ClientID%> :checked').length == 0) {
	                globalErrorMessage += '<li> Select one or more Deliverables </li>';
	                isValid = false;
	            }

	            if (checkForValidDeliverables('divDeliverables') == false) {
	                globalErrorMessage += '<li> Be sure all Deliverables textbox(s) are filled </li>';
	                isValid = false;
	            }

	            return isValid;
	        }

	        function validateStep2() {

	            var isValid = true;

	            //clear it
	            globalErrorMessage = '';

	            //PreparedBy
	            if ($('#<%=TextBoxPreparedBY.ClientID%>').val() == '') {
	                globalErrorMessage += '<li> Prepared By cannot be NULL</li>';
	                isValid = false;
	            }

	            //ApprovedBy
	            if ($('#<%=TextBoxApprovedBY.ClientID%>').val() == '') {
	                globalErrorMessage += '<li> Who will approved your BL </li>';
	                isValid = false;
	            }

	            //Delivery date
	            if ($('#<%=TextBoxDeliveryDate.ClientID%>').val() == '') {
	                globalErrorMessage += '<li> Enter the Delivery Date </li>';
	                isValid = false;
	            }

	            //Change / QC
	            if (checkForValidDeliverables('divChangeAndQC') == false) {
	                globalErrorMessage += '<li> Be sure all Change/QC textbox(s) are filled </li>';
	                isValid = false;
	            }

	            return isValid;
	        }

	        function validateStep3() {

	            var isValid = true;

	            //clear it
	            globalErrorMessage = '';

	            //SubmittedBY
	            if ($('#<%=TextBoxSubmittedBY.ClientID%>').val() == '') {
	                globalErrorMessage += '<li>  Who is submitting the RFC </li>';
	                isValid = false;
	            }

	            //SubmittedDate
	            if ($('#<%=TextBoxSubmittedDate.ClientID%>').val() == '') {
	                globalErrorMessage += '<li> Enter the Submitted Date </li>';
	                isValid = false;
	            }

	            //ChangeReason
	            if ($('#<%=TextBoxChangeReason.ClientID%>').val() == '') {
	                globalErrorMessage += '<li> Enter the Change Reason </li>';
	                isValid = false;
	            }

	            /*
	            //Change / QC
	            if (checkForValidDeliverables('divChangeAndQC') == false) {
	            globalErrorMessage += '<li> Be sure all Change/QC textbox(s) are filled </li>';
	            isValid = false;
	            }
	            */
	            return isValid;
	        }

	        function validateStep4() {

	            var isValid = false;

	            //clear it
	            globalErrorMessage = '';

	            if ($('#<%=CheckBoxHPSD1.ClientID%>').is(':checked') && $('#<%=CheckBoxHPSD2.ClientID%>').is(':checked')
                && $('#<%=CheckBoxCodeReview.ClientID%>').is(':checked') && $('#<%=CheckBoxQC.ClientID%>').is(':checked')
                && $('#<%=CheckBoxDocVersionTracking.ClientID%>').is(':checked')) {

	                isValid = true;
	            }
	            else {
	                globalErrorMessage += '<li> Please make sure you have done all the above steps before continuing </li>';
	            }

	            return isValid;
	        }

	        function FilterDeliverables() {

	            $.ajax({
	                type: "POST",
	                dataType: "json",
	                url: "ajax/AppHandler.ashx",
	                data: $('#<%=DropDownListApplicationList.ClientID%>').val()
	            }).done(function (response) {

	                //reunable everything
	                enableAllOptions('<%=CheckBoxListDeliverables.ClientID%>');

	                $(response).each(function (i, item) {
	                    $('#<%=CheckBoxListDeliverables.ClientID%> .delvListItem' + item + ' input')
                        .attr("disabled", true);

	                });
	            });

	        }

	    });                     //end jquery root

    </script>

</asp:Content>