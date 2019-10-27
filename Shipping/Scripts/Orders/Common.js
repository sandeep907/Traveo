var initialLoad = false;
$(function () {
    $(document).on('click', '.thbut', function () {
        manager = new ClickManager(this);
        manager.check();
    });
    var delay = (function () {
        var timer = 0;
        return function (callback, ms) {
            clearTimeout(timer);
            timer = setTimeout(callback, ms);
        };
    })();
    $('#ORorderNo').on("keyup", (function (e) {
        e.preventDefault();
        $('#OrdrStatusID').val('null');
        debugger;
        $('#orderSearch').val();
        delay(function () {
            var pageSizeId = ($('#ddlPageSize').val() == "") ? 10 : $('#ddlPageSize').val();
            if (pageSizeId == null) { pageSizeId = 10; }
            $('#dvorderlist').html('');
            $.ajax({
                url: RootUrl + "Order/Search",
                type: "GET",
                data: {
                    orderNo: $('#ORorderNo').val(),
                    consignneName: $('#ORconsigneeName').val(),
                    agentName: $('#ORagentName').val(),
                    consgnNo: $('#ORconsgnNo').val(),
                    date: $('#ORdate').val(),
                    daterange: $('#ORdateRange').val(),
                    page: 0,
                    pageSize: pageSizeId,
                    orderType: parseInt($('#OrdrStatusID').val())
                },
                success: function (resp) {
                    $("#dvorderlist").html(resp);
                    $('#ddlPageSize').val(pageSizeId);
                },
                error: function () {
                    alert("An error has occured!!!");
                   
                
                }
            });
        }, 500);
    }));
    $('#ORconsigneeName').on("keyup", (function (e) {
        e.preventDefault();
        $('#OrdrStatusID').val('null');
        debugger;
        $('#orderSearch').val()
        delay(function () {
            var pageSizeId = ($('#ddlPageSize').val() == "") ? 10 : $('#ddlPageSize').val();
            if (pageSizeId == null) { pageSizeId = 10; }
            $('#dvorderlist').html('');
            $.ajax({
                url: RootUrl + "Order/Search",
                type: "GET",
                data: {
                    orderNo: $('#ORorderNo').val(),
                    consignneName: $('#ORconsigneeName').val(),
                    agentName: $('#ORagentName').val(),
                    consgnNo: $('#ORconsgnNo').val(),
                    date: $('#ORdate').val(),
                    daterange: $('#ORdateRange').val(),
                    page: 0,
                    pageSize: pageSizeId,
                    orderType: parseInt($('#OrdrStatusID').val())
                },
                success: function (resp) {
                    $("#dvorderlist").html(resp);
                    $('#ddlPageSize').val(pageSizeId);
                },
                error: function () {
                    alert("An error has occured!!!");
                   
                
                }
            });
        }, 500);
    }));
    $('#ORagentName').on("keyup", (function (e) {
        e.preventDefault();
        $('#OrdrStatusID').val('null');
        debugger;
        $('#orderSearch').val()
        delay(function () {
            var pageSizeId = ($('#ddlPageSize').val() == "") ? 10 : $('#ddlPageSize').val();
            if (pageSizeId == null) { pageSizeId = 10; }
            $('#dvorderlist').html('');
            $.ajax({
                url: RootUrl + "Order/Search",
                type: "GET",
                data: {
                    orderNo: $('#ORorderNo').val(),
                    consignneName: $('#ORconsigneeName').val(),
                    agentName: $('#ORagentName').val(),
                    consgnNo: $('#ORconsgnNo').val(),
                    date: $('#ORdate').val(),
                    daterange: $('#ORdateRange').val(),
                    page: 0,
                    pageSize: pageSizeId,
                    orderType: parseInt($('#OrdrStatusID').val())
                },
                success: function (resp) {
                    $("#dvorderlist").html(resp);
                    $('#ddlPageSize').val(pageSizeId);
                },
                error: function () {
                    alert("An error has occured!!!");
                   
                
                }
            });
        }, 500);
    }));
    $('#ORconsgnNo').on("keyup", (function (e) {
        e.preventDefault();
        $('#OrdrStatusID').val('null');
        debugger;
        $('#orderSearch').val()
        delay(function () {
            var pageSizeId = ($('#ddlPageSize').val() == "") ? 10 : $('#ddlPageSize').val();
            if (pageSizeId == null) { pageSizeId = 10; }
            $('#dvorderlist').html('');
            $.ajax({
                url: RootUrl + "Order/Search",
                type: "GET",
                data: {
                    orderNo: $('#ORorderNo').val(),
                    consignneName: $('#ORconsigneeName').val(),
                    agentName: $('#ORagentName').val(),
                    consgnNo: $('#ORconsgnNo').val(),
                    date: $('#ORdate').val(),
                    daterange: $('#ORdateRange').val(),
                    page: 0,
                    pageSize: pageSizeId,
                    orderType: parseInt($('#OrdrStatusID').val())
                },
                success: function (resp) {
                    $("#dvorderlist").html(resp);
                    $('#ddlPageSize').val(pageSizeId);
                },
                error: function () {
                    alert("An error has occured!!!");
                   
                
                }
            });
        }, 500);
    }));
    $('#ORdate').on("change", (function (e) {
        e.preventDefault();
        $('#OrdrStatusID').val('null');
        debugger;
        $('#orderSearch').val()
        delay(function () {
            var pageSizeId = ($('#ddlPageSize').val() == "") ? 10 : $('#ddlPageSize').val();
            if (pageSizeId == null) { pageSizeId = 10; }
            $('#dvorderlist').html('');
            $.ajax({
                url: RootUrl + "Order/Search",
                type: "GET",
                data: {
                    orderNo: $('#ORorderNo').val(),
                    consignneName: $('#ORconsigneeName').val(),
                    agentName: $('#ORagentName').val(),
                    consgnNo: $('#ORconsgnNo').val(),
                    date: $('#ORdate').val(),
                    daterange: $('#ORdateRange').val(),
                    page: 0,
                    pageSize: pageSizeId,
                    orderType: parseInt($('#OrdrStatusID').val())
                },
                success: function (resp) {
                    $("#dvorderlist").html(resp);
                    $('#ddlPageSize').val(pageSizeId);
                },
                error: function () {
                    alert("An error has occured!!!");
                   
                
                }
            });
        }, 500);
    }));
    $('#ORdateRange').on("change", (function (e) {
        e.preventDefault();
        $('#OrdrStatusID').val('null');
        debugger;
        $('#orderSearch').val()
        delay(function () {
            var pageSizeId = ($('#ddlPageSize').val() == "") ? 10 : $('#ddlPageSize').val();
            if (pageSizeId == null) { pageSizeId = 10; }
            $('#dvorderlist').html('');
            $.ajax({
                url: RootUrl + "Order/Search",
                type: "GET",
                data: {
                    orderNo: $('#ORorderNo').val(),
                    consignneName: $('#ORconsigneeName').val(),
                    agentName: $('#ORagentName').val(),
                    consgnNo: $('#ORconsgnNo').val(),
                    date: $('#ORdate').val(),
                    daterange: $('#ORdateRange').val(),
                    page: 0,
                    pageSize: pageSizeId,
                    orderType: parseInt($('#OrdrStatusID').val())
                },
                success: function (resp) {
                    $("#dvorderlist").html(resp);
                    $('#ddlPageSize').val(pageSizeId);
                },
                error: function () {
                    alert("An error has occured!!!");
                   
                
                }
            });
        }, 500);
    }));
    //Butoon click
    $('#btFresh').on("click", (function (e) {

        $('#orderSearch').val();
        delay(function () {
            var pageSizeId = ($('#ddlPageSize').val() == "") ? 10 : $('#ddlPageSize').val();
            $('#dvorderlist').html('');
            $('#OrdrStatusID').val('1');
            $.ajax({
                url: RootUrl + "Order/Search",
                type: "GET",
                data: {
                    orderNo: $('#ORorderNo').val(),
                    consignneName: $('#ORconsigneeName').val(),
                    agentName: $('#ORagentName').val(),
                    consgnNo: $('#ORconsgnNo').val(),
                    date: $('#ORdate').val(),
                    daterange: $('#ORdateRange').val(),
                    page: 0,
                    pageSize: pageSizeId,
                    orderType: 1
                },
                success: function (resp) {
                    $("#dvorderlist").html(resp);
                    $('#ddlPageSize').val(pageSizeId);
                },
                error: function () {
                    alert("An error has occured!!!");
                   
                
                }
            });
        }, 500);
    }));
    $('#btInprogress').on("click", (function (e) {
        debugger;
        $('#OrdrStatusID').val(2);
        delay(function () {
            var pageSizeId = ($('#ddlPageSize').val() == "") ? 10 : $('#ddlPageSize').val();
            $('#dvorderlist').html('');
            $.ajax({
                url: RootUrl + "Order/Search",
                type: "GET",
                data: {
                    orderNo: $('#ORorderNo').val(),
                    consignneName: $('#ORconsigneeName').val(),
                    agentName: $('#ORagentName').val(),
                    consgnNo: $('#ORconsgnNo').val(),
                    date: $('#ORdate').val(),
                    daterange: $('#ORdateRange').val(),
                    page: 0,
                    pageSize: pageSizeId,
                    orderType: 2
                },
                success: function (resp) {
                    $("#dvorderlist").html(resp);
                    $('#ddlPageSize').val(pageSizeId);
                },
                error: function () {
                    alert("An error has occured!!!");
                   
                
                }
            });
        }, 500);
    }));
    $('#btOutforDelivery').on("click", (function (e) {
        e.preventDefault();
        debugger;
        $('#OrdrStatusID').val(3);
        delay(function () {
            var pageSizeId = ($('#ddlPageSize').val() == "") ? 10 : $('#ddlPageSize').val();
            $('#dvorderlist').html('');
            $.ajax({
                url: RootUrl + "Order/Search",
                type: "GET",
                data: {
                    orderNo: $('#ORorderNo').val(),
                    consignneName: $('#ORconsigneeName').val(),
                    agentName: $('#ORagentName').val(),
                    consgnNo: $('#ORconsgnNo').val(),
                    date: $('#ORdate').val(),
                    daterange: $('#ORdateRange').val(),
                    page: 0,
                    pageSize: pageSizeId,
                    orderType: 3
                },
                success: function (resp) {
                    $("#dvorderlist").html(resp);
                    $('#ddlPageSize').val(pageSizeId);
                },
                error: function () {
                    alert("An error has occured!!!");
                   
                
                }
            });
        }, 500);
    }));
    $('#btDelivered').on("click", (function (e) {
        e.preventDefault();
        debugger;
        $('#OrdrStatusID').val(4);
        delay(function () {
            var pageSizeId = ($('#ddlPageSize').val() == "") ? 10 : $('#ddlPageSize').val();
            $('#dvorderlist').html('');
            $.ajax({
                url: RootUrl + "Order/Search",
                type: "GET",
                data: {
                    orderNo: $('#ORorderNo').val(),
                    consignneName: $('#ORconsigneeName').val(),
                    agentName: $('#ORagentName').val(),
                    consgnNo: $('#ORconsgnNo').val(),
                    date: $('#ORdate').val(),
                    daterange: $('#ORdateRange').val(),
                    page: 0,
                    pageSize: pageSizeId,
                    orderType: 4
                },
                success: function (resp) {
                    $("#dvorderlist").html(resp);
                    $('#ddlPageSize').val(pageSizeId);
                },
                error: function () {
                    alert("An error has occured!!!");
                   
                
                }
            });
        }, 500);
    }));
    $('#btReturned').on("click", (function (e) {
        e.preventDefault();
        debugger;
        $('#OrdrStatusID').val(5);
        delay(function () {
            var pageSizeId = ($('#ddlPageSize').val() == "") ? 10 : $('#ddlPageSize').val();
            $('#dvorderlist').html('');
            $.ajax({
                url: RootUrl + "Order/Search",
                type: "GET",
                data: {
                    orderNo: $('#ORorderNo').val(),
                    consignneName: $('#ORconsigneeName').val(),
                    agentName: $('#ORagentName').val(),
                    consgnNo: $('#ORconsgnNo').val(),
                    date: $('#ORdate').val(),
                    daterange: $('#ORdateRange').val(),
                    page: 0,
                    pageSize: pageSizeId,
                    orderType: 5
                },
                success: function (resp) {
                    $("#dvorderlist").html(resp);
                    $('#ddlPageSize').val(pageSizeId);
                },
                error: function () {
                    alert("An error has occured!!!");
                   
                
                }
            });
        }, 500);
    }));
    $('#btnTotal').on("click", (function (e) {
        debugger;
        delay(function () {
            var pageSizeId = ($('#ddlPageSize').val() == "") ? 10 : parseInt($('#orderTl').val());
            $('#dvorderlist').html('');
            $('#OrdrStatusID').val('1');
            $.ajax({
                url: RootUrl + "Order/Search",
                type: "GET",
                data: {
                    orderNo: $('#ORorderNo').val(),
                    consignneName: $('#ORconsigneeName').val(),
                    agentName: $('#ORagentName').val(),
                    consgnNo: $('#ORconsgnNo').val(),
                    date: $('#ORdate').val(),
                    daterange: $('#ORdateRange').val(),
                    page: 0,
                    pageSize: pageSizeId
                },
                success: function (resp) {
                    $("#dvorderlist").html(resp);
                    //$('#ddlPageSize').val(pageSizeId);
                },
                error: function () {
                    alert("An error has occured!!!");
                   
                
                }
            });
        }, 500);
    }));
});
function LoadOrderDetail(pageId) {
    debugger;
    var pageSizeId = ($('#ddlPageSize').val() == "") ? 10 : $('#ddlPageSize').val();
    $('#dvorderlist').html('');
    $.ajax({
        url: RootUrl + "Order/Search",
        type: "GET",
        data: {
            orderNo: $('#ORorderNo').val(),
            consignneName: $('#ORconsigneeName').val(),
            agentName: $('#ORagentName').val(),
            consgnNo: $('#ORconsgnNo').val(),
            date: $('#ORdate').val(),
            daterange: $('#ORdateRange').val(),
            page: pageId,
            pageSize: pageSizeId,
            orderType: parseInt($('#OrdrStatusID').val())
        },
        success: function (resp) {
            debugger;
            $("#dvorderlist").html(resp);
            $('#ddlPageSize').val(pageSizeId);
        },
        error: function () {
            alert("An error has occured!!!");
        }
    });
}