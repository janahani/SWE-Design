document.addEventListener("DOMContentLoaded", function () {
    var modal = document.getElementById("myFreezeModal");
    var btn = document.getElementById("btnFreeze");
    var span = document.getElementsByClassName("freezeClose")[0];

    btn.onclick = function () {
        modal.style.display = "block";
    };

    span.onclick = function () {
        modal.style.display = "none";
    };

    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    };
});