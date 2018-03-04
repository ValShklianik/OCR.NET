$(document).ready(function () {

    var c = document.getElementById("image_canvas");
    var ctx = c.getContext("2d");
    var canvasWith = parseInt($("#image_canvas").css("width"));
    var canvasHeigt = parseInt($("#image_canvas").css("height"));

   $("#find_button").click(function () {
        $(".loader").show();
        var phrase = $("#phrase").val();
        render();

        $.ajax({
            method: "GET",
            url: 'http://localhost:49556/api/process_image/' + phrase + "/coordinates"
        }).then(function(data) {
            console.log(data);
            highlight(data);
            $(".loader").hide();
        },
        function() {
            $(".loader").hide();
        });
    });

    setTimeout(function() {
        render();
    }, 100);

    function render() {
        ctx.clearRect(0, 0, canvasWith, canvasHeigt);
        var img = document.getElementById("immage_for_canvas");
        ctx.drawImage(img, 0, 0);
    }

    function highlight(coordinates) {
        ctx.globalAlpha = 0.5;
        ctx.strokeStyle = "yellow";
        ctx.fillStyle = "yellow";


        for (var i = 0; i < coordinates.length; i++) {
            ctx.fillRect(coordinates[i].x1 - 5, coordinates[i].y1-3, coordinates[i].width + 15, coordinates[i].height+6);
        }
        ctx.globalAlpha = 1.0;
        
    }
});