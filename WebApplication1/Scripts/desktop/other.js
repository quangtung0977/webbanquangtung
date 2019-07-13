var CHAT_GL_ENABLED = 0;
$(document).ready(function () {
    console.log('lien-he');
    var url = window.location.href;
    if (url.indexOf('#phu-kien') > -1) {
        var position = $('#phu-kien').offset();
        $("html, body").animate({ scrollTop: position.top }, 1500);
    }
    $("img.footermoneylazy").lazyload({
    });
    $("img.footerpinlazy").lazyload({
    });
    $("img.lazy").lazyload({
        threshold: 150
    });

    $(window).scroll(function () {
        try {
            LoadJsComment();
            // $.lockfixed(".nolist", { offset: { top: 100, bottom: 380 } });
        } catch (e) {
        }
    })

    //active menu 
    try {
        var curURL = window.location.href;
        $("#help-navigation > ul > li > a").each(function () {
            $(this).parent().removeClass("active");
            var ahref = $(this).attr("href");
            if (curURL.indexOf(ahref) > -1) {
                $(this).parent().addClass("active");
                return false;
            }
        });
    } catch (e) {
    }
    //dành cho linh kiện thay thế sửa chữa
    if (window.location.href.indexOf("bang-gia-sua-chua") > -1)
        InitTypePart();
});

