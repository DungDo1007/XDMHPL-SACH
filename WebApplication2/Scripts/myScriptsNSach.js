$(document).ready(function () {
    $('#nxb').on('change', function () {
        var nxbid = $('#nxb option:selected').val();
        $.ajax({
            type: "GET",
            data: { nxbid: nxbid },
            url: '/NhapSach/getsach',
            success: function (result) {
                var s = '<option value="-1">--Chọn sách--</option>';
                for (var i = 0; i < result.length; i++) {
                    s += '<option value="' + result[i].Id + '">' + result[i].Name + '</option>';
                }
                $("#product").html(s);
            }
        })
    })
    //Add button click event
    $('#add').click(function () {
        //validation and add order items
        var isAllValid = true;

        if ($('#product').val() == -1) {
            isAllValid = false;
            $('#product').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#product').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#quantity').val().trim() != '' && (parseInt($('#quantity').val()) || 0))) {
            isAllValid = false;
            $('#quantity').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#quantity').siblings('span.error').css('visibility', 'hidden');
        }
        if (isAllValid) {
            var $newRow = $('#mainrow').clone().removeAttr('id');
            $('.product', $newRow).val($('#product').val());

            //Replace add button with remove button
            $('#add', $newRow).addClass('remove').val('Xóa').removeClass('btn-success').addClass('btn-danger');

            //remove id attribute from new clone row
            $('#product,#quantity,#add', $newRow).removeAttr('id');
            $('span.error', $newRow).remove();
            //append clone row
            $('#orderdetailsItems').append($newRow);

            //clear select data
            $('#product').val('-1');
            $('#quantity').val('');
            $('#orderItemError').empty();
        }

    })

    //remove button click event
    $('#orderdetailsItems').on('click', '.remove', function () {
        $(this).parents('tr').remove();
    });
    //Save
    $('#submit').click(function () {
        var isAllValid = true;

        //validate order items
        $('#orderItemError').text('');
        var list = [];
        var errorItemCount = 0;
        $('#orderdetailsItems > tr').each(function () {
            if (
                $('select.product', this).val() == "0" ||
                (parseInt($('.quantity', this).val()) || 0) == 0
                ) {

                errorItemCount++;
                $(this).addClass('error');
            }
            else {

                var orderItem = {
                    fk_sachID: $('select.product', this).val(),
                    soluong: parseInt($('.quantity', this).val()),
                }
                list.push(orderItem);
            }
        })

        if (errorItemCount > 0) {
            $('#orderItemError').text(errorItemCount + " dữ liệu không hợp lệ trong danh sách.");
            isAllValid = false;
        }

        if (list.length == 0) {
            $('#orderItemError').text('Yêu cầu nhập ít nhất 1 cuốn sách.');
            isAllValid = false;
        }

        if ($('#nxb').val() == -1) {
            isAllValid = false;
            $('#nxb').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#nxb').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#orderDate').val().trim() == '') {
            $('#orderDate').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#orderDate').siblings('span.error').css('visibility', 'hidden');
        }
        if ($('#description').val().trim() == '') {
            $('#description').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#description').siblings('span.error').css('visibility', 'hidden');
        }
        if (isAllValid) {
            var data = {
                fk_nxbID: $('#nxb option:selected').val(),
                nhapsach_ngaygiao: $('#orderDate').val().trim(),
                nhapsach_nguoigiao: $('#description').val().trim(),
                NhapSachDetails: list,
            }

            $(this).val('Chờ chút...');

            $.ajax({
                type: 'POST',
                url: '/NhapSach/save',
                data: JSON.stringify(data),
                dataType: "JSON",
                contentType: 'application/json',
                success: function (data) {
                    if (data.status) {
                        alert('Lưu thành công!');
                        //here we will clear the form
                        list = [];
                        $('#orderDate,#description').val('');
                        $('#nxb').val('-1');
                        $('#product').val('0');
                        $('#orderdetailsItems').empty();
                    }
                    else {
                        alert('Lỗi');
                    }
                    $('#submit').val('Lưu');
                },
                error: function (error) {
                    console.log(error);
                    $('#submit').val('Lưu');
                }
            });
        }
    });
});