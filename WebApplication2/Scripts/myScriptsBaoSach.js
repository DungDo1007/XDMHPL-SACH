$(document).ready(function () {
    $('#sachdebt').on('change', function () {
        var sachid = $('#sachdebt option:selected').val();
        $.ajax({
            type: "GET",
            data: { sachid: sachid },
            url: '/DaiLy/getdebt',
            success: function (result) {
                $("#sldebt").html(result.sldebt);
                $("#gia").html(result.gia);
            }
        })
    })
    $.get("/DaiLy/GetDebtList", null, DataBind);
    function DataBind(m) {
        var SetData = $("#SetBookList");
        for (var i = 0; i < m.length; i++) {
            var Data = "<tr class='row_" + m[i].dailydebtID + "'>" +
                "<td>" + m[i].SACH_NAME + "</td>" +
                "<td>" + m[i].SACH_GIA + "</td>" +
                "<td class='sld'>" + m[i].dailydebt_soluong + "</td>" +
                "</tr>";
            SetData.append(Data);
        }
        var s = '<option value="-1">--Chọn sách--</option>';
        for (var i = 0; i < m.length; i++) {
            s += '<option value="' + m[i].fk_sachID + '">' + m[i].SACH_NAME + '</option>';
        }
        $("#sachdebt").html(s);
    }
    $('#save').click(function () {
     
       var isAllValid = true;
        if (parseInt($('#quantity').val()) > parseInt($('#sldebt').text()))
        {
            $('#orderItemError').text('Không được nhập số lượng báo lớn hơn số lượng đang nợ');
            isAllValid = false;
        }
        else {
            $('#orderItemError').text('');
        }
        if ($('#sachdebt').val() == -1) {
            isAllValid = false;
            $('#sachdebt').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#sachdebt').siblings('span.error').css('visibility', 'hidden');
        }
        if (!($('#quantity').val().trim() != '' && (parseInt($('#quantity').val()) || 0))) {
            isAllValid = false;
            $('#quantity').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#quantity').siblings('span.error').css('visibility', 'hidden');
        }
        if (isAllValid) {

            var tongtien = parseInt($('#quantity').val()) * parseFloat($('#gia').text());
            var socu = parseInt($('#tongtien').text());
            var somoi = socu + tongtien;
            var data = {
                fk_sachID: $('#sachdebt option:selected').val(),
                dailydebt_soluong: $('#quantity').val().trim(),
            }
            $.ajax({
                type: 'POST',
                url: '/DaiLy/saveBaoSach',
                data: JSON.stringify(data),
                dataType: "JSON",
                contentType: 'application/json',
                success: function (data) {
                    if (data.status) {
                        alert('Lưu thành công!'); $("#tongtien").html(somoi);
                        $("#SetBookList").empty();
                        $('#sachdebt').val('-1');
                        $('#quantity').val('');
                        $("#gia,#sldebt").text('');
                        $.get("/DaiLy/GetDebtList", null, DataBind);
                        DataBind();               
                    }
                    else {
                        alert('Lỗi');
                    }          
                },               
            });
          
        }
    });
});