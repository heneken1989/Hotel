


$("#btn-delete-room-type").click(function () {
    let data_id = $(this).attr("data-id")

    $("#a-link-delete-room-type").attr("href", `/admin/RoomType/delete/?id=${data_id}`)
});

$(document).ready(() => {
    $(".paginate_button.next").text("trang tiep theo ")
    $(".paginate_button.previous").text("trang truoc ")
    $(".dataTables_info").attr("style","display:none")
})



