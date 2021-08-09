$(document).ready(function () {

    $(function () {

        var options = {
            "appIconUrl": '../Content/images/CCVCorrespondanceLogo.PNG', //@ViewBag.SPHostUrl/siteassets/doj_logo.png',

            "appTitle": "CCV Correspondance",
            //"appHelpPageUrl": "Help.html?"
            //    + document.URL.split("?")[1],
            "settingsLinks": [
                //{
                //    "linkUrl": "Account.html?"
                //        + document.URL.split("?")[1],
                //    "displayName": "Account settings"
                //},
                //{
                //    "linkUrl": "Contact.html?"
                //        + document.URL.split("?")[1],
                //    "displayName": "Contact us"
                //}
            ]
        };


        var nav = new SP.UI.Controls.Navigation("chrome_ctrl_container", options);
        nav.setVisible(true);
    });

    $("form").submit(function () {
        $('input[type=submit]', this).attr('disabled', 'disabled');
    });

    $("#CorrespondanceFromDataList").click(function () {

        $.getJSON($('#CorrespondanceFromTo').attr("data-autocompleteme"), function (data) {
            var items; 
            $.each(data, function (i, correspondance) {
                items += "<option>" + correspondance.CorrespondanceFromTo + "</option>";
            });
            $('#CorrespondanceFromTo').html(items);
        });

    });

    $("#CorrespondanceDepartmentDataList").click(function () {

        $.getJSON($('#CorrespondanceDepartment').attr("data-autocompleteme"), function (data) {
            var items;
            $.each(data, function (i, correspondance) {
                items += "<option>" + correspondance.CorrespondanceDepartment + "</option>";
            });
            $('#CorrespondanceDepartment').html(items);
        });

    });

    $("#UsersDataList").click(function () {

        $.getJSON($('#UserName').attr("data-autocompleteme"), function (data) {
            var items;
            $.each(data, function (i, alrs) {
                items += "<option>" + data[i] + "</option>";
            });
            $('#UserName').html(items);
        });

    });

    $('#accessgroups').dataTable
        ({
            "bLengthChange": false,
            "bPaginate": true,
            "sScrollY": 400,
            "bJQueryUI": true,
            "sPaginationType": "full_numbers",

        })
            .rowGrouping({
                iGroupingColumnIndex: 0,
                iGroupingOrderByColumnIndex: 0,
                //iGroupingColumnIndex2: 1,
                iGroupingOrderByColumnIndex: 0,
                bExpandableGrouping: true,
                iExpandGroupOffset: 1,
                bExpandableGrouping2: true,
                iExpandGroupOffset: 2,
            });

    $(".datepicker").datepicker({
        changeMonth: true,
        changeYear: true,
        showotherMonths: true,
        selectotherMonths: true,
        firstDay: 1,
        dateFormat: "dd-MM-yy",
        timeFormat: "hh:mm tt"
    }).change(function () {
        if (this.id == 'StartDate') {
            var startValue = $(this).val() + ' ' + $('#StartTime').val();
            $('#StartDateTime').val(startValue);
        }
        else
            if (this.id == 'EndDate') {
                var endValue = $(this).val() + ' ' + $('#EndTime').val();;
                $('#EndDateTime').val(endValue);
            }
    });


    $('#CorrespondanceDateOnLetter').datepicker({
        changeMonth: true,
        changeYear: true,
        showotherMonths: true,
        selectotherMonths: true,
        firstDay: 1,
        duration: 0, dateFormat: 'dd/mm/yy',
        onClose: function (dateText, inst) {

            var myDate = $('#CorrespondanceDateOnLetter').val()
            
            if ((myDate.length == 0) || (isDate(myDate)))
                day = myDate.substring(0, 2)
            else
                alert('Invalid Date On Letter : Please enter the format as dd/mm/yyyy');
            
            //if (myDate.length === 10) {
            //    day = myDate.substring(0, 2);
            //    month = myDate.substring(2, 4);
            //    year = myDate.substring(4);
            //    date.dateFormat.
            //    if (month > 12)
            //    {
            //        alert("Invalid Date On Letter Month: Please enter the format as dd/mm/yyyy");
            //    }
            //    //alert($.datepicker.parseDate("dd/mm/yyyy", day + "/" + month + "/" + year));
            //}
            //else
            //{
            //    alert("Invalid Date: Please enter the format as dd/mm/yyyy");
            //}
        }
    });

    $('#CorrespondanceDateReceivedOrSent').datepicker({
        changeMonth: true,
        changeYear: true,
        showotherMonths: true,
        selectotherMonths: true,
        firstDay: 1,
        duration: 0, dateFormat: 'dd/mm/yy',
        onClose: function (dateText, inst) {

            var myDate = $('#CorrespondanceDateReceivedOrSent').val()

            if ((myDate.length == 0) || (isDate(myDate)))
                day = myDate.substring(0, 2)
            else
                alert('Invalid Date Received or Sent: Please enter the format as dd/mm/yyyy');

        }
    });

    $('#SSDate1').datepicker({
        changeMonth: true,
        changeYear: true,
        showotherMonths: true,
        selectotherMonths: true,
        firstDay: 1,
        duration: 0, dateFormat: 'dd/mm/yy',
        onClose: function (dateText, inst) {

            var myDate = $('#SSDate1').val()

            if ((myDate.length == 0) || (isDate(myDate)))
                day = myDate.substring(0, 2)
            else
                alert('Invalid Report Start Date: Please enter the format as dd/mm/yyyy');

        }
    });

    $('#SEDate1').datepicker({
        changeMonth: true,
        changeYear: true,
        showotherMonths: true,
        selectotherMonths: true,
        firstDay: 1,
        duration: 0, dateFormat: 'dd/mm/yy',
        onClose: function (dateText, inst) {

            var myDate = $('#SEDate1').val()

            if ((myDate.length == 0) || (isDate(myDate)))
                day = myDate.substring(0, 2)
            else
                alert('Invalid Report End Date: Please enter the format as dd/mm/yyyy');

        }
    });

    //$('#CorrespondanceDateOnLetter').datepicker({
    //    duration: 0, dateFormat: 'dd/mm/yy',
    //    onClose: function () {
    //        var myDate = $('#CorrespondanceDateOnLetter').val()
    //        var myDate = Globalize.parseDate(myDate);
    //        if (myDate == null) {
    //            $(this).val('');
    //        }
    //        else {
    //            $(this).val(Globalize.format(myDate, 'd'));
    //        }
    //    }
    //});

    //$('#datepicker').datepicker({ showOn: 'button', buttonImage: '/Content/images/calendar.gif', duration: 0, dateFormat: 'dd/mm/yyyy' });
    //$('#CorrespondanceDateOnLetter').datepicker({ duration: 0, dateFormat: 'dd/mm/yy' });
    //$('#CorrespondanceDateReceivedOrSent').datepicker({ duration: 0, dateFormat: 'dd/mm/yy' });

    //$('#example').dataTable
    //    ({
    //    "bLengthChange": false,
    //    "bPaginate": false

    //})

    //                  .rowGrouping({
    //                      iGroupingColumnIndex: 0,
    //                      iGroupingOrderByColumnIndex: 0,
    //                      iGroupingColumnIndex2: 1,
    //                      iGroupingOrderByColumnIndex: 0,
    //                      bExpandableGrouping: true,
    //                      iExpandGroupOffset: 1,
    //                      //asExpandedGroups: [""],
    //                      bExpandableGrouping2: true,
    //                      iExpandGroupOffset: 2,
    //                      //asExpandedGroups2: [""]
    //                      //asExpandedGroups2: [], 
    //                  });
    


    TableTools.BUTTONS.Back = $.extend(true, TableTools.buttonBase, {
        "sAction": "div",
        "sTag": "default",
        "sToolTip": "Back to main list",
        "sNewLine": " ",
        "sButtonText": "Back to List",
        "fnClick": function (nButton, oConfig) {
            document.location.href = window.location.href;
            //document.location.href = window.location.href.split('?')[0];
        }
    });

    var aSelected = [];

    // ************* Datatables Server Side ********************/
    var oTable = $('#example').dataTable({

        "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            $(nRow).on('click', function () {
                //document.location.href = window.location.toLocaleString() + "/Correspondance/Details/" + aData[7];
                document.location.href = window.location.href.split('?')[0] + "/Correspondance/Details/" + aData[7] + "?" + window.location.href.split('?')[1];


                //dochref = window.location.href.replace(/SPHostURL/g, "/Correspondance/Details/" + aData[7]);
                //document.location.href = dochref;

            })
        },

        "sDom": 'T<"clear">lfrtip',
        "oTableTools": {
            "aButtons": [
                    {
                        "sExtends": "Back",
                        "sButtonText": "Back to List"
                    },
            ]

        },

        "bLengthChange": false,
        "bPaginate": false,
        "bServerSide": true,
        "sAjaxSource": "Home/GetAjaxData",
        "bProcessing": true,
        "bFilter": true,
        "aoColumn": [

        { "sName": "Type" },
        { "sName": "Year" },
        { "sName": "From/To" },
        { "sName": "Department" },
        { "sName": "Subject" },
        { "sName": "Date on Letter" },
        { "sName": "Date Received/Sent" },
        { "sName:": "Id", "bSearchable": false, "bVisible": false }

        ]
    })
      .rowGrouping({
          iGroupingColumnIndex2: 1,
      });

    $('#example_filter input').unbind();

    $('#example_filter input').bind('keyup', function (e) {
        if ($(this).val().length > 2) {
            oTable.fnFilter(this.value);
        }
    });

    //$('#example tbody').on('click', 'td.group', function () {
    $('#example tbody').on('click', 'td.subgroup', function () {
        //oTable.fnFilter($(this).html());   
        oSettings = oTable.fnSettings();

        value2 = $(this).html().toLowerCase();
        test5 = value2.replace(/ /g, "").replace(/-/g, "");
        test1 = $(this).attr("class");
        test3 = test1.replace(/(subgroup|-)/g, "");
        test4 = test3.replace(test5, "");
        value1 = test4.replace(/ /g, "").replace(/-/g, "");


        if (oSettings != null) {
            oSettings.aoServerParams.push({
                "sName": "user",
                "fn": function (aoData) {
                    aoData.push(
                        { "name": "first_data", "value": value1 },
                        { "name": "second_data", "value": value2 }
                        );
                }
            });

            oTable.fnDraw();
        }//if
    });

    //Subject Datatable
    var oTableSubject = $('#examplesubject').dataTable({

        "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            $(nRow).on('click', function () {
                //var pathArray = window.location.pathname.split('/');
                //document.location.href = pathArray[0] + "/" + pathArray[1] + "/Correspondance/Details/" + aData[7];
                //document.location.href = window.location.href.split('/')[1] + "/Correspondance/Details/" + aData[7];

                dochref = window.location.href.replace(/HomeSubject/g, "/Correspondance/Details/" + aData[7]);
                document.location.href = dochref;


            })
        },

        "sDom": 'T<"clear">lfrtip',
        "oTableTools": {
            "aButtons": [
                    {
                        "sExtends": "Back",
                        "sButtonText": "Back to List"
                    },
            ]

        },

        "bLengthChange": false,
        "bPaginate": false,
        "bServerSide": true,
        "sAjaxSource": "HomeSubject/GetAjaxData",
        "bProcessing": true,
        "bFilter": true,
        "aoColumn": [

        { "sName": "Subject" },
        { "sName": "Type" },
        { "sName": "Year" },
        { "sName": "FromTo" },
        { "sName": "Department" },
        { "sName": "Date on Letter" },
        { "sName": "Date Received/Sent" },
        { "sName:": "Id", "bSearchable": false, "bVisible": false }

        ]
    })
      .rowGrouping({
          iGroupingColumnIndex2: 1,
      });

    $('#examplesubject_filter input').unbind();

    $('#examplesubject_filter input').bind('keyup', function (e) {
        if ($(this).val().length > 2) {
            oTableSubject.fnFilter(this.value);
        }
    });

    //$('#example tbody').on('click', 'td.group', function () {
    $('#examplesubject tbody').on('click', 'td.subgroup', function () {
        //oTable.fnFilter($(this).html());   
        oSettings = oTableSubject.fnSettings();

        value2 = $(this).html().toLowerCase();
        test5 = value2.replace(/ /g, "").replace(/-/g, "");
        test1 = $(this).attr("class");
        test3 = test1.replace(/(subgroup|-)/g, "");
        test4 = test3.replace(test5, "");
        value1 = test4.replace(/ /g, "").replace(/-/g, "");


        if (oSettings != null) {
            oSettings.aoServerParams.push({
                "sName": "user",
                "fn": function (aoData) {
                    aoData.push(
                        { "name": "first_data", "value": value1 },
                        { "name": "second_data", "value": value2 }
                        );
                }
            });

            oTableSubject.fnDraw();
        }
    });
    //

    //Department Datatable
    var oTableDepartment = $('#exampledepartment').dataTable({

        "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            $(nRow).on('click', function () {
                //var pathArray = window.location.pathname.split('/');
                //document.location.href = pathArray[0] + "/" + pathArray[1] + "/Correspondance/Details/" + aData[7];
                dochref = window.location.href.replace(/HomeDepartment/g, "/Correspondance/Details/" + aData[7]);
                document.location.href = dochref;
            })
        },

        "sDom": 'T<"clear">lfrtip',
        "oTableTools": {
            "aButtons": [
                    {
                        "sExtends": "Back",
                        "sButtonText": "Back to List"
                    },
            ]

        },

        "bLengthChange": false,
        "bPaginate": false,
        "bServerSide": true,
        "sAjaxSource": "HomeDepartment/GetAjaxData",
        "bProcessing": true,
        "bFilter": true,
        "aoColumn": [

        { "sName": "Department" },
        { "sName": "Type" },
        { "sName": "Year" },
        { "sName": "FromTo" },
        { "sName": "Subject" },
        { "sName": "Date on Letter" },
        { "sName": "Date Received/Sent" },
        { "sName:": "Id", "bSearchable": false, "bVisible": false }

        ]
    })
      .rowGrouping({
          iGroupingColumnIndex2: 1,
      });

    $('#exampledepartment_filter input').unbind();

    $('#exampledepartment_filter input').bind('keyup', function (e) {
        if ($(this).val().length > 2) {
            oTableDepartment.fnFilter(this.value);
        }
    });

    $('#exampledepartment tbody').on('click', 'td.subgroup', function () {
        oSettings = oTableDepartment.fnSettings();

        value2 = $(this).html().toLowerCase();
        test5 = value2.replace(/ /g, "").replace(/-/g, "");
        test1 = $(this).attr("class");
        test3 = test1.replace(/(subgroup|-)/g, "");
        test4 = test3.replace(test5, "");
        value1 = test4.replace(/ /g, "").replace(/-/g, "");


        if (oSettings != null) {
            oSettings.aoServerParams.push({
                "sName": "user",
                "fn": function (aoData) {
                    aoData.push(
                        { "name": "first_data", "value": value1 },
                        { "name": "second_data", "value": value2 }
                        );
                }
            });

            oTableDepartment.fnDraw();
        }
    });
    //

    //FromTo Datatable
    var oTableFromTo = $('#examplefromto').dataTable({

        "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            $(nRow).on('click', function () {
                //var pathArray = window.location.pathname.split('/');
                //document.location.href = pathArray[0] + "/" + pathArray[1] + "/Correspondance/Details/" + aData[7];
                dochref = window.location.href.replace(/HomeFrom/g, "/Correspondance/Details/" + aData[7]);
                document.location.href = dochref;
            })
        },

        "sDom": 'T<"clear">lfrtip',
        "oTableTools": {
            "aButtons": [
                    {
                        "sExtends": "Back",
                        "sButtonText": "Back to List"
                    },
            ]

        },

        "bLengthChange": false,
        "bPaginate": false,
        "bServerSide": true,
        "sAjaxSource": "HomeFrom/GetAjaxData",
        "bProcessing": true,
        "bFilter": true,
        "aoColumn": [

        { "sName": "FromTo" },
        { "sName": "Type" },
        { "sName": "Year" },
        { "sName": "Department" },
        { "sName": "Subject" },
        { "sName": "Date on Letter" },
        { "sName": "Date Received/Sent" },
        { "sName:": "Id", "bSearchable": false, "bVisible": false }

        ]
    })
      .rowGrouping({
          iGroupingColumnIndex2: 1,
      });

    $('#examplefromto_filter input').unbind();

    $('#examplefromto_filter input').bind('keyup', function (e) {
        if ($(this).val().length > 2) {
            oTableFromTo.fnFilter(this.value);
        }
    });

    $('#examplefromto tbody').on('click', 'td.subgroup', function () {
        oSettings = oTableFromTo.fnSettings();

        value2 = $(this).html().toLowerCase();
        test5 = value2.replace(/ /g, "").replace(/-/g, "");
        test1 = $(this).attr("class");
        test3 = test1.replace(/(subgroup|-)/g, "");
        test4 = test3.replace(test5, "");
        value1 = test4.replace(/ /g, "").replace(/-/g, "");


        if (oSettings != null) {
            oSettings.aoServerParams.push({
                "sName": "user",
                "fn": function (aoData) {
                    aoData.push(
                        { "name": "first_data", "value": value1 },
                        { "name": "second_data", "value": value2 }
                        );
                }
            });

            oTableFromTo.fnDraw();
        }
    });
    //

    //Date Datatable
    var oTableDate = $('#exampledate').dataTable({

        "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            $(nRow).on('click', function () {
                //var pathArray = window.location.pathname.split('/');
                //document.location.href = pathArray[0] + "/" + pathArray[1] + "/Correspondance/Details/" + aData[7];
                dochref = window.location.href.replace(/HomeDate/g, "/Correspondance/Details/" + aData[7]);
                document.location.href = dochref;
            })
        },

        "sDom": 'T<"clear">lfrtip',
        "oTableTools": {
            "aButtons": [
                    {
                        "sExtends": "Back",
                        "sButtonText": "Back to List"
                    },
            ]

        },

        "bLengthChange": false,
        "bPaginate": false,
        "bServerSide": true,
        "sAjaxSource": "HomeDate/GetAjaxData",
        "bProcessing": true,
        "bFilter": true,
        "aoColumn": [

        { "sName": "Date" },
        { "sName": "Type" },
        { "sName": "Year" },
        { "sName": "FromTo" },
        { "sName": "Department" },
        { "sName": "Subject" },
        { "sName": "Date Received/Sent" },
        { "sName:": "Id", "bSearchable": false, "bVisible": false }

        ]
    })
      .rowGrouping({
          iGroupingColumnIndex2: 1,
      });

    $('#exampledate_filter input').unbind();

    $('#exampledate_filter input').bind('keyup', function (e) {
        if ($(this).val().length > 2) {
            oTableDate.fnFilter(this.value);
        }
    });

    $('#exampledate tbody').on('click', 'td.subgroup', function () {
        oSettings = oTableDate.fnSettings();

        value2 = $(this).html().toLowerCase();
        test5 = value2.replace(/ /g, "").replace(/-/g, "");
        test1 = $(this).attr("class");
        test3 = test1.replace(/(subgroup|-)/g, "");
        test4 = test3.replace(test5, "");
        value1 = test4.replace(/ /g, "").replace(/-/g, "");


        if (oSettings != null) {
            oSettings.aoServerParams.push({
                "sName": "user",
                "fn": function (aoData) {
                    aoData.push(
                        { "name": "first_data", "value": value1 },
                        { "name": "second_data", "value": value2 }
                        );
                }
            });

            oTableDate.fnDraw();
        }
    });
    //


    //moxiemanger addin for uploading files
    tinymce.init({
        selector: "textarea",
        theme: "modern",
        plugins: [
            "advlist autolink lists link image charmap print preview hr anchor pagebreak",
            "searchreplace wordcount visualblocks visualchars code fullscreen",
            "insertdatetime media nonbreaking save table contextmenu directionality",
            "emoticons template paste textcolor"
        ],
        toolbar1: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",
        toolbar2: "print preview media | forecolor backcolor emoticons",
        image_advtab: true,
        templates: [
            { title: 'Test template 1', content: 'Test 1' },
            { title: 'Test template 2', content: 'Test 2' }
        ]
    });

    //tinymce.baseURL = "@Url.Content('~/Scripts/tinymce')";

});


