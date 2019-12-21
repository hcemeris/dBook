
    $("#btn1").click(function () {
        $("#collapseExample2").hide();
        $("#collapseExample3").hide();
        $("#collapseExample1").show();
        $("#collapseExample4").hide();
});
    $("#btn2").click(function () {
        $("#collapseExample1").hide();
        $("#collapseExample3").hide();
        $("#collapseExample2").show();
        $("#collapseExample4").hide();
});
    $("#btn3").click(function () {
        $("#collapseExample1").hide();
        $("#collapseExample4").hide();
        $("#collapseExample2").hide();
        $("#collapseExample3").show();
                });
$("#btn4").click(function () {
    $("#collapseExample1").hide();
    $("#collapseExample2").hide();
    $("#collapseExample3").hide();
    $("#collapseExample4").show();
});

$("#soneklenen").click(function () {
    $("#example4").hide();
    $("#example1").show();
});
$("#cokokunan").click(function () {
    $("#example1").hide();
    $("#example4").show();
});
$("#ktp").click(function () {
    $("#yazarlar").hide();
    $("#kullanicilar").hide();
    $("#kitaplar").show();
});
$("#yzr").click(function () {
    $("#yazarlar").show();
    $("#kullanicilar").hide();
    $("#kitaplar").hide();
});
$("#kllnc").click(function () {
    $("#yazarlar").hide();
    $("#kullanicilar").show();
    $("#kitaplar").hide();
});
//AUTHORLIST
$("#authorAdaGore").click(function () {
    $("#link2").hide();
    $("#link3").hide();
    $("#link1").show();
});
$("#authorFavoriSayi").click(function () {
    $("#link1").hide();
    $("#link3").hide();
    $("#link2").show();
});
$("#authorEnCokOkunan").click(function () {
    $("#link1").hide();
    $("#link2").hide();
    $("#link3").show();
});