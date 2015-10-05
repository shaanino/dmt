//Get the selected value from
//a radiobutton list by pasing
//the element ID
function getSelectValueByID(elementID) {
    return $('#' + elementID + ' input:checked').val();
}

//Get the deliverable div name
//based on the element id
function getDeliverableDivName(elementID) {

    var divName;

    if (elementID == "WEB") {
        divName = "divWebDeliverable";
    }
    else if ((elementID == "WS") || (elementID == "K2WS"))  {
        divName = "divWebServiceDeliverable";
    }
    else if (elementID == "DB") {
        divName = "divDatabaseDeliverable";
    }
    else if (elementID == "K2PROCESS") {
        divName = "divK2ProcessDeliverable";
    }
    else if (elementID == "REPORT") {
        divName = "divReportDeliverable";
    }
    else if (elementID == "BATCH") {
        divName = "divBatchDeliverable";
    }
    else {
        divName = "divTestScriptDeliverable";
    }

    return divName;
}

//Return a JSON object for checkbox list
//base on the element ID
function getJsonObjectForCheckBox(elementID, elementName) {

    var jsonObject = '[';

    var checkedItems = [];

    var count = 0;
    
    var selectedItemsCount = $('#' + elementID + ' :checked').length;

    $('#' + elementID + ' :checked').each(function () {

        if ($.inArray($(this).val(), checkedItems) == -1) {

            count++;

            if (count != selectedItemsCount) {
                jsonObject += '"' + $(this).val() + '",';
                //jsonObject += '"' + elementName + $(this).val() + '" :"' + $(this).val() + '",';
            }
            else {
                jsonObject += '"' + $(this).val() + '"';
                //jsonObject += '"' + elementName + $(this).val() + '" :"' + $(this).val() + '"';
            
            }

            checkedItems.push($(this).val());
        }
    });

    jsonObject += ']'

    return jsonObject;
}


//Return a JSON object for texboxs
//base on the element ID/Class
function getJsonObjectForSVNLinkAndVersion(elementID, elementName) {

    var activeItems = [];

    $('#' + elementID + ' div').each(function () {

        if ($(this).attr('class') == elementName) {

            activeItems.push($(this).attr('id'));
        }
    });

    var jsonObject = '[';

    var count = 0;

    var selectedItemsCount = activeItems.length;

    $.each(activeItems, function (index, value) {

        var concatText = $('#' + value + ' .hiddenFieldDelCode').val() + ';' + $('#' + value + ' .svnlinkTextBox').val() + ';' + $('#' + value + ' .versionTextBox').val();

        count++;

        if (count != selectedItemsCount) {
            jsonObject += '"' + concatText + '",';
        }
        else {
            jsonObject += '"' + concatText + '"';

        }

    });


    jsonObject += ']'

    return jsonObject;
}

function getJsonObjectForDocumentation(elementID, elementName) {

    var activeItems = [];

    var selectedItemsCount = $('#' + elementID + ' div').length;

    var jsonObject = '[';

    var count = 0;

    $('#' + elementID + ' div').each(function (index, value) {

        //console.log($(this).find('.delvDocName').first().val());

        var _this = $(this);

        //var concatText = $('.delvDocName').val() + ';' + $('.delvDocLink').val() + ';' + $('.delvDocRemark').val();

        var concatText = _this.find('.delvDocName').first().val() + ';' + _this.find('.delvDocLink').first().val() + ';' + _this.find('.delvDocRemark').first().val();

        count++;

        if (count != selectedItemsCount) {
            jsonObject += '"' + concatText + '",';
        }
        else {
            jsonObject += '"' + concatText + '"';

        }

    });


    jsonObject += ']'

    return jsonObject;
}


function hideShowDeliverables(checkedItemsArray, unCheckedItemsArray) {

    $.each(unCheckedItemsArray, function (index, value) {

        var elementDivName = getDeliverableDivName(value);

        //if this item have the activeDeliverable
        //class we remove it and fade it out
        if ($('#' + elementDivName).not(".activeDeliverable")) {

            $('#' + elementDivName).removeClass(".activeDeliverable");
            $('#' + elementDivName).fadeOut("5000");
        }

    });

    $.each(checkedItemsArray, function (index, value) {

        var elementDivName = getDeliverableDivName(value);

        //if this item don't have the activeDeliverable
        //class we add it and fade it in on the UI
        if ($('#' + elementDivName).not(".activeDeliverable")) {
            $('#' + elementDivName).addClass(".activeDeliverable");
            $('#' + elementDivName).fadeIn("slow");
        }
    });
}

function getDeliverablesCheckbox(elementID) {

    var unCheckedItems = [];
    var checkedItems = [];

    $('#' + elementID + ' :checkbox').each(function () {

        //get the item value
        itemVal = $(this).val();

        //if it is checked
        if ($(this).attr("checked")) {
            checkedItems.push(itemVal);
        }
        else {
            unCheckedItems.push(itemVal);
        }
    });

    hideShowDeliverables(checkedItems, unCheckedItems);
}

function pivot_goTo_ByName(headerName) {
    $("div.metro-pivot").data("controller").goToItemByName(headerName);
}

function pivot_goTo_ByIndex(headerIndex) {
    $("div.metro-pivot").data("controller").goToItemByIndex(headerIndex);
}

function checkForValidDeliverables(elementName) {

    var isValid = true;

    $('#' + elementName + ' :input').each(function () {

        //the item is being displayed on screen
        var isVisible = $(this).is(':visible');

        //only check those that are displayed i.e active
        if (isVisible) {

            //get the item value
            var itemVal = $(this).val();

            if ($(this).val() == '') {
                isValid = false;
            }

        }

    }); //end of each

    return isValid;
}

function replaceBreakLineWithVerticalBar(strToReplace) {

    var replacedString = strToReplace.replace(/\n/g, "|");

    return replacedString;
}

function enableAllOptions(elementListName) {
    $('#' + elementListName + ' :input').each(function () {
        $(this).removeAttr("disabled");
        //$(this).attr('checked', false);

    });
}

function displayMessage(messageObject) {

    if (messageObject.Status == "1") {
        displayUIMessage("1");
    }

    $('#divUIMessage').html('<span>' + messageObject.Message + '<span>');
}

function displayUIMessage(status) {

    switch (status) {

        case "0":
            $("#divUIMessage").addClass("unHideElement")
            .addClass("uiErrorMessage");
            break;
        case "1":
            $("#divUIMessage").addClass("unHideElement")
            .addClass("uiSuccessMessage");
            break;
        default:
            $("#divUIMessage").addClass("hideElement")
            .removeClass("unHideElement")
            .removeClass("uiErrorMessage")
            .removeClass("uiSuccessMessage");
            break;
    }
}

//Show or Hide the loader animation on the screen
function showHideLoader(isVisible) {

    $("#ajaxloading").css("position", "absolute");
    $("#ajaxloading").css("top", (($(window).height() - $("#ajaxloading").outerHeight()) / 2) + $(window).scrollTop() + "px");
    $("#ajaxloading").css("left", (($(window).width() - $("#ajaxloading").outerWidth()) / 1.85) + $(window).scrollLeft() + "px");

    if (isVisible == 1) {
        $("#ajaxloading").css("visibility", "visible");
    }
    else {
       $("#ajaxloading").css("visibility", "hidden");
    }

}