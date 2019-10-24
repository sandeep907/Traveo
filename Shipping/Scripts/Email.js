    $(document).ready(function () {
        $('#AddEmail').on('click', function (e) {
            debugger;
            $.ajax({
                url: RootUrl+"Email/AddEmail",
                dataType: "html",
                type: "GET",
                contentType: "application/json",
                success: function (response) {
                    debugger;
                    $('#NewEmail').html(response);
                    $('#modal-default').modal();
                    //Datemask dd/mm/yyyy
                    $('#datemask').inputmask('dd/mm/yyyy', { 'placeholder': 'dd/mm/yyyy' });
                    //Datemask2 mm/dd/yyyy
                    $('#datemask2').inputmask('mm/dd/yyyy', { 'placeholder': 'mm/dd/yyyy' });
                    //Money Euro
                    $('[data-mask]').inputmask();

                    //Date range picker
                    $('#reservation').daterangepicker();
                    //Date range picker with time picker
                    $('#reservationtime').daterangepicker({ timePicker: true, timePickerIncrement: 30, format: 'MM/DD/YYYY h:mm A' });
                    //Date range as a button
                    $('#daterange-btn').daterangepicker(
                        {
                            ranges: {
                                'Today': [moment(), moment()],
                                'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                                'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                                'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                                'This Month': [moment().startOf('month'), moment().endOf('month')],
                                'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                            },
                            startDate: moment().subtract(29, 'days'),
                            endDate: moment()
                        },
                        function (start, end) {
                            $('#daterange-btn span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'))
                        }
                    );

                    //Date picker
                    $('.datepicker').datepicker({
                        autoclose: true
                    });

                    //iCheck for checkbox and radio inputs
                    $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
                        checkboxClass: 'icheckbox_minimal-blue',
                        radioClass: 'iradio_minimal-blue'
                    });
                    //Red color scheme for iCheck
                    $('input[type="checkbox"].minimal-red, input[type="radio"].minimal-red').iCheck({
                        checkboxClass: 'icheckbox_minimal-red',
                        radioClass: 'iradio_minimal-red'
                    });
                    //Flat red color scheme for iCheck
                    $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
                        checkboxClass: 'icheckbox_flat-green',
                        radioClass: 'iradio_flat-green'
                    });

                    //Colorpicker
                    $('.my-colorpicker1').colorpicker();
                    //color picker with addon
                    $('.my-colorpicker2').colorpicker();

                    //Timepicker
                    $('.timepicker').timepicker({
                        showInputs: false
                    });

                    //$('#myTable1').on('click', '#row', function (e) {
                    //    var name = $(this).children("td").map(function () {
                    //        return $(this).text();
                    //    }).get();
                    //    e.preventDefault();
                    //    var mymodal = $('#modal-default');
                    //    mymodal.find('.modal-body').text(name);
                    //    mymodal.modal('show');
                    //});
                    //$('#CopyClipboard').on('click', function () {
                    //    debugger;
                    //    var $temp = $("<input>");
                    //    $("body").append($temp);
                    //    $temp.val($('.modal-body').text()).select();
                    //    document.execCommand("copy");
                    //    $temp.remove();
                    //});
                    //$('#Print').on('click', function () {
                    //    debugger;
                    //    $("#Table").show();
                    //    window.print();
                    //});

                },
                error: function (err) {
                    alert("error");
                }
            })
        })
    })
function LoadSentEmails(pageId) {
    debugger;
    //NProgress.start();
    var pageSizeId = ($('#ddlPageSize').val() == "") ? 10 : $('#ddlPageSize').val();
    $('#dvEmailSntList').html('');
    $.ajax({
        url: RootUrl + "Email/Search",
        type: "GET",
        data: {page: pageId, pageSize: pageSizeId },
        success: function (resp) {
            $("#dvEmailSntList").html(resp);
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