var flsc = false;
function LoadJsComment() {
    if (flsc == true)
        return;
    if (typeof cmtaddcommentclick == 'undefined') {
        //Chỉ load một lần thôi
        flsc = true;
        var tgddc = document.createElement('script');
        tgddc.type = 'text/javascript';
        tgddc.async = true;
        tgddc.src = jsCommentUrl;
        (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(tgddc);
        setTimeout(function () {
            InitEvent();
        }, 500);
    }
}


//#region Tra cứu bảng giá linh kiện
var typingTimer;                //timer identifier
var doneTypingInterval = 80;  //time in ms

//#region Suggest sản phẩm 
function SearchProduct(e) {
    if (typingTimer)
        clearTimeout(typingTimer);
    var keyword = $('#txtsuggest').val();
    typingTimer = setTimeout(function () {
        if (e.which != 8 && keyword.length >= 2)
            SuggestProducts(e);
        else if (e.which == 8 && keyword.length == 0) //xóa hết kí tự tìm kiếm
        {
            e.preventDefault;
            $('#listsuggest').hide(200);
        }
    }, doneTypingInterval);
}

function SuggestProducts(e) {
    var keyword = $('#txtsuggest').val();
    if (keyword == '' || keyword.length < 2)
        return;

    //#region Dùng phím để chọn sản phẩm
    var sl = '#listsuggest li';
    if (e.which == 40) {
        if ($(sl + '.selected').length == 0) {
            $(sl + ':first').addClass('selected');
        }
        else {
            var t = $(sl + '.selected').next();
            if (t.hasClass('li-group'))
                t = t.next();
            $(sl + '.selected').removeClass('selected');
            t.addClass('selected');
        }
        return;
    }
    else if (e.which == 38) {
        if ($(sl + '.selected').length == 0) {
            $(sl + ':last').addClass('selected');
        }
        else {
            var t = $(sl + '.selected').prev();
            if (t.hasClass('li-group'))
                t = t.prev();
            $(sl + '.selected').removeClass('selected');
            t.addClass('selected');
        }
        return;
    } else if (e.which == 13) {
        e.preventDefault();
        if ($(sl + '.selected').length == 0)
            $(sl + ':first').addClass('selected');
        var url = $(sl + '.selected a').attr('href');
        if (url != undefined && url != '') {
            window.location = url;
        }
        return;
    }
    //#endregion
    var clearcache = false;
    var tmpclr = getQuerystring('clearcache');
    if (tmpclr != undefined && tmpclr == '1')
        clearcache = true;
    keyword = keyword.replace(/:|;|!|@|@@|#|\$|%|\+|\^|&|'|"|>|<|,|\?|\/|`|~|=|_|\(|\)|{|}|\[|\]|\\|\|' '|/gi, '');
    keyword = keyword.replace(/ /g, '+');
    keyword = keyword.trim();
    POSTAjax('/aj/Other/AccessoriesSuggest', { Key: keyword, clearcache: clearcache }, BeforeSendAjax, function (result) {
        EndSendAjax();
        if (result != undefined && result != '') {
            $('.listcity').hide();
            $('#listsuggest').show(200);
            $('#listsuggest').html(result);
        } else {
            $('#listsuggest').html('<li>Không tìm thấy kết quả phù hợp</li>');
        }
    }, EndSendAjax, true);
}
//#endregion

//Lưu sản phẩm được chọn, lấy danh sách loại linh kiện thay thế
function SaveProduct(productid, productname) {
    if (productid == undefined || productid == '') return;
    $('#hdproductid').val(productid);
    $('#listsuggest').slideUp();
    $('#txtsuggest').val(productname);
    $('#hdproductname').val(productname);
    $('.list-result').hide();
    BuildListReplacementPartType(productid);
    //$('.citydis').show();
    //$('#cur-acc').attr('data-id', '-1').html('Chọn loại sửa chữa');

}
//Danh sách linh kiện thay thế
function BuildListReplacementPartType(productid) {
    var clearcache = false;
    var tmpclr = getQuerystring('clearcache');
    if (tmpclr != undefined && tmpclr == '1')
        clearcache = true;
    POSTAjax('/aj/Other/GenListReplacementPartType', { productid: productid, clearcache: clearcache }, BeforeSendAjax, function (result) {
        EndSendAjax();
        $('.citydis').show();
        if (result != undefined && result != '') {
            $('.list-result').hide();
            $('#cur-acc').attr('data-id', '-1').html('Chọn loại sửa chữa');
            $('.scroll').html(result);
        } else {
            $('.list-result').show();
            $('.list-result').css('color', 'red');
            $('.list-result').html('Danh sách linh kiện thay thế chưa được khai báo, vui lòng quay lại sau.');
            $('.citydis').hide();
        }
    }, EndSendAjax, true);
}
//Khởi tạo sự kiện cho dropdown loại linh kiện
function InitTypePart() {
    $('.city').live('click', function () {
        $('.layer').not($(this).next()).hide();
        $(this).next().slideToggle();
    });
    $('.listcity .scroll a').live('click', function () {
        $('.listcity').hide();
        $('#cur-acc').attr('data-id', $(this).attr('data-value')).html($(this).text());
        var productid = $('#hdproductid').val();
        var parttypeid = $('#cur-acc').attr('data-id');
        if (productid == undefined || productid == '' || parttypeid == undefined || parttypeid == '') {
            $('.list-result').show();
            $('.list-result').css('color', 'red');
            $('.list-result').html('Không tìm thấy bảng giá linh kiện sửa chữa, vui lòng thử lại sau.');
        }
        else
            GetListReplacementPrice(productid, parttypeid);
    });
}
//Lấy bảng giá linh kiện thay thế
function GetListReplacementPrice(productid, parttypeid) {
    //productid = '72373'; //test
    if (productid == undefined || productid == '' || parttypeid == undefined || parttypeid == '') return;
    var clearcache = false;
    var tmpclr = getQuerystring('clearcache');
    if (tmpclr != undefined && tmpclr == '1')
        clearcache = true;
    POSTAjax('/aj/Other/AccessoriesReplacementPriceList', { productid: productid, parttypeid: parttypeid, clearcache: clearcache }, function () { BeforeSendAjax(); $('.list-result').show(); $('.list-result').html('Đang tải dữ liệu...'); }, function (result) {
        EndSendAjax();
        if (result != undefined && result != '') {
            $('.list-result').show();
            $('.list-result').css('color', '#333');
            $('.list-result').html(result);
            $('.sp-name').html("Bảng giá sửa chữa " + $('#hdproductname').val());
        }
        else {
            $('.list-result').show();
            $('.list-result').css('color', 'red');
            $('.list-result').html('Không tìm thấy bảng giá linh kiện sửa chữa, vui lòng thử lại sau.');
        }
    }, EndSendAjax, true);
}
//#endregion

//#region Xử lý trang B2B mới

$('.label-dknbg').click(function () {
    $("body,html").animate({ scrollTop: 600 }, "slow");
    $('#search-keyword-b2b').focus();
});
function GetAllFormData($form) {
    var unindexed_array = $form.serializeArray();
    var indexed_array = {};
    $.map(unindexed_array, function (n, i) {
        indexed_array[n['name']] = n['value'];
    });
    return indexed_array;
}

var SUBMIT_CUSTOMER_REGISTER = true;
function SubmitCustomerRegister() {
    if (!SUBMIT_CUSTOMER_REGISTER)
        return;
    SUBMIT_CUSTOMER_REGISTER = false;
    var data = GetAllFormData($('#frmCompanyCustomer'))
    $.ajax({
        type: 'POST',
        cache: false,
        beforeSend: function () { },
        data: data,
        url: '/aj/Other/SubmitCompanyCustomerRegister',
        success: function (e) {
            if (e != null || e != '') {
                var msg = e.msg;
                var result = e.result;
                if (result == 1) {
                    alert(msg);
                }
                else if (result == -3) {

                    alert(msg);
                }
                else {
                    alert('Chức năng đang được bảo trì, vui lòng thử lại sau');
                }
            }
            $('#dlding').fadeOut(1000);
            SUBMIT_CUSTOMER_REGISTER = true;
        }
    })
}

// Đăng ký nhận thông tin liên lạc của khách hàng
function SubmitContact() {
    var data = GetAllFormData($('#contactinfo'));
    $.ajax({
        type: 'POST',
        cache: false,
        beforeSend: function () { },
        data: data,
        url: '/aj/Other/SubmitContact',
        success: function (e) {
            if (e != null || e != '') {
                if (e.status == 1) {
                    alert('Đã gửi thông tin thành công!');
                }
                else if (e.status == -3) {
                    var html = 'Rất tiếc, thông tin bạn cung cấp chưa đúng: \n';
                    for (var i in e.errors) {
                        html += '-';
                        html += e.errors[i];
                        html += '\n';
                    }
                    alert(html);
                }
                else {
                    var html = alert('Chức năng đang được bảo trì, vui lòng thử lại sau');
                }
            }
        }
    })
    return false;
}

//Kiểm tra thông tin phiếu mua hàng
function CheckCouponInfo() {
    var coupon = $('#txtCouponCode').val().trim();
    if (coupon == '') {
        $('.business-coupon .r .error').html('Vui lòng nhập mã giảm giá/mã phiếu mua hàng cần tra cứu');
        $('#txtCouponCode').focus();
        $('.couponinfo-hide').html('');
        return;
    }
    if (coupon.length != 12) {
        $('.business-coupon .r .error').html('Mã thẻ không chính xác, vui lòng kiểm tra lại');
        $('#txtCouponCode').focus();
        $('.couponinfo-hide').html('');
        return;
    }
    POSTAjax('/aj/Voucher/VoucherDetail', { strC: coupon }, function () { $('#dlding').show(); }, function (e) {
        if (e == null || e == '') {
            $('#err').html('Chức năng đang được bảo trì, vui lòng thử lại sau');
            window.location.reload();
        }
        if (typeof (e) == 'object') {
            $('.r .error').html(e._errors[0]);
            $('.couponinfo-hide').html('');
        }
        else {
            $('.r .error').html('');
            $('.couponinfo-hide').html(e);
        }
    }, function () {
    }, true);
}

// Gợi ý sản phẩm khi search B2b
var lastSuggest = new Date().getTime();
var timmer;
function B2BSuggestSearch(e, box, category, call) {
    $('.wrap-suggestion').remove();
    if (typeof (call) == 'undefined')
        call = false;
    var keyword = ($(box) === undefined) ? '' : $(box).val();
    keyword = keyword.trim().toLowerCase();
    var $currentForm = $(box).parents('form');
    var $suggest = $currentForm.find('.wrap-suggestion-b2b');
    if (e.which == 40 || e.which == 38) {
        e.preventDefault();
        UpDownSuggest(e.which);
        return false;
    }
    else if (e.which == 13) {
        if ($('ul.wrap-suggestion li.selected').length > 0) {
            location.href = $('ul.wrap-suggestion-b2b li.selected a').attr('href');
            e.preventDefault();
            return false;
        }
        if (keyword.length < 2) {
            $suggest.hide();
            return;
        }
    }
    if (!call) {
        clearTimeout(timmer);
        timmer = setTimeout(function () {
            B2BSuggestSearch(e, box, category, true);
        }, 300);
        return;
    }
    if (keyword.length < 2) {
        $suggest.hide();
        return;
    }
    $.ajax({
        url: '/aj/CommonV3/B2BSuggestSearch',
        type: 'GET',
        data: { keyword: keyword, categoryID: category },
        cache: false,
        success: function (res) {
            clearTimeout(timmer);
            if (res == '') {
                $('.wrap-suggestion-b2b').remove();
                $('.wrap-suggestion').remove();
            }
            else {
                if ($suggest.length > 0) {
                    $suggest.replaceWith(res);
                }
                else {
                    $currentForm.append(res);
                }
            }
        }
    })
}
function UpDownSuggest(key) {
    var sl = 'ul.wrap-suggestion-b2b li';
    if (key == 40) {
        if ($(sl + '.selected').length == 0) {
            $(sl + ':first').addClass('selected');
        }
        else {
            var t = $(sl + '.selected').next();
            $(sl + '.selected').removeClass('selected');
            t.addClass('selected');
        }
        return;
    }
    else if (key == 38) {
        if ($(sl + '.selected').length == 0) {
            $(sl + ':last').addClass('selected');
        }
        else {
            var t = $(sl + '.selected').prev();
            $(sl + '.selected').removeClass('selected');
            t.addClass('selected');
        }
        return;
    }
}
//Sự kiện load slide tonggle câu hỏi thường gặp
$(".question-area").click(function () {
    var parent = $(this).closest(".question-content");
    parent.find(".answer-busi").slideToggle();

});
function formatNumberValue(number) {
    var intLength = number.toString().length;
    var intLeft = 0;
    var strNumber = '';
    var strNewNumber = '';
    while (intLength % 3 != 0) {
        intLength++;
        intLeft++;
    }
    if (intLeft != 0) {
        for (var intCount = 0; intCount < intLeft; intCount++) {
            strNumber += '0';
        }
    }
    strNumber += number.toString();
    for (var intCount = 0; intCount < intLength; intCount++) {
        strNewNumber += strNumber.charAt(intCount);

        if (intCount > 0 && (intCount + 1) % 3 == 0 && intCount != intLength - 1) {
            strNewNumber += '.';
        }
    }
    strNewNumber = strNewNumber.substring(intLeft);
    return strNewNumber;
}
//#region Xử lý form phiếu mua hàng
$('.sl-voucher .choosenumber .augment').click(function () {
    $(".sl-voucher .choosenumber .abate").addClass('active');
    var number = Number($(".sl-voucher .choosenumber .number").html()) + 1;
    $(".sl-voucher .choosenumber .number").html(number);
    if (number === 50) {
        $(".sl-voucher .choosenumber .augment").addClass('disable');
    }
    $("#pop-voucher .voucherQuantity").val(number);
    totalVoucher();
});
$(".sl-voucher .choosenumber .abate").click(function () {
    $(".sl-voucher .choosenumber .augment").removeClass('disable');
    var number = Number($(".sl-voucher .choosenumber .number").html());
    number--;
    $(".sl-voucher .choosenumber .number").html(number);
    if (number === 1) {
        $(".sl-voucher .choosenumber .abate").removeClass('active');
    }
    $("#pop-voucher .voucherQuantity").val(number);
    totalVoucher()
});
$(".openPopupVoucher").click(function () {
    $(".pop-chk").each(function () {
        value = this.getAttribute('data-prince');
        if (value === ($("#prince-hidden").val())) {
            $(this).addClass('pop-chk-active');
        }
        else {
            $(this).removeClass('pop-chk-active');
        }
        $("#pop-voucher .pop-err").html('');
    });
    totalVoucher();
    $('#pop-voucher .voucherPrice').val($(".pop-chk-active").attr('data-prince'));
    $('#pop-voucher .voucherCode').val($(".pop-chk-active").attr('data-code'));
    $('.popup-voucher').show();
});
$('.popupCloseButton').click(function () {
    $('.popup-voucher').hide();
});
$(".pop-chk").click(function () {
    $("#prince-hidden").val(this.getAttribute('data-prince'));
    $(".pop-chk").removeClass('pop-chk-active');
    $(this).addClass('pop-chk-active');
    $('#pop-voucher .voucherPrice').val($(".pop-chk-active").attr('data-prince'));
    $('#pop-voucher .voucherCode').val($(".pop-chk-active").attr('data-code'));
    totalVoucher();
});
//Sự kiện chọn mệnh giá phiếu mua hàng
$(".chk").click(function () {
    $("#prince-hidden").val(this.getAttribute('data-prince'));
    $(".chk").removeClass('chk-active');
    $(this).addClass('chk-active');
    totalVoucher();
});
// Hàm get tổng tiền voucher
function totalVoucher() {
    return $('.total-voucher').html(formatNumberValue(Number($("#prince-hidden").val()) * Number($(".choosenumber .number").html())) + 'đ');
}
//#endregion
var IS_SubmitVoucherOrder = true;
function SubmitVouchertOrder() {
    if (!IS_SubmitVoucherOrder)
        return;
    IS_SubmitVoucherOrder = false;
    var data = GetAllFormData($('#pop-voucher'))
    $.ajax({
        type: 'POST',
        cache: false,
        beforeSend: function () { },
        data: data,
        url: '/aj/Voucher/SubmitVoucherCartb2b',
        success: function (e) {
            if (e != null || e != '') {
                var msg = e.msg;
                var result = e.result;
                if (result < 0) {
                    $("#pop-voucher .pop-err").html(msg);
                } else {
                    window.location.href = "/b2b/succsess";
                }
            }
            IS_SubmitVoucherOrder = true;
        }
    })
}
//#region xử lý form nhận báo giá sản phẩm mới
$(".sl-product .choosenumber .number").change(function () {
    var val = $(".sl-product .choosenumber .number").val();
    if (/\D/.test(val)) {
        val = 1;
        $(".sl-product .choosenumber .number").val(val);
        $(".sl-product .choosenumber .abate ").removeClass('active');
        $(".sl-product .choosenumber .augment ").removeClass('disable');
        alert("Số lượng không hợp lệ, vui lòng nhập số nguyên");
        $("#frmBaogia .txtQuantity").val(val);
        return;
    }
    else {
        if (val < 1) {
            val = 1;
            $(".sl-product .choosenumber .number").val(val);
            $(".sl-product .choosenumber .abate ").removeClass('active');
            $(".sl-product .choosenumber .augment ").removeClass('disable');
            alert("Số lượng không hợp lệ, tối thiểu là 1");
            $("#frmBaogia .txtQuantity").val(val);
            return;
        }
        if (val == 1) {
            $(".sl-product .choosenumber .number").val(val);
            $(".sl-product .choosenumber .abate ").removeClass('active');
            $(".sl-product .choosenumber .augment ").removeClass('disable');
            $("#frmBaogia .txtQuantity").val(val);
            return;
        }
        if (val > 1 && val < 50) {
            $(".sl-product .choosenumber .number").val(val);
            $(".sl-product .choosenumber .abate ").addClass('active');
            $(".sl-product .choosenumber .augment ").removeClass('disable');
            $("#frmBaogia .txtQuantity").val(val);
            return;
        }
        if (val == 50) {
            $(".sl-product .choosenumber .number").val(val);
            $(".sl-product .choosenumber .abate ").addClass('active');
            $(".sl-product .choosenumber .augment ").addClass('disable');
            $("#frmBaogia .txtQuantity").val(val);
            return;
        }
        if (val > 50) {
            val = 1;
            $(".sl-product .choosenumber .number").val(val);
            $(".sl-product .choosenumber .abate ").removeClass('active');
            $(".sl-product .choosenumber .augment ").removeClass('disable');
            alert("Số lượng không hợp lệ, tối đa là 50");
            $("#frmBaogia .txtQuantity").val(val);
            return;
        }
    }
});
$('.sl-product .choosenumber .augment').click(function () {
    $(".sl-product .choosenumber .abate ").addClass('active');
    var number = Number($(".sl-product .choosenumber .number").val()) + 1;
    $(".sl-product .choosenumber .number").val(number);
    if (number === 50) {
        $(".sl-product .choosenumber .augment").addClass('disable');
    }
    $("#frmBaogia .txtQuantity").val(number);
});
$(".sl-product .choosenumber .abate").click(function () {
    $(".sl-product .choosenumber .augment ").removeClass('disable');
    var number = Number($(".sl-product .choosenumber .number").val());
    number--;
    $(".sl-product .choosenumber .number").val(number);
    if (number === 1) {
        $(".sl-product .choosenumber .abate").removeClass('active');
    }
    $("#frmBaogia .txtQuantity").val(number);
});
var IS_SubmitProductOrder = true;
function SubmitProductOrder() {
    if (!IS_SubmitProductOrder)
        return;
    IS_SubmitProductOrder = false;
    var data = GetAllFormData($('#frmBaogia'))
    $.ajax({
        type: 'POST',
        cache: false,
        beforeSend: function () { },
        data: data,
        url: '/aj/Voucher/SubmitProducPriceb2b',
        success: function (e) {
            if (e != null || e != '') {
                var msg = e.msg;
                var result = e.result;
                if (result < 0) {
                    $("#frmBaogia .err-msg-popProduct").html(msg);
                } else {
                    //$("#frmBaogia .err-msg-popProduct").html(msg);
                    window.location.href = msg;
                }
            }
            IS_SubmitProductOrder = true;
        }
    })
};

//#endregion
//#endregion trang B2B mới

