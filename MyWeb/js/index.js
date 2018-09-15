
var flag = true;// 菜单是否打开

//菜单显示隐藏
function SwitchMenu() {
    if (flag) {
        $("#left").width('0px');
        $("#right").width('100%');
        flag = false;
        $("#showLogo").show();
        $("#hideLogo").hide();//菜单显示按钮

    } else {
        $("#left").width('240px');
        $("#right").width('calc(100% - 240px)');
        flag = true;
        $("#hideLogo").show();
        $("#showLogo").hide();//菜单显示按钮
    }
}

//遮罩层
function CloseZheZhao() {
    $("#ZheZhao").hide();
}
function OpenZheZhao() {
    $("#ZheZhao").fadeIn();
}

//点击主体内容将菜单收起
$(function () {
    $("#mobileMenuIcon").click(function () {
        $("#mobileMenu").stop(true,false);
        $("#mobileMenu").slideToggle();
    })
    $(document).click(function () {
        //$("#mobileMenu").css("display", "none");
        if ($("#mobileMenu").css("display")=="block") {
            $("#mobileMenu").stop(true, false);
            $("#mobileMenu").slideToggle();
        }
    });
    //取消冒泡
    $("#rightTop").click(function (event) {
        event.stopPropagation();
    });
    $("#mobileMenu").click(function (event) {
        event.stopPropagation();
    });
    $("#ZheZhao").click(function (event) {
        event.stopPropagation();
    });
})