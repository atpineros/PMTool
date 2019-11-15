

$(document).ready(
    $('#projectTabs a').on('click', function (e) {

        $(this).tab('show')
    }),
    $('.dropdown-toggle').dropdown(),

    $("#addToPreview").on("click", function (e) {
        var memberID = $("#SelectedMemeberID option:selected").text();
        var roleID = $("#SelectedRoleID option:selected").text();
        $("#teamPreviewTable").append('<tr><td>' + memberID + '</td><td>' + roleID +'</td></tr>')

        console.log(roleID, memberID);
    })
)
