$("#modaldis-login").click(function () {
    window.setTimeout(function () {
        $("#loginmodal").modal('hide');
    }, 1000);
    window.setTimeout(function () {
        $("#registermodal").modal('show');
    }, 1000);
    //$("#loginmodal").modal('hide');
    //$("#registermodal").modal('show');
});

$("#modaldis-res").click(function () {
    window.setTimeout(function () {
        $("#registermodal").modal('hide');
    }, 1000);
    window.setTimeout(function () {
        $("#loginmodal").modal('show');
    }, 1000);
    //$("#registermodal").modal('hide');
    //$("#loginmodal").modal('show');
});