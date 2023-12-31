﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(".custom-file-input").on("change", function () {
    var fileName = $(this).val().split("\\").pop();
    $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
});

function calcTotalExperiences() {
    var x = document.getElementsByClassName('YearsWorked');
    var totalExp = 0;
    var i;
    for (i = 0; i < x.length; i++) {
        totalExp = totalExp + eval(x[i].value);
    }
    document.getElementById('TotalExperint').value = totalExp;
    return;
}
document.addEventListener('change', function
    (e){
    if (hasClass(e.target, 'YearsWorked')) {
        calcTotalExperiences();
    }
}, false);


function DeleteItem(btn) {
    $(btn).closest('tr').remove();
}



function AddItem(btn) {

    var table = document.getElementById('ExpTable');
    var rows = table.getElementsByTagName('tr');

    var rowOuterHtml = rows[rows.length - 1].outerHTML;

    var lastrowIdx = document.getElementById('hdnLastIndex').value;

    var nextrowIdx = eval(lastrowIdx) + 1;

    document.getElementById('hdnLastIndex').value = nextrowIdx;

    rowOuterHtml = rowOuterHtml.replaceAll('_' + lastrowIdx + '_', '_' + nextrowIdx + '_');
    rowOuterHtml = rowOuterHtml.replaceAll('[' + lastrowIdx + ']', '[' + nextrowIdx + ']');
    rowOuterHtml = rowOuterHtml.replaceAll('-' + lastrowIdx, '-' + nextrowIdx);


    var newRow = table.insertRow();
    newRow.innerHTML = rowOuterHtml;



    var btnAddID = btn.id;
    var btnDeleteid = btnAddID.replaceAll('btnadd', 'btnremove');

    var delbtn = document.getElementById(btnDeleteid);
    delbtn.classList.add("visible");
    delbtn.classList.remove("invisible");


    var addbtn = document.getElementById(btnAddID);
    addbtn.classList.remove("visible");
    addbtn.classList.add("invisible");

} function rebindvalidators() {
    var $form = $("#ApplicantForm");
    $form.unbind();
    $form.data("validator", null);
    $.validator.unobtrusive.parse($form);
    $form.validator($form.data("unobtrusiveValidation").options);
}