function isDate(txtDate) 

    { 
        var currVal = txtDate; 
        if(currVal == '') 
        return false; 
    
        //Declare Regex   
        var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;  
        var dtArray = currVal.match(rxDatePattern); // is format OK? 

        if (dtArray == null) 
        return false; 

        //Checks for mm/dd/yyyy format. 
        dtMonth = dtArray[3]; 
        dtDay= dtArray[1]; 
        dtYear = dtArray[5]; 
  
        if (dtMonth < 1 || dtMonth > 12) 
        return false; 

        else if (dtDay < 1 || dtDay> 31) 

        return false; 

        else if ((dtMonth==4 || dtMonth==6 || dtMonth==9 || dtMonth==11) && dtDay ==31) 

        return false; 

        else if (dtMonth == 2) 
        { 
            var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0)); 
            if (dtDay> 29 || (dtDay ==29 && !isleap)) 

            return false; 
        } 
        return true; 
    } 

function _fnOnGrouped() {
    $('.group, .subgroup').click();
}

//This script hides and shows on click events
//$(function () {
   
//    var vidCount = $('#Documents').data('rec-length')
//    window.onload = addListeners;

//    var number = 0;

       
    
//    function addListeners() {
//        if  (window.addEventListener) {	  	
//            for (number = 1; number <= vidCount; number++) {                
//                document.getElementById(number).addEventListener('click', BtnClick, false);
//            }
		
//        }		
//        else {
//            if  (window.attachEvent) { //internet explorer pre-version 9
//                for (number = 1; number <= vidCount; number++) {
//                    $('#Documents').data('custom-id').attachEvent('onclick', BtnClick);
//                }
		
//            }		
//        }
//    }

   
//    function BtnClick() {       
//        var elem = this;			    
//        var vid = elem.getAttribute("data-custom-id");				 			    
//        var disp = document.getElementById(vid);  // display the text of the video		  									
//        var Btn = document.getElementById("Document" + vid);

//        if(Btn.value == 'Close') {   // USE innerHTML not innerText for firefox           	
//            Btn.value= "Open";  					                  				
//            disp.style.display="none";                                                         				                                                     
//        }
//        else {
//            if (Btn.value == 'Open') {	
                
//                disp.style.display="none";
//                Btn.value= "Close";
//            }
//        }
//        return true;
//    }
//});