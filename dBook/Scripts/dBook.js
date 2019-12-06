
    $("#btn1").click(function () {
        $("#collapseExample2").hide();
    $("#collapseExample3").hide();
    $("#collapseExample1").show();
});
                $("#btn2").click(function () {
        $("#collapseExample1").hide();
    $("#collapseExample3").hide();
    $("#collapseExample2").show();
});
                $("#btn3").click(function () {
        $("#collapseExample1").hide();
    $("#collapseExample2").hide();
    $("#collapseExample3").show();
});

$("#soneklenen").click(function () {
    $("#example3").hide();
    $("#example4").hide();
    $("#example2").hide();
    $("#example1").show();
});
$("#begenilen").click(function () {
    $("#example3").hide();
    $("#example4").hide();
    $("#example1").hide();
    $("#example2").show();
});
$("#cokokunan").click(function () {
    $("#example1").hide();
    $("#example4").hide();
    $("#example2").hide();
    $("#example3").show();
});
$("#yorumalan").click(function () {
    $("#example3").hide();
    $("#example1").hide();
    $("#example2").hide();
    $("#example4").show();
});