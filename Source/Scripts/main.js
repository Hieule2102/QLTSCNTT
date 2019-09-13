$(document).ready(function () {
    $('.nav-icon').click(function () {
        $('.nav-icon').toggleClass('nav-close');
    });
    $('.nav-icon').click(function () {
        $('header').toggleClass('header-show');
    });
    $('.nav-icon').click(function () {
        $('.navbar').toggleClass('navbar-show');
    });
    $('.nav-icon').click(function () {
        $('.nav').toggleClass('d-none');
    });


    $('.login-close').click(function () {
        $('#login').removeClass('login-show');
    });

    $('#chct').click(function () {
        $('.cau-hinh-chi-tiet').addClass('cau-hinh-chi-tiet-show');
    });
    $('.close-cauhinh').click(function () {
        $('.cau-hinh-chi-tiet').removeClass('cau-hinh-chi-tiet-show');
    });




    // Accordion
    function close_accordion_section() {
        $('.accordion .accordion-section-title').removeClass('active');
        $('.accordion .accordion-section-content').slideUp(300).removeClass('open');
    }

    $('.accordion-section-title').click(function (e) {
        // Grab current anchor value
        var currentAttrValue = $(this).attr('href');

        if ($(e.target).is('.active')) {
            close_accordion_section();
        } else {
            close_accordion_section();

            // Add active class to section title
            $(this).addClass('active');
            // Open up the hidden content panel
            $('.accordion ' + currentAttrValue).slideDown(300).addClass('open');
        }

        e.preventDefault();
    });



    $.ajax({
        type: "GET",
        url: "/NhapKho/get_NHOMTB",
        data: "{}",
        success: function (data) {
            var s = '<option value="-1">Xin chọn nhóm thiết bị</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i] + '">' + data[i] + '</option>';
            }
            $("#MA_NHOMTB").html(s);
        }
    });
});


function Fill_LoaiTB() {
    var nhom_TB = $('#MA_NHOMTB').val();
    $.ajax({
        type: "POST",
        url: "/NhapKho/get_LOAITB",
        data: { nhom_TB: nhom_TB },
        success: function (data) {
            $("#MA_LOAITB").html("");
            var options = '<option value="-1">Xin chọn loại thiết bị</option>';
            for (var i = 0; i < data.length; i++) {
                options += '<option value="' + data[i] + '">' + data[i] + '</option>';
            }
            $("#MA_LOAITB").html(options);
        }
    });
};

function Fill_TenTB() {
    var maTB = $('#MATB').val();
    $.ajax({
        type: "POST",
        url: "/XuatKho/get_TENTB",
        data: { maTB: maTB },
        success: function (data) {
            $("#TENTB").val(data);
        }
    });
};


function on_click() {
    var table = document.getElementById('myTable');

    for (var i = 1; i < table.rows.length; i++) {
        table.rows[i].onclick = function () {
            //rIndex = this.rowIndex;
            var maTB = this.cells[0].innerHTML;
            $.ajax({
                type: "POST",
                url: "/DanhSachThietBi/get_CauHinh",
                data: { maTB: maTB },
                success: function (data) {
                    if (data.MALOAI == "PC") {
                        document.getElementById("first").firstElementChild.innerHTML = "CPU:";

                        document.getElementById("second").firstElementChild.innerHTML = "Màn hình:";

                        document.getElementById("third").firstElementChild.innerHTML = "VGA:";

                        document.getElementById("fourth").firstElementChild.innerHTML = "RAM:";
                        $("#TENTB").html(data.TENTB);
                        if (data.CPU == null) {
                            $("#CPU").html("null");
                        }
                        else {
                            $("#CPU").html(data.CPU);
                        }
                        $("#MAN_HINH").html(data.MAN_HINH);
                        $("#RAM").html(data.RAM);
                        $("#O_CUNG").html(data.O_CUNG);
                        $("#VGA").html(data.VGA);
                        $("#HDH").html(data.HDH);
                    }
                    else if (data.MALOAI == "KH") {
                        document.getElementById("first").firstElementChild.innerHTML = "Cảm biến:";

                        document.getElementById("second").firstElementChild.innerHTML = "Độ phân giải:";

                        document.getElementById("third").firstElementChild.innerHTML = "Ống kính:";

                        document.getElementById("fourth").firstElementChild.innerHTML = "Hỗ trợ luồng:";
                    }
                }
            });
            $.ajax({
                type: "POST",
                url: "/DanhSachThietBi/get_HinhAnh",
                data: { maTB: maTB },
                success: function (data) {
                    var img = document.createElement("img");
                    img.src = "Images/" + data;
                    img.height = 100;
                    img.width = 100;

                    //document.getElementById("img").firstElementChild.innerHTML = img;
                    document.getElementById("img").appendChild(img);
                }
            });
        };
    }
};