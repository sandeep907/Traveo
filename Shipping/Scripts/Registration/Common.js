var initialLoad = false;

$(function () {
    var delay = (function () {
        var timer = 0;
        return function (callback, ms) {
            clearTimeout(timer);
            timer = setTimeout(callback, ms);
        };
    })();

    $('#userserch').on("keyup", (function (e) {
        e.preventDefault();
        debugger;
        $('#userserch').val()
        //NProgress.start();
        delay(function () {
            var pageSizeId = ($('#ddlPageSize').val() == "") ? 10 : $('#ddlPageSize').val();
            $('#dvuserlist').html('');
            $.ajax({
                url: RootUrl + "Registration/Search",
                type: "GET",
                data: { search: $('#userserch').val(), page: 0, pageSize: pageSizeId },
                success: function (resp) {
                    $("#dvuserlist").html(resp);
                    //NProgress.done();
                    //NProgress.remove();
                },
                error: function () {
                    alert("An error has occured!!!");
                    NProgress.done();
                    NProgress.remove();
                }
            });
        }, 500);
    }));

});
// Set the initialLoad is true when page is fully loaded 
//jQuery(window).load(function () {
//    initialLoad = true;
//});
function LoadUserDetail(pageId) {
    //NProgress.start();
    var pageSizeId = ($('#ddlPageSize').val() == "") ? 10 : $('#ddlPageSize').val();
    $('#dvuserlist').html('');
    $.ajax({
        url: RootUrl + "Registration/Search",
        type: "GET",
        data: { search: $('#userserch').val(), page: pageId, pageSize: pageSizeId },
        success: function (resp) {
            $("#dvuserlist").html(resp);
            $('#ddlPageSize').val(pageSizeId);
            //NProgress.done();
            //NProgress.remove();
        },
        error: function () {
            alert("An error has occured!!!");
            NProgress.done();
            NProgress.remove();
        }
    });
    }
 